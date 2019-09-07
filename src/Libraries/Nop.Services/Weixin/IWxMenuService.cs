using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxMenu service interface
    /// </summary>
    public partial interface IWxMenuService
    {
        /// <summary>
        /// Gets an WxMenu by WxMenu identifier
        /// </summary>
        /// <param name="WxMenu">WxMenu identifier</param>
        /// <returns>Affiliate</returns>
        WxMenu GetWxMenuById(int wxMenuId);

        /// <summary>
        /// Inserts an WxMenu
        /// </summary>
        /// <param name="WxMenu">WxMenu</param>
        void InsertWxMenu(WxMenu wxMenu);

        /// <summary>
        /// Updates the WxMenu
        /// </summary>
        /// <param name="WxMenu">WxMenu</param>
        void UpdateWxMenu(WxMenu wxMenu);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxMenu"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxMenu(WxMenu wxMenu);


        IPagedList<WxMenu> GetWxMenus(
                   int pageIndex = 0, int pageSize = int.MaxValue);
    }
}