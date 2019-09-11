using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxLocation service
    /// </summary>
    public partial class WxLocationService : IWxLocationService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxLocation> _wxLocationRepository;

        #endregion

        #region Ctor

        public WxLocationService(IEventPublisher eventPublisher,
            IRepository<WxLocation> wxLocationRepository)
        {
            _eventPublisher = eventPublisher;
            _wxLocationRepository = wxLocationRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxLocation by WxLocation identifier
        /// </summary>
        /// <param name="openIdHash">WxLocation identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxLocation GetWxLocationByOpenIdHash(long openIdHash)
        {
            if (openIdHash == 0)
                return null;

            return _wxLocationRepository.GetById(openIdHash);
        }

        /// <summary>
        /// Inserts an WxLocation
        /// </summary>
        /// <param name="WxLocation">WxLocation</param>
        public virtual void InsertWxLocation(WxLocation wxLocation)
        {
            if (wxLocation == null)
                throw new ArgumentNullException(nameof(wxLocation));

            _wxLocationRepository.Insert(wxLocation);

            //event notification
            _eventPublisher.EntityInserted(wxLocation);
        }

        /// <summary>
        /// Updates the WxLocation
        /// </summary>
        /// <param name="WxLocation">WxLocation</param>
        public virtual void UpdateWxLocation(WxLocation wxLocation)
        {
            if (wxLocation == null)
                throw new ArgumentNullException(nameof(wxLocation));

            _wxLocationRepository.Update(wxLocation);

            //event notification
            _eventPublisher.EntityUpdated(wxLocation);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxLocation"></param>
        public virtual void DeleteWxLocation(WxLocation wxLocation)
        {
            if (wxLocation == null)
                throw new ArgumentNullException(nameof(wxLocation));

            _wxLocationRepository.Delete(wxLocation);

            //event notification
            _eventPublisher.EntityDeleted(wxLocation);
        }


        public virtual IPagedList<WxLocation> GetWxLocations(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxLocationRepository.Table;

            query = query.OrderByDescending(a => a.UpdateTime);

            var wxLocations = new PagedList<WxLocation>(query, pageIndex, pageSize);
            return wxLocations;
        }

        #endregion
    }
}