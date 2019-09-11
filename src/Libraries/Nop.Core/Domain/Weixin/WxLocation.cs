using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信用户地理位置
    /// </summary>
    public partial class WxLocation : BaseEntity
    {
        /// <summary>
        /// Openidhash
        /// </summary>
        public long OpenIdHash { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public decimal Precision { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public int UpdateTime { get; set; }
    }
}