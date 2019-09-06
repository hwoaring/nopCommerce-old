using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信永久二维码扩展信息
    /// </summary>
    public partial class WxQrLimitExtension : BaseEntity
    {
        private IList<WxMessage> _messages;

        /// <summary>
        /// 微信永久二维码使用情况扩展名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 使用情况描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 绑定的永久二维码ID
        /// </summary>
        public int QrLimitId { get; set; }
        /// <summary>
        /// 如果有指定绑定用户的ID
        /// </summary>
        public long? OpenIdHash { get; set; }
        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 用户被打上的标签ID列表（扫描该二维码用户自动打上标签）
        /// </summary>
        public string TagIds { get; set; }
        /// <summary>
        /// 扫码后发送消息类型
        /// </summary>
        public byte MessageTypeId { get; set; }
        /// <summary>
        /// 扫码回复wxMessageIds，以逗号分开。第一条作为自动回复，如果多余1条，就采用客服消息回复,为空，就检查Content内容以文本形式回复
        /// </summary>
        public string MessageIds { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容是否使用消息ID
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


        public MessageType MessageType
        {
            get => (MessageType)MessageTypeId;
            set => MessageTypeId = (byte)value;
        }

        public IList<WxMessage> Messages
        {
            get => _messages ?? (_messages = new List<WxMessage>());
            protected set => _messages = value;
        }


        public virtual WxQrLimit QrLimit { get; set; }

        public virtual WxUserInfoBase UserInfoBase { get; set; }
    }
}