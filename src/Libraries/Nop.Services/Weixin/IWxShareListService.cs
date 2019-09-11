using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxShareList service interface
    /// </summary>
    public partial interface IWxShareListService
    {
        /// <summary>
        /// Gets an WxShareList by WxShareList identifier
        /// </summary>
        /// <param name="WxShareList">WxShareList identifier</param>
        /// <returns>Affiliate</returns>
        WxShareList GetWxShareListById(int wxShareListId);

        /// <summary>
        /// Inserts an WxShareList
        /// </summary>
        /// <param name="WxShareList">WxShareList</param>
        void InsertWxShareList(WxShareList wxShareList);

        /// <summary>
        /// Updates the WxShareList
        /// </summary>
        /// <param name="WxShareList">WxShareList</param>
        void UpdateWxShareList(WxShareList wxShareList);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxShareList"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxShareList(WxShareList wxShareList);


        IPagedList<WxShareList> GetWxShareLists(long openIdHash = 0,
                    int productId = 0,
                    int pageIndex = 0, int pageSize = int.MaxValue);
    }
}