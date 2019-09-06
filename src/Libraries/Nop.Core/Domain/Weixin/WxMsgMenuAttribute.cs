using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 菜单选项属性值
    /// </summary>
    public partial class WxMsgMenuAttribute : BaseEntity
    {
        private ICollection<WxMsgMenuValue> _msgMenuValues;

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MsgMenuId { get; set; }
        /// <summary>
        /// 菜单选项内容
        /// </summary>
        public string Content { get; set; }

        public bool Deleted { get; set; }

        public bool Published { get; set; }

        public int SortId { get; set; }



        public virtual WxMsgMenu MsgMenu { get; set; }

        public virtual ICollection<WxMsgMenuValue> MsgMenuValues
        {
            get => _msgMenuValues ?? (_msgMenuValues = new List<WxMsgMenuValue>());
            protected set => _msgMenuValues = value;
        }
    }
}