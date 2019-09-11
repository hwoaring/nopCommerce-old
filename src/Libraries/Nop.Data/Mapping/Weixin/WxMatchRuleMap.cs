using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxMatchRuleMap : NopEntityTypeConfiguration<WxMatchRule>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxMatchRule> builder)
        {
            builder.ToTable(nameof(WxMatchRule));
            builder.HasKey(rule => rule.Id);

            builder.Property(rule => rule.Name).HasMaxLength(50).IsRequired();
            builder.Property(rule => rule.SystemName).HasMaxLength(50).IsRequired();
            builder.Property(rule => rule.TagId).HasMaxLength(50).IsUnicode(false);
            builder.Property(rule => rule.Sex).HasMaxLength(15).IsUnicode(false);
            builder.Property(rule => rule.ClientPlatformType).HasMaxLength(50).IsUnicode(false);
            builder.Property(rule => rule.Language).HasMaxLength(15).IsUnicode(false);

            base.Configure(builder);
        }

        #endregion
    }
}