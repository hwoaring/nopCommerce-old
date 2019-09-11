using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrLimitExtension service interface
    /// </summary>
    public partial interface IWxQrLimitExtensionService
    {
        /// <summary>
        /// Gets an WxQrLimitExtension by WxQrLimitExtension identifier
        /// </summary>
        /// <param name="WxQrLimitExtension">WxQrLimitExtension identifier</param>
        /// <returns>Affiliate</returns>
        WxQrLimitExtension GetWxQrLimitExtensionById(int wxQrLimitExtensionId);

        WxQrLimitExtension GetWxQrLimitExtensionByQrLimitId(int qrLimitId);

        /// <summary>
        /// Inserts an WxQrLimitExtension
        /// </summary>
        /// <param name="WxQrLimitExtension">WxQrLimitExtension</param>
        void InsertWxQrLimitExtension(WxQrLimitExtension wxQrLimitExtension);

        /// <summary>
        /// Updates the WxQrLimitExtension
        /// </summary>
        /// <param name="WxQrLimitExtension">WxQrLimitExtension</param>
        void UpdateWxQrLimitExtension(WxQrLimitExtension wxQrLimitExtension);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrLimitExtension"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxQrLimitExtension(WxQrLimitExtension wxQrLimitExtension, bool fromDB = false);


        IPagedList<WxQrLimitExtension> GetWxQrLimitExtensions(
                   int pageIndex = 0, int pageSize = int.MaxValue);
    }
}