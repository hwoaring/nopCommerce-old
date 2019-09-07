using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxUserGroup service
    /// </summary>
    public partial class WxUserGroupService : IWxUserGroupService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxUserGroup> _wxUserGroupRepository;

        #endregion

        #region Ctor

        public WxUserGroupService(IEventPublisher eventPublisher,
            IRepository<WxUserGroup> wxUserGroupRepository)
        {
            _eventPublisher = eventPublisher;
            _wxUserGroupRepository = wxUserGroupRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxUserGroup by WxUserGroup identifier
        /// </summary>
        /// <param name="WxUserGroup">WxUserGroup identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxUserGroup GetWxUserGroupById(int wxUserGroupId)
        {
            if (wxUserGroupId == 0)
                return null;

            return _wxUserGroupRepository.GetById(wxUserGroupId);
        }

        /// <summary>
        /// Inserts an WxUserGroup
        /// </summary>
        /// <param name="WxUserGroup">WxUserGroup</param>
        public virtual void InsertWxUserGroup(WxUserGroup wxUserGroup)
        {
            if (wxUserGroup == null)
                throw new ArgumentNullException(nameof(wxUserGroup));

            _wxUserGroupRepository.Insert(wxUserGroup);

            //event notification
            _eventPublisher.EntityInserted(wxUserGroup);
        }

        /// <summary>
        /// Updates the WxUserGroup
        /// </summary>
        /// <param name="WxUserGroup">WxUserGroup</param>
        public virtual void UpdateWxUserGroup(WxUserGroup wxUserGroup)
        {
            if (wxUserGroup == null)
                throw new ArgumentNullException(nameof(wxUserGroup));

            _wxUserGroupRepository.Update(wxUserGroup);

            //event notification
            _eventPublisher.EntityUpdated(wxUserGroup);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxUserGroup"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxUserGroup(WxUserGroup wxUserGroup)
        {
            if (wxUserGroup == null)
                throw new ArgumentNullException(nameof(wxUserGroup));

            _wxUserGroupRepository.Delete(wxUserGroup);

            //event notification
            _eventPublisher.EntityDeleted(wxUserGroup);
        }


        public virtual IPagedList<WxUserGroup> GetWxUserGroups(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxUserGroupRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxUserGroups = new PagedList<WxUserGroup>(query, pageIndex, pageSize);
            return wxUserGroups;
        }

        #endregion
    }
}