using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// CustomerPoint service interface
    /// </summary>
    public partial interface ICustomerPointService
    {
        /// <summary>
        /// Gets an CustomerPoint by CustomerPoint identifier
        /// </summary>
        /// <param name="CustomerPoint">CustomerPoint identifier</param>
        /// <returns>Affiliate</returns>
        CustomerPoint GetCustomerPointById(int customerPointId);

        /// <summary>
        /// Inserts an CustomerPoint
        /// </summary>
        /// <param name="WxAutoReplyKeyRule">WxAutoReplyKeyRule</param>
        void InsertCustomerPoint(CustomerPoint customerPoint);

        /// <summary>
        /// Updates the CustomerPoint
        /// </summary>
        /// <param name="CustomerPoint">CustomerPoint</param>
        void UpdateCustomerPoint(CustomerPoint customerPoint);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerPoint"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteCustomerPoint(CustomerPoint customerPoint);


        IPagedList<CustomerPoint> GetCustomerPoints(long openIdHash = 0,
            int orderId = 0,
            byte consumeTypeId = 0,
            int expiredTime = 0,
            byte? status = null,
            bool showDeleted = false,
        int pageIndex = 0, int pageSize = int.MaxValue);
    }
}