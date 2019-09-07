using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxQrLimitMap : NopEntityTypeConfiguration<WxQrLimit>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxQrLimit> builder)
        {
            builder.ToTable(nameof(WxQrLimit));
            builder.HasKey(image => image.Id);

            builder.Property(limit => limit.Name).HasMaxLength(50).IsRequired();
            builder.Property(limit => limit.Description).HasMaxLength(255);
            builder.Property(limit => limit.Ticket).HasMaxLength(255).IsUnicode(false);
            builder.Property(limit => limit.Url).HasMaxLength(1024).IsUnicode(false);

            builder.HasOne(limit => limit.QrGroup)
                .WithMany()
                .HasForeignKey(limit => limit.QrGroupId);

            builder.HasOne(limit => limit.QrChannel)
                .WithMany()
                .HasForeignKey(limit => limit.QrChannelId);

            base.Configure(builder);
        }

        #endregion
    }
}