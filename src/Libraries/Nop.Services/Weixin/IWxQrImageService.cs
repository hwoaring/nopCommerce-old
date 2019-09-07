using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrImage service interface
    /// </summary>
    public partial interface IWxQrImageService
    {
        /// <summary>
        /// Gets an WxQrImage by WxQrImage identifier
        /// </summary>
        /// <param name="WxQrImage">WxQrImage identifier</param>
        /// <returns>Affiliate</returns>
        WxQrImage GetWxQrImageById(int wxQrImageId);

        /// <summary>
        /// Inserts an WxQrImage
        /// </summary>
        /// <param name="WxQrImage">WxQrImage</param>
        void InsertWxQrImage(WxQrImage wxQrImage);

        /// <summary>
        /// Updates the WxQrImage
        /// </summary>
        /// <param name="WxQrImage">WxQrImage</param>
        void UpdateWxQrImage(WxQrImage wxQrImage);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrImage"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxQrImage(WxQrImage wxQrImage, bool fromDB = false);


        IPagedList<WxQrImage> GetWxQrImages(
                    int pageIndex = 0, int pageSize = int.MaxValue);
    }
}