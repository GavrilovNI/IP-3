using IP1.Imaging.ColorNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP1.Imaging.Filters
{
    public abstract class Filter
    {
        public abstract Image Run(Image image);
    }
}
