/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：CustomMessageHandler_Events.cs
    文件功能描述：自定义MessageHandler
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

//DPBMARK_FILE MP
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar.Agents;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.CommonService.Download;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Nop.Core;
using Nop.Core.Helpers;
using Nop.Core.Infrastructure;
using Nop.Core.Domain.Weixin;
using Nop.Services.Weixin;

namespace Senparc.Weixin.MP.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        private string GetWelcomeInfo()
        {
            //获取Senparc.Weixin.MP.dll版本信息
            //var filePath = ServerUtility.ContentRootMapPath("~/bin/Release/netcoreapp2.2/Senparc.Weixin.MP.dll");//本地测试路径
            var filePath = ServerUtility.ContentRootMapPath("~/Senparc.Weixin.MP.dll");//发布路径
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);

            string version = fileVersionInfo == null
                ? "-"
                : string.Format("{0}.{1}.{2}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart);

            return string.Format(
@"欢迎关注【Senparc.Weixin 微信公众平台SDK】，当前运行版本：v{0}。
您可以发送【文字】【位置】【图片】【语音】【文件】等不同类型的信息，查看不同格式的回复。

您也可以直接点击菜单查看各种类型的回复。
还可以点击菜单体验微信支付。

SDK官方地址：https://weixin.senparc.com
SDK Demo：https://sdk.weixin.senparc.com
源代码及Demo下载地址：https://github.com/JeffreySu/WeiXinMPSDK
Nuget地址：https://www.nuget.org/packages/Senparc.Weixin.MP
QQ群：289181996

===============
更多：

1、JSSDK测试：https://sdk.weixin.senparc.com/WeixinJSSDK

2、开放平台测试（建议PC上打开）：https://sdk.weixin.senparc.com/OpenOAuth/JumpToMpOAuth

3、回复关键字：

【open】   进入第三方开放平台（Senparc.Weixin.Open）测试

【tm】     测试异步模板消息

【openid】 获取OpenId等用户信息

【约束】   测试微信浏览器约束

【AsyncTest】 异步并发测试

【错误】    体验发生错误无法返回正确信息

【容错】    体验去重容错

【ex】      体验错误日志推送提醒

【mute】     不返回任何消息，也无出错信息

【jssdk】    测试JSSDK图文转发接口

格式：【数字#数字】，如2010#0102，调用正则表达式匹配

【订阅】     测试“一次性订阅消息”接口
",
                version);
        }

        public string GetDownloadInfo(CodeRecord codeRecord)
        {
            return string.Format(@"您已通过二维码验证，浏览器即将开始下载 Senparc.Weixin SDK 帮助文档。
当前选择的版本：v{0}（{1}）

我们期待您的意见和建议，客服热线：400-031-8816。

感谢您对盛派网络的支持！

© {2} Senparc", codeRecord.Version, codeRecord.IsWebVersion ? "网页版" : ".chm文档版", SystemTime.Now.Year);
        }

        public override IResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            // 预处理文字或事件类型请求。
            // 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
            // 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
            // 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
            // 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
            // 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey

            if (requestMessage.Content == "OneClick")
            {
                var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                strongResponseMessage.Content = "您点击了底部按钮。\r\n为了测试微信软件换行bug的应对措施，这里做了一个——\r\n换行";
                return strongResponseMessage;
            }
            return null;//返回null，则继续执行OnTextRequest或OnEventRequest
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            //菜单点击，需要跟创建菜单时的Key匹配

            switch (requestMessage.EventKey)
            {
                case "OneClick":
                    {
                        //这个过程实际已经在OnTextOrEventRequest中命中“OneClick”关键字，并完成回复，这里不会执行到。
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了底部按钮。\r\n为了测试微信软件换行bug的应对措施，这里做了一个——\r\n换行";
                    }
                    break;
                case "SubClickRoot_Text":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了子菜单按钮。";
                    }
                    break;
                case "SubClickRoot_News":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "您点击了子菜单图文按钮",
                            Description = "您点击了子菜单图文按钮，这是一条图文信息。这个区域是Description内容\r\n可以使用\\r\\n进行换行。",
                            PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg",
                            Url = "https://sdk.weixin.senparc.com"
                        });

                        //随机添加一条图文，或只输出一条图文信息
                        if (SystemTime.Now.Second % 2 == 0)
                        {
                            strongResponseMessage.Articles.Add(new Article()
                            {
                                Title = "这是随机产生的第二条图文信息，用于测试多条图文的样式",
                                Description = "这是随机产生的第二条图文信息，用于测试多条图文的样式",
                                PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg",
                                Url = "https://sdk.weixin.senparc.com"
                            });
                        }
                    }
                    break;
                case "SubClickRoot_Music":
                    {
                        //上传缩略图
                        var filePath = "~/wwwroot/Images/Logo.thumb.jpg";
                        var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.thumb,
                                                                    ServerUtility.ContentRootMapPath(filePath));
                        //PS：缩略图官方没有特别提示文件大小限制，实际测试哪怕114K也会返回文件过大的错误，因此尽量控制在小一点（当前图片39K）

                        //设置音乐信息
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageMusic>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Music.Title = "天籁之音";
                        strongResponseMessage.Music.Description = "真的是天籁之音";
                        strongResponseMessage.Music.MusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3";
                        strongResponseMessage.Music.HQMusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3";
                        strongResponseMessage.Music.ThumbMediaId = uploadResult.thumb_media_id;
                    }
                    break;
                case "SubClickRoot_Image":
                    {
                        //上传图片
                        var filePath = "~/wwwroot/Images/Logo.jpg";

                        var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.image,
                                                                     ServerUtility.ContentRootMapPath(filePath));
                        //设置图片信息
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageImage>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Image.MediaId = uploadResult.media_id;
                    }
                    break;
                case "SendMenu"://菜单消息
                    {
                        //注意：
                        //1、此接口可以在任意地方调用（包括后台线程），此处演示为通过
                        //2、一下"s:"前缀只是 Senparc.Weixin 的内部约定，可以使用 OnTextRequest事件中的 requestHandler.SelectMenuKeyword() 方法自动匹配到后缀（如101）

                        var menuContentList = new List<SendMenuContent>(){
                            new SendMenuContent("101","满意"),
                            new SendMenuContent("102","一般"),
                            new SendMenuContent("103","不满意")
                        };
                        //使用异步接口
                        CustomApi.SendMenuAsync(_appId, OpenId, "请对 Senparc.Weixin SDK 给出您的评价", menuContentList, "感谢您的参与！");

                        reponseMessage = new ResponseMessageNoResponse();//不返回任何消息
                    }
                    break;
                case "SubClickRoot_Agent"://代理消息
                    {
                        //获取返回的XML
                        var dt1 = SystemTime.Now;
                        reponseMessage = MessageAgent.RequestResponseMessage(this, _agentUrl, _agentToken, RequestDocument.ToString());
                        //上面的方法也可以使用扩展方法：this.RequestResponseMessage(this,agentUrl, agentToken, RequestDocument.ToString());

                        var dt2 = SystemTime.Now;

                        if (reponseMessage is ResponseMessageNews)
                        {
                            (reponseMessage as ResponseMessageNews)
                                .Articles[0]
                                .Description += string.Format("\r\n\r\n代理过程总耗时：{0}毫秒", (dt2 - dt1).Milliseconds);
                        }
                    }
                    break;
                case "Member"://托管代理会员信息
                    {
                        //原始方法为：MessageAgent.RequestXml(this,agentUrl, agentToken, RequestDocument.ToString());//获取返回的XML
                        reponseMessage = this.RequestResponseMessage(_agentUrl, _agentToken, RequestDocument.ToString());
                    }
                    break;
                case "OAuth"://OAuth授权测试
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();

                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "OAuth2.0测试",
                            Description = "选择下面两种不同的方式进行测试，区别在于授权成功后，最后停留的页面。",
                            //Url = "https://sdk.weixin.senparc.com/oauth2",
                            //PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
                        });

                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "OAuth2.0测试（不带returnUrl），测试环境可使用",
                            Description = "OAuth2.0测试（不带returnUrl）",
                            Url = "https://sdk.weixin.senparc.com/oauth2",
                            PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
                        });

                        var returnUrl = "/OAuth2/TestReturnUrl";
                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "OAuth2.0测试（带returnUrl），生产环境强烈推荐使用",
                            Description = "OAuth2.0测试（带returnUrl）",
                            Url = "https://sdk.weixin.senparc.com/oauth2?returnUrl=" + returnUrl.UrlEncode(),
                            PicUrl = "https://sdk.weixin.senparc.com/Images/qrcode.jpg"
                        });

                        reponseMessage = strongResponseMessage;

                    }
                    break;
                case "Description":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = GetWelcomeInfo();
                        reponseMessage = strongResponseMessage;
                    }
                    break;
                case "SubClickRoot_PicPhotoOrAlbum":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了【微信拍照】按钮。系统将会弹出拍照或者相册发图。";
                    }
                    break;
                case "SubClickRoot_ScancodePush":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了【微信扫码】按钮。";
                    }
                    break;
                case "ConditionalMenu_Male":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了个性化菜单按钮，您的微信性别设置为：男。";
                    }
                    break;
                case "ConditionalMenu_Femle":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了个性化菜单按钮，您的微信性别设置为：女。";
                    }
                    break;
                case "GetNewMediaId"://获取新的MediaId
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        try
                        {
                            var result = AdvancedAPIs.MediaApi.UploadForeverMedia(_appId, ServerUtility.ContentRootMapPath("~/Images/logo.jpg"));
                            strongResponseMessage.Content = result.media_id;
                        }
                        catch (Exception e)
                        {
                            strongResponseMessage.Content = "发生错误：" + e.Message;
                            WeixinTrace.SendCustomLog("调用UploadForeverMedia()接口发生异常", e.Message);
                        }
                    }
                    break;
                default:
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您点击了按钮，EventKey：" + requestMessage.EventKey;
                        reponseMessage = strongResponseMessage;
                    }
                    break;
            }

            return reponseMessage;
        }

        /// <summary>
        /// 进入事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "您刚才发送了ENTER事件请求。";
            return responseMessage;
        }

        /// <summary>
        /// 位置事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            var locationService = EngineContext.Current.Resolve<IWxLocationService>();

            var openIdHash = WeixinHelper.OpenIdToLong(requestMessage.FromUserName);
            var locModel = locationService.GetWxLocationByOpenIdHash(openIdHash);

            if (locModel != null)
            {
                //10分钟更新一次
                if (Nop.Core.Helpers.DateTimeHelper.ConvertToDateTimeLong(DateTime.Now) > locModel.UpdateTime + 600)
                {
                    locModel.Latitude = Convert.ToDecimal(requestMessage.Latitude);
                    locModel.Longitude = Convert.ToDecimal(requestMessage.Longitude);
                    locModel.Precision = Convert.ToDecimal(requestMessage.Precision);
                    locModel.UpdateTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now));
                    //更新
                    locationService.UpdateWxLocation(locModel);
                }
            }
            else
            {
                locModel = new WxLocation()
                {
                    OpenIdHash = openIdHash,
                    Latitude = Convert.ToDecimal(requestMessage.Latitude),
                    Longitude = Convert.ToDecimal(requestMessage.Longitude),
                    Precision = Convert.ToDecimal(requestMessage.Precision),
                    UpdateTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now))
                };
                //添加
                locationService.InsertWxLocation(locModel);
            }

            return new SuccessResponseMessage();  //或者回复欢迎信息

            ////这里是微信客户端（通过微信服务器）自动发送过来的位置信息
            //var responseMessage = CreateResponseMessage<ResponseMessageText>();
            //responseMessage.Content = "这里写什么都无所谓，比如：上帝爱你！";
            //return responseMessage;//这里也可以返回null（需要注意写日志时候null的问题）
        }

        /// <summary>
        /// 通过二维码扫描关注扫描事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            var _openIdReferer = 0L; //推荐人ID
            var _sceneId = 0; //二维码整形值
            var _messageIds = new List<int>();   //多个消息ID
            var _tagList = new List<int>();  //设置微信服务端客户标签列表

            //0=默认，11=ADVER 广告，22=VERIFY 验证码，33=CMD 命令行，44=LOGIN 登录，55=BIND 绑定，66=VOTE 投票，77=CARD 个人名片
            byte _sceneTypeId = 0;  //来源场景类型：0=默认,临时二维码前缀值

            //二维码分析
            if (!string.IsNullOrEmpty(requestMessage.EventKey))
            {
                var sceneStr = requestMessage.EventKey.Replace("qrscene_", "");

                if (sceneStr.Contains("_")) //非纯数字,临时二维码
                {
                    var splitParam = sceneStr.Split('_');

                    var qrCodePrefix = (QrcodePrefix)Enum.Parse(typeof(QrcodePrefix), splitParam[0].ToUpper());  //来源场景类型前缀值赋值
                    _sceneTypeId = (byte)qrCodePrefix;


                    switch (qrCodePrefix)
                    {
                        /// <summary>
                        /// 广告,二维码Id格式：adver+openIdHash + imageId + addTime
                        /// </summary>
                        case QrcodePrefix.ADVER:
                            {
                                long.TryParse(splitParam[1], out _openIdReferer); //获取openIdHash
                                int.TryParse(splitParam[2], out _sceneId);  //获取imageId

                                if (_sceneId > 0)
                                {
                                    var _wxQrImageService = EngineContext.Current.Resolve<IWxQrImageService>();

                                    var qrImageModel = _wxQrImageService.GetWxQrImageById(_sceneId);
                                    if (qrImageModel != null && qrImageModel.MessageIds.Length > 0)
                                    {
                                        var ids = qrImageModel.MessageIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (var id in ids)
                                        {
                                            int.TryParse(id, out var result);
                                            if (result > 0)
                                                _messageIds.Add(result);
                                        }
                                    }
                                }
                            }
                            break;
                        /// <summary>
                        /// 二维码验证码
                        /// </summary>
                        case QrcodePrefix.VERIFY:
                            {
                                //verify + openid_hash + 验证码
                            }
                            break;
                        /// <summary>
                        /// 命令行
                        /// </summary>
                        case QrcodePrefix.CMD:
                            {
                                //cmd + openid_hash + 命令
                            }
                            break;
                        /// <summary>
                        /// 登录
                        /// </summary>
                        case QrcodePrefix.LOGIN:
                            {

                            }
                            break;
                        /// <summary>
                        /// 绑定
                        /// </summary>
                        case QrcodePrefix.BIND:
                            {

                            }
                            break;
                        /// <summary>
                        /// 个人名片
                        /// </summary>
                        case QrcodePrefix.CARD:
                            {

                            }
                            break;
                        /// <summary>
                        /// 投票，用于记录参与投票的人群
                        /// vote + openid_hash + 投票项目ID（场景值）+ 创建时间
                        /// 注释：vote + (投票发起人，系统官方发起为0) +项目ID（场景值）+创建时间
                        /// </summary>
                        case QrcodePrefix.VOTE:
                            {
                                long.TryParse(splitParam[1], out _openIdReferer);
                                int.TryParse(splitParam[2], out _sceneId);

                                #region  登记投票人群数据
                                if (_sceneId > 0)
                                {
                                    var _wxVoteService = EngineContext.Current.Resolve<IWxVoteService>();
                                    var voteModel = _wxVoteService.GetWxVoteById(_sceneId);

                                    if (voteModel != null)
                                    {
                                        var _wxVoteUserService = EngineContext.Current.Resolve<IWxVoteUserService>();
                                        var voteUserExist = false;

                                        //签到登记
                                        if (voteModel.SignOn)
                                        {
                                            var voteUserModel = _wxVoteUserService.GetWxVoteUserByOpenIdHashAndVoteId(WeixinHelper.OpenIdToLong(requestMessage.FromUserName), _sceneId);
                                            voteUserExist = (voteUserModel != null);

                                            if (voteUserModel == null)
                                            {
                                                _wxVoteUserService.InsertWxVoteUser(new WxVoteUser
                                                {
                                                    VoteId = _sceneId,
                                                    OpenIdHash = WeixinHelper.OpenIdToLong(requestMessage.FromUserName),
                                                    Avalid = true,
                                                    CreatTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now))
                                                });
                                            }
                                        }

                                        //设置回复消息ID
                                        if (voteUserExist)
                                        {
                                            if (!string.IsNullOrEmpty(voteModel.SignedMessageId))    //已签到消息
                                            {
                                                var ids = voteModel.SignedMessageId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                                foreach (var id in ids)
                                                {
                                                    int.TryParse(id, out var result);
                                                    if (result > 0)
                                                        _messageIds.Add(result);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(voteModel.UnsignMessageId))  //未签到消息
                                            {
                                                var ids = voteModel.UnsignMessageId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                                foreach (var id in ids)
                                                {
                                                    int.TryParse(id, out var result);
                                                    if (result > 0)
                                                        _messageIds.Add(result);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion  //登记投票人数据结束
                            }
                            break;
                    }
                }
                else //纯数字，永久二维码
                {
                    int.TryParse(sceneStr, out _sceneId);
                    if (_sceneId > 0)
                    {
                        var _wxQrLimitExtensionService = EngineContext.Current.Resolve<IWxQrLimitExtensionService>();
                        var wxQrLimitExtensionModel = _wxQrLimitExtensionService.GetWxQrLimitExtensionByQrLimitId(_sceneId);
                        if (wxQrLimitExtensionModel != null)
                        {
                            if (wxQrLimitExtensionModel.OpenIdHash.HasValue && wxQrLimitExtensionModel.OpenIdHash > 0)
                            {
                                if (!wxQrLimitExtensionModel.Deleted && wxQrLimitExtensionModel.Published)
                                {
                                    var started = wxQrLimitExtensionModel.StartTime.HasValue ? (wxQrLimitExtensionModel.StartTime < DateTime.Now) : true;
                                    var ended = wxQrLimitExtensionModel.EndTime.HasValue ? (wxQrLimitExtensionModel.EndTime < DateTime.Now) : false;

                                    if (started && !ended)
                                        _openIdReferer = Convert.ToInt64(wxQrLimitExtensionModel.OpenIdHash);//永久二维码在指定过期时间前，分配给指定用户
                                }
                            }
                        }
                    }
                }
            }

            //设置推荐人信息
            if (_openIdReferer > 0)
            {
                var _wxUserInfoBaseService = EngineContext.Current.Resolve<IWxUserInfoBaseService>();
                var userModel = _wxUserInfoBaseService.GetWxUserInfoBaseByOpenIdHash(_openIdReferer);
                if (userModel == null || !userModel.AllowReferer || !userModel.Subscribe)
                {
                    _openIdReferer = 0;
                }
            }

            //回复二维码参数消息
            var responseMessage = GetResponseMessage(_messageIds, "", _sceneId);
            if (responseMessage != null)
                return responseMessage;

            //默认回复
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 打开网页事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            //说明：这条消息只作为接收，下面的responseMessage到达不了客户端，类似OnEvent_UnsubscribeRequest
            //可以处理一些计数操作或用户状态操作
            switch (requestMessage.EventKey)
            {
                case "000000":
                    break;
                case "111111":
                    break;
                case "222222":
                    break;
                default:
                    break;
            }

            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您点击了view按钮，将打开网页：" + requestMessage.EventKey;
            return responseMessage;
        }

        /// <summary>
        /// 群发完成事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_MassSendJobFinishRequest(RequestMessageEvent_MassSendJobFinish requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "接收到了群发完成的信息。";
            return responseMessage;
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var _openIdReferer = 0L; //推荐人ID
            var _sceneId = 0; //二维码整形值
            var _messageIds = new List<int>();   //多个消息ID
            var _tagList = new List<int>();  //设置微信服务端客户标签列表

            //0=默认，11=ADVER 广告，22=VERIFY 验证码，33=CMD 命令行，44=LOGIN 登录，55=BIND 绑定，66=VOTE 投票，77=CARD 个人名片
            byte _sceneTypeId = 0;  //来源场景类型：0=默认,临时二维码前缀值

            //二维码分析(扫码进入)
            if (!string.IsNullOrWhiteSpace(requestMessage.EventKey))
            {
                var sceneStr = requestMessage.EventKey.Replace("qrscene_", "");

                if (sceneStr.Contains("_")) //非纯数字,临时二维码
                {
                    var splitParam = sceneStr.Split('_');

                    var qrCodePrefix = (QrcodePrefix)Enum.Parse(typeof(QrcodePrefix), splitParam[0].ToUpper());  //来源场景类型前缀值赋值
                    _sceneTypeId = (byte)qrCodePrefix;

                    switch (qrCodePrefix)
                    {
                        /// <summary>
                        /// 广告,二维码Id格式：adver+openIdHash + imageId + addTime
                        /// </summary>
                        case QrcodePrefix.ADVER:
                            {
                                long.TryParse(splitParam[1], out _openIdReferer); //获取openIdHash
                                int.TryParse(splitParam[2], out _sceneId);  //获取imageId

                                if (_sceneId > 0)
                                {
                                    var _wxQrImageService = EngineContext.Current.Resolve<IWxQrImageService>();

                                    var qrImageModel = _wxQrImageService.GetWxQrImageById(_sceneId);
                                    if (qrImageModel != null && qrImageModel.MessageIds.Length > 0)
                                    {
                                        var ids = qrImageModel.MessageIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (var id in ids)
                                        {
                                            int.TryParse(id, out var result);
                                            if (result > 0)
                                                _messageIds.Add(result);
                                        }
                                    }
                                }
                            }
                            break;
                        /// <summary>
                        /// 二维码验证码
                        /// </summary>
                        case QrcodePrefix.VERIFY:
                            {
                                //verify + openid_hash + 验证码
                            }
                            break;
                        /// <summary>
                        /// 命令行
                        /// </summary>
                        case QrcodePrefix.CMD:
                            {
                                //cmd + openid_hash + 命令
                            }
                            break;
                        /// <summary>
                        /// 登录
                        /// </summary>
                        case QrcodePrefix.LOGIN:
                            {

                            }
                            break;
                        /// <summary>
                        /// 绑定
                        /// </summary>
                        case QrcodePrefix.BIND:
                            {

                            }
                            break;
                        /// <summary>
                        /// 个人名片
                        /// </summary>
                        case QrcodePrefix.CARD:
                            {

                            }
                            break;
                        /// <summary>
                        /// 投票，用于记录参与投票的人群
                        /// vote + openid_hash + 投票项目ID（场景值）+ 创建时间
                        /// 注释：vote + (投票发起人，系统官方发起为0) +项目ID（场景值）+创建时间
                        /// </summary>
                        case QrcodePrefix.VOTE:
                            {
                                long.TryParse(splitParam[1], out _openIdReferer);
                                int.TryParse(splitParam[2], out _sceneId);

                                #region  登记投票人群数据
                                if (_sceneId > 0)
                                {
                                    var _wxVoteService = EngineContext.Current.Resolve<IWxVoteService>();
                                    var voteModel = _wxVoteService.GetWxVoteById(_sceneId);

                                    if (voteModel != null)
                                    {
                                        var _wxVoteUserService = EngineContext.Current.Resolve<IWxVoteUserService>();
                                        var voteUserExist = false;

                                        //签到登记
                                        if (voteModel.SignOn)
                                        {
                                            var voteUserModel = _wxVoteUserService.GetWxVoteUserByOpenIdHashAndVoteId(WeixinHelper.OpenIdToLong(requestMessage.FromUserName), _sceneId);
                                            voteUserExist = (voteUserModel != null);

                                            if (voteUserModel == null)
                                            {
                                                _wxVoteUserService.InsertWxVoteUser(new WxVoteUser
                                                {
                                                    VoteId = _sceneId,
                                                    OpenIdHash = WeixinHelper.OpenIdToLong(requestMessage.FromUserName),
                                                    Avalid = true,
                                                    CreatTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now))
                                                });
                                            }
                                        }

                                        //设置回复消息ID
                                        if (voteUserExist)
                                        {
                                            if (!string.IsNullOrEmpty(voteModel.SignedMessageId))    //已签到消息
                                            {
                                                var ids = voteModel.SignedMessageId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                                foreach (var id in ids)
                                                {
                                                    int.TryParse(id, out var result);
                                                    if (result > 0)
                                                        _messageIds.Add(result);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(voteModel.UnsignMessageId))  //未签到消息
                                            {
                                                var ids = voteModel.UnsignMessageId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                                foreach (var id in ids)
                                                {
                                                    int.TryParse(id, out var result);
                                                    if (result > 0)
                                                        _messageIds.Add(result);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion  //登记投票人数据结束
                            }
                            break;
                    }
                }
                else //纯数字，永久二维码
                {
                    int.TryParse(sceneStr, out _sceneId);
                    if (_sceneId > 0)
                    {
                        var _wxQrLimitExtensionService = EngineContext.Current.Resolve<IWxQrLimitExtensionService>();
                        var wxQrLimitExtensionModel = _wxQrLimitExtensionService.GetWxQrLimitExtensionByQrLimitId(_sceneId);
                        if (wxQrLimitExtensionModel != null)
                        {
                            if (wxQrLimitExtensionModel.OpenIdHash.HasValue && wxQrLimitExtensionModel.OpenIdHash > 0)
                            {
                                if (!wxQrLimitExtensionModel.Deleted && wxQrLimitExtensionModel.Published)
                                {
                                    var started = wxQrLimitExtensionModel.StartTime.HasValue ? (wxQrLimitExtensionModel.StartTime < DateTime.Now) : true;
                                    var ended = wxQrLimitExtensionModel.EndTime.HasValue ? (wxQrLimitExtensionModel.EndTime < DateTime.Now) : false;

                                    if (started && !ended)
                                        _openIdReferer = Convert.ToInt64(wxQrLimitExtensionModel.OpenIdHash);//永久二维码在指定过期时间前，分配给指定用户
                                }
                            }
                        }
                    }
                }
            }


            var _wxUserInfoBaseService = EngineContext.Current.Resolve<IWxUserInfoBaseService>();

            //设置推荐人信息
            WxUserInfoBase userRefererModel = null;
            if (_openIdReferer > 0)
            {
                userRefererModel = _wxUserInfoBaseService.GetWxUserInfoBaseByOpenIdHash(_openIdReferer);
                if (userRefererModel == null || !userRefererModel.AllowReferer || !userRefererModel.Subscribe)
                {
                    _openIdReferer = 0;
                }
            }


            //添加或更新用户扩展信息
            var userInfoResponse = AdvancedAPIs.UserApi.Info(_appId, requestMessage.FromUserName);
            WxUserInfo wxUserInfoModel = null;
            if (userInfoResponse.errcode == ReturnCode.请求成功)
            {
                var _wxUserInfoService = EngineContext.Current.Resolve<IWxUserInfoService>();
                wxUserInfoModel = _wxUserInfoService.GetWxUserInfoByOpenIdHash(WeixinHelper.OpenIdToLong(requestMessage.FromUserName));
                if (wxUserInfoModel != null) //修改
                {
                    wxUserInfoModel.OpenIdHash = WeixinHelper.OpenIdToLong(requestMessage.FromUserName);
                    wxUserInfoModel.NickName = userInfoResponse.nickname;
                    wxUserInfoModel.Sex = (byte)userInfoResponse.sex;
                    wxUserInfoModel.LanguageTypeId = string.IsNullOrWhiteSpace(userInfoResponse.language) ? Convert.ToByte(1) : Convert.ToByte(Enum.Parse(typeof(LanguageType), userInfoResponse.language.ToUpper()));
                    wxUserInfoModel.SubscribeSceneTypeId = string.IsNullOrWhiteSpace(userInfoResponse.subscribe_scene) ? Convert.ToByte(0) : Convert.ToByte(Enum.Parse(typeof(SubscribeSceneType), userInfoResponse.subscribe_scene.ToUpper()));
                    wxUserInfoModel.City = userInfoResponse.city;
                    wxUserInfoModel.Province = userInfoResponse.province;
                    wxUserInfoModel.Country = userInfoResponse.country;
                    wxUserInfoModel.Headimgurl = "";
                    wxUserInfoModel.Remark = userInfoResponse.remark;
                    wxUserInfoModel.GroupId = userInfoResponse.groupid;
                    wxUserInfoModel.TagIdList = string.Join(',', userInfoResponse.tagid_list);
                    wxUserInfoModel.QrScene = userInfoResponse.qr_scene.ToString();
                    wxUserInfoModel.QrSceneStr = userInfoResponse.qr_scene_str;

                    _wxUserInfoService.UpdateWxUserInfo(wxUserInfoModel);
                }
                else  //添加
                {
                    wxUserInfoModel = new WxUserInfo
                    {
                        OpenIdHash = WeixinHelper.OpenIdToLong(requestMessage.FromUserName),
                        NickName = userInfoResponse.nickname,
                        Sex = (byte)userInfoResponse.sex,
                        LanguageTypeId = string.IsNullOrWhiteSpace(userInfoResponse.language) ? Convert.ToByte(1) : Convert.ToByte(Enum.Parse(typeof(LanguageType), userInfoResponse.language.ToUpper())),
                        SubscribeSceneTypeId = string.IsNullOrWhiteSpace(userInfoResponse.subscribe_scene) ? Convert.ToByte(0) : Convert.ToByte(Enum.Parse(typeof(SubscribeSceneType), userInfoResponse.subscribe_scene.ToUpper())),
                        City = userInfoResponse.city,
                        Province = userInfoResponse.province,
                        Country = userInfoResponse.country,
                        Headimgurl = "",
                        Remark = userInfoResponse.remark,
                        GroupId = userInfoResponse.groupid,
                        TagIdList = string.Join(',', userInfoResponse.tagid_list),
                        CreatTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now)),
                        QrScene = userInfoResponse.qr_scene.ToString(),
                        QrSceneStr = userInfoResponse.qr_scene_str
                    };

                    _wxUserInfoService.InsertWxUserInfo(wxUserInfoModel);
                }
            }


            //添加/修改新用户基础信息
            var userBaseModel = _wxUserInfoBaseService.GetWxUserInfoBaseByOpenId(requestMessage.FromUserName);

            if (userBaseModel != null)  //修改
            {
                userBaseModel.OpenIdReferer = _openIdReferer;
                userBaseModel.Subscribe = true;
                userBaseModel.SceneTypeId = _sceneTypeId;
                userBaseModel.SceneId = _sceneId;
                userBaseModel.LastActiveTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now));
                userBaseModel.SubscribeTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now));

                _wxUserInfoBaseService.UpdateWxUserInfoBase(userBaseModel);
            }
            else  //添加
            {
                userBaseModel = new WxUserInfoBase
                {
                    OpenIdHash = WeixinHelper.OpenIdToLong(requestMessage.FromUserName),
                    OpenId = requestMessage.FromUserName,
                    OpenIdReferer = _openIdReferer,
                    Subscribe = true,
                    AllowReferer = true,
                    AllowRequest = true,
                    AllowOrder = true,
                    AllowNotice = false,
                    Locked = false,
                    Deleted = false,
                    SceneTypeId = _sceneTypeId,
                    RoleTypeId = 0,
                    Status = 0,
                    UnionId = "",
                    SceneId = _sceneId,
                    LastActiveTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now)),
                    SubscribeTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now)),
                    CreatTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now)),
                };

                if (userInfoResponse.errcode == ReturnCode.请求成功)
                    userBaseModel.UnionId = userInfoResponse.unionid;

                _wxUserInfoBaseService.InsertWxUserInfoBase(userBaseModel);


                //新用户添加成功后操作,赠送给（推荐人奖励） 和 （赠送新人奖励）
                if (_openIdReferer > 0)
                {
                    //操作推荐人奖励
                }

            }


            //是否给推荐人发送模板信息（暂时不用）
            if (userRefererModel != null && userRefererModel.Subscribe && userRefererModel.AllowNotice)
            {

            }


            //回复二维码参数消息
            var responseMessage = GetResponseMessage(_messageIds, "", _sceneId);
            if (responseMessage != null)
                return responseMessage;


            //循环消息中没有被动消息，检查关注消息设置
            if (_messageIds.Count == 0)
            {
                var _wxAutoReplyService = EngineContext.Current.Resolve<IWxAutoReplyService>();
                var wxAutoReplyModel = _wxAutoReplyService.GetWxAutoReplyById(1);
                if (wxAutoReplyModel != null && wxAutoReplyModel.IsAddFriendReplyOpen)
                {
                    if (!string.IsNullOrWhiteSpace(wxAutoReplyModel.AddFriendMessage))
                    {
                        var ids = wxAutoReplyModel.AddFriendMessage.Split('_', StringSplitOptions.RemoveEmptyEntries);
                        foreach (var id in ids)
                        {
                            int.TryParse(id, out var result);
                            if (result > 0)
                                _messageIds.Add(result);
                        }
                    }

                    //回复二维码参数消息
                    responseMessage = GetResponseMessage(_messageIds, "", _sceneId);
                    if (responseMessage != null)
                        return responseMessage;

                }
            }


            //默认回复
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 退订
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var _wxUserInfoBaseService = EngineContext.Current.Resolve<IWxUserInfoBaseService>();

            var wxUserInfoBaseModel = _wxUserInfoBaseService.GetWxUserInfoBaseByOpenId(requestMessage.FromUserName);

            //基础表
            if (wxUserInfoBaseModel != null)
            {
                wxUserInfoBaseModel.Subscribe = false;
                wxUserInfoBaseModel.UnSubscribeTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now));

                _wxUserInfoBaseService.UpdateWxUserInfoBase(wxUserInfoBaseModel);

                //短时间取关，扣除用户奖励或推荐奖励
                if (wxUserInfoBaseModel.UnSubscribeTime - wxUserInfoBaseModel.SubscribeTime < 86400) //24小时=86400秒
                {

                }
            }

            //用户无法收到非订阅账号的消息，所以这里可以随便写
            return new SuccessResponseMessage();
        }

        /// <summary>
        /// 事件之扫码推事件(scancode_push)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScancodePushRequest(RequestMessageEvent_Scancode_Push requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之扫码推事件";
            return responseMessage;
        }

        /// <summary>
        /// 事件之扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之扫码推事件且弹出“消息接收中”提示框";
            return responseMessage;
        }

        /// <summary>
        /// 事件之弹出拍照或者相册发图（pic_photo_or_album）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之弹出拍照或者相册发图";
            return responseMessage;
        }

        /// <summary>
        /// 事件之弹出系统拍照发图(pic_sysphoto)
        /// 实际测试时发现微信并没有推送RequestMessageEvent_Pic_Sysphoto消息，只能接收到用户在微信中发送的图片消息。
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicSysphotoRequest(RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之弹出系统拍照发图";
            return responseMessage;
        }

        /// <summary>
        /// 事件之弹出微信相册发图器(pic_weixin)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicWeixinRequest(RequestMessageEvent_Pic_Weixin requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之弹出微信相册发图器";
            return responseMessage;
        }

        /// <summary>
        /// 事件之弹出地理位置选择器（location_select）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "事件之弹出地理位置选择器";
            return responseMessage;
        }

        #region 微信认证事件推送

        public override IResponseMessageBase OnEvent_QualificationVerifySuccessRequest(RequestMessageEvent_QualificationVerifySuccess requestMessage)
        {
            //以下方法可以强制定义返回的字符串值
            //TextResponseMessage = "your content";
            //return null;

            return new SuccessResponseMessage();//返回"success"字符串
        }

        #endregion

        #region 循环检查回复消息

        /// <summary>
        /// 解析回复消息
        /// </summary>
        /// <param name="messageIds"></param>
        /// <param name="sceneType"></param>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        private IResponseMessageBase GetResponseMessage(List<int> messageIds, string sceneType, int sceneId)
        {
            if (messageIds == null || messageIds.Count == 0)
                return null;

            IResponseMessageBase responseMessage = null;
            var _wxMessageService = EngineContext.Current.Resolve<IWxMessageService>();

            foreach (var messageId in messageIds)
            {
                if (messageId <= 0)
                    continue;

                var wxMessageModel = _wxMessageService.GetWxMessageById(messageId);
                if (wxMessageModel == null)
                    continue;

                //客服消息
                if (wxMessageModel.ResponseType == ResponseType.CUSTOM)
                {
                    //TODO:客服消息

                }
                else  //被动消息
                {
                    switch (wxMessageModel.MsgType)
                    {
                        case MessageType.TEXT: //文本消息
                            {
                                if (!string.IsNullOrWhiteSpace(wxMessageModel.Content))
                                {
                                    var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                                    strongResponseMessage.Content = wxMessageModel.Content;
                                    responseMessage = strongResponseMessage;
                                }
                            }
                            break;
                        case MessageType.IMAGE: //图片image
                            {
                                var strongResponseMessage = CreateResponseMessage<ResponseMessageImage>();
                                if (!wxMessageModel.IsMaterial)
                                {
                                    if (string.IsNullOrWhiteSpace(wxMessageModel.MediaId))
                                    {
                                        if (!string.IsNullOrWhiteSpace(wxMessageModel.PicUrl))
                                        {
                                            //上传素材
                                            var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.image,
                                                                                         ServerUtility.ContentRootMapPath(wxMessageModel.PicUrl));

                                            //更新
                                            wxMessageModel.MediaId = uploadResult.media_id;
                                            wxMessageModel.CreatTime = uploadResult.created_at;
                                            _wxMessageService.UpdateWxMessage(wxMessageModel);
                                        }
                                    }
                                    else if (wxMessageModel.CreatTime.HasValue && wxMessageModel.CreatTime + 259200 - 3600 < Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime(DateTime.Now))
                                    {
                                        //判断过期，临时素材保存3天
                                        if (!string.IsNullOrWhiteSpace(wxMessageModel.PicUrl))
                                        {
                                            //上传素材
                                            var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.image,
                                                                                         ServerUtility.ContentRootMapPath(wxMessageModel.PicUrl));

                                            //更新
                                            wxMessageModel.MediaId = uploadResult.media_id;
                                            wxMessageModel.CreatTime = uploadResult.created_at;
                                            _wxMessageService.UpdateWxMessage(wxMessageModel);
                                        }
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(wxMessageModel.MediaId))
                                {
                                    strongResponseMessage.Image.MediaId = wxMessageModel.MediaId;
                                    responseMessage = strongResponseMessage;
                                }
                            }
                            break;
                        case MessageType.VOICE: //语音voice
                            {
                                var strongResponseMessage = CreateResponseMessage<ResponseMessageVoice>();
                                if (!wxMessageModel.IsMaterial)
                                {
                                    if (string.IsNullOrWhiteSpace(wxMessageModel.MediaId))
                                    {
                                        if (!string.IsNullOrWhiteSpace(wxMessageModel.MusicUrl))
                                        {
                                            //上传素材
                                            var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.voice,
                                                                                         ServerUtility.ContentRootMapPath(wxMessageModel.MusicUrl));

                                            //更新
                                            wxMessageModel.MediaId = uploadResult.media_id;
                                            wxMessageModel.CreatTime = uploadResult.created_at;
                                            _wxMessageService.UpdateWxMessage(wxMessageModel);
                                        }
                                    }
                                    else if (wxMessageModel.CreatTime.HasValue && wxMessageModel.CreatTime + 259200 - 3600 < Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime(DateTime.Now))
                                    {
                                        //判断过期，临时素材保存3天
                                        if (!string.IsNullOrWhiteSpace(wxMessageModel.MusicUrl))
                                        {
                                            //上传素材
                                            var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.voice,
                                                                                         ServerUtility.ContentRootMapPath(wxMessageModel.MusicUrl));

                                            //更新
                                            wxMessageModel.MediaId = uploadResult.media_id;
                                            wxMessageModel.CreatTime = uploadResult.created_at;
                                            _wxMessageService.UpdateWxMessage(wxMessageModel);
                                        }
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(wxMessageModel.MediaId))
                                {
                                    strongResponseMessage.Voice.MediaId = wxMessageModel.MediaId;
                                    responseMessage = strongResponseMessage;
                                }
                            }
                            break;
                        case MessageType.VIDEO: //视频video
                            {
                                var strongResponseMessage = CreateResponseMessage<ResponseMessageVideo>();
                                if (!wxMessageModel.IsMaterial)
                                {
                                    if (string.IsNullOrWhiteSpace(wxMessageModel.MediaId))
                                    {
                                        if (!string.IsNullOrWhiteSpace(wxMessageModel.MusicUrl))
                                        {
                                            //上传素材
                                            var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.video,
                                                                                         ServerUtility.ContentRootMapPath(wxMessageModel.MusicUrl));

                                            //更新
                                            wxMessageModel.MediaId = uploadResult.media_id;
                                            wxMessageModel.CreatTime = uploadResult.created_at;
                                            _wxMessageService.UpdateWxMessage(wxMessageModel);
                                        }
                                    }
                                    else if (wxMessageModel.CreatTime.HasValue && wxMessageModel.CreatTime + 259200 - 3600 < Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime(DateTime.Now))
                                    {
                                        //判断过期，临时素材保存3天
                                        if (!string.IsNullOrWhiteSpace(wxMessageModel.MusicUrl))
                                        {
                                            //上传素材
                                            var uploadResult = AdvancedAPIs.MediaApi.UploadTemporaryMedia(_appId, UploadMediaFileType.video,
                                                                                         ServerUtility.ContentRootMapPath(wxMessageModel.MusicUrl));

                                            //更新
                                            wxMessageModel.MediaId = uploadResult.media_id;
                                            wxMessageModel.CreatTime = uploadResult.created_at;
                                            _wxMessageService.UpdateWxMessage(wxMessageModel);
                                        }
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(wxMessageModel.MediaId))
                                {
                                    strongResponseMessage.Video.MediaId = wxMessageModel.MediaId;

                                    strongResponseMessage.Video.Title = wxMessageModel.Title;
                                    strongResponseMessage.Video.Description = wxMessageModel.Description;

                                    responseMessage = strongResponseMessage;
                                }
                            }
                            break;
                        case MessageType.SHORTVIDEO: //短视频video
                            {

                            }
                            break;
                        case MessageType.MUSIC: //音乐music
                            {
                                if (!string.IsNullOrWhiteSpace(wxMessageModel.ThumbMediaId))
                                {
                                    var strongResponseMessage = CreateResponseMessage<ResponseMessageMusic>();
                                    strongResponseMessage.Music.Title = wxMessageModel.Title;
                                    strongResponseMessage.Music.Description = wxMessageModel.Description;
                                    strongResponseMessage.Music.MusicUrl = wxMessageModel.MusicUrl;
                                    strongResponseMessage.Music.HQMusicUrl = wxMessageModel.HqMusicUrl;
                                    strongResponseMessage.Music.ThumbMediaId = wxMessageModel.ThumbMediaId;

                                    responseMessage = strongResponseMessage;
                                }
                            }
                            break;
                        case MessageType.NEWS: //图文
                            {
                                var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();

                                if (!string.IsNullOrWhiteSpace(wxMessageModel.Content))
                                {
                                    //获取文章ID列表
                                    var strIds = wxMessageModel.Content.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                    var intIds = new List<int>();
                                    foreach (var id in strIds)
                                    {
                                        if (!string.IsNullOrEmpty(id.Trim()))
                                        {
                                            int.TryParse(id.Trim(), out var id_out);
                                            if (id_out > 0 && !intIds.Contains(id_out))
                                            {
                                                intIds.Add(Convert.ToInt32(id));
                                            }
                                        }
                                    }
                                    //赋值
                                    var messages = _wxMessageService.GetWxMessageByIds(intIds.ToArray(), MessageType.NEWS);

                                    if (messages != null && messages.Count > 0)
                                    {
                                        var newsCount = 0;
                                        foreach (var message in messages)
                                        {
                                            strongResponseMessage.Articles.Add(new NeuChar.Entities.Article
                                            {
                                                Title = message.Title,
                                                Description = message.Description,
                                                PicUrl = strongResponseMessage.Articles.Count == 0 ? message.PicUrl : message.ThumbPicUrl,  //第二条开始为小图
                                                Url = message.SourceUrl
                                            });
                                            if (newsCount == 8)
                                                break;
                                        }
                                    }
                                }

                                if (strongResponseMessage.Articles.Count > 0)
                                    responseMessage = strongResponseMessage;
                            }
                            break;
                    }
                }
            }

            return responseMessage;
        }

        #endregion

        #region 扫码二维码参数解析
        //requestMessage.EventKey
        private void GetQrCodeParams(string eventKey, bool isLimitQrCode, byte _sceneTypeId, long _openIdReferer, int _sceneId, List<int> _messageIds, IRequestMessageBase requestMessage)
        {
            if (string.IsNullOrWhiteSpace(eventKey))
                return;
            //二维码分析(扫码进入)

            var sceneStr = eventKey.Replace("qrscene_", "");

            if (sceneStr.Contains("_")) //非纯数字,临时二维码
            {
                isLimitQrCode = false;
                var splitParam = sceneStr.Split('_');

                var qrCodePrefix = (QrcodePrefix)Enum.Parse(typeof(QrcodePrefix), splitParam[0].ToUpper());  //来源场景类型前缀值赋值
                _sceneTypeId = (byte)qrCodePrefix;

                switch (qrCodePrefix)
                {
                    /// <summary>
                    /// 广告,二维码Id格式：adver+openIdHash + imageId + addTime
                    /// </summary>
                    case QrcodePrefix.ADVER:
                        {
                            long.TryParse(splitParam[1], out _openIdReferer); //获取openIdHash
                            int.TryParse(splitParam[2], out _sceneId);  //获取imageId

                            if (_sceneId > 0)
                            {
                                var _wxQrImageService = EngineContext.Current.Resolve<IWxQrImageService>();

                                var qrImageModel = _wxQrImageService.GetWxQrImageById(_sceneId);
                                if (qrImageModel != null && qrImageModel.MessageIds.Length > 0)
                                {
                                    var ids = qrImageModel.MessageIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var id in ids)
                                    {
                                        int.TryParse(id, out var result);
                                        if (result > 0)
                                            _messageIds.Add(result);
                                    }
                                }
                            }
                        }
                        break;
                    /// <summary>
                    /// 二维码验证码
                    /// </summary>
                    case QrcodePrefix.VERIFY:
                        {
                            //verify + openid_hash + 验证码
                        }
                        break;
                    /// <summary>
                    /// 命令行
                    /// </summary>
                    case QrcodePrefix.CMD:
                        {
                            //cmd + openid_hash + 命令
                        }
                        break;
                    /// <summary>
                    /// 登录
                    /// </summary>
                    case QrcodePrefix.LOGIN:
                        {

                        }
                        break;
                    /// <summary>
                    /// 绑定
                    /// </summary>
                    case QrcodePrefix.BIND:
                        {

                        }
                        break;
                    /// <summary>
                    /// 个人名片
                    /// </summary>
                    case QrcodePrefix.CARD:
                        {

                        }
                        break;
                    /// <summary>
                    /// 投票，用于记录参与投票的人群
                    /// vote + openid_hash + 投票项目ID（场景值）+ 创建时间
                    /// 注释：vote + (投票发起人，系统官方发起为0) +项目ID（场景值）+创建时间
                    /// </summary>
                    case QrcodePrefix.VOTE:
                        {
                            long.TryParse(splitParam[1], out _openIdReferer);
                            int.TryParse(splitParam[2], out _sceneId);

                            #region  登记投票人群数据
                            if (_sceneId > 0)
                            {
                                var _wxVoteService = EngineContext.Current.Resolve<IWxVoteService>();
                                var voteModel = _wxVoteService.GetWxVoteById(_sceneId);

                                if (voteModel != null)
                                {
                                    var _wxVoteUserService = EngineContext.Current.Resolve<IWxVoteUserService>();
                                    var voteUserExist = false;

                                    //签到登记
                                    if (voteModel.SignOn)
                                    {
                                        var voteUserModel = _wxVoteUserService.GetWxVoteUserByOpenIdHashAndVoteId(WeixinHelper.OpenIdToLong(requestMessage.FromUserName), _sceneId);
                                        voteUserExist = (voteUserModel != null);

                                        if (voteUserModel == null)
                                        {
                                            _wxVoteUserService.InsertWxVoteUser(new WxVoteUser
                                            {
                                                VoteId = _sceneId,
                                                OpenIdHash = WeixinHelper.OpenIdToLong(requestMessage.FromUserName),
                                                Avalid = true,
                                                CreatTime = Convert.ToInt32(DateTimeHelper.ConvertToDateTimeLong(DateTime.Now))
                                            });
                                        }
                                    }

                                    //设置回复消息ID
                                    if (voteUserExist)
                                    {
                                        if (!string.IsNullOrEmpty(voteModel.SignedMessageId))    //已签到消息
                                        {
                                            var ids = voteModel.SignedMessageId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            foreach (var id in ids)
                                            {
                                                int.TryParse(id, out var result);
                                                if (result > 0)
                                                    _messageIds.Add(result);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(voteModel.UnsignMessageId))  //未签到消息
                                        {
                                            var ids = voteModel.UnsignMessageId.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                            foreach (var id in ids)
                                            {
                                                int.TryParse(id, out var result);
                                                if (result > 0)
                                                    _messageIds.Add(result);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion  //登记投票人数据结束
                        }
                        break;
                }
            }
            else //纯数字，永久二维码
            {
                isLimitQrCode = true;
                int.TryParse(sceneStr, out _sceneId);
                if (_sceneId > 0)
                {
                    var _wxQrLimitExtensionService = EngineContext.Current.Resolve<IWxQrLimitExtensionService>();
                    var wxQrLimitExtensionModel = _wxQrLimitExtensionService.GetWxQrLimitExtensionByQrLimitId(_sceneId);
                    if (wxQrLimitExtensionModel != null)
                    {
                        if (wxQrLimitExtensionModel.OpenIdHash.HasValue && wxQrLimitExtensionModel.OpenIdHash > 0)
                        {
                            if (!wxQrLimitExtensionModel.Deleted && wxQrLimitExtensionModel.Published)
                            {
                                var started = wxQrLimitExtensionModel.StartTime.HasValue ? (wxQrLimitExtensionModel.StartTime < DateTime.Now) : true;
                                var ended = wxQrLimitExtensionModel.EndTime.HasValue ? (wxQrLimitExtensionModel.EndTime < DateTime.Now) : false;

                                if (started && !ended)
                                    _openIdReferer = Convert.ToInt64(wxQrLimitExtensionModel.OpenIdHash);//永久二维码在指定过期时间前，分配给指定用户
                            }
                        }
                    }
                }
            }

        }

        #endregion
    }
}