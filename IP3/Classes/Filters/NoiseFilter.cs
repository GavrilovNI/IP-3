using System;
using System.Collections.Generic;
using System.Text;
using IP1.Imaging.ColorNS;

namespace IP1.Imaging.Filters
{
    public class NoiseFilter : PointFilter
    {
        Random rnd;
        byte noisePower;
        public NoiseFilter(byte noisePower)
        {
            rnd = new Random((int)(DateTime.Now - DateTime.MinValue).TotalMilliseconds);
            this.noisePower = noisePower;
        }
        public override ColorRGB RunColor(ColorRGB color)
        {
            int noiseR = (int)(rnd.Next(-noisePower, noisePower));
            int noiseG = (int)(rnd.Next(-noisePower, noisePower));
            int noiseB = (int)(rnd.Next(-noisePower, noisePower));
            return new ColorRGB((byte)Math.Clamp((color.r + noiseR),0,255), (byte)Math.Clamp((color.g + noiseG), 0, 255), (byte)Math.Clamp((color.b + noiseB), 0, 255));
        }
    }
}
