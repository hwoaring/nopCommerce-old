using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;
using Nop.Services.Events;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// CustomerAsset service
    /// </summary>
    public partial class CustomerAssetService : ICustomerAssetService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<CustomerAsset> _customerAssetRepository;

        #endregion

        #region Ctor

        public CustomerAssetService(IEventPublisher eventPublisher,
            IRepository<CustomerAsset> customerAssetRepository
            )
        {
            _eventPublisher = eventPublisher;
            _customerAssetRepository = customerAssetRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an CustomerAsset by CustomerAsset identifier
        /// </summary>
        /// <param name="OpenIdHash">CustomerAsset identifier</param>
        /// <returns>CustomerAmount</returns>
        public virtual CustomerAsset GetCustomerAssetByOpenIdHash(long openIdHash)
        {
            if (openIdHash == 0)
                return null;

            return _customerAssetRepository.GetById(openIdHash);
        }

        /// <summary>
        /// Marks CustomerAsset as deleted 
        /// </summary>
        /// <param name="CustomerAsset">CustomerAsset</param>
        public virtual void DeleteCustomerAsset(CustomerAsset customerAsset)
        {
            if (customerAsset == null)
                throw new ArgumentNullException(nameof(customerAsset));

            _customerAssetRepository.Delete(customerAsset);

            //event notification
            _eventPublisher.EntityDeleted(customerAsset);
        }

        /// <summary>
        /// Inserts an CustomerAsset
        /// </summary>
        /// <param name="CustomerAsset">CustomerAsset</param>
        public virtual void InsertCustomerAsset(CustomerAsset customerAsset)
        {
            if (customerAsset == null)
                throw new ArgumentNullException(nameof(customerAsset));

            _customerAssetRepository.Insert(customerAsset);

            //event notification
            _eventPublisher.EntityInserted(customerAsset);
        }

        /// <summary>
        /// Updates the CustomerAsset
        /// </summary>
        /// <param name="CustomerAsset">CustomerAsset</param>
        public virtual void UpdateCustomerAsset(CustomerAsset customerAsset)
        {
            if (customerAsset == null)
                throw new ArgumentNullException(nameof(customerAsset));

            _customerAssetRepository.Update(customerAsset);

            //event notification
            _eventPublisher.EntityUpdated(customerAsset);
        }

        public virtual IPagedList<CustomerAsset> GetCustomerAssets(long openIdHash = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerAssetRepository.Table;
            if (openIdHash > 0)
                query = query.Where(a => a.OpenIdHash == openIdHash);

            query = query.OrderByDescending(a => a.CreatTime);

            var customerAssets = new PagedList<CustomerAsset>(query, pageIndex, pageSize);
            return customerAssets;
        }

        #endregion
    }
}