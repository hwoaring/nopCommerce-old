using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxMsgMenuAttributeMap : NopEntityTypeConfiguration<WxMsgMenuAttribute>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxMsgMenuAttribute> builder)
        {
            builder.ToTable(nameof(WxMsgMenuAttribute));
            builder.HasKey(attr => attr.Id);
            builder.Property(attr => attr.Id).ValueGeneratedNever().IsRequired();

            builder.Property(attr => attr.Content).HasMaxLength(255).IsRequired();


            builder.HasOne(attr => attr.MsgMenu)
                .WithMany(menu => menu.MsgMenuAttributes)
                .HasForeignKey(attr => attr.MsgMenuId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }

        #endregion
    }
}