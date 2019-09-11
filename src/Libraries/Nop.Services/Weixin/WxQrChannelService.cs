using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// CustomerPoint service
    /// </summary>
    public partial class WxQrChannelService : IWxQrChannelService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxQrChannel> _wxQrChannelRepository;

        #endregion

        #region Ctor

        public WxQrChannelService(IEventPublisher eventPublisher,
            IRepository<WxQrChannel> wxQrChannelRepository)
        {
            _eventPublisher = eventPublisher;
            _wxQrChannelRepository = wxQrChannelRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxQrChannel by WxQrChannel identifier
        /// </summary>
        /// <param name="WxQrChannel">WxQrChannel identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxQrChannel GetWxQrChannelById(int wxQrChannelId)
        {
            if (wxQrChannelId == 0)
                return null;

            return _wxQrChannelRepository.GetById(wxQrChannelId);
        }

        /// <summary>
        /// Inserts an WxQrChannel
        /// </summary>
        /// <param name="WxQrChannel">WxQrChannel</param>
        public virtual void InsertWxQrChannel(WxQrChannel wxQrChannel)
        {
            if (wxQrChannel == null)
                throw new ArgumentNullException(nameof(wxQrChannel));

            _wxQrChannelRepository.Insert(wxQrChannel);

            //event notification
            _eventPublisher.EntityInserted(wxQrChannel);
        }

        /// <summary>
        /// Updates the WxQrChannel
        /// </summary>
        /// <param name="WxQrChannel">WxQrChannel</param>
        public virtual void UpdateWxQrChannel(WxQrChannel wxQrChannel)
        {
            if (wxQrChannel == null)
                throw new ArgumentNullException(nameof(wxQrChannel));

            _wxQrChannelRepository.Update(wxQrChannel);

            //event notification
            _eventPublisher.EntityUpdated(wxQrChannel);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrChannel"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxQrChannel(WxQrChannel wxQrChannel, bool fromDB = false)
        {
            if (wxQrChannel == null)
                throw new ArgumentNullException(nameof(wxQrChannel));

            if (fromDB)
            {
                _wxQrChannelRepository.Delete(wxQrChannel);
            }
            else
            {
                wxQrChannel.Deleted = true;
                UpdateWxQrChannel(wxQrChannel);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxQrChannel);
        }


        public virtual IPagedList<WxQrChannel> GetWxQrChannels(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxQrChannelRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxQrChannels = new PagedList<WxQrChannel>(query, pageIndex, pageSize);
            return wxQrChannels;
        }

        #endregion
    }
}