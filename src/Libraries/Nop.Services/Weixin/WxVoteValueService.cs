using System;
using System.Linq;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxVoteValue service
    /// </summary>
    public partial class WxVoteValueService : IWxVoteValueService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxVoteValue> _wxVoteValueRepository;

        #endregion

        #region Ctor

        public WxVoteValueService(IEventPublisher eventPublisher,
            IRepository<WxVoteValue> wxVoteValueRepository)
        {
            _eventPublisher = eventPublisher;
            _wxVoteValueRepository = wxVoteValueRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxVoteValue by WxVoteValue identifier
        /// </summary>
        /// <param name="WxVoteValue">WxVoteValue identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxVoteValue GetWxVoteValueById(int id)
        {
            if (id == 0)
                return null;

            return _wxVoteValueRepository.GetById(id);
        }

        /// <summary>
        /// Inserts an WxVoteValue
        /// </summary>
        /// <param name="WxVoteValue">WxVoteValue</param>
        public virtual void InsertWxVoteValue(WxVoteValue wxVoteValue)
        {
            if (wxVoteValue == null)
                throw new ArgumentNullException(nameof(wxVoteValue));

            _wxVoteValueRepository.Insert(wxVoteValue);

            //event notification
            _eventPublisher.EntityInserted(wxVoteValue);
        }

        /// <summary>
        /// Updates the WxVoteValue
        /// </summary>
        /// <param name="WxVoteValue">WxVoteValue</param>
        public virtual void UpdateWxVoteValue(WxVoteValue wxVoteValue)
        {
            if (wxVoteValue == null)
                throw new ArgumentNullException(nameof(wxVoteValue));

            _wxVoteValueRepository.Update(wxVoteValue);

            //event notification
            _eventPublisher.EntityUpdated(wxVoteValue);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxVoteValue"></param>
        public virtual void DeleteWxVoteValue(WxVoteValue wxVoteValue)
        {
            if (wxVoteValue == null)
                throw new ArgumentNullException(nameof(wxVoteValue));

            _wxVoteValueRepository.Delete(wxVoteValue);

            //event notification
            _eventPublisher.EntityDeleted(wxVoteValue);
        }

        public virtual void DeleteWxVoteValues(IList<WxVoteValue> wxVoteValues)
        {
            if (wxVoteValues == null)
                throw new ArgumentNullException(nameof(wxVoteValues));

            _wxVoteValueRepository.Delete(wxVoteValues);

            //event notification
            foreach (var wxVoteValue in wxVoteValues)
                _eventPublisher.EntityDeleted(wxVoteValue);
        }

        public virtual IList<WxVoteValue> GetWxVoteValuesByOpenIdHash(long openIdHash)
        {
            if (openIdHash == 0)
                return new List<WxVoteValue>();

            var query = from p in _wxVoteValueRepository.Table
                        where p.OpenIdHash == openIdHash
                        select p;
            var values = query.ToList();
            return values;
        }

        public virtual IList<WxVoteValue> GetWxVoteValuesByVoteId(int voteId)
        {
            if (voteId == 0)
                return new List<WxVoteValue>();

            var query = from p in _wxVoteValueRepository.Table
                        where p.VoteId == voteId
                        select p;
            var values = query.ToList();
            return values;
        }

        public virtual IPagedList<WxVoteValue> GetWxVoteValues(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxVoteValueRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxVoteValues = new PagedList<WxVoteValue>(query, pageIndex, pageSize);
            return wxVoteValues;
        }

        #endregion
    }
}