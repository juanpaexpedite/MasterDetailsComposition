using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MasterDetailsComposition.Behaviors
{

    public class AritmeticEffectBehavior : DependencyObject, IBehavior
    {
        #region Foreground
        private string foreground;
        public string Foreground
        {
            get { return (string)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); foreground = value; }
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(nameof(Foreground), typeof(string), typeof(ArithmeticCompositeEffect), new PropertyMetadata(null));
        #endregion

        #region Background
        private string background;
        public string Background
        {
            get { return (string)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); background = value; }
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(nameof(Background), typeof(string), typeof(ArithmeticCompositeEffect), new PropertyMetadata(null));
        #endregion

        #region Transition
        public bool ShowForeground
        {
            get { return (bool)GetValue(ShowForegroundProperty); }
            set { SetValue(ShowForegroundProperty, value); }
        }
        public static readonly DependencyProperty ShowForegroundProperty =
            DependencyProperty.Register(nameof(ShowForeground), typeof(bool), typeof(ArithmeticCompositeEffect), new PropertyMetadata(false, OnShowForegroundChanged));

        private static void OnShowForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AritmeticEffectBehavior).Invalidate((bool)e.NewValue);
        }

        private void Invalidate(bool showforeground)
        {
            if(showforeground)
            {
                associatedPanel.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(foreground)) };
            }
            else
            {
                associatedPanel.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(background)) };
            }
        }
        #endregion

        public DependencyObject AssociatedObject { get; set; }
        private Panel associatedPanel;

        public void Attach(DependencyObject associatedObject)
        {
            associatedPanel = associatedObject as Panel;
        }

        public void Detach()
        {
            
        }
    }
}
