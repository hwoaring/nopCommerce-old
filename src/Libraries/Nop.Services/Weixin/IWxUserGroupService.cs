using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxUserGroup service interface
    /// </summary>
    public partial interface IWxUserGroupService
    {
        /// <summary>
        /// Gets an WxUserGroup by WxUserGroup identifier
        /// </summary>
        /// <param name="WxUserGroup">WxUserGroup identifier</param>
        /// <returns>Affiliate</returns>
        WxUserGroup GetWxUserGroupById(int wxUserGroupId);

        /// <summary>
        /// Inserts an WxUserGroup
        /// </summary>
        /// <param name="WxUserGroup">WxUserGroup</param>
        void InsertWxUserGroup(WxUserGroup wxUserGroup);

        /// <summary>
        /// Updates the WxUserGroup
        /// </summary>
        /// <param name="WxUserGroup">WxUserGroup</param>
        void UpdateWxUserGroup(WxUserGroup wxUserGroup);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxUserGroup"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxUserGroup(WxUserGroup wxUserGroup);


        IPagedList<WxUserGroup> GetWxUserGroups(
                   int pageIndex = 0, int pageSize = int.MaxValue);
    }
}