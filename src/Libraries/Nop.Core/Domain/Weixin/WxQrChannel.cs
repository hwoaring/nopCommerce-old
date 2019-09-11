using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信二维码渠道
    /// </summary>
    public partial class WxQrChannel : BaseEntity
    {
        private ICollection<WxQrChannel> _childChannels;

        /// <summary>
        /// 父级Id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 渠道名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Published { get; set; }
        /// <summary>
        /// 是否删除，不允许直接删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }



        public virtual ICollection<WxQrChannel> ChildChannels
        {
            get => _childChannels ?? (_childChannels = new List<WxQrChannel>());
            protected set => _childChannels = value;
        }
    }
}