using System;
using System.Collections.Generic;
using System.Drawing;
using IP1.Imaging.Filters;
using IP1.Imaging;
using System.Linq;
using System.Text;
using Image = IP1.Imaging.Image;

namespace IP1
{
    class FilterHough : Filter
    {
        private int lowDF;
        private int highDF;

        public FilterHough(int lowDF, int highDF)
        {
            this.lowDF = lowDF;
            this.highDF = highDF;
        }

        public override Image Run(Image image)
        {
            int cross_num = 100;
            FilterCannyEdge filter = new FilterCannyEdge(lowDF, highDF);

            Bitmap inputBitmap = filter.Run(image);
            int width = inputBitmap.Width;
            int height = inputBitmap.Height;
            int rho_max = (int)Math.Floor(Math.Sqrt(width * width + height * height)) + 1;

            var PreLines = new Dictionary<(int, int), int>();


            static double AngleToRadians(int angle) => angle * Math.PI / 180.0;
            static double GetRho(int x, int y, int k) => (y * Math.Sin(AngleToRadians(k))) + (x * Math.Cos(AngleToRadians(k)));

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = inputBitmap.GetPixel(x, y);
                    if (pixel.ToArgb() != Color.White.ToArgb())
                    {
                        for (int k = 0; k < 180; k++)
                        {
                            double rho = GetRho(x, y, k);
                            int rhoIndex = (int)Math.Round(rho / 2 + rho_max / 2);
                            var key = (rhoIndex, k);
                            if (!PreLines.TryGetValue(key, out int count))
                            {
                                PreLines.Add(key, 1);
                            }
                            else
                            {
                                PreLines[key] = count + 1;
                            }
                        }
                    }
                }
            }

            var lines = PreLines.Where(z => z.Value >= cross_num).Select(z => (z.Key.Item1, z.Key.Item2)).ToList();
            
            Bitmap outputBitmap = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    outputBitmap.SetPixel(x, y, Color.White);

                    Color pixel = inputBitmap.GetPixel(x, y);
                    if (pixel.ToArgb() != Color.White.ToArgb())
                    {
                        for (int k = 0; k < 180; k++)
                        {
                            double rho = GetRho(x, y, k);
                            int rho_int = (int)Math.Round(rho / 2 + rho_max / 2);

                            if (lines.Any(l => l.Item1 == rho_int && l.Item2 == k))
                            {
                                outputBitmap.SetPixel(x, y, Color.Black);
                            }
                        }
                    }
                }
            }
            System.Drawing.Image resImage = outputBitmap;
            return (Image)resImage;
        }
    }
}
