using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// CustomerAsset service interface
    /// </summary>
    public partial interface ICustomerAssetService
    {
        /// <summary>
        /// Gets an CustomerAsset by CustomerAsset identifier
        /// </summary>
        /// <param name="OpenIdHash">CustomerAsset identifier</param>
        /// <returns>CustomerAmount</returns>
        CustomerAsset GetCustomerAssetByOpenIdHash(long openIdHash);

        /// <summary>
        /// Marks CustomerAsset as deleted 
        /// </summary>
        /// <param name="CustomerAsset">CustomerAsset</param>
        void DeleteCustomerAsset(CustomerAsset customerAsset);

        /// <summary>
        /// Inserts an CustomerAsset
        /// </summary>
        /// <param name="CustomerAsset">CustomerAsset</param>
        void InsertCustomerAsset(CustomerAsset customerAsset);

        /// <summary>
        /// Updates the CustomerAsset
        /// </summary>
        /// <param name="CustomerAsset">CustomerAsset</param>
        void UpdateCustomerAsset(CustomerAsset customerAsset);

        IPagedList<CustomerAsset> GetCustomerAssets(long openIdHash = 0,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}