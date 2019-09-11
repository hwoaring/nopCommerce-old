namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 二维码用途前缀，用于在临时二维码生成时作为类别前缀
    /// 0=默认，11=ADVER 广告，22=VERIFY 验证码，33=CMD 命令行，44=LOGIN 登录，55=BIND 绑定，66=VOTE 投票，77=CARD 个人名片
    /// </summary>
    public enum QrcodePrefix : byte
    {
        /// <summary>
        /// 默认
        /// </summary>
        DEFAULT = 0,
        /// <summary>
        /// 广告
        /// </summary>
        ADVER = 11,
        /// <summary>
        /// 二维码验证码
        /// </summary>
        VERIFY = 22,
        /// <summary>
        /// 命令行
        /// </summary>
        CMD = 33,
        /// <summary>
        /// 登录
        /// </summary>
        LOGIN = 44,
        /// <summary>
        /// 绑定
        /// </summary>
        BIND = 55,
        /// <summary>
        /// 投票
        /// </summary>
        VOTE = 66,
        /// <summary>
        /// 个人名片
        /// </summary>
        CARD = 77
    }
}
