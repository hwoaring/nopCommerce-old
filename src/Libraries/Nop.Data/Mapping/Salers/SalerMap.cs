using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Salers;

namespace Nop.Data.Mapping.Salers
{
    /// <summary>
    /// Represents a saler mapping configuration
    /// </summary>
    public partial class SalerMap : NopEntityTypeConfiguration<Saler>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Saler> builder)
        {
            builder.ToTable(nameof(Saler));
            builder.HasKey(saler => saler.Id);

            builder.Property(saler => saler.Name).HasMaxLength(400).IsRequired();
            builder.Property(saler => saler.Email).HasMaxLength(400);
            builder.Property(saler => saler.MetaKeywords).HasMaxLength(400);
            builder.Property(saler => saler.MetaTitle).HasMaxLength(400);
            builder.Property(saler => saler.PageSizeOptions).HasMaxLength(200);

            base.Configure(builder);
        }

        #endregion
    }
}