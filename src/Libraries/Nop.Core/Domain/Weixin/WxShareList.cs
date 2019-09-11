using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信分享列表
    /// </summary>
    public partial class WxShareList : BaseEntity
    {
        private ICollection<WxShareCount> _shareCounts;

        /// <summary>
        /// 微信ID
        /// </summary>
        public long OpenIdHash { get; set; }
        /// <summary>
        /// 分享的产品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        public int ReadCount { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int ThumbCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreatTime { get; set; }


        /// <summary>
        /// 产品导航
        /// </summary>
        public virtual Catalog.Product Product { get; set; }

        /// <summary>
        /// 阅读和点赞详情
        /// </summary>
        public virtual ICollection<WxShareCount> ShareCounts
        {
            get => _shareCounts ?? (_shareCounts = new List<WxShareCount>());
            protected set => _shareCounts = value;
        }
    }
}