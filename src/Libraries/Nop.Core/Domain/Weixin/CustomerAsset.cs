using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 用户资产等级表
    /// </summary>
    public partial class CustomerAsset : BaseEntity
    {
        private ICollection<CustomerAmount> _customerAmounts;
        private ICollection<CustomerPoint> _customerPoints;


        /// <summary>
        /// 用户OpenIdhash
        /// </summary>
        public long OpenIdHash { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public byte Level { get; set; }
        /// <summary>
        /// 用户星级
        /// </summary>
        public byte Stars { get; set; }
        /// <summary>
        /// 活跃度
        /// </summary>
        public byte Activity { get; set; }
        /// <summary>
        /// 用户折扣
        /// </summary>
        public byte Discount { get; set; }
        /// <summary>
        /// VIP等级
        /// </summary>
        public byte Vip { get; set; }
        /// <summary>
        /// Plus会员
        /// </summary>
        public byte Plus { get; set; }
        /// <summary>
        /// 用户总积分
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 虚拟币：单位根据需求自定义
        /// </summary>
        public decimal VirtualCurrency { get; set; }
        /// <summary>
        /// 用户总金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 用户总分享次数
        /// </summary>
        public int ShareCount { get; set; }
        /// <summary>
        /// 用户升级经验值
        /// </summary>
        public int Exp { get; set; }
        /// <summary>
        /// 用户积分过期时间
        /// </summary>
        public int PointExpired { get; set; }
        /// <summary>
        /// VIP过期时间
        /// </summary>
        public int VipExpired { get; set; }
        /// <summary>
        /// Plus会员过期时间
        /// </summary>
        public int PlusExpired { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreatTime { get; set; }


        public virtual ICollection<CustomerAmount> CustomerAmounts
        {
            get => _customerAmounts ?? (_customerAmounts = new List<CustomerAmount>());
            protected set => _customerAmounts = value;
        }

        public virtual ICollection<CustomerPoint> CustomerPoints
        {
            get => _customerPoints ?? (_customerPoints = new List<CustomerPoint>());
            protected set => _customerPoints = value;
        }
    }
}