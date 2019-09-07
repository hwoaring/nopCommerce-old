using System;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;

namespace Nop.Core.Domain.News
{
    /// <summary>
    /// Represents a news comment
    /// </summary>
    public partial class NewsCommentVote : BaseEntity
    {
        public int NewsCommentId { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}