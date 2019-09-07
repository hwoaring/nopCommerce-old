using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxQrTempMap : NopEntityTypeConfiguration<WxQrTemp>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxQrTemp> builder)
        {
            builder.ToTable(nameof(WxQrTemp));
            builder.HasKey(temp => new { temp.OpenIdHash, temp.QrImageId });

            builder.Property(temp => temp.Ticket).HasMaxLength(255).IsUnicode(false);
            builder.Property(temp => temp.Url).HasMaxLength(1024).IsUnicode(false);

            builder.HasOne(temp => temp.QrImage)
                .WithMany()
                .HasForeignKey(temp => temp.QrImageId);

            builder.HasOne(temp => temp.UserInfoBase)
                .WithMany()
                .HasForeignKey(temp => temp.OpenIdHash)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(temp => temp.Id);
            builder.Ignore(temp => temp.QrcodePrefix);

            base.Configure(builder);
        }

        #endregion
    }
}