using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.UtilsNS
{
    public class Bounds
    {
        public int minX, minY, maxX, maxY;
        public int Size => (maxX - minX) * (maxY - minY);
        public Bounds(int minX, int minY, int maxX, int maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }
    }
}
