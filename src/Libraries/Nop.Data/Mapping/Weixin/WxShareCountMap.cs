using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxShareCountMap : NopEntityTypeConfiguration<WxShareCount>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxShareCount> builder)
        {
            builder.ToTable(nameof(WxShareCount));
            builder.HasKey(sharecount => new { sharecount.ShareListId, sharecount.OpenIdHash });

            builder.HasOne(sharecount => sharecount.ShareList)
                .WithMany(sharelist => sharelist.ShareCounts)
                .HasForeignKey(sharecount => sharecount.ShareListId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sharecount => sharecount.UserInfoBase)
                .WithMany()
                .HasForeignKey(sharecount => sharecount.OpenIdHash)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(sharecount => sharecount.Id);

            base.Configure(builder);
        }

        #endregion
    }
}