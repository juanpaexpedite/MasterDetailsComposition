using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace AnimatedControls
{
    public sealed partial class Rating : UserControl
    {
        public double Score
        {
            get { return (double)GetValue(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }

        public static readonly DependencyProperty ScoreProperty =
            DependencyProperty.Register(nameof(Score), typeof(double), typeof(Rating), new PropertyMetadata(0.0, (d, e) => (d as Rating).ScoreChanged()));
        private void ScoreChanged()
        {
            var width = ForegroundStars.ActualWidth;
            var height = ForegroundStars.ActualHeight;

            var clipped = (Score / 10) * width;

        }

        private void Animate()
        {

        }

        public Rating()
        {
            this.InitializeComponent();

            this.Loaded += (s, e) =>
            {
                InitializeComposition();
            };
        }


        InsetClip ForegroundStarsClip;

        private void InitializeComposition()
        {
            var starsVisual = ElementCompositionPreview.GetElementVisual(ForegroundStars);

            var size = new Vector2((float)ForegroundStars.ActualWidth, (float)ForegroundStars.ActualHeight);

            starsVisual.Size = size;
            ForegroundStarsClip = starsVisual.Compositor.CreateInsetClip(0, 0, 0.1f, 0.5f);
            starsVisual.Clip = ForegroundStarsClip;
            
        }
    }
}
