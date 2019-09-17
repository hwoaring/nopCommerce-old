using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Salers;

namespace Nop.Data.Mapping.Salers
{
    /// <summary>
    /// Represents an saler attribute mapping configuration
    /// </summary>
    public partial class SalerAttributeMap : NopEntityTypeConfiguration<SalerAttribute>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SalerAttribute> builder)
        {
            builder.ToTable(nameof(SalerAttribute));
            builder.HasKey(attribute => attribute.Id);

            builder.Property(attribute => attribute.Name).HasMaxLength(400).IsRequired();

            builder.Ignore(attribute => attribute.AttributeControlType);

            base.Configure(builder);
        }

        #endregion
    }
}