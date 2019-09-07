﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Weixin;

namespace Nop.Data.Mapping.Weixin
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class WxMsgMenuMap : NopEntityTypeConfiguration<WxMsgMenu>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<WxMsgMenu> builder)
        {
            builder.ToTable(nameof(WxMsgMenu));
            builder.HasKey(menu => menu.Id);

            builder.Property(menu => menu.Title).HasMaxLength(50).IsRequired();
            builder.Property(menu => menu.HeadContent).HasMaxLength(255).IsRequired();
            builder.Property(menu => menu.TailContent).HasMaxLength(255).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}