using IP1.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IP1.Imaging.ColorNS;

namespace IP1
{
    public class Metrics
    {
        private double _CalcMSE(Image first, Image second)
        {
            if (first.Height != second.Height || first.Width != second.Width)
                throw new Exception("Images have different sizes");
            var bytesFirst = first.GetBytesBGR24();
            var bytesSecond = second.GetBytesBGR24();
            var different = bytesFirst.Zip(bytesSecond, (a, b) => Math.Abs(a - b));
            return different.Sum() / different.Count();
        }
        public double CompareImage(Image first, Image second)
        {
            if (first.Height != second.Height || first.Width != second.Width)
                throw new Exception("Images have different sizes");
            return 10 * Math.Log10(255 * 255 / _CalcMSE(first, second));
        }

    }
}
