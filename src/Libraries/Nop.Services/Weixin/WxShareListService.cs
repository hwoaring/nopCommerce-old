using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxShareList service
    /// </summary>
    public partial class WxShareListService : IWxShareListService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxShareList> _wxShareListRepository;

        #endregion

        #region Ctor

        public WxShareListService(IEventPublisher eventPublisher,
            IRepository<WxShareList> wxShareListRepository)
        {
            _eventPublisher = eventPublisher;
            _wxShareListRepository = wxShareListRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxShareList by WxShareList identifier
        /// </summary>
        /// <param name="WxShareList">WxShareList identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxShareList GetWxShareListById(int wxShareListId)
        {
            if (wxShareListId == 0)
                return null;

            return _wxShareListRepository.GetById(wxShareListId);
        }

        /// <summary>
        /// Inserts an WxShareList
        /// </summary>
        /// <param name="WxShareList">WxShareList</param>
        public virtual void InsertWxShareList(WxShareList wxShareList)
        {
            if (wxShareList == null)
                throw new ArgumentNullException(nameof(wxShareList));

            _wxShareListRepository.Insert(wxShareList);

            //event notification
            _eventPublisher.EntityInserted(wxShareList);
        }

        /// <summary>
        /// Updates the WxShareList
        /// </summary>
        /// <param name="WxShareList">WxShareList</param>
        public virtual void UpdateWxShareList(WxShareList wxShareList)
        {
            if (wxShareList == null)
                throw new ArgumentNullException(nameof(wxShareList));

            _wxShareListRepository.Update(wxShareList);

            //event notification
            _eventPublisher.EntityUpdated(wxShareList);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxShareList"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxShareList(WxShareList wxShareList)
        {
            if (wxShareList == null)
                throw new ArgumentNullException(nameof(wxShareList));

            _wxShareListRepository.Delete(wxShareList);

            //event notification
            _eventPublisher.EntityDeleted(wxShareList);
        }


        public virtual IPagedList<WxShareList> GetWxShareLists(long openIdHash = 0,
            int productId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxShareListRepository.Table;
            if (openIdHash > 0)
                query = query.Where(a => a.OpenIdHash == openIdHash);
            if (productId > 0)
                query = query.Where(a => a.ProductId == productId);

            query = query.OrderByDescending(a => a.Id);

            var wxShareLists = new PagedList<WxShareList>(query, pageIndex, pageSize);
            return wxShareLists;
        }

        #endregion
    }
}