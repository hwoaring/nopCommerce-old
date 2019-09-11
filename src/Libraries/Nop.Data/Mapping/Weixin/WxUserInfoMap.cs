using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxUserInfoMap : NopEntityTypeConfiguration<WxUserInfo>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxUserInfo> builder)
        {
            builder.ToTable(nameof(WxUserInfo));
            builder.HasKey(userinfo => userinfo.OpenIdHash);
            builder.Property(userinfo => userinfo.OpenIdHash).ValueGeneratedNever().IsRequired();

            builder.Property(userinfo => userinfo.NickName).HasMaxLength(64);
            builder.Property(userinfo => userinfo.Sex).HasDefaultValue(0);
            builder.Property(userinfo => userinfo.LanguageTypeId).HasDefaultValue(1);
            builder.Property(userinfo => userinfo.City).HasMaxLength(32);
            builder.Property(userinfo => userinfo.Province).HasMaxLength(32);
            builder.Property(userinfo => userinfo.Country).HasMaxLength(32);
            builder.Property(userinfo => userinfo.Headimgurl).HasMaxLength(255).IsUnicode(false);
            builder.Property(userinfo => userinfo.Remark).HasMaxLength(64);
            builder.Property(userinfo => userinfo.RemarkSystem).HasMaxLength(255);
            builder.Property(userinfo => userinfo.TagIdList).HasMaxLength(255).IsUnicode(false);
            builder.Property(userinfo => userinfo.QrScene).HasMaxLength(255).IsUnicode(false);
            builder.Property(userinfo => userinfo.QrSceneStr).HasMaxLength(255).IsUnicode(false);

            builder.HasOne(userinfo => userinfo.UserGroup)
                .WithMany(usergroup => usergroup.UserInfos)
                .HasForeignKey(userinfo => userinfo.UserGroupId);

            builder.HasOne(userinfo => userinfo.UserInfoBase)
                .WithOne(userbase => userbase.UserInfo)
                .HasForeignKey<WxUserInfo>(userinfo => userinfo.OpenIdHash)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(userinfo => userinfo.LanguageType);
            builder.Ignore(userbase => userbase.SubscribeSceneType);
            builder.Ignore(userinfo => userinfo.Id);

            base.Configure(builder);
        }

        #endregion
    }
}