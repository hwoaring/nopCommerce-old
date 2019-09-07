using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxMatchRule service interface
    /// </summary>
    public partial interface IWxMatchRuleService
    {
        /// <summary>
        /// Gets an WxMatchRule by WxMatchRule identifier
        /// </summary>
        /// <param name="WxMatchRule">WxMatchRule identifier</param>
        /// <returns>Affiliate</returns>
        WxMatchRule GetWxMatchRuleById(int wxMatchRuleId);

        /// <summary>
        /// Inserts an WxMatchRule
        /// </summary>
        /// <param name="WxMatchRule">WxMatchRule</param>
        void InsertWxMatchRule(WxMatchRule wxMatchRule);

        /// <summary>
        /// Updates the WxMatchRule
        /// </summary>
        /// <param name="WxMatchRule">WxMatchRule</param>
        void UpdateWxMatchRule(WxMatchRule wxMatchRule);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxMatchRule"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxMatchRule(WxMatchRule wxMatchRule, bool fromDB = false);

        IPagedList<WxMatchRule> GetWxMatchRules(
                   int pageIndex = 0, int pageSize = int.MaxValue);
    }
}