using System;
using Nop.Core;
using Nop.Core.Domain.Weixin;

namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrGroup service interface
    /// </summary>
    public partial interface IWxQrGroupService
    {
        WxQrGroup GetWxQrGroupById(int wxQrGroupId);

        void InsertWxQrGroup(WxQrGroup wxQrGroup);


        void UpdateWxQrGroup(WxQrGroup wxQrGroup);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrGroup"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        void DeleteWxQrGroup(WxQrGroup wxQrGroup, bool fromDB = false);


        IPagedList<WxQrGroup> GetWxQrGroups(
                    int pageIndex = 0, int pageSize = int.MaxValue);
    }
}