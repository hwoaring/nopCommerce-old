using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    ///  用户金额详情表
    /// </summary>
    public partial class CustomerAmount : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long OpenIdHash { get; set; }
        /// <summary>
        /// 订单号ID
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 积分值
        /// </summary>
        public decimal AmountValue { get; set; }
        /// <summary>
        /// 消费类型说明ID
        /// </summary>
        public byte ConsumeTypeId { get; set; }
        /// <summary>
        /// 积分过期时间
        /// </summary>
        public int ExpiredTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreatTime { get; set; }
        /// <summary>
        /// 状态（可设置pending状态等）
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// 消息类型内容
        /// </summary>
        public ConsumeType ConsumeType
        {
            get => (ConsumeType)ConsumeTypeId;
            set => ConsumeTypeId = (byte)value;
        }

        public virtual CustomerAsset CustomerAsset { get; set; }

        public virtual Orders.Order Order { get; set; }
    }
}