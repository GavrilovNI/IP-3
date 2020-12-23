using IP1.Imaging.ColorNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.Filters
{
    public class FilterBGR2HSV : PointFilter
    {
        public override ColorRGB RunColor(ColorRGB color)
        {
            double h, s, v;

            double red = color.r / 255.0;
            double green = color.g / 255.0;
            double blue = color.b / 255.0;

            double cMax = Math.Max(red, Math.Max(green, blue));
            double cMin = Math.Min(red, Math.Min(green, blue));
            double delta = cMax - cMin;

            if (delta == 0)
                h = 0;
            else if (cMax == red)
                h = 60 * ((green - blue) / delta % 6);
            else if (cMax == green)
                h = 60 * ((blue - red) / delta + 2);
            else
                h = 60 * ((red - green) / delta + 4);

            if (cMax == 0)
                s = 0;
            else
                s = delta / cMax;

            v = cMax;

            return new ColorRGB((byte)(v * 255), (byte)(s * 255), (byte)(h / 360 * 255));
            //return new ColorRGB((byte)(h / 360 * 255), (byte)(s * 255), (byte)(v * 255));
        }

    }
}
