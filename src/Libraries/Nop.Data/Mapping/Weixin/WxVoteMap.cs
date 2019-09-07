using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an WxVote attribute mapping configuration
    /// </summary>
    public partial class WxVoteMap : NopEntityTypeConfiguration<WxVote>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxVote> builder)
        {
            builder.ToTable(nameof(WxVote));
            builder.HasKey(vote => vote.Id);

            builder.Property(vote => vote.Title).HasMaxLength(50);
            builder.Property(vote => vote.PicUrl).HasMaxLength(1024).IsUnicode(false);
            builder.Property(vote => vote.CycleType).HasMaxLength(50).IsUnicode(false);
            builder.Property(vote => vote.ChannelName).HasMaxLength(50);
            builder.Property(vote => vote.ArticleType).HasMaxLength(15).IsUnicode(false);
            builder.Property(vote => vote.Author).HasMaxLength(50);
            builder.Property(vote => vote.CrowdType).HasMaxLength(50).IsUnicode(false);
            builder.Property(vote => vote.CrowdValue).HasMaxLength(50).IsUnicode(false);
            builder.Property(vote => vote.Ticket).HasMaxLength(1024).IsUnicode(false);
            builder.Property(vote => vote.QrcodeUrl).HasMaxLength(1024).IsUnicode(false);
            builder.Property(vote => vote.SignedMessageType).HasMaxLength(15).IsUnicode(false);
            builder.Property(vote => vote.SignedMessageId).HasMaxLength(64).IsUnicode(false);
            builder.Property(vote => vote.UnsignMessageType).HasMaxLength(15).IsUnicode(false);
            builder.Property(vote => vote.UnsignMessageId).HasMaxLength(64).IsUnicode(false);
            builder.Property(vote => vote.IndexPage).HasMaxLength(1024).IsUnicode(false);
            builder.Property(vote => vote.ShowPage).HasMaxLength(1024).IsUnicode(false);
            builder.Property(vote => vote.DetailPage).HasMaxLength(1024).IsUnicode(false);

            base.Configure(builder);
        }

        #endregion
    }
}