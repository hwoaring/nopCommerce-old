using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxAutoReplyKeyRule service interface
    /// </summary>
    public partial interface IWxAutoReplyKeyRuleService
    {
        /// <summary>
        /// Gets an affiliate by affiliate identifier
        /// </summary>
        /// <param name="affiliateId">Affiliate identifier</param>
        /// <returns>Affiliate</returns>
        WxAutoReplyKeyRule GetWxAutoReplyKeyRuleById(int wxAutoReplyKeyRuleId);

        /// <summary>
        /// Inserts an WxAutoReplyKeyRule
        /// </summary>
        /// <param name="WxAutoReplyKeyRule">WxAutoReplyKeyRule</param>
        void InsertWxAutoReplyKeyRule(WxAutoReplyKeyRule wxAutoReplyKeyRule);

        /// <summary>
        /// Updates the WxAutoReplyKeyRule
        /// </summary>
        /// <param name="WxAutoReplyKeyRule">WxAutoReplyKeyRule</param>
        void UpdateWxAutoReplyKeyRule(WxAutoReplyKeyRule wxAutoReplyKeyRule);


        /// <summary>
        /// delete wxAutoReplyKeyRule
        /// </summary>
        /// <param name="wxAutoReplyKeyRule"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void Delete(WxAutoReplyKeyRule wxAutoReplyKeyRule, bool fromDB = false);


        IPagedList<WxAutoReplyKeyRule> GetWxAutoReplyKeyRules(string keyWords = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}