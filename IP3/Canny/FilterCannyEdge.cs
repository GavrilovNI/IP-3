using IP1.Imaging;
using IP1.Imaging.ColorNS;
using IP1.Imaging.Filters;
using IP1.Imaging.UtilsNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.Filters
{
    public class FilterCannyEdge : Filter
    {
        private const int low = 0;
        private const int mid = 127;
        private const int high = 255;

        private Filter filterGrayScale = new FilterGrayScale(FilterGrayScale.GrayScaleType.Gimp);
        //private Filter filterBlur = new FilterGaussian();
        private Filter filterBlur = new FilterMedian(2);

        private int lowDF;
        private int highDF;

        public FilterCannyEdge(int lowDF, int highDF)
        {
            this.lowDF = lowDF;
            this.highDF = highDF;
        }


        protected float GetGradientAngle(int posX, int posY, Image image, out ColorRGB color)
        {
            float[,] matY = new float[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            float[,] matX = new float[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };

            float Gx = 0;
            float Gy = 0;

            /*float Gy = image[posY + 1, posX - 1].r + 2 * image[posY + 1, posX].r + image[posY + 1, posX + 1].r
                - image[posY - 1, posX - 1].r - 2 * image[posY - 1, posX].r - image[posY - 1, posX + 1].r;
            float Gx = image[posY - 1, posX + 1].r + 2 * image[posY, posX + 1].r + image[posY + 1, posX + 1].r
                - image[posY - 1, posX - 1].r - 2 * image[posY, posX - 1].r - image[posY + 1, posX - 1].r;
            */

            for (int y = (int)MathF.Max(0, posY-1); y <= posY+1 && y<image.Height; y++)
            {
                for (int x = (int)MathF.Max(0, posX - 1); x <= posX + 1 && x < image.Width; x++)
                {
                    int matrixY = y - posY + 1;
                    int matrixX = x - posX + 1;

                    
                    Gx += matX[matrixY, matrixX] * image[y, x].r;
                    Gy += matY[matrixY, matrixX] * image[y, x].r;
                }
            }

            byte colorGray = (byte)MathF.Sqrt(Gx * Gx + Gy * Gy);

            if (colorGray == 0)
            {
                color = ColorRGB.Black;
                return float.PositiveInfinity;
            }

            float Pi4 = MathF.PI / 4;
            
            color = new ColorRGB(colorGray, colorGray, colorGray);
            return (MathF.Round(MathF.Atan2(Gy, Gx) / Pi4) * Pi4);// * 360 / (MathF.PI * 2);
        }

        protected Image ToGradient(Image image, out float[,] gradientAngles)
        {
            Image result = new Image(image.Width, image.Height);
            gradientAngles = new float[image.Height, image.Width];
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    ColorRGB color;
                    gradientAngles[y, x] = GetGradientAngle(x, y, image, out color);
                    result[y, x] = color;
                }
            }

            return result;
        }

        protected void Tracing(ref Image image, int x, int y)
        {
            int nx, ny;

            nx = x - 1; ny = y;

            ColorRGB nColor = new ColorRGB(high, high, high);

            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
            nx = x - 1; ny = y - 1;
            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
            nx = x - 1; ny = y + 1;
            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
            nx = x + 1; ny = y;
            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
            nx = x + 1; ny = y - 1;
            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
            nx = x + 1; ny = y + 1;
            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
            nx = x; ny = y + 1;
            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
            nx = x; ny = y - 1;
            if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height && image[ny, nx].r == 127)
            {
                image[ny, nx] = nColor;
                Tracing(ref image, nx, ny);
            }
        }

        protected Image ImageTracing(Image image, int value)
        {
            Image result = new Image(image);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    if (image[y, x].r == value)
                        Tracing(ref result, x, y);
                }
            }

            return result;
        }

        protected Image MaxSuppresion(Image image, float[,] gradientAngles)
        {
            Image result = new Image(image);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    if (gradientAngles[y, x] == float.PositiveInfinity)
                    {
                        continue;
                    }

                    const float small = 0.0001f;

                    float Pi2 = MathF.PI / 2;
                    float Pi4 = MathF.PI / 4;

                    if (MathF.Abs(gradientAngles[y, x] + MathF.PI) < small
                        || MathF.Abs(gradientAngles[y, x] - MathF.PI) < small
                        || MathF.Abs(gradientAngles[y, x]) < small)
                    {
                        if (x > 0 && image[y, x].r < image[y, x - 1].r
                            || x < image.Width - 1 && image[y, x].r < image[y, x + 1].r)
                            result[y, x] = ColorRGB.Black;
                    }
                    else if (MathF.Abs(gradientAngles[y, x] + Pi2) < small
                        || MathF.Abs(gradientAngles[y, x] - Pi2) < small)
                    {
                        if (y > 0 && image[y, x].r < image[y - 1, x].r
                            || y < image.Height - 1 && image[y, x].r < image[y + 1, x].r)
                            result[y, x] = ColorRGB.Black;
                    }
                    else if (MathF.Abs(gradientAngles[y, x] + Pi4 * 3) < small
                        || MathF.Abs(gradientAngles[y, x] - Pi4) < small)
                    {
                        if (y > 0 && x > 0 && image[y, x].r < image[y - 1, x - 1].r
                            || y < image.Height - 1 && x < image.Width - 1 && image[y, x].r < image[y + 1, x + 1].r)
                            result[y, x] = ColorRGB.Black;
                    }
                    else if (MathF.Abs(gradientAngles[y, x] - Pi4 * 3) < small
                        || MathF.Abs(gradientAngles[y, x] + Pi4) < small)
                    {
                        if (y > 0 && x < image.Width - 1 && image[y, x].r < image[y - 1, x + 1].r
                            || y < image.Height - 1 && x > 0 && image[y, x].r < image[y + 1, x - 1].r)
                            result[y, x] = ColorRGB.Black;
                    }

                }
            }

            return result;
        }
        protected Image DoubleFiltration(Image image, int down, int up)
        {
            Image result = new Image(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image[y, x].r >= up)
                        result[y, x] = new ColorRGB(high, high, high);
                    else if (image[y, x].r >= down)
                        result[y, x] = new ColorRGB(mid, mid, mid);
                    else
                        result[y, x] = new ColorRGB(low, low, low);

                }
            }

            return result;
        }

        public override Image Run(Image image)
        {
            //Image result = new Image(image);
            Image result = filterGrayScale.Run(image);
            result = filterBlur.Run(result);

            float[,] gradientAngles;
            result = ToGradient(result, out gradientAngles);
            result = MaxSuppresion(result, gradientAngles);
            result = DoubleFiltration(result, lowDF, highDF);


            result = ImageTracing(result, high);
            result = ImageTracing(result, mid);

            return result;
        }
    }
}
