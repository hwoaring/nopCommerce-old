using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nop.Core.Helpers
{
    /// <summary>
    /// 图片服务类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 将图片Image转换成Byte[]
        /// </summary>
        /// <param name="Image">image对象</param>
        /// <param name="imageFormat">后缀名</param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image, ImageFormat imageFormat)
        {

            if (image == null)
                return null;

            using (var ms = new MemoryStream())
            {
                using (var bitmap = new Bitmap(image))
                {
                    bitmap.Save(ms, imageFormat);
                    ms.Position = 0;

                    var data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();

                    return data;
                }

            }
        }

        /// <summary>
        /// Bitmap转byte[]  
        /// </summary>
        /// <param name="Bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            if (bitmap == null)
                return null;

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);

                var byteImage = new byte[ms.Length];
                byteImage = ms.ToArray();

                return byteImage;
            }
        }

        /// <summary>
        /// byte[]转换成Image
        /// </summary>
        /// <param name="byteArray">二进制图片流</param>
        /// <returns>Image</returns>
        public static Image BytesToImage(byte[] bytes)
        {
            if (bytes == null)
                return null;

            using (var ms = new MemoryStream(bytes))
            {
                var returnImage = Image.FromStream(ms);
                ms.Flush();

                return returnImage;
            }
        }

        /// <summary>
        /// byte[] 转换 Bitmap
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static Bitmap BytesToBitmap(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (var ms = new MemoryStream(bytes))
            {
                //return new Bitmap((Image)new Bitmap(ms));
                return new Bitmap(ms);

            }
        }

        /// <summary>
        /// 将图片数据转换为Base64字符串
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageToBase64(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// 将Base64字符串转换为图片
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return null;

            var data = string.Empty;

            if (base64String.StartsWith("data:image"))
                data = base64String.Substring(base64String.IndexOf(',') + 1);
            else
                data = base64String;

            using (var stream = new MemoryStream(Convert.FromBase64String(data)))
            {
                return new Bitmap(stream) as Image;
            }
        }

        public static byte[] Base64ToBytes(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return null;

            var data = string.Empty;

            if (base64String.StartsWith("data:image"))
                data = base64String.Substring(base64String.IndexOf(',') + 1);
            else
                data = base64String;

            return Convert.FromBase64String(data);
        }

        public static void SaveImage(Image image, string savePath, bool dispose = true)
        {
            ImageFormat imageFormat;
            switch (Path.GetExtension(savePath).ToUpper())
            {
                case ".PNG":
                    imageFormat = ImageFormat.Png;
                    break;
                case ".ICO":
                    imageFormat = ImageFormat.Icon;
                    break;
                case ".GIF":
                    imageFormat = ImageFormat.Gif;
                    break;
                default:
                    imageFormat = ImageFormat.Jpeg;
                    break;
            }

            var fileStr = savePath.Substring(0, savePath.LastIndexOf("\\"));
            if (!Directory.Exists(fileStr))
                Directory.CreateDirectory(fileStr);

            image.Save(savePath, imageFormat);

            if (dispose)
                image.Dispose();
        }

        /// <summary>
        /// 下载远程图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] Download(string url)
        {
            var wc = new WebClient();

            //return new MemoryStream(wc.DownloadData(url));
            return wc.DownloadData(url);
        }

        /// <summary>
        /// 获取网络图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static Stream GetRemoteImage(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentLength = 0;
            request.Timeout = 20000;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                return response.GetResponseStream();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageString">Base64或http地址或本地图片地址</param>
        /// <param name="base64Code"></param>
        /// <returns></returns>
        public static byte[] GetImageByteFromString(string imageString, bool base64Code = false)
        {
            if (string.IsNullOrWhiteSpace(imageString))
                return null;

            if (base64Code)
            {
                return Base64ToBytes(imageString);
            }

            if (imageString.StartsWith("http:") || imageString.StartsWith("https:"))  //网络图片
            {
               return Download(imageString);
            }
            else
            {
                //本地图片
                if (File.Exists(imageString))
                    return File.ReadAllBytes(imageString);
            }
            return null;
        }

        public static Image GetImageFromString(string imageString, bool base64Code = false)
        {
            if (string.IsNullOrWhiteSpace(imageString))
                return null;

            if (base64Code)
            {
                return Base64ToImage(imageString);   //base64值
            }

            if (imageString.StartsWith("http:") || imageString.StartsWith("https:"))  //网络图片
            {
                var stream = Download(imageString);
                if (stream != null)
                    return BytesToBitmap(stream);
            }
            else
            {
                //本地图片
                if (File.Exists(imageString))
                    return Image.FromFile(imageString);
            }
            return null;
        }

        public static Image Combin(string frontImage, string backImage, int xPosition = 0, int yPosition = 0, int size = 0, int posionModel = 1, string newPath = "")
        {
            return Combin(GetImageFromString(frontImage), GetImageFromString(backImage), xPosition, yPosition, size, posionModel, newPath);
        }

        public static Image Combin(string frontImage, Image backImage, int xPosition = 0, int yPosition = 0, int size = 0, int posionModel = 1, string newPath = "")
        {
            return Combin(GetImageFromString(frontImage), backImage, xPosition, yPosition, size, posionModel, newPath);
        }


        public static Image Combin(Image frontImage, string backImage, int xPosition = 0, int yPosition = 0, int size = 0, int posionModel = 1, string newPath = "")
        {
            return Combin(frontImage, GetImageFromString(backImage), xPosition, yPosition, size, posionModel, newPath);
        }

        /// <summary>
        /// 图片合并
        /// </summary>
        /// <param name="frontImage">前景图</param>
        /// <param name="backImage">背景图</param>
        /// <param name="xPosition">前景图x位置偏移量</param>
        /// <param name="yPosition">前景图y位置偏移量</param>
        /// <param name="size">前景图尺寸，0=不缩放</param>
        /// <param name="posionModel">1-9数值，表示9宫格位置，1=左上</param>
        /// <param name="newPath">新图片存储位置，为空表示不存储在本地</param>
        public static Image Combin(Image frontImage, Image backImage, int xPosition = 0, int yPosition = 0, int size = 0, int posionModel = 1, string newPath = "")
        {
            if (frontImage == null && backImage == null)//返回空
                return null;

            if (frontImage != null && backImage == null)//返回前景图
                return frontImage;

            if (frontImage == null && backImage != null)//返回背景图
                return backImage;

            //按照指定宽进行前景图缩放(size=宽=高，这里图片缩放只针对正方形)
            var maxSize = backImage.Width > backImage.Height ? backImage.Height : backImage.Width;
            if (size > maxSize)
                size = maxSize;

            //前景图缩放
            if (size > 0 && size != frontImage.Size.Width)
                ResizeImage(frontImage, size, size, 0);

            //计算前景图位置
            int x = 0;
            int y = 0;

            switch (posionModel)
            {
                case 1:
                    x = (int)((backImage.Width * (float).01) + xPosition);
                    y = (int)((backImage.Height * (float).01) + yPosition);
                    break;
                case 2:
                    x = (int)((backImage.Width * (float).50) - (frontImage.Width / 2) + xPosition);
                    y = (int)((backImage.Height * (float).01) + yPosition);
                    break;
                case 3:
                    x = (int)((backImage.Width * (float).99) - (frontImage.Width) + xPosition);
                    y = (int)((backImage.Height * (float).01) + yPosition);
                    break;
                case 4:
                    x = (int)((backImage.Width * (float).01) + xPosition);
                    y = (int)((backImage.Height * (float).50) - (frontImage.Height / 2) + yPosition);
                    break;
                case 5:
                    x = (int)((backImage.Width * (float).50) - (frontImage.Width / 2) + xPosition);
                    y = (int)((backImage.Height * (float).50) - (frontImage.Height / 2) + yPosition);
                    break;
                case 6:
                    x = (int)((backImage.Width * (float).99) - (frontImage.Width) + xPosition);
                    y = (int)((backImage.Height * (float).50) - (frontImage.Height / 2) + yPosition);
                    break;
                case 7:
                    x = (int)((backImage.Width * (float).01) + xPosition);
                    y = (int)((backImage.Height * (float).99) - frontImage.Height + yPosition);
                    break;
                case 8:
                    x = (int)((backImage.Width * (float).50) - (frontImage.Width / 2) + xPosition);
                    y = (int)((backImage.Height * (float).99) - frontImage.Height + yPosition);
                    break;
                case 9:
                    x = (int)((backImage.Width * (float).99) - (frontImage.Width) + xPosition);
                    y = (int)((backImage.Height * (float).99) - frontImage.Height + yPosition);
                    break;
            }

            //开始合并
            using (var g = Graphics.FromImage(backImage))
            {
                //设置画布的描绘质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //背景画布
                g.DrawImage(backImage, 0, 0, backImage.Width, backImage.Height);

                //前景画布
                g.DrawImage(frontImage, x, y, frontImage.Width, frontImage.Height);

                //新路径不为空，保存图片
                if (!string.IsNullOrEmpty(newPath))
                {
                    SaveImage(backImage, newPath, false);  //这里不释放，后面统一释放
                }
            }

            frontImage.Dispose();

            return backImage;
        }


        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="imageSource">原始Bitmap</param>
        /// <param name="newWidth">新的宽度</param>
        /// <param name="newHight">新的高度</param>
        /// <param name="mode">保留着，暂时未用</param>
        /// <returns>处理以后的图片</returns>
        public static void ResizeImage(Image imageSource, int newWidth, int newHight, int mode)
        {
            using (var bitmap = new Bitmap(newWidth, newHight))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    //设置画布的描绘质量
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //清空画布并以透明背景色填充
                    g.Clear(Color.Transparent);
                    g.DrawImage(imageSource, new Rectangle(0, 0, newWidth, newHight), new Rectangle(0, 0, imageSource.Width, imageSource.Height), GraphicsUnit.Pixel);

                    imageSource = bitmap;
                }
            } 
        }


        //返回图片解码器信息用于jpg图片
        private ImageCodecInfo GetCodecInfo(string str)
        {
            var ext = str.Substring(str.LastIndexOf(".") + 1);
            var mimeType = string.Empty;
            switch (ext.ToUpper())
            {
                case "JPE":
                case "JPG":
                case "JPEG":
                    mimeType = "image/jpeg";
                    break;
                case "BMP":
                    mimeType = "image/bmp";
                    break;
                case "PNG":
                    mimeType = "image/png";
                    break;
                case "TIF":
                case "TIFF":
                    mimeType = "image/tiff";
                    break;
                default:
                    mimeType = "image/jpeg";
                    break;
            }
            var codeInfo = ImageCodecInfo.GetImageEncoders();
            foreach (var ici in codeInfo)
            {
                if (ici.MimeType == mimeType)
                    return ici;
            }
            return null;
        }
    }
}
