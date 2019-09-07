using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxVoteUserService service interface
    /// </summary>
    public partial interface IWxVoteUserService
    {

        void InsertWxVoteUser(WxVoteUser wxVoteUser);

        void UpdateWxVoteUser(WxVoteUser wxVoteUser);

        void DeleteWxVoteUser(WxVoteUser wxVoteUser);

        void DeleteWxVoteUser(IList<WxVoteUser> wxVoteUsers);

        WxVoteUser GetWxVoteUserByOpenIdHashAndVoteId(long openIdHash, int voteId);

        IList<WxVoteUser> GetWxVoteUsersByOpenIdHash(long openIdHash);

        IList<WxVoteUser> GetWxVoteUsersByVoteId(int voteId);

        IPagedList<WxVoteUser> GetWxVoteUsers(
                int pageIndex = 0, int pageSize = int.MaxValue);
    }
}