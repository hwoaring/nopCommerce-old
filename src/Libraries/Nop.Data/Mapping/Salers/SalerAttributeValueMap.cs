using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Salers;

namespace Nop.Data.Mapping.Salers
{
    /// <summary>
    /// Represents a saler attribute value mapping configuration
    /// </summary>
    public partial class SalerAttributeValueMap : NopEntityTypeConfiguration<SalerAttributeValue>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SalerAttributeValue> builder)
        {
            builder.ToTable(nameof(SalerAttributeValue));
            builder.HasKey(value => value.Id);

            builder.Property(value => value.Name).HasMaxLength(400).IsRequired();

            builder.HasOne(value => value.SalerAttribute)
                .WithMany(attribute => attribute.SalerAttributeValues)
                .HasForeignKey(value => value.SalerAttributeId)
                .IsRequired();

            base.Configure(builder);
        }
        
        #endregion
    }
}