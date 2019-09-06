using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class WxUserInfoBase : BaseEntity
    {
        private ICollection<WxUserInfoBase> _childUserInfoBases;

        /// <summary>
        /// 用户ID Hash
        /// </summary>
        public long OpenIdHash { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 推荐人ID Hash
        /// </summary>
        public long? OpenIdReferer { get; set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool Subscribe { get; set; }
        /// <summary>
        /// 是否允许推荐用户，不推荐时，该用户无法执行正常推荐的后续操作
        /// </summary>
        public bool AllowReferer { get; set; }
        /// <summary>
        /// 是否允许和平台交互信息，不交互时，全部返回success
        /// </summary>
        public bool AllowRequest { get; set; }
        /// <summary>
        /// 是否允许下单
        /// </summary>
        public bool AllowOrder { get; set; }
        /// <summary>
        /// 是否接受被推荐人的订单提示信息
        /// </summary>
        public bool AllowNotice { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 来源场景类型：0=默认，11=ADVER 广告，22=VERIFY 验证码，33=CMD 命令行，44=LOGIN 登录，55=BIND 绑定，66=VOTE 投票，77=CARD 个人名片
        /// </summary>
        public byte SceneTypeId { get; set; }
        /// <summary>
        /// 自定义角色类型分组，默认0,如：0常规用户，1销售员，2商家
        /// </summary>
        public byte RoleTypeId { get; set; }
        /// <summary>
        /// 用户状态，可用于管理是否屏蔽用户请求信息
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string UnionId { get; set; }
        /// <summary>
        /// 用户来源场景ID（永久或临时，默认0）,存储永久二维码ID或临时二维码广告图ID(SceneTypeId=0是永久二维码，大于0是临时二维码广告图)
        /// </summary>
        public int SceneId { get; set; }
        /// <summary>
        /// 最后活跃时间
        /// </summary>
        public int? LastActiveTime { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public int? SubscribeTime { get; set; }

        /// <summary>
        /// 取消关注时间
        /// </summary>
        public int? UnSubscribeTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreatTime { get; set; }

        /// <summary>
        /// 名下被自己推荐的人名单
        /// </summary>
        public virtual ICollection<WxUserInfoBase> ChildUserInfoBases
        {
            get => _childUserInfoBases ?? (_childUserInfoBases = new List<WxUserInfoBase>());
            protected set => _childUserInfoBases = value;
        }

        /// <summary>
        /// WxUserInfo中定义1v1
        /// </summary>
        public virtual WxUserInfo UserInfo { get; set; }
        /// <summary>
        /// Customer中定义了1v1
        /// </summary>
        public virtual Customers.Customer Customer { get; set; }
    }
}