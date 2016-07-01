using CompPac.Common;
using CompPac.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompPac
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private GameManager game;

        public MainPage()
        {

            game = new GameManager();
            MapManager.CreateMap();

            this.InitializeComponent();

            MapManager.DrawMap(GameGrid);

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            game.AddComposable(Pacman);

            game.Start();
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            var key = e.VirtualKey;
            if (key ==  VirtualKey.Up)
            {
                Pacman.ChangeTo(Directions.Up);
            }
            else if (key == VirtualKey.Down)
            {
                Pacman.ChangeTo(Directions.Down);
            }
            else if (key == VirtualKey.Right)
            {
                Pacman.ChangeTo(Directions.Right);
            }
            else if (key == VirtualKey.Left)
            {
                Pacman.ChangeTo(Directions.Left);
            }

        }
    }
}
