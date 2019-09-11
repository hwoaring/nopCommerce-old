using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxShareCount service interface
    /// </summary>
    public partial interface IWxShareCountService
    {
        /// <summary>
        /// Inserts an WxShareCount
        /// </summary>
        /// <param name="WxShareCount">WxShareCount</param>
        void InsertWxShareCount(WxShareCount wxShareCount);

        /// <summary>
        /// Updates the WxShareCount
        /// </summary>
        /// <param name="WxShareCount">WxShareCount</param>
        void UpdateWxShareCount(WxShareCount wxShareCount);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxShareCount"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxShareCount(WxShareCount wxShareCount);


        IPagedList<WxShareCount> GetWxShareCounts(
                 int pageIndex = 0, int pageSize = int.MaxValue);
    }
}