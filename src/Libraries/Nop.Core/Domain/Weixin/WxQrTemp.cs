using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信临时二维码
    /// （验证、命令用的临时二维码只存储在缓存中，不用保存到该表中。）
    /// 二维码id为：adver+openid_hash + image_id + add_time，的方式生成ID值，用户获取场景值后，可直接提取出推荐人ID和广告图imageID（广告场景值）
    /// 多的add_time字段用于防止过期前提前生成后造成相同id二维码马上失效
    /// </summary>
    public partial class WxQrTemp : BaseEntity
    {
        /// <summary>
        /// 创建二维码的用户ID，不能为0
        /// </summary>
        public long OpenIdHash { get; set; }
        /// <summary>
        /// 【二维码-图片合成宣传广告原图表】，不使用背景图，值为0
        /// </summary>
        public int? QrImageId { get; set; }

        /// <summary>
        /// 二维码用途前缀ID：adver广告，
        /// </summary>
        public byte QrcodePrefixId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreatTime { get; set; }

        /// <summary>
        /// 过期时间=创建时间+过期秒数
        /// </summary>
        public int ExpireTime { get; set; }
        /// <summary>
        /// 二维码Ticket
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 二维码Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 状态或删除标识，1，正常，2，过期，3删除
        /// </summary>
        public byte Status { get; set; }


        /// <summary>
        /// 二维码用途前缀ID：11=adver=广告，
        /// </summary>
        public QrcodePrefix QrcodePrefix
        {
            get => (QrcodePrefix)QrcodePrefixId;
            set => QrcodePrefixId = (byte)value;
        }

        /// <summary>
        /// 【二维码-图片合成宣传广告原图表】
        /// </summary>
        public virtual WxQrImage QrImage { get; set; }

        public virtual WxUserInfoBase UserInfoBase { get; set; }
    }
}