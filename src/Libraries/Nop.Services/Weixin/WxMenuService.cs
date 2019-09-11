using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxMenu service
    /// </summary>
    public partial class WxMenuService : IWxMenuService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxMenu> _wxMenuRepository;

        #endregion

        #region Ctor

        public WxMenuService(IEventPublisher eventPublisher,
            IRepository<WxMenu> wxMenuRepository)
        {
            _eventPublisher = eventPublisher;
            _wxMenuRepository = wxMenuRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxMenu by WxMenu identifier
        /// </summary>
        /// <param name="WxMenu">WxMenu identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxMenu GetWxMenuById(int wxMenuId)
        {
            if (wxMenuId == 0)
                return null;

            return _wxMenuRepository.GetById(wxMenuId);
        }

        /// <summary>
        /// Inserts an WxMenu
        /// </summary>
        /// <param name="WxMenu">WxMenu</param>
        public virtual void InsertWxMenu(WxMenu wxMenu)
        {
            if (wxMenu == null)
                throw new ArgumentNullException(nameof(wxMenu));

            _wxMenuRepository.Insert(wxMenu);

            //event notification
            _eventPublisher.EntityInserted(wxMenu);
        }

        /// <summary>
        /// Updates the WxMenu
        /// </summary>
        /// <param name="WxMenu">WxMenu</param>
        public virtual void UpdateWxMenu(WxMenu wxMenu)
        {
            if (wxMenu == null)
                throw new ArgumentNullException(nameof(wxMenu));

            _wxMenuRepository.Update(wxMenu);

            //event notification
            _eventPublisher.EntityUpdated(wxMenu);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxMenu"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxMenu(WxMenu wxMenu)
        {
            if (wxMenu == null)
                throw new ArgumentNullException(nameof(wxMenu));

            _wxMenuRepository.Delete(wxMenu);

            //event notification
            _eventPublisher.EntityDeleted(wxMenu);
        }


        public virtual IPagedList<WxMenu> GetWxMenus(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxMenuRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxMenus = new PagedList<WxMenu>(query, pageIndex, pageSize);
            return wxMenus;
        }

        #endregion
    }
}