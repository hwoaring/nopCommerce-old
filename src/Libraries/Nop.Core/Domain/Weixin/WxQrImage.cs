using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Weixin
{
    /// <summary>
    /// 微信二维码-图片合成宣传广告原图表
    /// </summary>
    public partial class WxQrImage : BaseEntity
    {
        private IList<WxMessage> _messages;

        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片底图链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 原始图片链接地址（用于打印）
        /// </summary>
        public string UrlOriginal { get; set; }
        /// <summary>
        /// 二维码类型：1=subscribe=扫码关注，2=url=url形式二维码
        /// </summary>
        public byte QrcodeTypeId { get; set; }
        /// <summary>
        /// 二维码合成x坐标
        /// </summary>
        public int Xposition { get; set; }
        /// <summary>
        /// 二维码合成y坐标
        /// </summary>
        public int Yposition { get; set; }
        /// <summary>
        /// 对齐类型：1左上，2上中，3右上，4左中，5居中，6右中
        /// 7左下，8下中，9右下
        /// </summary>
        public byte AlignmentType { get; set; }
        /// <summary>
        /// 二维码图片尺寸（宽=高，只设置一个size）
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 图片宣传的产品ID（如果有）
        /// </summary>
        public int? ProductId { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public byte MessageTypeId { get; set; }
        /// <summary>
        /// WxMessage表ID，以逗号分开。第一条作为自动回复，如果多余1条，就采用客服消息回复,为空，就检查Content内容以文本形式回复
        /// </summary>
        public string MessageIds { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图文消息封面图url
        /// </summary>
        public string CoverUrl { get; set; }
        /// <summary>
        /// 图文消息点击跳转url
        /// </summary>
        public string ContentUrl { get; set; }
        /// <summary>
        /// 用户被自动打上的标签ID列表
        /// </summary>
        public string TagIds { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreatTime { get; set; }

        /// <summary>
        /// 内容是否使用消息ID
        /// </summary>
        public bool UseMessageId { get; set; }
        /// <summary>
        /// 是否显示封面
        /// </summary>
        public bool ShowCover { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool Published { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }
        

        /// <summary>
        /// 
        /// </summary>
        public WxQrImageType QrImageType
        {
            get => (WxQrImageType)QrcodeTypeId;
            set => QrcodeTypeId = (byte)value;
        }

        public MessageType MessageType
        {
            get => (MessageType)MessageTypeId;
            set => MessageTypeId = (byte)value;
        }

        public IList<WxMessage> Messages
        {
            get => _messages ?? (_messages = new List<WxMessage>());
            protected set => _messages = value;
        }


        public virtual Catalog.Product Product { get; set; }


    }
}