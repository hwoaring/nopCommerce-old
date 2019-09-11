using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrGroup service
    /// </summary>
    public partial class WxQrGroupService : IWxQrGroupService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxQrGroup> _wxQrGroupRepository;

        #endregion

        #region Ctor

        public WxQrGroupService(IEventPublisher eventPublisher,
            IRepository<WxQrGroup> wxQrGroupRepository)
        {
            _eventPublisher = eventPublisher;
            _wxQrGroupRepository = wxQrGroupRepository;
        }

        #endregion

        #region Methods


        public virtual WxQrGroup GetWxQrGroupById(int wxQrGroupId)
        {
            if (wxQrGroupId == 0)
                return null;

            return _wxQrGroupRepository.GetById(wxQrGroupId);
        }

        public virtual void InsertWxQrGroup(WxQrGroup wxQrGroup)
        {
            if (wxQrGroup == null)
                throw new ArgumentNullException(nameof(wxQrGroup));

            _wxQrGroupRepository.Insert(wxQrGroup);

            //event notification
            _eventPublisher.EntityInserted(wxQrGroup);
        }


        public virtual void UpdateWxQrGroup(WxQrGroup wxQrGroup)
        {
            if (wxQrGroup == null)
                throw new ArgumentNullException(nameof(wxQrGroup));

            _wxQrGroupRepository.Update(wxQrGroup);

            //event notification
            _eventPublisher.EntityUpdated(wxQrGroup);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrGroup"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxQrGroup(WxQrGroup wxQrGroup, bool fromDB = false)
        {
            if (wxQrGroup == null)
                throw new ArgumentNullException(nameof(wxQrGroup));

            if (fromDB)
            {
                _wxQrGroupRepository.Delete(wxQrGroup);
            }
            else
            {
                wxQrGroup.Deleted = true;
                UpdateWxQrGroup(wxQrGroup);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxQrGroup);
        }


        public virtual IPagedList<WxQrGroup> GetWxQrGroups(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxQrGroupRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxQrGroups = new PagedList<WxQrGroup>(query, pageIndex, pageSize);
            return wxQrGroups;
        }

        #endregion
    }
}