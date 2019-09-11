using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxButtonValue service
    /// </summary>
    public partial class WxButtonValueService : IWxButtonValueService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxButtonValue> _wxButtonValueRepository;

        #endregion

        #region Ctor

        public WxButtonValueService(IEventPublisher eventPublisher,
            IRepository<WxButtonValue> wxButtonValueRepository)
        {
            _eventPublisher = eventPublisher;
            _wxButtonValueRepository = wxButtonValueRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxButtonValue by WxButtonValue identifier
        /// </summary>
        /// <param name="WxButtonValue">WxButtonValue identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxButtonValue GetWxButtonValueById(int wxButtonValueId)
        {
            if (wxButtonValueId == 0)
                return null;

            return _wxButtonValueRepository.GetById(wxButtonValueId);
        }

        /// <summary>
        /// Inserts an WxButtonValue
        /// </summary>
        /// <param name="WxButtonValue">WxButtonValue</param>
        public virtual void InsertWxButtonValue(WxButtonValue wxButtonValue)
        {
            if (wxButtonValue == null)
                throw new ArgumentNullException(nameof(wxButtonValue));

            _wxButtonValueRepository.Insert(wxButtonValue);

            //event notification
            _eventPublisher.EntityInserted(wxButtonValue);
        }

        /// <summary>
        /// Updates the WxButtonValue
        /// </summary>
        /// <param name="WxButtonValue">WxButtonValue</param>
        public virtual void UpdateWxButtonValue(WxButtonValue wxButtonValue)
        {
            if (wxButtonValue == null)
                throw new ArgumentNullException(nameof(wxButtonValue));

            _wxButtonValueRepository.Update(wxButtonValue);

            //event notification
            _eventPublisher.EntityUpdated(wxButtonValue);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxButtonValue"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxButtonValue(WxButtonValue wxButtonValue, bool fromDB = false)
        {
            if (wxButtonValue == null)
                throw new ArgumentNullException(nameof(wxButtonValue));

            if (fromDB)
            {
                _wxButtonValueRepository.Delete(wxButtonValue);
            }
            else
            {
                wxButtonValue.Deleted = true;
                UpdateWxButtonValue(wxButtonValue);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxButtonValue);
        }


        public virtual IPagedList<WxButtonValue> GetWxButtonValues(string type = null,
            string key = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxButtonValueRepository.Table;

            if (!string.IsNullOrEmpty(type))
                query = query.Where(a => a.Type == type);
            if (!string.IsNullOrEmpty(key))
                query = query.Where(a => a.Key == key);

            query = query.OrderByDescending(a => a.Id);

            var wxButtonValues = new PagedList<WxButtonValue>(query, pageIndex, pageSize);
            return wxButtonValues;
        }

        #endregion
    }
}