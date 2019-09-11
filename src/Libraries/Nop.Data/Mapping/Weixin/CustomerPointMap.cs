using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class CustomerPointMap : NopEntityTypeConfiguration<CustomerPoint>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<CustomerPoint> builder)
        {
            builder.ToTable(nameof(CustomerPoint));
            builder.HasKey(point => point.Id);

            builder.HasOne(point => point.CustomerAsset)
                .WithMany(asset => asset.CustomerPoints)
                .HasForeignKey(point => point.OpenIdHash)
                .IsRequired();

            builder.HasOne(point => point.Order)
                .WithMany()
                .HasForeignKey(point => point.OrderId)
                .IsRequired();

            builder.Ignore(amount => amount.ConsumeType);

            base.Configure(builder);
        }

        #endregion
    }
}