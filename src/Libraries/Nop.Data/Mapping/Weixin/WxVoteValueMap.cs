using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an WxVoteValue attribute mapping configuration
    /// </summary>
    public partial class WxVoteValueMap : NopEntityTypeConfiguration<WxVoteValue>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxVoteValue> builder)
        {
            builder.ToTable(nameof(WxVoteValue));
            builder.HasKey(value => value.Id);

            builder.HasOne(value => value.Vote)
                .WithMany()
                .HasForeignKey(value => value.VoteId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }

        #endregion
    }
}