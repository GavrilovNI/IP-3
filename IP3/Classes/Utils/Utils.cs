using IP1.Imaging.ColorNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IP1.Imaging.UtilsNS
{ 
    public static class Utils
    {
        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
                throw new Exception("Clamp error min>max");

            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static BitmapSource ImageToBitmapSource(IP1.Imaging.Image image)
        {
            System.Windows.Media.PixelFormat pf = PixelFormats.Bgr24;
            int width = image.Width;
            int height = image.Height;
            int rawStride = (width * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = image.GetBytesBGR24().ToArray();

            return BitmapSource.Create(width, height,
                96, 96, pf, null,
                rawImage, rawStride);
        }

        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgr24, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }

        /*public static Bitmap BitmapSourceToBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(
                source.PixelWidth,
                source.PixelHeight,
                PixelFormats.Format32bppPArgb);
            BitmapData data = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppPArgb);
            source.CopyPixels(
                Int32Rect.Empty,
                data.Scan0,
                data.Height * data.Stride,
                data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }*/

        /*public static BitmapImage ImageToBitmapSource(System.Drawing.Image image)
        {
            var memory = new MemoryStream();
            image.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            memory.Position = 0;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            return bitmapImage;
        }*/
    }
}

