using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// CustomerPoint service
    /// </summary>
    public partial class CustomerPointService : ICustomerPointService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<CustomerPoint> _customerPointRepository;

        #endregion

        #region Ctor

        public CustomerPointService(IEventPublisher eventPublisher,
            IRepository<CustomerPoint> customerPointRepository)
        {
            _eventPublisher = eventPublisher;
            _customerPointRepository = customerPointRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an CustomerPoint by CustomerPoint identifier
        /// </summary>
        /// <param name="CustomerPoint">CustomerPoint identifier</param>
        /// <returns>Affiliate</returns>
        public virtual CustomerPoint GetCustomerPointById(int customerPointId)
        {
            if (customerPointId == 0)
                return null;

            return _customerPointRepository.GetById(customerPointId);
        }

        /// <summary>
        /// Inserts an CustomerPoint
        /// </summary>
        /// <param name="WxAutoReplyKeyRule">WxAutoReplyKeyRule</param>
        public virtual void InsertCustomerPoint(CustomerPoint customerPoint)
        {
            if (customerPoint == null)
                throw new ArgumentNullException(nameof(customerPoint));

            _customerPointRepository.Insert(customerPoint);

            //event notification
            _eventPublisher.EntityInserted(customerPoint);
        }

        /// <summary>
        /// Updates the CustomerPoint
        /// </summary>
        /// <param name="CustomerPoint">CustomerPoint</param>
        public virtual void UpdateCustomerPoint(CustomerPoint customerPoint)
        {
            if (customerPoint == null)
                throw new ArgumentNullException(nameof(customerPoint));

            _customerPointRepository.Update(customerPoint);

            //event notification
            _eventPublisher.EntityUpdated(customerPoint);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerPoint"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteCustomerPoint(CustomerPoint customerPoint)
        {
            if (customerPoint == null)
                throw new ArgumentNullException(nameof(customerPoint));

            customerPoint.Deleted = true;
            UpdateCustomerPoint(customerPoint);

            //event notification
            _eventPublisher.EntityDeleted(customerPoint);
        }


        public virtual IPagedList<CustomerPoint> GetCustomerPoints(long openIdHash = 0,
            int orderId = 0,
            byte consumeTypeId = 0,
            int expiredTime = 0,
            byte? status = null,
            bool showDeleted = false,
        int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerPointRepository.Table;

            if (openIdHash > 0)
                query = query.Where(a => a.OpenIdHash == openIdHash);
            if (orderId > 0)
                query = query.Where(a => a.OrderId == orderId);
            if (consumeTypeId > 0)
                query = query.Where(a => a.ConsumeTypeId == consumeTypeId);
            if (expiredTime > 0)
            {
                query = query.Where(a => a.ExpiredTime > 0);
                query = query.Where(a => a.ExpiredTime <= expiredTime);
            }
            if (status.HasValue)
                query = query.Where(a => a.Status == status);
            if (!showDeleted)
                query = query.Where(a => !a.Deleted);


            query = query.OrderByDescending(a => a.Id);

            var customerPoints = new PagedList<CustomerPoint>(query, pageIndex, pageSize);
            return customerPoints;
        }

        #endregion
    }
}