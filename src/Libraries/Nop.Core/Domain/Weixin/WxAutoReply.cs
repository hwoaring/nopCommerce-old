using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 消息自动回复设置
    /// </summary>
    public partial class WxAutoReply : BaseEntity
    {
        /// <summary>
        /// 公众号原始ID
        /// </summary>
        public int RawId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 关注时回复消息或ID列表
        /// </summary>
        public string AddFriendMessage { get; set; }
        /// <summary>
        /// 默认回复消息或ID列表
        /// </summary>
        public string DefaultMessage { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 是否关注时回复
        /// </summary>
        public bool IsAddFriendReplyOpen { get; set; }
        /// <summary>
        /// 消息自动回复是否开启
        /// </summary>
        public bool IsAutoReplyOpen { get; set; }
        /// <summary>
        /// 是否启用二维码参数判断，如果带有回访内容，就采用二维码对应内容回复。
        /// </summary>
        public bool IsQrcodeOpen { get; set; }
        /// <summary>
        /// 是否使用消息ID
        /// </summary>
        public bool UseAddFriendMessageId { get; set; }
        /// <summary>
        /// 是否使用消息ID
        /// </summary>
        public bool UseDefaultMessageId { get; set; }
    }
}