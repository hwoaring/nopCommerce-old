using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信菜单信息
    /// </summary>
    public partial class WxMenu : BaseEntity
    {
        private ICollection<WxButton> _buttons;


        /// <summary>
        /// 个性化菜单标题
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 微信返回的 "menuid": 208396993
        /// </summary>
        public long? ResponseMenuId { get; set; }

        /// <summary>
        /// 规则ID,0表示不使用个性化设置
        /// </summary>
        public int? MatchRuleId { get; set; }

        /// <summary>
        /// 更新时间或发布时间
        /// </summary>
        public int? UpdateTime { get; set; }

        /// <summary>
        /// 是否启用自定义
        /// </summary>
        public bool UseMatchRule { get; set; }
        /// <summary>
        /// 是否已发布
        /// </summary>
        public bool Published { get; set; }
        /// <summary>
        /// 菜单中必须一个缺省菜单发布，缺省菜单将忽略个性化设置
        /// </summary>
        public bool IsDefault { get; set; }
        




        /// <summary>
        /// 规则ID,0表示不使用个性化设置
        /// </summary>
        public virtual WxMatchRule MatchRule { get; set; }

        /// <summary>
        /// Button信息集ID
        /// </summary>
        public virtual ICollection<WxButton> Buttons
        {
            get => _buttons ?? (_buttons = new List<WxButton>());
            protected set => _buttons = value;
        }

    }
}