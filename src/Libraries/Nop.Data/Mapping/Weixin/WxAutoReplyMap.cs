using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxAutoReplyMap : NopEntityTypeConfiguration<WxAutoReply>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxAutoReply> builder)
        {
            builder.ToTable(nameof(WxAutoReply));
            builder.HasKey(reply => reply.Id);

            builder.Property(reply => reply.Title).HasMaxLength(50).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}