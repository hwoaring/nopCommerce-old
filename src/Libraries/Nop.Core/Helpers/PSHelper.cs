using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Nop.Core.Helpers
{
    /// <summary>
    /// PS 处理效果
    /// </summary>
    public class PSHelper
    {
        /// <summary>
        /// 底片效果
        /// </summary>
        /// <param name="_image"></param>
        public Image FilmeEffect(Image _image)
        {
            try
            {
                var height = _image.Height;
                var width = _image.Width;
                var newbitmap = new Bitmap(width, height);
                var oldbitmap = (Bitmap)_image;
                Color pixel;
                for (var x = 1; x < width; x++)
                {
                    for (var y = 1; y < height; y++)
                    {
                        int r, g, b;
                        pixel = oldbitmap.GetPixel(x, y);
                        r = 255 - pixel.R;
                        g = 255 - pixel.G;
                        b = 255 - pixel.B;
                        newbitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                return newbitmap;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 浮雕效果
        /// </summary>
        /// <param name="_image"></param>
        /// <returns></returns>
        public Image RelievoEffect(Image _image)
        {
            try
            {
                var height = _image.Height;
                var width = _image.Width;
                var newBitmap = new Bitmap(width, height);
                var oldBitmap = (Bitmap)_image;
                Color pixel1, pixel2;
                for (int x = 0; x < width - 1; x++)
                {
                    for (int y = 0; y < height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0;
                        pixel1 = oldBitmap.GetPixel(x, y);
                        pixel2 = oldBitmap.GetPixel(x + 1, y + 1);
                        r = Math.Abs(pixel1.R - pixel2.R + 128);
                        g = Math.Abs(pixel1.G - pixel2.G + 128);
                        b = Math.Abs(pixel1.B - pixel2.B + 128);
                        if (r > 255)
                            r = 255;
                        if (r < 0)
                            r = 0;
                        if (g > 255)
                            g = 255;
                        if (g < 0)
                            g = 0;
                        if (b > 255)
                            b = 255;
                        if (b < 0)
                            b = 0;
                        newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
                return newBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 黑白效果
        /// </summary>
        /// <param name="_image"></param>
        /// <returns></returns>
        public Image BlackWhiteEffect(Image _image)
        {
            return BlackWhiteEffect(_image, 2);
        }

        /// <summary>
        /// 黑白效果
        /// </summary>
        /// <param name="_image"></param>
        /// <returns></returns>
        public Image BlackWhiteEffect(Image _image, int type)
        {
            try
            {
                var height = _image.Height;
                var width = _image.Width;
                var newBitmap = new Bitmap(width, height);
                var oldBitmap = (Bitmap)_image;
                Color pixel;
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        pixel = oldBitmap.GetPixel(x, y);
                        int r, g, b, result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        //实例程序以加权平均值法产生黑白图像
                        switch (type)
                        {
                            case 0://平均值法
                                result = ((r + g + b) / 3);
                                break;
                            case 1://最大值法
                                result = r > g ? r : g;
                                result = result > b ? result : b;
                                break;
                            case 2://加权平均值法
                                result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                break;
                        }
                        newBitmap.SetPixel(x, y, Color.FromArgb(result, result, result));
                    }
                return newBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 模糊效果
        /// </summary>
        /// <param name="_image"></param>
        /// <returns></returns>
        public Image BlurEffect(Image _image)
        {
            try
            {
                var height = _image.Height;
                var width = _image.Width;
                var newBitmap = new Bitmap(width, height);
                var oldBitmap = (Bitmap)_image;
                Color pixel;
                //高斯模板
                int[] Gauss = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
                for (var x = 1; x < width - 1; x++)
                {
                    for (var y = 1; y < height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0;
                        var index = 0;
                        for (var col = -1; col <= 1; col++)
                        {
                            for (var row = -1; row <= 1; row++)
                            {
                                pixel = oldBitmap.GetPixel(x + row, y + col);
                                r += pixel.R * Gauss[index];
                                g += pixel.G * Gauss[index];
                                b += pixel.B * Gauss[index];
                                index++;
                            }
                        }
                        r /= 16;
                        g /= 16;
                        b /= 16;
                        //处理颜色值溢出
                        r = r > 255 ? 255 : r;
                        r = r < 0 ? 0 : r;
                        g = g > 255 ? 255 : g;
                        g = g < 0 ? 0 : g;
                        b = b > 255 ? 255 : b;
                        b = b < 0 ? 0 : b;
                        newBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                    }
                }
                return newBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 锐化效果
        /// </summary>
        /// <param name="_image"></param>
        /// <returns></returns>
        public Image SharpenEffect(Image _image)
        {
            try
            {
                var height = _image.Height;
                var width = _image.Width;
                var newBitmap = new Bitmap(width, height);
                var oldBitmap = (Bitmap)_image;
                Color pixel;
                //拉普拉斯模板
                int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
                for (var x = 1; x < width - 1; x++)
                {
                    for (var y = 1; y < height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0;
                        int index = 0;
                        for (var col = -1; col <= 1; col++)
                        {
                            for (var row = -1; row <= 1; row++)
                            {
                                pixel = oldBitmap.GetPixel(x + row, y + col);
                                r += pixel.R * Laplacian[index];
                                g += pixel.G * Laplacian[index];
                                b += pixel.B * Laplacian[index];
                                index++;
                            }
                        }
                        //处理颜色值溢出
                        r = r > 255 ? 255 : r;
                        r = r < 0 ? 0 : r;
                        g = g > 255 ? 255 : g;
                        g = g < 0 ? 0 : g;
                        b = b > 255 ? 255 : b;
                        b = b < 0 ? 0 : b;
                        newBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                    }
                }
                return newBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 雾化效果
        /// </summary>
        /// <param name="_image"></param>
        /// <returns></returns>
        public Image AtomizeEffect(Image _image)
        {
            try
            {
                int Height = _image.Height;
                int Width = _image.Width;
                Bitmap newBitmap = new Bitmap(Width, Height);
                Bitmap oldBitmap = (Bitmap)_image;
                Color pixel;
                for (int x = 1; x < Width - 1; x++)
                    for (int y = 1; y < Height - 1; y++)
                    {
                        System.Random MyRandom = new Random();
                        int k = MyRandom.Next(123456);
                        //像素块大小
                        int dx = x + k % 19;
                        int dy = y + k % 19;
                        if (dx >= Width)
                            dx = Width - 1;
                        if (dy >= Height)
                            dy = Height - 1;
                        pixel = oldBitmap.GetPixel(dx, dy);
                        newBitmap.SetPixel(x, y, pixel);
                    }
                return newBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //更多效果http://blog.sina.com.cn/s/blog_6e51df7f0100z2d6.html
        //方法二：使用html5+css3+js框架http://alloyteam.github.io/AlloyPhoto/alloyphoto.html
    }
}
