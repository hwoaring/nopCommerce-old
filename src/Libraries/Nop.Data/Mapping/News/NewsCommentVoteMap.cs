using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.News;

namespace Nop.Data.Mapping.News
{
    /// <summary>
    /// Represents a news NewsCommentVote mapping configuration
    /// </summary>
    public partial class NewsCommentVoteMap : NopEntityTypeConfiguration<NewsCommentVote>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<NewsCommentVote> builder)
        {
            builder.ToTable(nameof(NewsCommentVote));
            builder.HasKey(vote => vote.Id);

            base.Configure(builder);
        }

        #endregion
    }
}