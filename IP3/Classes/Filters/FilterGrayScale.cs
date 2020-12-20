using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP1.Imaging.Filters;
using IP1.Imaging.ColorNS;

namespace IP1.Imaging.Filters
{
    public class FilterGrayScale : PointFilter
    {
        public enum GrayScaleType
        {
            Average,
            Gimp
        }

        private GrayScaleType grayScaleType;

        public FilterGrayScale(GrayScaleType grayScaleType)
        {
            this.grayScaleType = grayScaleType;
        }

        public override ColorRGB RunColor(ColorRGB color)
        {
            
            byte newColor;
            switch(grayScaleType)
            {
                case GrayScaleType.Average:
                    newColor = (byte)((color.r + color.g + color.b) / 3.0);
                    break;
                case GrayScaleType.Gimp:
                    newColor = (byte)(0.3 * color.r + 0.59 * color.g + 0.11 * color.b);
                    break;
                default:
                    throw new Exception(grayScaleType.ToString() + " is not working.");
            }

            return new ColorRGB(newColor, newColor, newColor);
        }

    }

}
