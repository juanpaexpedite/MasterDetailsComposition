using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimatedControls
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
            Test();
        }

        private async void Test()
        {
            Indicator.IsBusy= !Indicator.IsBusy;
            await Task.Delay(Indicator.IsBusy ? 5000 : 1000);
            Test();
        }

        UniversalElement utextblock;

        private async void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var textblock = sender as TextBlock;
            utextblock = new UniversalElement(sender);

            var textformat = new CanvasTextFormat()
            {
                FontFamily = textblock.FontFamily.Source,
                FontSize = (float)textblock.FontSize,
                FontStretch = textblock.FontStretch,
                FontWeight = textblock.FontWeight
            };

            var textlayout = new CanvasTextLayout(utextblock.Masker.Device, textblock.Text, textformat, (float)textblock.ActualWidth + 6, (float)textblock.ActualHeight + 6);

            var geometry = CanvasGeometry.CreateText(textlayout);

            await utextblock.CreateMaskBrush(geometry.Stroke(3), Colors.Blue);

            var first = (utextblock.Element.Parent as Panel).Children.First();
            ElementCompositionPreview.SetElementChildVisual(first, utextblock.Visual);
        }
    }
}
