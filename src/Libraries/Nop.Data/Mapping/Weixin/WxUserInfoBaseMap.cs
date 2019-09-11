using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxUserInfoBaseMap : NopEntityTypeConfiguration<WxUserInfoBase>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxUserInfoBase> builder)
        {
            builder.ToTable(nameof(WxUserInfoBase));
            builder.HasKey(userbase => userbase.OpenIdHash);
            builder.Property(userbase => userbase.OpenIdHash).ValueGeneratedNever().IsRequired();

            builder.Property(userbase => userbase.OpenId).HasMaxLength(64).IsUnicode(false).IsRequired();
            builder.Property(userbase => userbase.UnionId).HasMaxLength(64).IsUnicode(false);

            builder.HasMany(userbase => userbase.ChildUserInfoBases)
                .WithOne()
                .HasForeignKey(userbase => userbase.OpenIdReferer);

            //builder.HasAlternateKey(userbase => userbase.OpenId);
            //builder.HasIndex(userbase => userbase.OpenId).IsUnique(); //外部单独配置索引

            
            builder.Ignore(userbase => userbase.Id);

            base.Configure(builder);
        }

        #endregion
    }
}