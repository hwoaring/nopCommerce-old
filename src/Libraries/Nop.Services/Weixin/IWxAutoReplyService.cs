using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxAutoReply service interface
    /// </summary>
    public partial interface IWxAutoReplyService
    {
        /// <summary>
        /// Gets an WxAutoReply by WxAutoReply identifier
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply identifier</param>
        /// <returns>Affiliate</returns>
        WxAutoReply GetWxAutoReplyById(int wxAutoReplyId);

        /// <summary>
        /// Inserts an WxAutoReply
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply</param>
        void InsertWxAutoReply(WxAutoReply wxAutoReply);

        /// <summary>
        /// Updates the WxAutoReply
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply</param>
        void UpdateWxAutoReplye(WxAutoReply wxAutoReply);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply</param>
        void DeleteWxAutoReply(WxAutoReply wxAutoReply);


        IPagedList<WxAutoReply> GetWxAutoReplys(int rawId = 0,
           int pageIndex = 0, int pageSize = int.MaxValue);
    }
}