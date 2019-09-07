using System;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;

namespace Nop.Core.Domain.News
{
    /// <summary>
    /// Represents a news item
    /// </summary>
    public partial class NewsItem : BaseEntity, ISlugSupported, IStoreMappingSupported
    {
        private ICollection<NewsComment> _newsComments;

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the news title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the short text
        /// </summary>
        public string Short { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文章来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 封面图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// URL链接,填写后直接跳转到该网址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 原文地址
        /// </summary>
        public string OriginalUrl { get; set; }

        /// <summary>
        /// Tags标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the full text
        /// </summary>
        public string Full { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the news item is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// 是否幻灯片
        /// </summary>
        public bool IsSlide { get; set; }

        /// <summary>
        /// 是否图片新闻
        /// </summary>
        public bool IsPicture { get; set; }

        /// <summary>
        /// 是否微信格式文章
        /// </summary>
        public bool IsWeixin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the news post comments are allowed 
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the news item start date and time
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the news item end date and time
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 指定模板页名称
        /// </summary>
        public string TemplatePage { get; set; }

        /// <summary>
        /// 点击数
        /// </summary>
        public int Click { get; set; }

        /// <summary>
        /// 需要扣除积分，大于零扣除，小于零增加
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the news comments
        /// </summary>
        public virtual ICollection<NewsComment> NewsComments
        {
            get => _newsComments ?? (_newsComments = new List<NewsComment>());
            protected set => _newsComments = value;
        }
        
        /// <summary>
        /// Gets or sets the language
        /// </summary>
        public virtual Language Language { get; set; }
    }
}