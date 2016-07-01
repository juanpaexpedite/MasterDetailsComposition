using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace UniversalComposition
{
    public class ColorManager
    {
        /// <summary>
        /// ARGB
        /// </summary>
        /// <param name="hexCode"></param>
        /// <returns></returns>
        public static Color FromHex(string hexCode)
        {
            if (hexCode.Length < 6)
                return Colors.Transparent;

            var color = new Color();

            int i = 0;

            if (hexCode[0] == '#')
                i = 1;

            if (hexCode.Length > 7) //contains Alpha
            {
                color.A = byte.Parse(hexCode.Substring(i, 2), NumberStyles.AllowHexSpecifier);
                i += 2;
            }
            else
            {
                color.A = 255;
            }

            color.R = byte.Parse(hexCode.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            i += 2;
            color.G = byte.Parse(hexCode.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            i += 2;
            color.B = byte.Parse(hexCode.Substring(i, 2), NumberStyles.AllowHexSpecifier);

            return color;
        }

        public static Color Darker(Color c1, double value = 0.2)
        {
            var rgb = new Spectrum.Color.RGB(c1.R, c1.G, c1.B);
            var hsl = rgb.ToHSL();
            hsl = hsl.ShiftLightness(-1*value);
            rgb = hsl.ToRGB();
            return Color.FromArgb(255, rgb.R, rgb.G, rgb.B);
        }

        public static Color Lighter(Color c1, double value = 0.2)
        {
            var rgb = new Spectrum.Color.RGB(c1.R, c1.G, c1.B);
            var hsl = rgb.ToHSL();
            hsl = hsl.ShiftLightness(value);
            rgb = hsl.ToRGB();
            return Color.FromArgb(255, rgb.R, rgb.G, rgb.B);
        }
    }
}
