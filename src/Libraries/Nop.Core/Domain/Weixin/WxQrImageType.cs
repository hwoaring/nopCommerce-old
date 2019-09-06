namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 二维码类型，返回关注二维码，还是URL链接形式二维码
    /// </summary>
    public enum WxQrImageType : byte
    {
        /// <summary>
        /// 关注公众号类型subscribe
        /// </summary>
        SUBSCRIBE = 1,
        /// <summary>
        /// url内容二维码
        /// </summary>
        URL = 2
    }
}
