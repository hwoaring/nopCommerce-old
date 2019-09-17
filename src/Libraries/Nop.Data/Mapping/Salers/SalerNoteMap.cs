using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Salers;

namespace Nop.Data.Mapping.Salers
{
    /// <summary>
    /// Represents a saler note mapping configuration
    /// </summary>
    public partial class SalerNoteMap : NopEntityTypeConfiguration<SalerNote>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SalerNote> builder)
        {
            builder.ToTable(nameof(SalerNote));
            builder.HasKey(note => note.Id);

            builder.Property(note => note.Note).IsRequired();

            builder.HasOne(note => note.Saler)
                .WithMany(saler => saler.SalerNotes)
                .HasForeignKey(note => note.SalerId)
                .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}