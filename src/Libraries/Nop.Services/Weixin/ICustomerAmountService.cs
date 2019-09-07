using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// CustomerAmount service interface
    /// </summary>
    public partial interface ICustomerAmountService
    {
        /// <summary>
        /// Gets an CustomerAmount by CustomerAmount identifier
        /// </summary>
        /// <param name="customerAmountId">CustomerAmount identifier</param>
        /// <returns>Affiliate</returns>
        CustomerAmount GetCustomerAmountById(int customerAmountId);

        /// <summary>
        /// Marks CustomerAmount as deleted 
        /// </summary>
        /// <param name="customerAmount">CustomerAmount</param>
        void DeleteCustomerAmount(CustomerAmount customerAmount);

        /// <summary>
        /// Inserts an CustomerAmount
        /// </summary>
        /// <param name="customerAmount">CustomerAmount</param>
        void InsertCustomerAmount(CustomerAmount customerAmount);

        /// <summary>
        /// Updates the customerAmount
        /// </summary>
        /// <param name="customerAmount">CustomerAmount</param>
        void UpdateCustomerAmount(CustomerAmount customerAmount);

        IPagedList<CustomerAmount> GetCustomerAmounts(long openIdHash = 0, int orderId = 0,
            decimal amountValue = 0, //大于0查询值增加的项，小于0查询减少的项
            byte consumeTypeId = 0, //查询对应消费类型
            int startTime = 0, int endTime = 0, //创建时间介于两个时间间的值
            byte status = 0, //状态（可设置pending状态等）
            bool deleted = false,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}