using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxUserInfoBase service
    /// </summary>
    public partial class WxUserInfoBaseService : IWxUserInfoBaseService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxUserInfoBase> _wxUserInfoBaseRepository;

        #endregion

        #region Ctor

        public WxUserInfoBaseService(IEventPublisher eventPublisher,
            IRepository<WxUserInfoBase> wxUserInfoBaseRepository)
        {
            _eventPublisher = eventPublisher;
            _wxUserInfoBaseRepository = wxUserInfoBaseRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxUserInfoBase by WxUserInfoBase identifier
        /// </summary>
        /// <param name="WxUserInfoBase">WxUserInfoBase identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxUserInfoBase GetWxUserInfoBaseByOpenIdHash(long openIdHash)
        {
            if (openIdHash == 0)
                return null;

            return _wxUserInfoBaseRepository.GetById(openIdHash);
        }

        public virtual WxUserInfoBase GetWxUserInfoBaseByOpenId(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
                return null;

            var query = from c in _wxUserInfoBaseRepository.Table
                        orderby c.OpenIdHash
                        where c.OpenId == openId
                        select c;
            var wxUserInfoBase = query.FirstOrDefault();
            return wxUserInfoBase;
        }

        /// <summary>
        /// Inserts an WxUserInfoBase
        /// </summary>
        /// <param name="WxUserInfoBase">WxUserInfoBase</param>
        public virtual void InsertWxUserInfoBase(WxUserInfoBase wxUserInfoBase)
        {
            if (wxUserInfoBase == null)
                throw new ArgumentNullException(nameof(wxUserInfoBase));

            _wxUserInfoBaseRepository.Insert(wxUserInfoBase);

            //event notification
            _eventPublisher.EntityInserted(wxUserInfoBase);
        }

        /// <summary>
        /// Updates the WxUserInfoBase
        /// </summary>
        /// <param name="WxUserInfoBase">WxUserInfoBase</param>
        public virtual void UpdateWxUserInfoBase(WxUserInfoBase wxUserInfoBase)
        {
            if (wxUserInfoBase == null)
                throw new ArgumentNullException(nameof(wxUserInfoBase));

            _wxUserInfoBaseRepository.Update(wxUserInfoBase);

            //event notification
            _eventPublisher.EntityUpdated(wxUserInfoBase);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxUserInfoBase"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxUserInfoBase(WxUserInfoBase wxUserInfoBase, bool fromDB = false)
        {
            if (wxUserInfoBase == null)
                throw new ArgumentNullException(nameof(wxUserInfoBase));

            if (fromDB)
            {
                _wxUserInfoBaseRepository.Delete(wxUserInfoBase);
            }
            else
            {
                wxUserInfoBase.Deleted = true;
                UpdateWxUserInfoBase(wxUserInfoBase);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxUserInfoBase);
        }


        public virtual IPagedList<WxUserInfoBase> GetWxUserInfoBases(long openIdReferer = 0,
            bool? subscribe = null,
            bool? allowReferer = null,
            bool? allowRequest = null,
            bool? allowOrder = null,
            bool? allowNotice = null,
            bool? locked = null,
            bool? deleted = null,
            int sceneTypeId = -1,
            int roleTypeId = -1,
            bool orderByActiveTime = false,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxUserInfoBaseRepository.Table;

            if (openIdReferer > 0)
                query = query.Where(a => a.OpenIdHash == openIdReferer);
            if (subscribe.HasValue)
                query = query.Where(a => a.Subscribe == subscribe);
            if (allowReferer.HasValue)
                query = query.Where(a => a.AllowReferer == allowReferer);
            if (allowRequest.HasValue)
                query = query.Where(a => a.AllowRequest == allowRequest);
            if (allowOrder.HasValue)
                query = query.Where(a => a.AllowOrder == allowOrder);
            if (allowNotice.HasValue)
                query = query.Where(a => a.AllowNotice == allowNotice);
            if (locked.HasValue)
                query = query.Where(a => a.Locked == locked);
            if (deleted.HasValue)
                query = query.Where(a => a.Deleted == deleted);
            if (sceneTypeId >= 0)
                query = query.Where(a => a.SceneTypeId == sceneTypeId);
            if (roleTypeId >= 0)
                query = query.Where(a => a.RoleTypeId == roleTypeId);

            //bool orderByActiveTime = false,
            if (orderByActiveTime)
                query = query.OrderByDescending(a => a.LastActiveTime);
            else
                query = query.OrderByDescending(a => a.CreatTime);

            var wxUserInfoBases = new PagedList<WxUserInfoBase>(query, pageIndex, pageSize);
            return wxUserInfoBases;
        }

        #endregion
    }
}