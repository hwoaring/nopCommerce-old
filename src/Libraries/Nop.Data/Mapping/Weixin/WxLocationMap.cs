using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxLocationMap : NopEntityTypeConfiguration<WxLocation>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxLocation> builder)
        {
            builder.ToTable(nameof(WxLocation));
            builder.HasKey(loc => loc.OpenIdHash);
            builder.Property(loc => loc.OpenIdHash).ValueGeneratedNever().IsRequired();

            builder.Property(loc => loc.Latitude).HasColumnType("decimal(9, 6)");
            builder.Property(loc => loc.Longitude).HasColumnType("decimal(9, 6)");
            builder.Property(loc => loc.Precision).HasColumnType("decimal(9, 6)");

            builder.Ignore(loc => loc.Id);

            base.Configure(builder);
        }

        #endregion
    }
}