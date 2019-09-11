using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxVoteValueService service interface
    /// </summary>
    public partial interface IWxVoteValueService
    {
        WxVoteValue GetWxVoteValueById(int id);

        void InsertWxVoteValue(WxVoteValue wxVoteValue);

        void UpdateWxVoteValue(WxVoteValue wxVoteValue);

        void DeleteWxVoteValue(WxVoteValue wxVoteValue);

        void DeleteWxVoteValues(IList<WxVoteValue> wxVoteValues);

        IList<WxVoteValue> GetWxVoteValuesByOpenIdHash(long openIdHash);

        IList<WxVoteValue> GetWxVoteValuesByVoteId(int voteId);

        IPagedList<WxVoteValue> GetWxVoteValues(
             int pageIndex = 0, int pageSize = int.MaxValue);
    }
}