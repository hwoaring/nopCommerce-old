namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 返回用户关注的渠道来源:SubscribeScene
    /// </summary>
    public enum SubscribeSceneType : byte
    {
        //0代表其他合计
        //1代表公众号搜索
        //17代表名片分享
        //30代表扫描二维码
        //43代表图文页右上角菜单
        //51代表支付后关注（在支付完成页）
        //57代表图文页内公众号名称
        //75代表公众号文章广告
        //78代表朋友圈广告


        /// <summary>
        /// 公众号搜索
        /// </summary>
        ADD_SCENE_SEARCH = 1,
        /// <summary>
        /// 公众号迁移
        /// </summary>
        ADD_SCENE_ACCOUNT_MIGRATION = 90,   //临时数字编号
        /// <summary>
        /// 名片分享
        /// </summary>
        ADD_SCENE_PROFILE_CARD = 17,
        /// <summary>
        /// 扫描二维码
        /// </summary>
        ADD_SCENE_QR_CODE = 30,
        /// <summary>
        /// 图文页内名称点击
        /// </summary>
        ADD_SCENE_PROFILE_LINK = 57,
        /// <summary>
        /// 图文页右上角菜单
        /// </summary>
        ADD_SCENE_PROFILE_ITEM = 43,
        /// <summary>
        /// 支付后关注
        /// </summary>
        ADD_SCENE_PAID = 51,
        /// <summary>
        /// 公众号文章广告
        /// </summary>
        ADD_SCENE_AD_ARTICLES = 75,   //临时英文编码
        /// <summary>
        /// 朋友圈广告
        /// </summary>
        ADD_SCENE_AD_FRIENDS = 78,   //临时英文编码
        /// <summary>
        /// 其他或未统计到的
        /// </summary>
        ADD_SCENE_OTHERS = 0
    }
}
