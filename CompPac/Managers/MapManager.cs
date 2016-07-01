using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace CompPac.Managers
{
    public class MapManager
    {
        public static Rect[] Map;

        public static void CreateMap()
        {
            var noRects = 55;
            var rects = new Rect[noRects];
            rects[0] = new Rect(138, 15, 25, 207);
            rects[1] = new Rect(200, 66, 78, 42);
            rects[2] = new Rect(320, 66, 102, 42);
            rects[3] = new Rect(536, 66, 102, 42);
            rects[4] = new Rect(680, 66, 78, 42);
            rects[5] = new Rect(200, 150, 78, 21);
            rects[6] = new Rect(392, 150, 174, 21);
            rects[7] = new Rect(680, 150, 78, 21);
            rects[8] = new Rect(138, 0, 687, 25);
            rects[9] = new Rect(800, 15, 25, 207);
            rects[10] = new Rect(464, 15, 30, 93);
            rects[11] = new Rect(138, 213, 140, 21);
            rects[12] = new Rect(320, 213, 102, 21);
            rects[13] = new Rect(536, 213, 102, 21);
            rects[14] = new Rect(680, 213, 145, 21);
            rects[15] = new Rect(138, 414, 25, 249);
            rects[16] = new Rect(800, 414, 25, 249);
            rects[17] = new Rect(320, 150, 30, 147);
            rects[18] = new Rect(608, 150, 30, 147);
            rects[19] = new Rect(464, 150, 30, 84);
            rects[20] = new Rect(608, 339, 30, 84);
            rects[21] = new Rect(320, 339, 30, 84);
            rects[22] = new Rect(138, 654, 687, 25);
            rects[23] = new Rect(392, 402, 174, 21);
            rects[24] = new Rect(200, 465, 78, 21);
            rects[25] = new Rect(680, 465, 78, 21);
            rects[26] = new Rect(138, 276, 140, 21);
            rects[27] = new Rect(680, 276, 145, 21);
            rects[28] = new Rect(138, 339, 140, 21);
            rects[29] = new Rect(680, 339, 145, 21);
            rects[30] = new Rect(138, 402, 140, 21);
            rects[31] = new Rect(680, 402, 145, 21);
            rects[32] = new Rect(248, 339, 30, 84);
            rects[33] = new Rect(680, 339, 30, 84);
            rects[34] = new Rect(248, 213, 30, 84);
            rects[35] = new Rect(680, 213, 30, 84);
            rects[36] = new Rect(320, 465, 102, 21);
            rects[37] = new Rect(536, 465, 102, 21);
            rects[38] = new Rect(536, 591, 222, 21);
            rects[39] = new Rect(200, 591, 222, 21);
            rects[40] = new Rect(392, 528, 174, 21);
            rects[41] = new Rect(150, 528, 56, 21);
            rects[42] = new Rect(752, 528, 56, 21);
            rects[43] = new Rect(248, 469, 30, 80);
            rects[44] = new Rect(680, 469, 30, 80);
            rects[45] = new Rect(464, 406, 30, 80);
            rects[46] = new Rect(464, 532, 30, 80);
            rects[47] = new Rect(320, 528, 30, 80);
            rects[48] = new Rect(608, 528, 30, 80);
            rects[49] = new Rect(392, 276, 9, 84);
            rects[50] = new Rect(557, 276, 9, 84);
            rects[51] = new Rect(392, 351, 170, 9);
            rects[52] = new Rect(392, 276, 66, 9);
            rects[53] = new Rect(500, 276, 66, 9);
            rects[54] = new Rect(1, 1, 1, 1);

            double scale = 1.4;

            for(int i=0;i< noRects;i++)
            {
                rects[i].X = rects[i].X * scale;
                rects[i].Y = rects[i].Y * scale;
                rects[i].Width = rects[i].Width *scale;
                rects[i].Height = rects[i].Height * scale;
            }

            Map = rects;
        }

        public static void DrawMap(Canvas canvas)
        {
            var blue = new SolidColorBrush(Colors.Blue);

            foreach (var rect in Map)
            {
                Rectangle rectangle = new Rectangle() { Width = rect.Width, Height = rect.Height };
                rectangle.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                rectangle.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                rectangle.SetValue(Canvas.LeftProperty, rect.Left);
                rectangle.SetValue(Canvas.TopProperty, rect.Top);
                rectangle.Fill = blue;
                canvas.Children.Add(rectangle);
            }
        }
    }
}
