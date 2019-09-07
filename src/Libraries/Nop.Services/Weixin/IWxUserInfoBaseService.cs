using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxUserInfoBase service interface
    /// </summary>
    public partial interface IWxUserInfoBaseService
    {
        /// <summary>
        /// Gets an WxUserInfoBase by WxUserInfoBase identifier
        /// </summary>
        /// <param name="WxUserInfoBase">WxUserInfoBase identifier</param>
        /// <returns>Affiliate</returns>
        WxUserInfoBase GetWxUserInfoBaseByOpenIdHash(long openIdHash);

        WxUserInfoBase GetWxUserInfoBaseByOpenId(string openId);

        /// <summary>
        /// Inserts an WxUserInfoBase
        /// </summary>
        /// <param name="WxUserInfoBase">WxUserInfoBase</param>
        void InsertWxUserInfoBase(WxUserInfoBase wxUserInfoBase);

        /// <summary>
        /// Updates the WxUserInfoBase
        /// </summary>
        /// <param name="WxUserInfoBase">WxUserInfoBase</param>
        void UpdateWxUserInfoBase(WxUserInfoBase wxUserInfoBase);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxUserInfoBase"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxUserInfoBase(WxUserInfoBase wxUserInfoBase, bool fromDB = false);

        IPagedList<WxUserInfoBase> GetWxUserInfoBases(long openIdReferer = 0,
                  bool? subscribe = null,
                  bool? allowReferer = null,
                  bool? allowRequest = null,
                  bool? allowOrder = null,
                  bool? allowNotice = null,
                  bool? locked = null,
                  bool? deleted = null,
                  int sceneTypeId = -1,
                  int roleTypeId = -1,
                  bool orderByActiveTime = false,
                  int pageIndex = 0, int pageSize = int.MaxValue);
    }
}