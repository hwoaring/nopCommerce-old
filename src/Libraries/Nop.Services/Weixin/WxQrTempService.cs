using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrTemp service
    /// </summary>
    public partial class WxQrTempService : IWxQrTempService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxQrTemp> _wxQrTempRepository;

        #endregion

        #region Ctor

        public WxQrTempService(IEventPublisher eventPublisher,
            IRepository<WxQrTemp> wxQrTempRepository)
        {
            _eventPublisher = eventPublisher;
            _wxQrTempRepository = wxQrTempRepository;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Inserts an WxQrTemp
        /// </summary>
        /// <param name="WxQrTemp">WxQrTemp</param>
        public virtual void InsertWxQrTemp(WxQrTemp wxQrTemp)
        {
            if (wxQrTemp == null)
                throw new ArgumentNullException(nameof(wxQrTemp));

            _wxQrTempRepository.Insert(wxQrTemp);

            //event notification
            _eventPublisher.EntityInserted(wxQrTemp);
        }

        /// <summary>
        /// Updates the WxQrTemp
        /// </summary>
        /// <param name="WxQrTemp">WxQrTemp</param>
        public virtual void UpdateWxQrTemp(WxQrTemp wxQrTemp)
        {
            if (wxQrTemp == null)
                throw new ArgumentNullException(nameof(wxQrTemp));

            _wxQrTempRepository.Update(wxQrTemp);

            //event notification
            _eventPublisher.EntityUpdated(wxQrTemp);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrTemp"></param>
        public virtual void DeleteWxQrTemp(WxQrTemp wxQrTemp)
        {
            if (wxQrTemp == null)
                throw new ArgumentNullException(nameof(wxQrTemp));

            _wxQrTempRepository.Delete(wxQrTemp);

            //event notification
            _eventPublisher.EntityDeleted(wxQrTemp);
        }


        public virtual IPagedList<WxQrTemp> GetWxQrTemps(
            long? openIdHash = null,
            int? qrImageId = null,
            int qrcodePrefixId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxQrTempRepository.Table;
            if (openIdHash.HasValue)
                query = query.Where(a => a.OpenIdHash == openIdHash);
            if (qrImageId.HasValue)
                query = query.Where(a => a.QrImageId == qrImageId);
            if(qrcodePrefixId>0)
                query = query.Where(a => a.QrcodePrefixId == qrcodePrefixId);

            query = query.OrderByDescending(a => a.CreatTime);

            var wxQrTemps = new PagedList<WxQrTemp>(query, pageIndex, pageSize);
            return wxQrTemps;
        }

        #endregion
    }
}