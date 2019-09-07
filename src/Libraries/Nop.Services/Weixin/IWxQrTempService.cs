using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrTemp service interface
    /// </summary>
    public partial interface IWxQrTempService
    {
        /// <summary>
        /// Inserts an WxQrTemp
        /// </summary>
        /// <param name="WxQrTemp">WxQrTemp</param>
        void InsertWxQrTemp(WxQrTemp wxQrTemp);

        /// <summary>
        /// Updates the WxQrTemp
        /// </summary>
        /// <param name="WxQrTemp">WxQrTemp</param>
        void UpdateWxQrTemp(WxQrTemp wxQrTemp);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrTemp"></param>
        void DeleteWxQrTemp(WxQrTemp wxQrTemp);


        IPagedList<WxQrTemp> GetWxQrTemps(
                 long? openIdHash = null,
                 int? qrImageId = null,
                 int qrcodePrefixId = 0,
                 int pageIndex = 0, int pageSize = int.MaxValue);
    }
}