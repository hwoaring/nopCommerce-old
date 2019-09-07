using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxQrChannelMap : NopEntityTypeConfiguration<WxQrChannel>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxQrChannel> builder)
        {
            builder.ToTable(nameof(WxQrChannel));
            builder.HasKey(channel => channel.Id);

            builder.Property(channel => channel.Name).HasMaxLength(50).IsRequired();


            builder.HasMany(channel => channel.ChildChannels)
                .WithOne()
                .HasForeignKey(channel => channel.ParentId);


            base.Configure(builder);
        }

        #endregion
    }
}