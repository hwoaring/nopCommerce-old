using System;

namespace Nop.Core.Domain.Salers
{
    /// <summary>
    /// Represents a saler note
    /// </summary>
    public partial class SalerNote : BaseEntity
    {
        /// <summary>
        /// Gets or sets the saler identifier
        /// </summary>
        public int SalerId { get; set; }

        /// <summary>
        /// Gets or sets the note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the date and time of saler note creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets the saler
        /// </summary>
        public virtual Saler Saler { get; set; }
    }
}
