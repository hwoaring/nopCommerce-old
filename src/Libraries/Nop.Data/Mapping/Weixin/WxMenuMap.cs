using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxMenuMap : NopEntityTypeConfiguration<WxMenu>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxMenu> builder)
        {
            builder.ToTable(nameof(WxMenu));
            builder.HasKey(menu => menu.Id);

            builder.Property(menu => menu.Name).HasMaxLength(50).IsRequired();
            builder.Property(menu => menu.SystemName).HasMaxLength(50).IsRequired();

            builder.HasOne(menu => menu.MatchRule)
                .WithMany(rule => rule.Menus)
                .HasForeignKey(menu => menu.MatchRuleId);

            base.Configure(builder);
        }

        #endregion
    }
}