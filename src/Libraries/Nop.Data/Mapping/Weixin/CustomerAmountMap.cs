using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class CustomerAmountMap : NopEntityTypeConfiguration<CustomerAmount>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<CustomerAmount> builder)
        {
            builder.ToTable(nameof(CustomerAmount));
            builder.HasKey(amount => amount.Id);

            builder.Property(amount => amount.AmountValue).HasColumnType("decimal(9, 2)");

            builder.HasOne(amount => amount.CustomerAsset)
                .WithMany(asset => asset.CustomerAmounts)
                .HasForeignKey(amount => amount.OpenIdHash)
                .IsRequired();

            builder.HasOne(amount => amount.Order)
                .WithMany()
                .HasForeignKey(amount => amount.OrderId)
                .IsRequired();

            builder.Ignore(amount => amount.ConsumeType);

            base.Configure(builder);
        }

        #endregion
    }
}