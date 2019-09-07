using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrLimit service interface
    /// </summary>
    public partial interface IWxQrLimitService
    {
        /// <summary>
        /// Gets an WxQrLimit by WxQrLimit identifier
        /// </summary>
        /// <param name="WxQrLimit">WxQrLimit identifier</param>
        /// <returns>Affiliate</returns>
        WxQrLimit GetWxQrLimitById(int wxQrLimitId);

        /// <summary>
        /// Inserts an WxQrLimit
        /// </summary>
        /// <param name="WxQrLimit">WxQrLimit</param>
        void InsertWxQrLimit(WxQrLimit wxQrLimit);

        /// <summary>
        /// Updates the WxQrLimit
        /// </summary>
        /// <param name="WxQrLimit">WxQrLimit</param>
        void UpdateWxQrLimit(WxQrLimit wxQrLimit);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrLimit"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxQrLimit(WxQrLimit wxQrLimit);


        IPagedList<WxQrLimit> GetWxQrLimits(
                    int pageIndex = 0, int pageSize = int.MaxValue);
    }
}