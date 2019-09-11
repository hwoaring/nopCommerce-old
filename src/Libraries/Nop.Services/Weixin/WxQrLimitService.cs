using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrLimit service
    /// </summary>
    public partial class WxQrLimitService : IWxQrLimitService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxQrLimit> _wxQrLimitRepository;

        #endregion

        #region Ctor

        public WxQrLimitService(IEventPublisher eventPublisher,
            IRepository<WxQrLimit> wxQrLimitRepository)
        {
            _eventPublisher = eventPublisher;
            _wxQrLimitRepository = wxQrLimitRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxQrLimit by WxQrLimit identifier
        /// </summary>
        /// <param name="WxQrLimit">WxQrLimit identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxQrLimit GetWxQrLimitById(int wxQrLimitId)
        {
            if (wxQrLimitId == 0)
                return null;

            return _wxQrLimitRepository.GetById(wxQrLimitId);
        }

        /// <summary>
        /// Inserts an WxQrLimit
        /// </summary>
        /// <param name="WxQrLimit">WxQrLimit</param>
        public virtual void InsertWxQrLimit(WxQrLimit wxQrLimit)
        {
            if (wxQrLimit == null)
                throw new ArgumentNullException(nameof(wxQrLimit));

            _wxQrLimitRepository.Insert(wxQrLimit);

            //event notification
            _eventPublisher.EntityInserted(wxQrLimit);
        }

        /// <summary>
        /// Updates the WxQrLimit
        /// </summary>
        /// <param name="WxQrLimit">WxQrLimit</param>
        public virtual void UpdateWxQrLimit(WxQrLimit wxQrLimit)
        {
            if (wxQrLimit == null)
                throw new ArgumentNullException(nameof(wxQrLimit));

            _wxQrLimitRepository.Update(wxQrLimit);

            //event notification
            _eventPublisher.EntityUpdated(wxQrLimit);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrLimit"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxQrLimit(WxQrLimit wxQrLimit)
        {
            if (wxQrLimit == null)
                throw new ArgumentNullException(nameof(wxQrLimit));

            _wxQrLimitRepository.Delete(wxQrLimit);

            //event notification
            _eventPublisher.EntityDeleted(wxQrLimit);
        }


        public virtual IPagedList<WxQrLimit> GetWxQrLimits(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxQrLimitRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxQrLimits = new PagedList<WxQrLimit>(query, pageIndex, pageSize);
            return wxQrLimits;
        }

        #endregion
    }
}