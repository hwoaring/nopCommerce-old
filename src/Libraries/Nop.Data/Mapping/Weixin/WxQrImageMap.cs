using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxQrImageMap : NopEntityTypeConfiguration<WxQrImage>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxQrImage> builder)
        {
            builder.ToTable(nameof(WxQrImage));
            builder.HasKey(image => image.Id);

            builder.Property(image => image.Title).HasMaxLength(50).IsRequired();
            builder.Property(image => image.Url).HasMaxLength(1024).IsRequired();
            builder.Property(image => image.MessageIds).HasMaxLength(255).IsUnicode(false);
            builder.Property(image => image.TagIds).HasMaxLength(255).IsUnicode(false);

            builder.HasOne(image => image.Product)
                .WithMany()
                .HasForeignKey(image => image.ProductId);


            builder.Ignore(image => image.QrImageType);
            builder.Ignore(image => image.Messages);
            builder.Ignore(image => image.MessageType);

            base.Configure(builder);
        }

        #endregion
    }
}