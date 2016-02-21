using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Particles2D.Managers
{
    public class ColorManager
    {
        public static Color AlphaColor(Color source, byte value)
        {
            return Color.FromArgb(value, source.R, source.G, source.B);
        }
    }
}
