using IP1.Imaging.ColorNS;
using IP1.Imaging.UtilsNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.Filters
{
    public class FilterMedian : Filter
    {
        int radius;

        public FilterMedian(int radius)
        {
            if (radius <= 0)
                throw new Exception("Radius should be more than 0");
            this.radius = radius;
        }

        public override Image Run(Image image)
        {
            Image result = new Image(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    result[y, x] = RunColor(x, y, image);
                }
            }
            return result;
        }

        protected Bounds GetBounds(int posX, int posY, Image image)
        {
            int minX = Math.Max(0, posX - radius);
            int minY = Math.Max(0, posY - radius);
            int maxX = Math.Min(image.Width - 1, minX + radius * 2 + 1);
            int maxY = Math.Min(image.Height - 1, minY + radius * 2 + 1);
            return new Bounds(minX, minY, maxX, maxY);
        }

        public ColorRGB RunColor(int posX, int posY, Image image)
        {
            Bounds bounds = GetBounds(posX, posY, image);


            byte[] colorsR = new byte[bounds.Size];
            byte[] colorsG = new byte[bounds.Size];
            byte[] colorsB = new byte[bounds.Size];

            int i = 0;

            for (int y = bounds.minY; y < bounds.maxY; y++)
            {
                for (int x = bounds.minX; x < bounds.maxX; x++)
                {
                    colorsR[i] = image[y, x].r;
                    colorsG[i] = image[y, x].g;
                    colorsB[i] = image[y, x].b;
                    i++;
                }
            }

            Array.Sort(colorsR);
            Array.Sort(colorsG);
            Array.Sort(colorsB);

            int resultId = bounds.Size / 2;

            return new ColorRGB(colorsR[resultId], colorsG[resultId], colorsB[resultId]);
        }
    }
}
