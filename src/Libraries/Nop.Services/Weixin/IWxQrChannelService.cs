using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrChannel service interface
    /// </summary>
    public partial interface IWxQrChannelService
    {
        /// <summary>
        /// Gets an WxQrChannel by WxQrChannel identifier
        /// </summary>
        /// <param name="WxQrChannel">WxQrChannel identifier</param>
        /// <returns>Affiliate</returns>
        WxQrChannel GetWxQrChannelById(int wxQrChannelId);

        /// <summary>
        /// Inserts an WxQrChannel
        /// </summary>
        /// <param name="WxQrChannel">WxQrChannel</param>
        void InsertWxQrChannel(WxQrChannel wxQrChannel);

        /// <summary>
        /// Updates the WxQrChannel
        /// </summary>
        /// <param name="WxQrChannel">WxQrChannel</param>
        void UpdateWxQrChannel(WxQrChannel wxQrChannel);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrChannel"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxQrChannel(WxQrChannel wxQrChannel, bool fromDB = false);


        IPagedList<WxQrChannel> GetWxQrChannels(
                   int pageIndex = 0, int pageSize = int.MaxValue);
    }
}