using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxButton service interface
    /// </summary>
    public partial interface IWxButtonService
    {
        /// <summary>
        /// Gets an WxButton by WxButton identifier
        /// </summary>
        /// <param name="WxButton">WxButton identifier</param>
        /// <returns>WxButton</returns>
        WxButton GetWxButtonById(int wxButtonId);

        /// <summary>
        /// Inserts an WxButton
        /// </summary>
        /// <param name="WxButton">WxButton</param>
        void InsertWxButton(WxButton wxButton);

        /// <summary>
        /// Updates the WxButton
        /// </summary>
        /// <param name="WxButton">WxButton</param>
        void UpdateWxButton(WxButton wxButton);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxButton"></param>
        void DeleteWxButton(WxButton wxButton);


        IPagedList<WxButton> GetWxButtons(int menuId = 0,
         int pageIndex = 0, int pageSize = int.MaxValue);
    }
}