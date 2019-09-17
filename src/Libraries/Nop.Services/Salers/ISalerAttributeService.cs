using System.Collections.Generic;
using Nop.Core.Domain.Salers;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents a saler attribute service
    /// </summary>
    public partial interface ISalerAttributeService
    {
        #region Saler attributes

        /// <summary>
        /// Gets all saler attributes
        /// </summary>
        /// <returns>Saler attributes</returns>
        IList<SalerAttribute> GetAllSalerAttributes();

        /// <summary>
        /// Gets a saler attribute 
        /// </summary>
        /// <param name="salerAttributeId">Saler attribute identifier</param>
        /// <returns>Saler attribute</returns>
        SalerAttribute GetSalerAttributeById(int salerAttributeId);

        /// <summary>
        /// Inserts a saler attribute
        /// </summary>
        /// <param name="salerAttribute">Saler attribute</param>
        void InsertSalerAttribute(SalerAttribute salerAttribute);

        /// <summary>
        /// Updates a saler attribute
        /// </summary>
        /// <param name="salerAttribute">Saler attribute</param>
        void UpdateSalerAttribute(SalerAttribute salerAttribute);

        /// <summary>
        /// Deletes a saler attribute
        /// </summary>
        /// <param name="salerAttribute">Saler attribute</param>
        void DeleteSalerAttribute(SalerAttribute salerAttribute);

        #endregion

        #region Saler attribute values

        /// <summary>
        /// Gets saler attribute values by saler attribute identifier
        /// </summary>
        /// <param name="salerAttributeId">The saler attribute identifier</param>
        /// <returns>Saler attribute values</returns>
        IList<SalerAttributeValue> GetSalerAttributeValues(int salerAttributeId);

        /// <summary>
        /// Gets a saler attribute value
        /// </summary>
        /// <param name="salerAttributeValueId">Saler attribute value identifier</param>
        /// <returns>Saler attribute value</returns>
        SalerAttributeValue GetSalerAttributeValueById(int salerAttributeValueId);

        /// <summary>
        /// Inserts a saler attribute value
        /// </summary>
        /// <param name="salerAttributeValue">Saler attribute value</param>
        void InsertSalerAttributeValue(SalerAttributeValue salerAttributeValue);

        /// <summary>
        /// Updates a saler attribute value
        /// </summary>
        /// <param name="salerAttributeValue">Saler attribute value</param>
        void UpdateSalerAttributeValue(SalerAttributeValue salerAttributeValue);

        /// <summary>
        /// Deletes a saler attribute value
        /// </summary>
        /// <param name="salerAttributeValue">Saler attribute value</param>
        void DeleteSalerAttributeValue(SalerAttributeValue salerAttributeValue);

        #endregion
    }
}