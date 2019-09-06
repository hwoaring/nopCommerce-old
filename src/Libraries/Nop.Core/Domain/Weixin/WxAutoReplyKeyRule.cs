using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 消息自动回复关键词规则设置
    /// </summary>
    public partial class WxAutoReplyKeyRule : BaseEntity
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 关键词列表，以逗号分开
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 回复模式：0=全部回复，>1表示随机回复几条
        /// </summary>
        public byte ReplyMode { get; set; }
        /// <summary>
        /// 匹配模式:1=完全匹配，2=包含
        /// </summary>
        public byte MatchMode { get; set; }
        /// <summary>
        /// 回复消息类型
        /// </summary>
        public byte MessageTypeId { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreatTime { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 使用消息ID，设置后在内容中填写消息IDs
        /// </summary>
        public bool UseMessageId { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool Published { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType
        {
            get => (MessageType)MessageTypeId;
            set => MessageTypeId = (byte)value;
        }
    }
}