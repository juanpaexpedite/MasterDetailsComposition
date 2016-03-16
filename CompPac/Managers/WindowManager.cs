using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CompPac.Managers
{
    public class WindowManager
    {
        private static Vector2 CurrentBounds = Vector2.Zero;

        public static Vector2 Bounds()
        {
            if(CurrentBounds == Vector2.Zero)
            {
                CurrentBounds = new Vector2((float)Window.Current.Bounds.Width, (float)Window.Current.Bounds.Height);

                Window.Current.SizeChanged += Current_SizeChanged;
            }

            return CurrentBounds;
        }

        private static void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            CurrentBounds = new Vector2((float)e.Size.Width, (float)e.Size.Height);
        }
    }
}
