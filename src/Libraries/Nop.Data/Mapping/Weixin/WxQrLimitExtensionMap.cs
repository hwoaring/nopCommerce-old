using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxQrLimitExtensionMap : NopEntityTypeConfiguration<WxQrLimitExtension>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxQrLimitExtension> builder)
        {
            builder.ToTable(nameof(WxQrLimitExtension));
            builder.HasKey(ext => ext.Id);

            builder.Property(ext => ext.Name).HasMaxLength(64).IsRequired();
            builder.Property(ext => ext.TagIds).HasMaxLength(255).IsUnicode(false);
            builder.Property(ext => ext.MessageIds).HasMaxLength(64).IsUnicode(false);

            builder.HasOne(ext => ext.QrLimit)
                .WithMany(limit => limit.QrLimitExtensions)
                .HasForeignKey(ext => ext.QrLimitId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ext => ext.UserInfoBase)
                .WithMany()
                .HasForeignKey(ext => ext.OpenIdHash)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(ext => ext.MessageType);
            builder.Ignore(ext => ext.Messages);

            base.Configure(builder);
        }

        #endregion
    }
}