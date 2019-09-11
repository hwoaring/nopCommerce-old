namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// MessageType：TEXT，IMAGE，VOICE，VIDEO等
    /// </summary>
    public enum MessageType : byte
    {
        /// <summary>
        /// 图文消息
        /// </summary>
        TEXT = 1,
        /// <summary>
        /// 图片消息
        /// </summary>
        IMAGE = 2,
        /// <summary>
        /// 语音消息
        /// </summary>
        VOICE = 3,
        /// <summary>
        /// 视频消息
        /// </summary>
        VIDEO = 4,
        /// <summary>
        /// 小视频消息
        /// </summary>
        SHORTVIDEO = 5,
        /// <summary>
        /// 音乐消息
        /// </summary>
        MUSIC = 6,
        /// <summary>
        /// 图文消息（点击跳转到外链）
        /// </summary>
        NEWS = 7,
        /// <summary>
        /// 图文消息（点击跳转到图文消息页面）
        /// </summary>
        MPNEWS = 8,
        /// <summary>
        /// 卡券
        /// </summary>
        WXCARD = 9,
        /// <summary>
        /// 小程序
        /// </summary>
        MINIPROGRAMPAGE = 10,
        /// <summary>
        /// 地理位置消息
        /// </summary>
        LOCATION = 11,
        /// <summary>
        /// 链接消息
        /// </summary>
        LINK = 12,
        /// <summary>
        /// 事件消息
        /// </summary>
        EVENT = 13,
        /// <summary>
        /// 客服发送菜单消息
        /// </summary>
        MSGMENU
    }
}
