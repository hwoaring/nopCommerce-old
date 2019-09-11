using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 菜单选项用户点击后的返回值
    /// </summary>
    public partial class WxMsgMenuValue : BaseEntity
    {
        public long OpenIdHash { get; set; }
        public int BizMsgMenuId { get; set; }
        public int CreatTime { get; set; }

        public virtual WxMsgMenuAttribute MsgMenuAttribute { get; set; }
    }
}