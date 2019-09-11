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
    /// WxMessage service
    /// </summary>
    public partial class WxMessageService : IWxMessageService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxMessage> _wxMessageRepository;

        #endregion

        #region Ctor

        public WxMessageService(IEventPublisher eventPublisher,
            IRepository<WxMessage> wxMessageRepository)
        {
            _eventPublisher = eventPublisher;
            _wxMessageRepository = wxMessageRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxMessage by WxMessage identifier
        /// </summary>
        /// <param name="WxMessage">WxMessage identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxMessage GetWxMessageById(int wxMessageId)
        {
            if (wxMessageId == 0)
                return null;

            return _wxMessageRepository.GetById(wxMessageId);
        }

        public virtual IList<WxMessage> GetWxMessageByIds(int[] wxMessageIds, MessageType? messageType = null)
        {
            if (wxMessageIds == null || wxMessageIds.Length == 0)
                return new List<WxMessage>();

            var query = _wxMessageRepository.Table;

            query = query.Where(m => wxMessageIds.Contains(m.Id) && !m.Deleted);
            if (messageType != null)
                query = query.Where(m => m.MsgTypeId == (byte)messageType);

            var messages = query.ToList();
            //sort by passed identifiers
            var sortedMessages = new List<WxMessage>();
            foreach (var id in wxMessageIds)
            {
                var message = messages.Find(x => x.Id == id);
                if (message != null)
                    sortedMessages.Add(message);
            }

            return sortedMessages;
        }

        /// <summary>
        /// Inserts an WxMessage
        /// </summary>
        /// <param name="WxMessage">WxMessage</param>
        public virtual void InsertWxMessage(WxMessage wxMessage)
        {
            if (wxMessage == null)
                throw new ArgumentNullException(nameof(wxMessage));

            _wxMessageRepository.Insert(wxMessage);

            //event notification
            _eventPublisher.EntityInserted(wxMessage);
        }

        /// <summary>
        /// Updates the WxMessage
        /// </summary>
        /// <param name="WxMessage">WxMessage</param>
        public virtual void UpdateWxMessage(WxMessage wxMessage)
        {
            if (wxMessage == null)
                throw new ArgumentNullException(nameof(wxMessage));

            _wxMessageRepository.Update(wxMessage);

            //event notification
            _eventPublisher.EntityUpdated(wxMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxMessage"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxMessage(WxMessage wxMessage, bool fromDB = false)
        {
            if (wxMessage == null)
                throw new ArgumentNullException(nameof(wxMessage));

            if (fromDB)
            {
                _wxMessageRepository.Delete(wxMessage);
            }
            else
            {
                wxMessage.Deleted = true;
                UpdateWxMessage(wxMessage);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxMessage);
        }


        public virtual IPagedList<WxMessage> GetWxMessages(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxMessageRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxMessages = new PagedList<WxMessage>(query, pageIndex, pageSize);
            return wxMessages;
        }

        #endregion
    }
}