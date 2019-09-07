using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxAutoReplyKeyRule service
    /// </summary>
    public partial class WxAutoReplyKeyRuleService : IWxAutoReplyKeyRuleService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxAutoReplyKeyRule> _wxAutoReplyKeyRuleRepository;

        #endregion

        #region Ctor

        public WxAutoReplyKeyRuleService(IEventPublisher eventPublisher,
            IRepository<WxAutoReplyKeyRule> wxAutoReplyKeyRuleRepository)
        {
            _eventPublisher = eventPublisher;
            _wxAutoReplyKeyRuleRepository = wxAutoReplyKeyRuleRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxAutoReplyKeyRule by WxAutoReplyKeyRule identifier
        /// </summary>
        /// <param name="WxAutoReplyKeyRule">WxAutoReplyKeyRule identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxAutoReplyKeyRule GetWxAutoReplyKeyRuleById(int wxAutoReplyKeyRuleId)
        {
            if (wxAutoReplyKeyRuleId == 0)
                return null;

            return _wxAutoReplyKeyRuleRepository.GetById(wxAutoReplyKeyRuleId);
        }

        /// <summary>
        /// Inserts an WxAutoReplyKeyRule
        /// </summary>
        /// <param name="WxAutoReplyKeyRule">WxAutoReplyKeyRule</param>
        public virtual void InsertWxAutoReplyKeyRule(WxAutoReplyKeyRule wxAutoReplyKeyRule)
        {
            if (wxAutoReplyKeyRule == null)
                throw new ArgumentNullException(nameof(wxAutoReplyKeyRule));

            _wxAutoReplyKeyRuleRepository.Insert(wxAutoReplyKeyRule);

            //event notification
            _eventPublisher.EntityInserted(wxAutoReplyKeyRule);
        }

        /// <summary>
        /// Updates the WxAutoReplyKeyRule
        /// </summary>
        /// <param name="WxAutoReplyKeyRule">WxAutoReplyKeyRule</param>
        public virtual void UpdateWxAutoReplyKeyRule(WxAutoReplyKeyRule wxAutoReplyKeyRule)
        {
            if (wxAutoReplyKeyRule == null)
                throw new ArgumentNullException(nameof(wxAutoReplyKeyRule));

            _wxAutoReplyKeyRuleRepository.Update(wxAutoReplyKeyRule);

            //event notification
            _eventPublisher.EntityUpdated(wxAutoReplyKeyRule);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="wxAutoReplyKeyRule"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void Delete(WxAutoReplyKeyRule wxAutoReplyKeyRule, bool fromDB = false)
        {
            if (wxAutoReplyKeyRule == null)
                throw new ArgumentNullException(nameof(wxAutoReplyKeyRule));

            if(fromDB)
            {
                _wxAutoReplyKeyRuleRepository.Delete(wxAutoReplyKeyRule);
            }
            else
            {
                wxAutoReplyKeyRule.Deleted = true;
                UpdateWxAutoReplyKeyRule(wxAutoReplyKeyRule);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxAutoReplyKeyRule);
        }


        public virtual IPagedList<WxAutoReplyKeyRule> GetWxAutoReplyKeyRules(string keyWords = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxAutoReplyKeyRuleRepository.Table;

            if (!string.IsNullOrEmpty(keyWords))
            {
                var keys = keyWords.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (keys.Length > 0)
                {
                    //包含关键词的sql
                }
            }

            query = query.OrderByDescending(a => a.Id);

            var wxAutoReplyKeyRules = new PagedList<WxAutoReplyKeyRule>(query, pageIndex, pageSize);
            return wxAutoReplyKeyRules;
        }

        #endregion
    }
}