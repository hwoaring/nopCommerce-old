using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an WxVoteUser attribute mapping configuration
    /// </summary>
    public partial class WxVoteUserMap : NopEntityTypeConfiguration<WxVoteUser>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxVoteUser> builder)
        {
            builder.ToTable(nameof(WxVoteUser));
            builder.HasKey(user => new { user.OpenIdHash, user.VoteId });

            builder.HasOne(user => user.Vote)
                .WithMany()
                .HasForeignKey(user => user.VoteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(user => user.Id);

            base.Configure(builder);
        }

        #endregion
    }
}