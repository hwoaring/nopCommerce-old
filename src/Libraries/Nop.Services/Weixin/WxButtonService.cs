using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxButton service
    /// </summary>
    public partial class WxButtonService : IWxButtonService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxButton> _wxButtonRepository;

        #endregion

        #region Ctor

        public WxButtonService(IEventPublisher eventPublisher,
            IRepository<WxButton> wxButtonRepository)
        {
            _eventPublisher = eventPublisher;
            _wxButtonRepository = wxButtonRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxButton by WxButton identifier
        /// </summary>
        /// <param name="WxButton">WxButton identifier</param>
        /// <returns>WxButton</returns>
        public virtual WxButton GetWxButtonById(int wxButtonId)
        {
            if (wxButtonId == 0)
                return null;

            return _wxButtonRepository.GetById(wxButtonId);
        }

        /// <summary>
        /// Inserts an WxButton
        /// </summary>
        /// <param name="WxButton">WxButton</param>
        public virtual void InsertWxButton(WxButton wxButton)
        {
            if (wxButton == null)
                throw new ArgumentNullException(nameof(wxButton));

            _wxButtonRepository.Insert(wxButton);

            //event notification
            _eventPublisher.EntityInserted(wxButton);
        }

        /// <summary>
        /// Updates the WxButton
        /// </summary>
        /// <param name="WxButton">WxButton</param>
        public virtual void UpdateWxButton(WxButton wxButton)
        {
            if (wxButton == null)
                throw new ArgumentNullException(nameof(wxButton));

            _wxButtonRepository.Update(wxButton);

            //event notification
            _eventPublisher.EntityUpdated(wxButton);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxButton"></param>
        public virtual void DeleteWxButton(WxButton wxButton)
        {
            if (wxButton == null)
                throw new ArgumentNullException(nameof(wxButton));

            _wxButtonRepository.Delete(wxButton);

            //event notification
            _eventPublisher.EntityDeleted(wxButton);
        }


        public virtual IPagedList<WxButton> GetWxButtons(int menuId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxButtonRepository.Table;

            if (menuId > 0)
                query = query.Where(a => a.MenuId == menuId);

            query = query.OrderByDescending(a => a.Id);

            var wxButtons = new PagedList<WxButton>(query, pageIndex, pageSize);
            return wxButtons;
        }

        #endregion
    }
}