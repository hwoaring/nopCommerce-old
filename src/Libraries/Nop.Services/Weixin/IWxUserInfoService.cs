using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxUserInfo service interface
    /// </summary>
    public partial interface IWxUserInfoService
    {
        /// <summary>
        /// Gets an WxUserInfo by WxUserInfo identifier
        /// </summary>
        /// <param name="WxUserInfo">WxUserInfo identifier</param>
        /// <returns>Affiliate</returns>
        WxUserInfo GetWxUserInfoByOpenIdHash(long openIdHash);

        /// <summary>
        /// Inserts an WxUserInfo
        /// </summary>
        /// <param name="WxUserInfo">WxUserInfo</param>
        void InsertWxUserInfo(WxUserInfo wxUserInfo);

        /// <summary>
        /// Updates the WxUserInfo
        /// </summary>
        /// <param name="WxUserInfo">WxUserInfo</param>
        void UpdateWxUserInfo(WxUserInfo wxUserInfo);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxUserInfo"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxUserInfo(WxUserInfo wxUserInfo);


        IPagedList<WxUserInfo> GetWxUserInfos(long openIdHash = 0,
            string nickName = null,
            int sex = -1,
            string city = null,
            string province = null,
            int userGroupId = 0,
            string tagIdList = null,
            int subscribeSceneTypeId = -1,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}