using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxShareCount service
    /// </summary>
    public partial class WxShareCountService : IWxShareCountService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxShareCount> _wxShareCountRepository;

        #endregion

        #region Ctor

        public WxShareCountService(IEventPublisher eventPublisher,
            IRepository<WxShareCount> wxShareCountRepository)
        {
            _eventPublisher = eventPublisher;
            _wxShareCountRepository = wxShareCountRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts an WxShareCount
        /// </summary>
        /// <param name="WxShareCount">WxShareCount</param>
        public virtual void InsertWxShareCount(WxShareCount wxShareCount)
        {
            if (wxShareCount == null)
                throw new ArgumentNullException(nameof(wxShareCount));

            _wxShareCountRepository.Insert(wxShareCount);

            //event notification
            _eventPublisher.EntityInserted(wxShareCount);
        }

        /// <summary>
        /// Updates the WxShareCount
        /// </summary>
        /// <param name="WxShareCount">WxShareCount</param>
        public virtual void UpdateWxShareCount(WxShareCount wxShareCount)
        {
            if (wxShareCount == null)
                throw new ArgumentNullException(nameof(wxShareCount));

            _wxShareCountRepository.Update(wxShareCount);

            //event notification
            _eventPublisher.EntityUpdated(wxShareCount);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxShareCount"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxShareCount(WxShareCount wxShareCount)
        {
            if (wxShareCount == null)
                throw new ArgumentNullException(nameof(wxShareCount));

            _wxShareCountRepository.Delete(wxShareCount);

            //event notification
            _eventPublisher.EntityDeleted(wxShareCount);
        }


        public virtual IPagedList<WxShareCount> GetWxShareCounts(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxShareCountRepository.Table;

            query = query.OrderByDescending(a => a.CreatTime);

            var wxShareCounts = new PagedList<WxShareCount>(query, pageIndex, pageSize);
            return wxShareCounts;
        }

        #endregion
    }
}