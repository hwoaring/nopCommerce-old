namespace Nop.Services.Salers
{
    /// <summary>
    /// Represents default values related to saler services
    /// </summary>
    public static partial class NopSalersServiceDefaults
    {
        /// <summary>
        /// Gets a key for caching all saler attributes
        /// </summary>
        public static string SalerAttributesAllCacheKey => "Nop.salerattribute.all";

        /// <summary>
        /// Gets a key for caching a saler attribute
        /// </summary>
        /// <remarks>
        /// {0} : saler attribute ID
        /// </remarks>
        public static string SalerAttributesByIdCacheKey => "Nop.salerattribute.id-{0}";

        /// <summary>
        /// Gets a key for caching saler attribute values of the saler attribute
        /// </summary>
        /// <remarks>
        /// {0} : saler attribute ID
        /// </remarks>
        public static string SalerAttributeValuesAllCacheKey => "Nop.salerattributevalue.all-{0}";

        /// <summary>
        /// Gets a key for caching a saler attribute value
        /// </summary>
        /// <remarks>
        /// {0} : saler attribute value ID
        /// </remarks>
        public static string SalerAttributeValuesByIdCacheKey => "Nop.salerattributevalue.id-{0}";

        /// <summary>
        /// Gets a key pattern to clear cached saler attributes
        /// </summary>
        public static string SalerAttributesPrefixCacheKey => "Nop.salerattribute.";

        /// <summary>
        /// Gets a key pattern to clear cached saler attribute values
        /// </summary>
        public static string SalerAttributeValuesPrefixCacheKey => "Nop.salerattributevalue.";
    }
}