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
    /// WxQrLimitExtension service
    /// </summary>
    public partial class WxQrLimitExtensionService : IWxQrLimitExtensionService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxQrLimitExtension> _wxQrLimitExtensionRepository;

        #endregion

        #region Ctor

        public WxQrLimitExtensionService(IEventPublisher eventPublisher,
            IRepository<WxQrLimitExtension> wxQrLimitExtensionRepository)
        {
            _eventPublisher = eventPublisher;
            _wxQrLimitExtensionRepository = wxQrLimitExtensionRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxQrLimitExtension by WxQrLimitExtension identifier
        /// </summary>
        /// <param name="WxQrLimitExtension">WxQrLimitExtension identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxQrLimitExtension GetWxQrLimitExtensionById(int wxQrLimitExtensionId)
        {
            if (wxQrLimitExtensionId == 0)
                return null;

            return _wxQrLimitExtensionRepository.GetById(wxQrLimitExtensionId);
        }

        public virtual WxQrLimitExtension GetWxQrLimitExtensionByQrLimitId(int qrLimitId)
        {
            if (qrLimitId == 0)
                return null;

            var query = from c in _wxQrLimitExtensionRepository.Table
                        where c.QrLimitId == qrLimitId
                        select c;
            var limit = query.FirstOrDefault();
            return limit;
        }

        /// <summary>
        /// Inserts an WxQrLimitExtension
        /// </summary>
        /// <param name="WxQrLimitExtension">WxQrLimitExtension</param>
        public virtual void InsertWxQrLimitExtension(WxQrLimitExtension wxQrLimitExtension)
        {
            if (wxQrLimitExtension == null)
                throw new ArgumentNullException(nameof(wxQrLimitExtension));

            _wxQrLimitExtensionRepository.Insert(wxQrLimitExtension);

            //event notification
            _eventPublisher.EntityInserted(wxQrLimitExtension);
        }

        /// <summary>
        /// Updates the WxQrLimitExtension
        /// </summary>
        /// <param name="WxQrLimitExtension">WxQrLimitExtension</param>
        public virtual void UpdateWxQrLimitExtension(WxQrLimitExtension wxQrLimitExtension)
        {
            if (wxQrLimitExtension == null)
                throw new ArgumentNullException(nameof(wxQrLimitExtension));

            _wxQrLimitExtensionRepository.Update(wxQrLimitExtension);

            //event notification
            _eventPublisher.EntityUpdated(wxQrLimitExtension);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrLimitExtension"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxQrLimitExtension(WxQrLimitExtension wxQrLimitExtension, bool fromDB = false)
        {
            if (wxQrLimitExtension == null)
                throw new ArgumentNullException(nameof(wxQrLimitExtension));

            if (fromDB)
            {
                _wxQrLimitExtensionRepository.Delete(wxQrLimitExtension);
            }
            else
            {
                wxQrLimitExtension.Deleted = true;
                UpdateWxQrLimitExtension(wxQrLimitExtension);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxQrLimitExtension);
        }

        public virtual IPagedList<WxQrLimitExtension> GetWxQrLimitExtensions(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxQrLimitExtensionRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxQrLimitExtensions = new PagedList<WxQrLimitExtension>(query, pageIndex, pageSize);
            return wxQrLimitExtensions;
        }

        #endregion
    }
}