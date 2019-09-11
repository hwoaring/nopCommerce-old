using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信服务器返回的用户标签
    /// </summary>
    public partial class WxTag
    {
        /// <summary>
        /// 标签id，由微信分配(最多100个） 不能修改0/1/2这三个系统默认保留的标签
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标签名长度超过30个字节
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 此标签下粉丝数
        /// </summary>
        public int Count { get; set; }


    }
}