using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信分享文章阅读、点赞、统计【用于限制刷票】
    /// </summary>
    public partial class WxShareCount : BaseEntity
    {
        /// <summary>
        /// WxShareList表ID
        /// </summary>
        public int ShareListId { get; set; }
        public long OpenIdHash { get; set; }
        public int CreatTime { get; set; }
        public bool HasThumbUp { get; set; }

        public virtual WxShareList ShareList { get; set; }

        public virtual WxUserInfoBase UserInfoBase { get; set; }
    }
}