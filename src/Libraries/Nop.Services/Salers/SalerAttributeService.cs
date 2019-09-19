using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Salers;
using Nop.Services.Events;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents a saler attribute service implementation
    /// </summary>
    public partial class SalerAttributeService : ISalerAttributeService
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<SalerAttribute> _salerAttributeRepository;
        private readonly IRepository<SalerAttributeValue> _salerAttributeValueRepository;

        #endregion

        #region Ctor

        public SalerAttributeService(ICacheManager cacheManager,
            IEventPublisher eventPublisher,
            IRepository<SalerAttribute> salerAttributeRepository,
            IRepository<SalerAttributeValue> salerAttributeValueRepository)
        {
            _cacheManager = cacheManager;
            _eventPublisher = eventPublisher;
            _salerAttributeRepository = salerAttributeRepository;
            _salerAttributeValueRepository = salerAttributeValueRepository;
        }

        #endregion

        #region Methods

        #region Saler attributes

        /// <summary>
        /// Gets all saler attributes
        /// </summary>
        /// <returns>Saler attributes</returns>
        public virtual IList<SalerAttribute> GetAllSalerAttributes()
        {
            return _cacheManager.Get(NopSalersServiceDefaults.SalerAttributesAllCacheKey, () =>
            {
                return _salerAttributeRepository.Table
                    .OrderBy(salerAttribute => salerAttribute.DisplayOrder).ThenBy(salerAttribute => salerAttribute.Id)
                    .ToList();
            });
        }

        /// <summary>
        /// Gets a saler attribute 
        /// </summary>
        /// <param name="salerAttributeId">Saler attribute identifier</param>
        /// <returns>Saler attribute</returns>
        public virtual SalerAttribute GetSalerAttributeById(int salerAttributeId)
        {
            if (salerAttributeId == 0)
                return null;

            var key = string.Format(NopSalersServiceDefaults.SalerAttributesByIdCacheKey, salerAttributeId);
            return _cacheManager.Get(key, () => _salerAttributeRepository.GetById(salerAttributeId));
        }

        /// <summary>
        /// Inserts a saler attribute
        /// </summary>
        /// <param name="salerAttribute">Saler attribute</param>
        public virtual void InsertSalerAttribute(SalerAttribute salerAttribute)
        {
            if (salerAttribute == null)
                throw new ArgumentNullException(nameof(salerAttribute));

            _salerAttributeRepository.Insert(salerAttribute);

            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributesPrefixCacheKey);
            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributeValuesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(salerAttribute);
        }

        /// <summary>
        /// Updates a saler attribute
        /// </summary>
        /// <param name="salerAttribute">Saler attribute</param>
        public virtual void UpdateSalerAttribute(SalerAttribute salerAttribute)
        {
            if (salerAttribute == null)
                throw new ArgumentNullException(nameof(salerAttribute));

            _salerAttributeRepository.Update(salerAttribute);

            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributesPrefixCacheKey);
            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributeValuesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(salerAttribute);
        }

        /// <summary>
        /// Deletes a saler attribute
        /// </summary>
        /// <param name="salerAttribute">Saler attribute</param>
        public virtual void DeleteSalerAttribute(SalerAttribute salerAttribute)
        {
            if (salerAttribute == null)
                throw new ArgumentNullException(nameof(salerAttribute));

            _salerAttributeRepository.Delete(salerAttribute);

            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributesPrefixCacheKey);
            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributeValuesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(salerAttribute);
        }

        #endregion

        #region Saler attribute values

        /// <summary>
        /// Gets saler attribute values by saler attribute identifier
        /// </summary>
        /// <param name="salerAttributeId">The saler attribute identifier</param>
        /// <returns>Saler attribute values</returns>
        public virtual IList<SalerAttributeValue> GetSalerAttributeValues(int salerAttributeId)
        {
            var key = string.Format(NopSalersServiceDefaults.SalerAttributeValuesAllCacheKey, salerAttributeId);
            return _cacheManager.Get(key, () =>
            {
                return _salerAttributeValueRepository.Table
                    .OrderBy(salerAttributeValue => salerAttributeValue.DisplayOrder).ThenBy(salerAttributeValue => salerAttributeValue.Id)
                    .Where(salerAttributeValue => salerAttributeValue.SalerAttributeId == salerAttributeId)
                    .ToList();
            });
        }

        /// <summary>
        /// Gets a saler attribute value
        /// </summary>
        /// <param name="salerAttributeValueId">Saler attribute value identifier</param>
        /// <returns>Saler attribute value</returns>
        public virtual SalerAttributeValue GetSalerAttributeValueById(int salerAttributeValueId)
        {
            if (salerAttributeValueId == 0)
                return null;

            var key = string.Format(NopSalersServiceDefaults.SalerAttributeValuesByIdCacheKey, salerAttributeValueId);
            return _cacheManager.Get(key, () => _salerAttributeValueRepository.GetById(salerAttributeValueId));
        }

        /// <summary>
        /// Inserts a saler attribute value
        /// </summary>
        /// <param name="salerAttributeValue">Saler attribute value</param>
        public virtual void InsertSalerAttributeValue(SalerAttributeValue salerAttributeValue)
        {
            if (salerAttributeValue == null)
                throw new ArgumentNullException(nameof(salerAttributeValue));

            _salerAttributeValueRepository.Insert(salerAttributeValue);

            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributesPrefixCacheKey);
            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributeValuesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(salerAttributeValue);
        }

        /// <summary>
        /// Updates the saler attribute value
        /// </summary>
        /// <param name="salerAttributeValue">Saler attribute value</param>
        public virtual void UpdateSalerAttributeValue(SalerAttributeValue salerAttributeValue)
        {
            if (salerAttributeValue == null)
                throw new ArgumentNullException(nameof(salerAttributeValue));

            _salerAttributeValueRepository.Update(salerAttributeValue);

            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributesPrefixCacheKey);
            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributeValuesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(salerAttributeValue);
        }

        /// <summary>
        /// Deletes a saler attribute value
        /// </summary>
        /// <param name="salerAttributeValue">Saler attribute value</param>
        public virtual void DeleteSalerAttributeValue(SalerAttributeValue salerAttributeValue)
        {
            if (salerAttributeValue == null)
                throw new ArgumentNullException(nameof(salerAttributeValue));

            _salerAttributeValueRepository.Delete(salerAttributeValue);

            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributesPrefixCacheKey);
            _cacheManager.RemoveByPrefix(NopSalersServiceDefaults.SalerAttributeValuesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityDeleted(salerAttributeValue);
        }

        #endregion

        #endregion
    }
}