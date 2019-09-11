using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxLocation service interface
    /// </summary>
    public partial interface IWxLocationService
    {
        /// <summary>
        /// Gets an WxLocation by WxLocation identifier
        /// </summary>
        /// <param name="openIdHash">WxLocation identifier</param>
        /// <returns>Affiliate</returns>
        WxLocation GetWxLocationByOpenIdHash(long openIdHash);

        /// <summary>
        /// Inserts an WxLocation
        /// </summary>
        /// <param name="WxLocation">WxLocation</param>
        void InsertWxLocation(WxLocation wxLocation);

        /// <summary>
        /// Updates the WxLocation
        /// </summary>
        /// <param name="WxLocation">WxLocation</param>
        void UpdateWxLocation(WxLocation wxLocation);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxLocation"></param>
        void DeleteWxLocation(WxLocation wxLocation);


        IPagedList<WxLocation> GetWxLocations(
                   int pageIndex = 0, int pageSize = int.MaxValue);
    }
}