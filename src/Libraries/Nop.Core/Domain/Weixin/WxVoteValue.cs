﻿using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 投票
    /// </summary>
    public partial class WxVoteValue : BaseEntity
    {
        /// <summary>
        /// 微信投票人ID
        /// </summary>
        public long OpenIdHash { get; set; }

        /// <summary>
        /// 投票项目表ID
        /// </summary>
        public int VoteId { get; set; }

        /// <summary>
        /// 投票选项值
        /// </summary>
        public int VoteValue { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public int CreatTime { get; set; }



        public virtual WxVote Vote { get; set; }

    }
}