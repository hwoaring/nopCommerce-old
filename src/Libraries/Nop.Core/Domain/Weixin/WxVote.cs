using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 投票
    /// </summary>
    public partial class WxVote : BaseEntity
    {
        /// <summary>
        /// 投票主题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 频道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 栏目ID（所有数据调用栏目和栏目下的文章值）
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public string ArticleType { get; set; }

        /// <summary>
        /// 外链跳转链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 二维码Ticket值
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 微信返回的二维码图片URL地址
        /// </summary>
        public int QrcodeUrl { get; set; }

        /// <summary>
        /// 微信返回的有效期秒数
        /// </summary>
        public int ExpireSeconds { get; set; }

        /// <summary>
        /// 微信二维码过期时间：创建时间+过期秒数
        /// </summary>
        public int ExpireTime { get; set; }

        /// <summary>
        /// 已经签到人群回复消息类型
        /// </summary>
        public string SignedMessageType { get; set; }

        /// <summary>
        /// 已经签到人群回复消息ID【weixin_message表】
        /// </summary>
        public string SignedMessageId { get; set; }

        /// <summary>
        /// 未经签到人群回复消息类型
        /// </summary>
        public string UnsignMessageType { get; set; }

        /// <summary>
        /// 未经签到人群回复消息ID【weixin_message表】
        /// </summary>
        public string UnsignMessageId { get; set; }

        /// <summary>
        /// 投票首页模板
        /// </summary>
        public string IndexPage { get; set; }

        /// <summary>
        /// 投票页模板
        /// </summary>
        public string ShowPage { get; set; }

        /// <summary>
        /// 投票结果模板
        /// </summary>
        public string DetailPage { get; set; }

        /// <summary>
        /// 投票人群:ALL=所有可以投,SCENE=指定场景值可以投,GROUP=指定分组可投
        /// </summary>
        public string CrowdType { get; set; }
        /// <summary>
        /// 人群值:all时该项无效，scenic时多场景值用逗号分开,group时调用微信用户分组
        /// </summary>
        public string CrowdValue { get; set; }

        /// <summary>
        /// 周期类型:值为HOUR，DAY，WEEK，MONTH，YEAR
        /// </summary>
        public string CycleType { get; set; }

        /// <summary>
        /// 周期值
        /// </summary>
        public int CycleValue { get; set; }

        /// <summary>
        /// 投票基数：总投票数=基数+实际投票数
        /// </summary>
        public int BaseNumber { get; set; }

        /// <summary>
        /// 次数限制:每个周期次数限制（大于0的数字）
        /// </summary>
        public int FrequencyLimit { get; set; }

        /// <summary>
        /// 投票开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 投票结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 投票开始（人工控制投票有效期）,时间未到以该值优先
        /// </summary>
        public bool VoteStart { get; set; }

        /// <summary>
        /// 投票结束（人工控制投票有效期）,时间未到以该值优先
        /// </summary>
        public bool VoteEnd { get; set; }

        /// <summary>
        /// 签到开关（将签到人统计到VoteUser表中）
        /// </summary>
        public bool SignOn { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

    }
}