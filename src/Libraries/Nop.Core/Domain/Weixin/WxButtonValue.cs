using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信菜单单个按钮信息
    /// </summary>
    public partial class WxButtonValue : BaseEntity
    {
        private ICollection<WxButton> _buttons;

        /// <summary>
        /// 按钮标题（不是显示在菜单上的标题)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 按钮描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 必须：是，菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 必须：是 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// click等点击类型必须 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// view、miniprogram类型必须 网页链接，用户点击菜单可打开链接，不超过1024字节。当type为miniprogram时，不支持小程序的老版本客户端将打开本url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// media_id类型和view_limited类型必须 调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// miniprogram类型必须 小程序的appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// miniprogram类型必须 小程序的页面路径
        /// </summary>
        public string Pagepath { get; set; }


        /// <summary>
        /// 是否授权，空=不授权，snsapi_base，snsapi_userinfo，user_login
        /// </summary>
        public string OauthType { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public string ResponseType { get; set; }
        /// <summary>
        /// 响应消息ids
        /// </summary>
        public string ResponseMessageIds { get; set; }
        /// <summary>
        /// 响应内容或ID
        /// </summary>
        public string ResponseContent { get; set; }
        /// <summary>
        /// 默认响应类型
        /// </summary>
        public string DefaultType { get; set; }
        /// <summary>
        /// 响应消息ids
        /// </summary>
        public string DefaultMessageIds { get; set; }
        /// <summary>
        /// 默认相应内容或ID
        /// </summary>
        public string DefaultContent { get; set; }
        /// <summary>
        /// 响应消息ids
        /// </summary>
        public string NullMessageIds { get; set; }
        /// <summary>
        /// 空值相应内容，或ID
        /// </summary>
        public string NullContent { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<WxButton> Buttons
        {
            get => _buttons ?? (_buttons = new List<WxButton>());
            protected set => _buttons = value;
        }
    }
}