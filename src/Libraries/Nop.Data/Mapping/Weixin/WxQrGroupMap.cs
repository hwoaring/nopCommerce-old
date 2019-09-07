using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxQrGroupMap : NopEntityTypeConfiguration<WxQrGroup>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxQrGroup> builder)
        {
            builder.ToTable(nameof(WxQrGroup));
            builder.HasKey(group => group.Id);

            builder.Property(group => group.Name).HasMaxLength(50).IsRequired();


            builder.HasMany(group => group.ChildGroups)
                .WithOne()
                .HasForeignKey(group => group.ParentId);


            base.Configure(builder);
        }

        #endregion
    }
}