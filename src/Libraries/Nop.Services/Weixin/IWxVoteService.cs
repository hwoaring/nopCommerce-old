using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxVoteService service interface
    /// </summary>
    public partial interface IWxVoteService
    {
        WxVote GetWxVoteById(int id);

        void InsertWxVote(WxVote wxVote);

        void UpdateWxVote(WxVote wxVote);

        void UpdateWxVotes(IList<WxVote> wxVotes);

        void DeleteWxVote(WxVote wxVote, bool fromDB = false);

        void DeleteWxVotes(IList<WxVote> wxVotes, bool fromDB = false);

        IList<WxVote> GetWxVotesByIds(int[] ids);

        IPagedList<WxVote> GetWxVotes(
                    int pageIndex = 0, int pageSize = int.MaxValue);
    }
}