using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Seo;
using Nop.Services.Events;
using Nop.Services.Seo;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// CustomerAmount service
    /// </summary>
    public partial class CustomerAmountService : ICustomerAmountService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<CustomerAmount> _customerAmountRepository;

        #endregion

        #region Ctor

        public CustomerAmountService(IEventPublisher eventPublisher,
            IRepository<CustomerAmount> customerAmountRepository
            )
        {
            _eventPublisher = eventPublisher;
            _customerAmountRepository = customerAmountRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an CustomerAmount by CustomerAmount identifier
        /// </summary>
        /// <param name="affiliateId">CustomerAmount identifier</param>
        /// <returns>CustomerAmount</returns>
        public virtual CustomerAmount GetCustomerAmountById(int customerAmountId)
        {
            if (customerAmountId == 0)
                return null;

            return _customerAmountRepository.GetById(customerAmountId);
        }

        /// <summary>
        /// Marks CustomerAmount as deleted 
        /// </summary>
        /// <param name="customerAmount">CustomerAmount</param>
        public virtual void DeleteCustomerAmount(CustomerAmount customerAmount)
        {
            if (customerAmount == null)
                throw new ArgumentNullException(nameof(customerAmount));

            customerAmount.Deleted = true;
            UpdateCustomerAmount(customerAmount);

            //event notification
            _eventPublisher.EntityDeleted(customerAmount);
        }

        /// <summary>
        /// Inserts an CustomerAmount
        /// </summary>
        /// <param name="customerAmount">CustomerAmount</param>
        public virtual void InsertCustomerAmount(CustomerAmount customerAmount)
        {
            if (customerAmount == null)
                throw new ArgumentNullException(nameof(customerAmount));

            _customerAmountRepository.Insert(customerAmount);

            //event notification
            _eventPublisher.EntityInserted(customerAmount);
        }

        /// <summary>
        /// Updates the customerAmount
        /// </summary>
        /// <param name="customerAmount">CustomerAmount</param>
        public virtual void UpdateCustomerAmount(CustomerAmount customerAmount)
        {
            if (customerAmount == null)
                throw new ArgumentNullException(nameof(customerAmount));

            _customerAmountRepository.Update(customerAmount);

            //event notification
            _eventPublisher.EntityUpdated(customerAmount);
        }

        public virtual IPagedList<CustomerAmount> GetCustomerAmounts(long openIdHash = 0, int orderId = 0,
            decimal amountValue = 0, //大于0查询值增加的项，小于0查询减少的项
            byte consumeTypeId = 0, //查询对应消费类型
            int startTime = 0, int endTime = 0, //创建时间介于两个时间间的值
            byte status=0, //状态（可设置pending状态等）
            bool deleted=false,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerAmountRepository.Table;
            if (openIdHash > 0)
                query = query.Where(a => a.OpenIdHash == openIdHash);
            if (orderId > 0)
                query = query.Where(a => a.OrderId == orderId);
            if (amountValue > 0)
                query = query.Where(a => a.AmountValue >0);
            if (amountValue < 0)
                query = query.Where(a => a.AmountValue < 0);
            if (consumeTypeId > 0)
                query = query.Where(a => a.ConsumeTypeId == consumeTypeId);

            if (startTime > 0 && endTime == 0)
                query = query.Where(a => a.CreatTime >= startTime);
            else if (startTime == 0 && endTime > 0)
                query = query.Where(a => a.CreatTime <= endTime);
            else if (startTime > 0 && endTime > 0)
            {
                if (startTime < endTime)
                    query = query.Where(a => a.CreatTime >= startTime && a.CreatTime <= endTime);
                else if (startTime > endTime)
                    query = query.Where(a => a.CreatTime >= endTime && a.CreatTime <= startTime);
                else
                    query = query.Where(a => a.CreatTime == startTime);
            }

            if (status > 0)
                query = query.Where(a => a.Status == status);


            query = query.OrderByDescending(a => a.Id);

            var customerAmounts = new PagedList<CustomerAmount>(query, pageIndex, pageSize);
            return customerAmounts;
        }

        #endregion
    }
}