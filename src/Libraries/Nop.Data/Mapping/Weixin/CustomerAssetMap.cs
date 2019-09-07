using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class CustomerAssetMap : NopEntityTypeConfiguration<CustomerAsset>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<CustomerAsset> builder)
        {
            builder.ToTable(nameof(CustomerAsset));
            builder.HasKey(asset => asset.OpenIdHash);
            builder.Property(asset => asset.OpenIdHash).ValueGeneratedNever().IsRequired();

            builder.Property(asset => asset.Level).HasDefaultValue(1);
            builder.Property(asset => asset.Stars).HasDefaultValue(1);
            builder.Property(asset => asset.VirtualCurrency).HasColumnType("decimal(9, 2)");
            builder.Property(asset => asset.Amount).HasColumnType("decimal(9, 2)");


            builder.Ignore(asset => asset.Id);

            base.Configure(builder);
        }

        #endregion
    }
}