using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxMessage service interface
    /// </summary>
    public partial interface IWxMessageService
    {
        /// <summary>
        /// Gets an WxMessage by WxMessage identifier
        /// </summary>
        /// <param name="WxMessage">WxMessage identifier</param>
        /// <returns>Affiliate</returns>
        WxMessage GetWxMessageById(int wxMessageId);

        IList<WxMessage> GetWxMessageByIds(int[] wxMessageIds, MessageType? messageType = null);

        /// <summary>
        /// Inserts an WxMessage
        /// </summary>
        /// <param name="WxMessage">WxMessage</param>
        void InsertWxMessage(WxMessage wxMessage);

        /// <summary>
        /// Updates the WxMessage
        /// </summary>
        /// <param name="WxMessage">WxMessage</param>
        void UpdateWxMessage(WxMessage wxMessage);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxMessage"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxMessage(WxMessage wxMessage, bool fromDB = false);


        IPagedList<WxMessage> GetWxMessages(
                    int pageIndex = 0, int pageSize = int.MaxValue);
    }
}