using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxMessageMap : NopEntityTypeConfiguration<WxMessage>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxMessage> builder)
        {
            builder.ToTable(nameof(WxMessage));
            builder.HasKey(msg => msg.Id);

            builder.Property(msg => msg.Title).HasMaxLength(64).IsRequired();
            builder.Property(msg => msg.ProductIds).HasMaxLength(255).IsUnicode(false);
            builder.Property(msg => msg.MediaId).HasMaxLength(255).IsUnicode(false);
            builder.Property(msg => msg.CardId).HasMaxLength(255).IsUnicode(false);
            builder.Property(msg => msg.ProductIds).HasMaxLength(255).IsUnicode(false);
            builder.Property(msg => msg.ThumbMediaId).HasMaxLength(255).IsUnicode(false);
            builder.Property(msg => msg.AppId).HasMaxLength(255).IsUnicode(false);

            builder.Ignore(msg => msg.MsgType);
            builder.Ignore(msg => msg.ResponseType);
            builder.Ignore(msg => msg.Articles);

            base.Configure(builder);
        }

        #endregion
    }
}