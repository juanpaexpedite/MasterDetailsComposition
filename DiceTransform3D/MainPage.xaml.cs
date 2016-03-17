using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Media3D;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DiceTransform3D
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            CreateDice();
        }

        private void CreateDice()
        {

            var side1 = CreateSide(z:100);

            var side2 = CreateSide(z:-100, fliptext:true);

            var side3 = CreateSide(anglex:90, y:100);

            var side4 = CreateSide(anglex:90, y:-100, fliptext:true);

            var side5 = CreateSide(angley: 90, x:100, fliptext: true);

            var side6 = CreateSide(angley:90, x:-100);

            MainGrid.Children.Add(side1);
            MainGrid.Children.Add(side2);
            MainGrid.Children.Add(side3);
            MainGrid.Children.Add(side4);
            MainGrid.Children.Add(side5);
            MainGrid.Children.Add(side6);

        }

        int number = 1;
        private UIElement CreateSide(double x=0,double y=0, double z=0, double anglex=0, double angley=0, double size = 200, bool fliptext = false)
        {
            var orange = Colors.Orange;
            orange.A = 128;
            var color = new SolidColorBrush(orange);
            var bordercolor = new SolidColorBrush(Colors.White);
            var transform = new CompositeTransform3D()
            {
                CenterX = size / 2,
                CenterY = size / 2,
                CenterZ = 0,
                TranslateX = x,
                TranslateZ = z,
                TranslateY = y,
                RotationX = anglex,
                RotationY = angley
            };
            var newrectangle = new Grid()
            {
                Width = size,
                Height = size,
                Background = color,
                Transform3D = transform
            };
            var block = new TextBlock()
            {
                Text = number.ToString(),
                FontSize = 64,
                Foreground = bordercolor,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            if(fliptext)
            {
                block.RenderTransform = new ScaleTransform() { ScaleX = -1 };
            }

            newrectangle.Children.Add(block);
            number++;
            return newrectangle;

        }
    }
}
