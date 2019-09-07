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
    /// WxVoteUser service
    /// </summary>
    public partial class WxVoteUserService : IWxVoteUserService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxVoteUser> _wxVoteUserRepository;

        #endregion

        #region Ctor

        public WxVoteUserService(IEventPublisher eventPublisher,
            IRepository<WxVoteUser> wxVoteUserRepository)
        {
            _eventPublisher = eventPublisher;
            _wxVoteUserRepository = wxVoteUserRepository;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Inserts an WxVoteUser
        /// </summary>
        /// <param name="WxVoteUser">WxVoteUser</param>
        public virtual void InsertWxVoteUser(WxVoteUser wxVoteUser)
        {
            if (wxVoteUser == null)
                throw new ArgumentNullException(nameof(wxVoteUser));

            _wxVoteUserRepository.Insert(wxVoteUser);

            //event notification
            _eventPublisher.EntityInserted(wxVoteUser);
        }

        /// <summary>
        /// Updates the WxVoteUser
        /// </summary>
        /// <param name="WxVoteUser">WxVoteUser</param>
        public virtual void UpdateWxVoteUser(WxVoteUser wxVoteUser)
        {
            if (wxVoteUser == null)
                throw new ArgumentNullException(nameof(wxVoteUser));

            _wxVoteUserRepository.Update(wxVoteUser);

            //event notification
            _eventPublisher.EntityUpdated(wxVoteUser);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxVoteUser"></param>
        public virtual void DeleteWxVoteUser(WxVoteUser wxVoteUser)
        {
            if (wxVoteUser == null)
                throw new ArgumentNullException(nameof(wxVoteUser));

            _wxVoteUserRepository.Delete(wxVoteUser);

            //event notification
            _eventPublisher.EntityDeleted(wxVoteUser);
        }

        public virtual void DeleteWxVoteUser(IList<WxVoteUser> wxVoteUsers)
        {
            if (wxVoteUsers == null)
                throw new ArgumentNullException(nameof(wxVoteUsers));

            _wxVoteUserRepository.Delete(wxVoteUsers);

            //event notification
            foreach (var wxVoteUser in wxVoteUsers)
                _eventPublisher.EntityDeleted(wxVoteUser);
        }

        public virtual WxVoteUser GetWxVoteUserByOpenIdHashAndVoteId(long openIdHash, int voteId)
        {
            if (openIdHash == 0 || voteId == 0)
                return null;

            var query = from p in _wxVoteUserRepository.Table
                        where p.OpenIdHash == openIdHash && p.VoteId == voteId
                        select p;

            var user = query.FirstOrDefault();
            return user;
        }

        public virtual IList<WxVoteUser> GetWxVoteUsersByOpenIdHash(long openIdHash)
        {
            if (openIdHash == 0)
                return new List<WxVoteUser>();

            var query = from p in _wxVoteUserRepository.Table
                        where p.OpenIdHash == openIdHash
                        select p;
            var users = query.ToList();
            return users;
        }

        public virtual IList<WxVoteUser> GetWxVoteUsersByVoteId(int voteId)
        {
            if (voteId == 0)
                return new List<WxVoteUser>();

            var query = from p in _wxVoteUserRepository.Table
                        where p.VoteId == voteId
                        select p;
            var users = query.ToList();
            return users;
        }

        public virtual IPagedList<WxVoteUser> GetWxVoteUsers(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxVoteUserRepository.Table;

            query = query.OrderByDescending(a => a.CreatTime);

            var wxVoteUsers = new PagedList<WxVoteUser>(query, pageIndex, pageSize);
            return wxVoteUsers;
        }

        #endregion
    }
}