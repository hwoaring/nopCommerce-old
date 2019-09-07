using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxButtonValue service interface
    /// </summary>
    public partial interface IWxButtonValueService
    {
        /// <summary>
        /// Gets an WxButtonValue by WxButtonValue identifier
        /// </summary>
        /// <param name="WxButtonValue">WxButtonValue identifier</param>
        /// <returns>Affiliate</returns>
        WxButtonValue GetWxButtonValueById(int wxButtonValueId);

        /// <summary>
        /// Inserts an WxButtonValue
        /// </summary>
        /// <param name="WxButtonValue">WxButtonValue</param>
        void InsertWxButtonValue(WxButtonValue wxButtonValue);

        /// <summary>
        /// Updates the WxButtonValue
        /// </summary>
        /// <param name="WxButtonValue">WxButtonValue</param>
        void UpdateWxButtonValue(WxButtonValue wxButtonValue);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxButtonValue"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxButtonValue(WxButtonValue wxButtonValue, bool fromDB = false);


        IPagedList<WxButtonValue> GetWxButtonValues(string type = null,
                  string key = null,
                  int pageIndex = 0, int pageSize = int.MaxValue);
    }
}