using Nop.Core.Domain.Localization;

namespace Nop.Core.Domain.Salers
{
    /// <summary>
    /// Represents a saler attribute value
    /// </summary>
    public partial class SalerAttributeValue : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is pre-selected
        /// </summary>
        public bool IsPreSelected { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the saler attribute identifier
        /// </summary>
        public int SalerAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the saler attribute
        /// </summary>
        public virtual SalerAttribute SalerAttribute { get; set; }
    }
}