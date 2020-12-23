using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP1.Imaging.ColorNS
{
    
        public class ColorRGB
        {
            public static ColorRGB White => new ColorRGB(255, 255, 255);
            public static ColorRGB Black => new ColorRGB(0, 0, 0);
            public static ColorRGB Red => new ColorRGB(255, 0, 0);
            public static ColorRGB Green => new ColorRGB(0, 255, 0);
            public static ColorRGB Blue => new ColorRGB(0, 0, 255);
            public static ColorRGB Magenta => new ColorRGB(255, 0, 255);
            public static ColorRGB Yellow => new ColorRGB(255, 255, 0);
            public static ColorRGB Cyan => new ColorRGB(0, 255, 255);

            public byte r, g, b;
            public ColorRGB(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public ColorRGB(ColorRGB other)
            {
                this.r = other.r;
                this.g = other.g;
                this.b = other.b;
            }

            /*
            public void SetHSV(double hue, double sat, double val)
            {
                int ii;
                double fract;
                double c1, c2, c3;
                double red = 0.0, green = 0.0, blue = 0.0;
                if (sat == 0)
                    red = green = blue = val;
                else
                {
                    hue /= 60;
                    ii = (int)Math.Floor(hue);
                    fract = hue - ii;

                    c1 = val * sat;
                    c2 = c1 * (1 - Math.Abs(hue % 2 - 1));
                    c3 = val - c1;

                    //double[] newValues = new double[] { val, c2, c1, c1, c3, val, val, c2, c1, c1 };
                    //red = newValues[ii];
                    //green = newValues[ii + 4];
                    //blue = newValues[ii + 2];

                    double[,] newValues = new double[6, 3] {{ c1, c2, 0 },
                                                            { c2, c1, 0 },
                                                            { 0, c1, c2 },
                                                            { 0, c2, c1 },
                                                            { c2, 0, c1 },
                                                            { c1, 0, c2 }};

                    
                    red = newValues[ii, 0] + c3;
                    green = newValues[ii, 1] + c3;
                    blue = newValues[ii, 2] + c3;
                }

                r = (byte)Math.Round(red * 255);
                g = (byte)Math.Round(green * 255);
                b = (byte)Math.Round(blue * 255);
            }

            public void SetHue(double hue)
            {
                hue %= 360;
                if (hue < 0)
                    hue += 360;

                double sat = GetSaturation();
                double val = GetValue();
                SetHSV(hue, sat, val);

            }

            public void SetSaturation(double sat)
            {
                sat %= 1;
                if (sat < 0)
                    sat += 1;

                double hue = GetHue();
                double val = GetValue();
                SetHSV(hue, sat, val);
            }

            public void SetValue(double val)
            {
                val %= 1;
                if (val < 0)
                    val += 1;

                double hue = GetHue();
                double sat = GetSaturation();
                SetHSV(hue, sat, val);
            }

            public double GetHue()
            {
                double red = r / 255.0;
                double green = g / 255.0;
                double blue = b / 255.0;

                double hue = 0;
                double sat;
                double val;
                double tmp;


                val = GetValue();
                tmp = Math.Min(Math.Min(red, green), blue);

                if (val == 0)
                    sat = 0;
                else
                    sat = (val - tmp) / val;

                if (sat != 0)
                {
                    double delta = val - tmp;
                    double[] rgb = new double[5] { red, green, blue, red, green };

                    int index = Array.FindIndex(rgb, 0, 3, x => x == val);
                    hue = (index * 2 + (val - rgb[index + 2]) / delta - (val - rgb[index + 1]) / delta) * 60;


                   

                    if (hue < 0)
                        hue += 360;
                }

                return hue;

            }

            public double GetSaturation()
            {
                double red = r / 255.0;
                double green = g / 255.0;
                double blue = b / 255.0;

                double sat;
                double val;
                double tmp;

                val = GetValue();
                tmp = Math.Min(Math.Min(red, green), blue);

                if (val == 0)
                    sat = 0;
                else
                    sat = (val - tmp) / val;

                return sat;

            }

            public double GetValue()
            {
                return Math.Max(Math.Max(r, g), b) / 255.0;
            }
    */
        }
    
}
