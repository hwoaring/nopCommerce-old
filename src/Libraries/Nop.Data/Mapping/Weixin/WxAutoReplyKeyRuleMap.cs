using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxAutoReplyKeyRuleMap : NopEntityTypeConfiguration<WxAutoReplyKeyRule>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxAutoReplyKeyRule> builder)
        {
            builder.ToTable(nameof(WxAutoReplyKeyRule));
            builder.HasKey(rule => rule.Id);

            builder.Property(rule => rule.Title).HasMaxLength(50).IsRequired();
            builder.Property(rule => rule.KeyWords).IsRequired();

            builder.Ignore(rule => rule.MessageType);

            base.Configure(builder);
        }

        #endregion
    }
}