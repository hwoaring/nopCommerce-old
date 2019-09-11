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
    /// WxVote service
    /// </summary>
    public partial class WxVoteService : IWxVoteService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxVote> _wxVoteRepository;

        #endregion

        #region Ctor

        public WxVoteService(IEventPublisher eventPublisher,
            IRepository<WxVote> wxVoteRepository)
        {
            _eventPublisher = eventPublisher;
            _wxVoteRepository = wxVoteRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxVote by WxVote identifier
        /// </summary>
        /// <param name="WxVote">WxVote identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxVote GetWxVoteById(int id)
        {
            if (id == 0)
                return null;

            return _wxVoteRepository.GetById(id);
        }

        /// <summary>
        /// Inserts an WxVote
        /// </summary>
        /// <param name="WxVote">WxVote</param>
        public virtual void InsertWxVote(WxVote wxVote)
        {
            if (wxVote == null)
                throw new ArgumentNullException(nameof(wxVote));

            _wxVoteRepository.Insert(wxVote);

            //event notification
            _eventPublisher.EntityInserted(wxVote);
        }

        /// <summary>
        /// Updates the WxVote
        /// </summary>
        /// <param name="WxVote">WxVote</param>
        public virtual void UpdateWxVote(WxVote wxVote)
        {
            if (wxVote == null)
                throw new ArgumentNullException(nameof(wxVote));

            _wxVoteRepository.Update(wxVote);

            //event notification
            _eventPublisher.EntityUpdated(wxVote);
        }

        public virtual void UpdateWxVotes(IList<WxVote> wxVotes)
        {
            if (wxVotes == null)
                throw new ArgumentNullException(nameof(wxVotes));

            //update
            _wxVoteRepository.Update(wxVotes);

            //event notification
            foreach (var wxVote in wxVotes)
            {
                _eventPublisher.EntityUpdated(wxVote);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxVote"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxVote(WxVote wxVote, bool fromDB = false)
        {
            if (wxVote == null)
                throw new ArgumentNullException(nameof(wxVote));

            if (fromDB)
                _wxVoteRepository.Delete(wxVote);
            else
            {
                wxVote.Deleted = true;
                UpdateWxVote(wxVote);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxVote);
        }

        public virtual void DeleteWxVotes(IList<WxVote> wxVotes, bool fromDB = false)
        {
            if (wxVotes == null)
                throw new ArgumentNullException(nameof(wxVotes));

            if (fromDB)
            {
                _wxVoteRepository.Delete(wxVotes);
            }
            else
            {
                foreach (var wxVote in wxVotes)
                {
                    wxVote.Deleted = true;
                }
                //delete product
                UpdateWxVotes(wxVotes);
            }

            foreach (var wxVote in wxVotes)
            {
                //event notification
                _eventPublisher.EntityDeleted(wxVote);
            }
        }


        public virtual IList<WxVote> GetWxVotesByIds(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return new List<WxVote>();

            var query = from p in _wxVoteRepository.Table
                        where ids.Contains(p.Id) && !p.Deleted
                        select p;
            var votes = query.ToList();

            return votes;
        }

        public virtual IPagedList<WxVote> GetWxVotes(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxVoteRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxVotes = new PagedList<WxVote>(query, pageIndex, pageSize);
            return wxVotes;
        }

        #endregion
    }
}