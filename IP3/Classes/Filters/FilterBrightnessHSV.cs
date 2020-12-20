using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP1.Imaging.Filters;
using IP1.Imaging.ColorNS;

namespace IP1.Imaging.Filters
{
    public class FilterBrightnessHSV : PointFilter
    {
        double percent;
        public FilterBrightnessHSV(double percent)
        {
            if (percent < 0)
                throw new Exception("percent can't be less than 0");
            this.percent = percent;
        }

        public override ColorRGB RunColor(ColorRGB color)
        {
            return new ColorRGB(color.r, color.g, (byte)Math.Min(255, color.b * percent / 100));
        }

    }

}
