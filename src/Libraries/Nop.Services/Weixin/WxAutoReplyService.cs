using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxAutoReply service
    /// </summary>
    public partial class WxAutoReplyService : IWxAutoReplyService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxAutoReply> _wxAutoReplyRepository;

        #endregion

        #region Ctor

        public WxAutoReplyService(IEventPublisher eventPublisher,
            IRepository<WxAutoReply> wxAutoReplyRepository)
        {
            _eventPublisher = eventPublisher;
            _wxAutoReplyRepository = wxAutoReplyRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxAutoReply by WxAutoReply identifier
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxAutoReply GetWxAutoReplyById(int wxAutoReplyId)
        {
            if (wxAutoReplyId == 0)
                return null;

            return _wxAutoReplyRepository.GetById(wxAutoReplyId);
        }

        /// <summary>
        /// Inserts an WxAutoReply
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply</param>
        public virtual void InsertWxAutoReply(WxAutoReply wxAutoReply)
        {
            if (wxAutoReply == null)
                throw new ArgumentNullException(nameof(wxAutoReply));

            _wxAutoReplyRepository.Insert(wxAutoReply);

            //event notification
            _eventPublisher.EntityInserted(wxAutoReply);
        }

        /// <summary>
        /// Updates the WxAutoReply
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply</param>
        public virtual void UpdateWxAutoReplye(WxAutoReply wxAutoReply)
        {
            if (wxAutoReply == null)
                throw new ArgumentNullException(nameof(wxAutoReply));

            _wxAutoReplyRepository.Update(wxAutoReply);

            //event notification
            _eventPublisher.EntityUpdated(wxAutoReply);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxAutoReply">WxAutoReply</param>
        public virtual void DeleteWxAutoReply(WxAutoReply wxAutoReply)
        {
            if (wxAutoReply == null)
                throw new ArgumentNullException(nameof(wxAutoReply));

            _wxAutoReplyRepository.Delete(wxAutoReply);

            //event notification
            _eventPublisher.EntityDeleted(wxAutoReply);
        }


        public virtual IPagedList<WxAutoReply> GetWxAutoReplys(int rawId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxAutoReplyRepository.Table;

            if (rawId > 0)
                query = query.Where(a => a.RawId == rawId);

            query = query.OrderByDescending(a => a.Id);

            var wxAutoReplys = new PagedList<WxAutoReply>(query, pageIndex, pageSize);
            return wxAutoReplys;
        }

        #endregion
    }
}