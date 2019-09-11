using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信永久二维码
    /// </summary>
    public partial class WxQrLimit : BaseEntity
    {
        private ICollection<WxQrLimitExtension> _qrLimitExtensions;

        /// <summary>
        /// 永久二维码名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 保存永久二维码ID值（备份用）
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 二维码分组ID
        /// </summary>
        public int? QrGroupId { get; set; }
        
        /// <summary>
        /// 二维码渠道ID
        /// </summary>
        public int? QrChannelId { get; set; }
        
        /// <summary>
        /// 获取的二维码ticket
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
        /// </summary>
        public string Url { get; set; }



        /// <summary>
        /// 二维码分组
        /// </summary>
        public virtual WxQrGroup QrGroup { get; set; }

        /// <summary>
        /// 二维码渠道
        /// </summary>
        public virtual WxQrChannel QrChannel { get; set; }

        public virtual ICollection<WxQrLimitExtension> QrLimitExtensions
        {
            get => _qrLimitExtensions ?? (_qrLimitExtensions = new List<WxQrLimitExtension>());
            protected set => _qrLimitExtensions = value;
        }
    }
}