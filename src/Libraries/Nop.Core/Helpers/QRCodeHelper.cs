using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Nop.Core.Helpers
{
    /// <summary>
    /// 描述：条形码和二维码帮助类
    /// 时间：2018-02-18
    /// </summary>
    public class BarcodeHelper
    {
        /// <summary>
        /// Html中图片Src值。
        /// </summary>
        public static string GetBase64Url(string base64Code)
        {
            return string.Format("data:image/png;base64,{0}", base64Code);
        }

        /// <summary>
        /// 返回二维码图片Base64值
        /// </summary>
        /// <param name="content"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public static string CreatQrCodeBase64(string content, int width, int height, int margin = 1)
        {
            if (string.IsNullOrWhiteSpace(content))
                content = "内容不能为空.";

            var writer = new BarcodeWriterPixelData();
            writer.Format = BarcodeFormat.QR_CODE;

            var options = new QrCodeEncodingOptions()
            {
                DisableECI = true,  //设置内容编码
                CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                Width = width,
                Height = height,
                ErrorCorrection = ErrorCorrectionLevel.H,  //容错率:L,M,Q,H
                Margin = margin,  //设置二维码的边距,单位不是固定像素
            };

            writer.Options = options;
            var pixelData = writer.Write(content);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    // save to stream as PNG   
                    bitmap.Save(ms, ImageFormat.Png);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }


        public static string CreatBarCodeBase64(string content, int width, int height, int margin = 1)
        {
            if (string.IsNullOrWhiteSpace(content))
                content = "内容不能为空.";

            var writer = new BarcodeWriterPixelData();

            //规范：1、只支持数字，2、只支持偶数个，3、最大长度80（3个规范还未验证）
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;

            var options = new QrCodeEncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = margin,  //设置条形码的边距,单位不是固定像素
            };

            writer.Options = options;
            var pixelData = writer.Write(content);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    // save to stream as PNG   
                    bitmap.Save(ms, ImageFormat.Png);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }

        }


        ///// <summary>
        ///// 生成带Logo的二维码
        ///// </summary>
        ///// <param name="text">内容</param>
        ///// <param name="width">宽度</param>
        ///// <param name="height">高度</param>
        //public static Bitmap Generate3(string content, int width, int height)
        //{
        //    //Logo 图片
        //    var logoPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\img\logo.png";
        //    var logo = new Bitmap(logoPath);

        //    //配置
        //    var hint = new Dictionary<EncodeHintType, object>();
        //    hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
        //    hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
        //    //hint.Add(EncodeHintType.MARGIN, 2);//旧版本不起作用，需要手动去除白边


        //    //构造二维码写码器 生成二维码 
        //    var bitMatrix = new MultiFormatWriter().encode(content,
        //        BarcodeFormat.QR_CODE,
        //        width + 30,
        //        height + 30,
        //        hint);
        //    bitMatrix = DeleteWhite(bitMatrix);

        //   var writer = new BarcodeWriterPixelData();
        //    var pixelData = writer.Write(bitMatrix);
            
        //    //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
        //    var rectangle = bitMatrix.getEnclosingRectangle();

        //    //计算插入图片的大小和位置
        //    int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
        //    int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
        //    int middleL = (pixelData.Width - middleW) / 2;
        //    int middleT = (pixelData.Height - middleH) / 2;

        //    //将img转换成bmp格式，否则后面无法创建Graphics对象
        //    using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb)) //PixelFormat.Format32bppArgb
        //    {
        //        using (var g = Graphics.FromImage(bitmap))
        //        {
        //            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    
        //            g.DrawImage(pixelData, 0, 0, width, height);
        //            //白底将二维码插入图片
        //            g.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
        //            g.DrawImage(logo, middleL, middleT, middleW, middleH);
        //        }
        //    }

        //    return bmpimg;
        //}

        /// <summary>
        /// 删除默认对应的空白
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private static BitMatrix DeleteWhite(BitMatrix matrix)
        {
            var rec = matrix.getEnclosingRectangle();
            var resWidth = rec[2] + 1;
            var resHeight = rec[3] + 1;

            var resMatrix = new BitMatrix(resWidth, resHeight);
            resMatrix.clear();

            for (var i = 0; i < resWidth; i++)
            {
                for (var j = 0; j < resHeight; j++)
                {
                    if (matrix[i + rec[0], j + rec[1]])
                        resMatrix[i, j] = true;
                }
            }
            return resMatrix;
        }
    }
}
