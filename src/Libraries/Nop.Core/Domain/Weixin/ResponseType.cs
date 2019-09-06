namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 回复类型被动回复=passive,客服方式=custom
    /// </summary>
    public enum ResponseType : byte
    {
        /// <summary>
        /// 被动回复passive
        /// </summary>
        PASSIVE = 1,
        /// <summary>
        /// 客服方式 custom
        /// </summary>
        CUSTOM = 2
    }
}
