using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxMatchRule service
    /// </summary>
    public partial class WxMatchRuleService : IWxMatchRuleService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxMatchRule> _wxMatchRuleRepository;

        #endregion

        #region Ctor

        public WxMatchRuleService(IEventPublisher eventPublisher,
            IRepository<WxMatchRule> wxMatchRuleRepository)
        {
            _eventPublisher = eventPublisher;
            _wxMatchRuleRepository = wxMatchRuleRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxMatchRule by WxMatchRule identifier
        /// </summary>
        /// <param name="WxMatchRule">WxMatchRule identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxMatchRule GetWxMatchRuleById(int wxMatchRuleId)
        {
            if (wxMatchRuleId == 0)
                return null;

            return _wxMatchRuleRepository.GetById(wxMatchRuleId);
        }

        /// <summary>
        /// Inserts an WxMatchRule
        /// </summary>
        /// <param name="WxMatchRule">WxMatchRule</param>
        public virtual void InsertWxMatchRule(WxMatchRule wxMatchRule)
        {
            if (wxMatchRule == null)
                throw new ArgumentNullException(nameof(wxMatchRule));

            _wxMatchRuleRepository.Insert(wxMatchRule);

            //event notification
            _eventPublisher.EntityInserted(wxMatchRule);
        }

        /// <summary>
        /// Updates the WxMatchRule
        /// </summary>
        /// <param name="WxMatchRule">WxMatchRule</param>
        public virtual void UpdateWxMatchRule(WxMatchRule wxMatchRule)
        {
            if (wxMatchRule == null)
                throw new ArgumentNullException(nameof(wxMatchRule));

            _wxMatchRuleRepository.Update(wxMatchRule);

            //event notification
            _eventPublisher.EntityUpdated(wxMatchRule);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxMatchRule"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxMatchRule(WxMatchRule wxMatchRule, bool fromDB = false)
        {
            if (wxMatchRule == null)
                throw new ArgumentNullException(nameof(wxMatchRule));

            _wxMatchRuleRepository.Delete(wxMatchRule);

            //event notification
            _eventPublisher.EntityDeleted(wxMatchRule);
        }


        public virtual IPagedList<WxMatchRule> GetWxMatchRules(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxMatchRuleRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxMatchRules = new PagedList<WxMatchRule>(query, pageIndex, pageSize);
            return wxMatchRules;
        }

        #endregion
    }
}