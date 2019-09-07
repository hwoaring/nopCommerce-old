using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxMsgMenuValueMap : NopEntityTypeConfiguration<WxMsgMenuValue>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxMsgMenuValue> builder)
        {
            builder.ToTable(nameof(WxMsgMenuValue));
            builder.HasKey(value => new { value.OpenIdHash, value.BizMsgMenuId });

            builder.HasOne(value => value.MsgMenuAttribute)
                .WithMany(attr => attr.MsgMenuValues)
                .HasForeignKey(value => value.BizMsgMenuId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(value => value.Id);

            base.Configure(builder);
        }

        #endregion
    }
}