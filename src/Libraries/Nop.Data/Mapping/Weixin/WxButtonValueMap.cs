using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxButtonValueMap : NopEntityTypeConfiguration<WxButtonValue>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxButtonValue> builder)
        {
            builder.ToTable(nameof(WxButtonValue));
            builder.HasKey(value => value.Id);

            builder.Property(menu => menu.Title).HasMaxLength(50).IsRequired();
            builder.Property(menu => menu.Type).HasMaxLength(50).IsRequired();
            builder.Property(menu => menu.Name).HasMaxLength(50).IsRequired();
            builder.Property(menu => menu.Key).HasMaxLength(255);
            builder.Property(menu => menu.Url).HasMaxLength(1024);

            builder.Property(menu => menu.MediaId).HasMaxLength(255);
            builder.Property(menu => menu.Appid).HasMaxLength(255);
            builder.Property(menu => menu.Pagepath).HasMaxLength(1024);

            base.Configure(builder);
        }

        #endregion
    }
}