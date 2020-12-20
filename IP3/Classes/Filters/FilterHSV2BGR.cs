using IP1.Imaging.ColorNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.Filters
{
    public class FilterHSV2BGR : PointFilter
    {
        public override ColorRGB RunColor(ColorRGB color)
        {
            double v = color.r / 255.0;
            double s = color.g / 255.0;
            double h = color.b / 255.0 * 360 % 360;

            double c = v * s;
            double x = c * (1 - Math.Abs(h / 60 % 2 - 1));
            double m = v - c;

            double[,] newValues = new double[6, 3] {{ c, x, 0 },
                                                            { x, c, 0 },
                                                            { 0, c, x },
                                                            { 0, x, c },
                                                            { x, 0, c },
                                                            { c, 0, x }};

            int num = (int)Math.Floor(h / 60);


            double r = (newValues[num, 0] + m) * 255;
            double g = (newValues[num, 1] + m) * 255;
            double b = (newValues[num, 2] + m) * 255;

            return new ColorRGB((byte)r, (byte)g, (byte)b);
        }

    }
}
