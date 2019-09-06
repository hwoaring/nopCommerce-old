using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Nop.Core.Helpers
{
    /// <summary>
    /// 二维码生成助手
    /// </summary>
    public class QRCodeHelperTemp
    {
        ///// <summary>
        ///// 二维码解码
        ///// </summary>
        ///// <param name="img_url">图片地址</param>
        ///// <returns>解码结果</returns>
        //public static string DecodeImage(string img_url)
        //{
        //    QRCodeDecoder decoder = new QRCodeDecoder();
        //    return decoder.decode(new QRCodeBitmapImage(new Bitmap(img_url)));
        //}
        ///// <summary>
        ///// 生成二维码内存流
        ///// </summary>
        ///// <param name="content">要生成二维码内容（暂不支持中文内容）</param>
        ///// <param name="logoUrl">二维码中间logo图</param>
        ///// <param name="scale">比例：如 4</param>
        ///// <param name="version">版本默认0(自动)，当字符串长度不固定时推荐用0</param>
        ///// <param name="errorCorrect">容错率:L,M,Q,H</param>
        ///// <param name="encoding">编码：AlphaNumeric，Numeric，Byte</param>
        ///// <returns></returns>
        //public static MemoryStream GetImage(string content, string logoUrl, int scale, int version, string errorCorrect, string encoding)
        //{

        //    System.IO.MemoryStream MStream1 = new System.IO.MemoryStream();

        //    if (string.IsNullOrEmpty(content))
        //        content = "内容不能为空.";

        //    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

        //    switch (encoding.ToUpper())
        //    {
        //        case "ALPHANUMERIC":
        //            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
        //            break;
        //        case "NUMERIC":
        //            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
        //            break;
        //        case "BYTE":
        //            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        //            break;
        //        default:
        //            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        //            break;
        //    }

        //    switch (errorCorrect.ToUpper())
        //    {
        //        case "L":
        //            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
        //            break;
        //        case "M":
        //            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        //            break;
        //        case "Q":
        //            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
        //            break;
        //        case "H":
        //            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
        //            break;
        //        default:
        //            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        //            break;
        //    }

        //    qrCodeEncoder.QRCodeScale = scale < 1 ? 4 : scale;         //4
        //    qrCodeEncoder.QRCodeVersion = version < 0 ? 0 : version;   //8

        //    System.Drawing.Image image = qrCodeEncoder.Encode(content, Encoding.UTF8);
        //    System.IO.MemoryStream MStream = new System.IO.MemoryStream();
        //    image.Save(MStream, System.Drawing.Imaging.ImageFormat.Png);     //二维码原图
        //    if (!string.IsNullOrEmpty(logoUrl))
        //    {
        //        CombinImage(image, logoUrl).Save(MStream1, System.Drawing.Imaging.ImageFormat.Png);  //加了背景的图
        //    }
        //    else
        //    {
        //        MStream1 = MStream;
        //    }

        //    return MStream1;
        //}

        ///// <summary>
        ///// 调用此函数后使此两种图片合并，类似相册，有个背景图，中间贴自己的目标图片
        ///// </summary>
        ///// <param name="imgBack">粘贴的源图片地址</param>
        ///// <param name="destImg">粘贴的目标图片地址</param>
        ///// <returns></returns>
        //public static Image CombinImage(string imgBack, string destImg)
        //{
        //    return CombinImage(Image.FromFile(imgBack), destImg);
        //}

        ///// <summary>
        ///// 调用此函数后使此两种图片合并，类似相册，有个背景图，中间贴自己的目标图片
        ///// </summary>
        ///// <param name="imgBack">粘贴的源图片</param>
        ///// <param name="destImg">粘贴的目标图片</param>
        ///// <returns></returns>
        //public static Image CombinImage(Image imgBack, string destImg)
        //{
        //    Image img = Image.FromFile(destImg); //照片图片

        //    if (img == null)
        //    {
        //        return imgBack;
        //    }

        //    if (img.Height != 65 || img.Width != 65)
        //    {
        //        img = ImageHelper.ResizeImage(img, 65, 65, 0);
        //    }


        //    Graphics g = Graphics.FromImage(imgBack);
        //    g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
        //    //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);
        //    //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框
        //    //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
        //    g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
        //    //GC.Collect();
        //    return imgBack;
        //}




    }
}
