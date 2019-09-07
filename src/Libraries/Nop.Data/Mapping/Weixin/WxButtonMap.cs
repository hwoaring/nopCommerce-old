using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxButtonMap : NopEntityTypeConfiguration<WxButton>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxButton> builder)
        {
            builder.ToTable(nameof(WxButton));
            builder.HasKey(btn => btn.Id);

            builder.HasOne(btn => btn.Menu)
                .WithMany(menu => menu.Buttons)
                .HasForeignKey(btn => btn.MenuId)
                .IsRequired();

            builder.HasOne(btn => btn.ButtonValue)
                .WithMany(value => value.Buttons)
                .HasForeignKey(btn => btn.ButtonValueId)
                .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}