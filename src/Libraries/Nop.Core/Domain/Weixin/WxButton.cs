using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信菜单按钮信息
    /// </summary>
    public partial class WxButton : BaseEntity
    {
        /// <summary>
        /// 不是微信服务器返回的MenuId
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 按钮值ID
        /// </summary>
        public int ButtonValueId { get; set; }


        public int Row { get; set; }

        public int Column { get; set; }
        


        public virtual WxMenu Menu { get; set; }

        public virtual WxButtonValue ButtonValue { get; set; }

    }
}