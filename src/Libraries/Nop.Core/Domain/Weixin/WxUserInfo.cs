using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class WxUserInfo : BaseEntity
    {

        /// <summary>
        /// 微信用户ID
        /// </summary>
        public long OpenIdHash { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public byte Sex { get; set; }
        /// <summary>
        /// 用户语言枚举ID
        /// </summary>
        public byte LanguageTypeId { get; set; }

        /// <summary>
        /// 返回用户关注的渠道来源ID
        /// </summary>
        public byte SubscribeSceneTypeId { get; set; }

        /// <summary>
        /// 用户语言枚举
        /// </summary>
        public LanguageType LanguageType
        {
            get => (LanguageType)LanguageTypeId;
            set => LanguageTypeId = (byte)value;
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 省市
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string Headimgurl { get; set; }

        /// <summary>
        /// 公众号运营者对粉丝的备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 三方平台运营者对粉丝的备注
        /// </summary>
        public string RemarkSystem { get; set; }
        /// <summary>
        /// 微信用户所在的分组ID（暂时兼容用户分组旧接口）
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 系统用户分组ID(UserGroup表)
        /// </summary>
        public int? UserGroupId { get; set; }
        /// <summary>
        /// 微信后台用户标签列表
        /// </summary>
        public string TagIdList { get; set; }

        public int CreatTime { get; set; }

        /// <summary>
        /// 二维码扫码场景（开发者自定义）
        /// </summary>
        public string QrScene { get; set; }
        /// <summary>
        /// 二维码扫码场景描述（开发者自定义）
        /// </summary>
        public string QrSceneStr { get; set; }

        /// <summary>
        /// 返回用户关注的渠道来源
        /// </summary>
        public SubscribeSceneType SubscribeSceneType
        {
            get => (SubscribeSceneType)SubscribeSceneTypeId;
            set => SubscribeSceneTypeId = (byte)value;
        }

        public virtual WxUserInfoBase UserInfoBase { get; set; }

        public virtual WxUserGroup UserGroup { get; set; }
    }
}