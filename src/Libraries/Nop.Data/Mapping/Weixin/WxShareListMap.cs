using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxShareListMap : NopEntityTypeConfiguration<WxShareList>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxShareList> builder)
        {
            builder.ToTable(nameof(WxShareList));
            builder.HasKey(sharlist => sharlist.Id);

            builder.HasOne(sharlist => sharlist.Product)
                .WithMany()
                .HasForeignKey(sharlist => sharlist.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }

        #endregion
    }
}