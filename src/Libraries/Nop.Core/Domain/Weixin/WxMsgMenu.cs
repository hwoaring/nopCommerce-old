using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 客服发送的菜单消息选项
    /// </summary>
    public partial class WxMsgMenu : BaseEntity
    {
        private ICollection<WxMsgMenuAttribute> _msgMenuAttributes;

        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HeadContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TailContent { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int SortId { get; set; }

        public bool Deleted { get; set; }

        public bool Published { get; set; }


        public virtual ICollection<WxMsgMenuAttribute> MsgMenuAttributes
        {
            get => _msgMenuAttributes ?? (_msgMenuAttributes = new List<WxMsgMenuAttribute>());
            protected set => _msgMenuAttributes = value;
        }
    }
}