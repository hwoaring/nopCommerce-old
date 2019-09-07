using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxUserInfo service
    /// </summary>
    public partial class WxUserInfoService : IWxUserInfoService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxUserInfo> _wxUserInfoRepository;

        #endregion

        #region Ctor

        public WxUserInfoService(IEventPublisher eventPublisher,
            IRepository<WxUserInfo> wxUserInfoRepository)
        {
            _eventPublisher = eventPublisher;
            _wxUserInfoRepository = wxUserInfoRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxUserInfo by WxUserInfo identifier
        /// </summary>
        /// <param name="WxUserInfo">WxUserInfo identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxUserInfo GetWxUserInfoByOpenIdHash(long openIdHash)
        {
            if (openIdHash == 0)
                return null;

            return _wxUserInfoRepository.GetById(openIdHash);
        }

        /// <summary>
        /// Inserts an WxUserInfo
        /// </summary>
        /// <param name="WxUserInfo">WxUserInfo</param>
        public virtual void InsertWxUserInfo(WxUserInfo wxUserInfo)
        {
            if (wxUserInfo == null)
                throw new ArgumentNullException(nameof(wxUserInfo));

            _wxUserInfoRepository.Insert(wxUserInfo);

            //event notification
            _eventPublisher.EntityInserted(wxUserInfo);
        }

        /// <summary>
        /// Updates the WxUserInfo
        /// </summary>
        /// <param name="WxUserInfo">WxUserInfo</param>
        public virtual void UpdateWxUserInfo(WxUserInfo wxUserInfo)
        {
            if (wxUserInfo == null)
                throw new ArgumentNullException(nameof(wxUserInfo));

            _wxUserInfoRepository.Update(wxUserInfo);

            //event notification
            _eventPublisher.EntityUpdated(wxUserInfo);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxUserInfo"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxUserInfo(WxUserInfo wxUserInfo)
        {
            if (wxUserInfo == null)
                throw new ArgumentNullException(nameof(wxUserInfo));

            _wxUserInfoRepository.Delete(wxUserInfo);

            //event notification
            _eventPublisher.EntityDeleted(wxUserInfo);
        }


        public virtual IPagedList<WxUserInfo> GetWxUserInfos(long openIdHash = 0,
            string nickName = null,
            int sex = -1,
            string city = null,
            string province = null,
            int userGroupId = 0,
            string tagIdList = null,
            int subscribeSceneTypeId = -1,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxUserInfoRepository.Table;

            if (openIdHash > 0)
                query = query.Where(a => a.OpenIdHash == openIdHash);
            if (!string.IsNullOrEmpty(nickName))
                query = query.Where(a => a.NickName.Contains(nickName));
            if (sex >= 0)
                query = query.Where(a => a.Sex == sex);
            if (!string.IsNullOrEmpty(city))
                query = query.Where(a => a.City.Contains(city));
            if (!string.IsNullOrEmpty(province))
                query = query.Where(a => a.Province.Contains(province));
            if (userGroupId > 0)
                query = query.Where(a => a.UserGroupId == userGroupId);

            query = query.OrderByDescending(a => a.CreatTime);

            var wxUserInfos = new PagedList<WxUserInfo>(query, pageIndex, pageSize);
            return wxUserInfos;
        }

        #endregion
    }
}