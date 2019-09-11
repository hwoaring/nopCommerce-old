namespace Nop.Services.Weixin
{
    /// <summary>
    /// Represents default values related to Weixin services
    /// </summary>
    public static partial class NopWeixinDefaults
    {
        #region Weixin mappings


        #endregion

        #region Stores

        /// <summary>
        /// 微信授权登录验证Session
        /// </summary>
        public static string WeixinOauthSessionKey => "Nop.weixin.oauth";
        /// <summary>
        /// 存储临时分享推荐人ID，如果找不到再找关注时推荐人ID
        /// </summary>
        public static string WeixinRefererSessionKey => "Nop.weixin.referer";

        #endregion
    }
}