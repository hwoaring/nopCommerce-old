using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.News;

namespace Nop.Data.Mapping.News
{
    /// <summary>
    /// Represents a news mapping configuration
    /// </summary>
    public partial class NewsItemMap : NopEntityTypeConfiguration<NewsItem>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<NewsItem> builder)
        {
            builder.ToTable(NopMappingDefaults.NewsItemTable);
            builder.HasKey(newsItem => newsItem.Id);

            builder.Property(newsItem => newsItem.Title).IsRequired();
            builder.Property(newsItem => newsItem.Short).IsRequired();
            builder.Property(newsItem => newsItem.Full).IsRequired();
            builder.Property(newsItem => newsItem.MetaKeywords).HasMaxLength(400);
            builder.Property(newsItem => newsItem.MetaTitle).HasMaxLength(400);
            builder.Property(newsItem => newsItem.SubTitle).HasMaxLength(400);
            builder.Property(newsItem => newsItem.Author).HasMaxLength(400);
            builder.Property(newsItem => newsItem.Source).HasMaxLength(400).IsUnicode(false);
            builder.Property(newsItem => newsItem.ImgUrl).HasMaxLength(1024).IsUnicode(false);
            builder.Property(newsItem => newsItem.LinkUrl).HasMaxLength(1024).IsUnicode(false);
            builder.Property(newsItem => newsItem.OriginalUrl).HasMaxLength(1024).IsUnicode(false);
            builder.Property(newsItem => newsItem.Tags).HasMaxLength(400);
            builder.Property(newsItem => newsItem.Summary).HasMaxLength(400);
            builder.Property(newsItem => newsItem.TemplatePage).HasMaxLength(1024).IsUnicode(false);

            builder.HasOne(newsItem => newsItem.Language)
                .WithMany()
                .HasForeignKey(newsItem => newsItem.LanguageId)
                .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}