using IP1.Imaging.ColorNS;
using IP1.Imaging.UtilsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IP1.Imaging.Filters
{
    
    public abstract class MatrixFilter : Filter
    {
        protected abstract float[,] Matrix { get; } 

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
            int halfX = Matrix.GetLength(1) / 2;
            int halfY = Matrix.GetLength(0) / 2;
            int minX = Math.Max(0, posX - halfX);
            int minY = Math.Max(0, posY - halfY);
            int maxX = Math.Min(image.Width - 1, posX + halfX + Matrix.GetLength(1) % 2);
            int maxY = Math.Min(image.Height - 1, posY + halfY + Matrix.GetLength(0) % 2);
            return new Bounds(minX, minY, maxX, maxY);
        }

        public virtual ColorRGB RunColor(int posX, int posY, Image image)
        {
            Bounds bounds = GetBounds(posX, posY, image);


            float resultR = 0;
            float resultG = 0;
            float resultB = 0;

            float matrixSum = 0;

            for (int y = bounds.minY; y < bounds.maxY; y++)
            {
                for (int x = bounds.minX; x < bounds.maxX; x++)
                {
                    int matrixY = y - bounds.minY;
                    int matrixX = x - bounds.minX;
                    resultR += Matrix[matrixY, matrixX] * image[y, x].r;
                    resultG += Matrix[matrixY, matrixX] * image[y, x].g;
                    resultB += Matrix[matrixY, matrixX] * image[y, x].b;

                    matrixSum += Matrix[matrixY, matrixX];
                }
            }

            return new ColorRGB((byte)(resultR / matrixSum), (byte)(resultG / matrixSum), (byte)(resultB / matrixSum));
        }
    }
}
