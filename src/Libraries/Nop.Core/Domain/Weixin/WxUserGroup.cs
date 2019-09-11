using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 系统用户分组
    /// </summary>
    public partial class WxUserGroup : BaseEntity
    {
        private ICollection<WxUserInfo> _userInfos;

        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分组描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }



        public virtual ICollection<WxUserInfo> UserInfos
        {
            get => _userInfos ?? (_userInfos = new List<WxUserInfo>());
            protected set => _userInfos = value;
        }
    }
}