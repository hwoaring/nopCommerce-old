using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信消息表
    /// </summary>
    public partial class WxMessage : BaseEntity
    {
        private ICollection<MessageArticle> _articles;

        /// <summary>
        /// 是否显示封面
        /// </summary>
        public bool ShowCover { get; set; }
        /// <summary>
        /// media_id是否永久素材ID
        /// </summary>
        public bool IsMaterial { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }


        /// <summary>
        /// 图文消息/视频消息/音乐消息/小程序卡片的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息关键词
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 图文消息产品IDs
        /// </summary>
        public string ProductIds { get; set; }
        /// <summary>
        /// 回复方式ID：被动方式passive，客服方式custom
        /// </summary>
        public byte ResponseTypeId { get; set; }
        /// <summary>
        /// 客服ID
        /// </summary>
        public string KfAccount { get; set; }
        /// <summary>
        /// 消息类型ID
        /// </summary>
        public byte MsgTypeId { get; set; }
        /// <summary>
        /// 发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 卡卷消息卡的ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 图片素材上传时间戳
        /// </summary>
        public long? CreatTime { get; set; }
        /// <summary>
        /// 缩略图/小程序卡片图片的媒体ID，小程序卡片图片建议大小为520*416
        /// </summary>
        public string ThumbMediaId { get; set; }

        /// <summary>
        /// 存储图片地址,对应media_id
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 专门存储小图80*80,对应thumb_media_id
        /// </summary>
        public string ThumbPicUrl { get; set; }
        /// <summary>
        /// 图文消息/视频消息/音乐消息的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicUrl { get; set; }
        /// <summary>
        /// hqmusicurl 高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        public string HqMusicUrl { get; set; }
        /// <summary>
        /// 原文的URL，若置空则无查看原文入口
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 小程序的appid，要求小程序的appid需要与公众号有关联关系
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 小程序的页面路径，跟app.json对齐，支持参数，比如pages/index/index?foo=bar
        /// </summary>
        public string PagePath { get; set; }
        /// <summary>
        /// text时的回复内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 图文消息列表
        /// </summary>
        public ICollection<MessageArticle> Articles
        {
            get => _articles ?? (_articles = new List<MessageArticle>());
            protected set => _articles = value;
        }

        /// <summary>
        /// 消息类型内容
        /// </summary>
        public MessageType MsgType
        {
            get => (MessageType)MsgTypeId;
            set => MsgTypeId = (byte)value;
        }
        /// <summary>
        /// 回复方式
        /// </summary>
        public ResponseType ResponseType
        {
            get => (ResponseType)ResponseTypeId;
            set => ResponseTypeId = (byte)value;
        }
    }


    /// <summary>
    /// 图文信息
    /// </summary>
    public partial class MessageArticle
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url { get; set; }
    }


}