using MasterDetailsComposition.Managers;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Effects;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MasterDetailsComposition.Attached
{
    public class Effects : DependencyObject
    {
        #region Tag
        public static void SetEffectComposition(UIElement element, DependencyObject value)
        {
            element.SetValue(EffectCompositionProperty, value);
        }
        public static DependencyObject GetEffectComposition(UIElement element)
        {
            return (DependencyObject)element.GetValue(EffectCompositionProperty);
        }

        public static readonly DependencyProperty EffectCompositionProperty = DependencyProperty.RegisterAttached("EffectComposition",
          typeof(DependencyObject), typeof(Effects), new PropertyMetadata(null));

        #endregion

        #region BlendEffectComposition
        public static void SetBlendForeground(UIElement element, String value)
        {
            element.SetValue(BlendForegroundProperty, value);
        }
        public static String GetBlendForeground(UIElement element)
        {
            return (String)element.GetValue(BlendForegroundProperty);
        }

        public static readonly DependencyProperty BlendForegroundProperty = DependencyProperty.RegisterAttached("BlendForeground",
           typeof(String), typeof(Effects), new PropertyMetadata(null, BlendForegroundChanged));

        private static void BlendForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newUri = e.NewValue as String;

            if(d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as BlendEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new BlendEffectComposition(d as UIElement, newUri);
                    SetEffectComposition(d as UIElement, effectComposition);
                    
                }
                else
                {
                    effectComposition.SetForeground(newUri);
                }
            }

        }
        #endregion

        #region DropEffectComposition
        public static void SetDropText(UIElement element, String value)
        {
            element.SetValue(DropTextProperty, value);
        }
        public static String GetDropText(UIElement element)
        {
            return (String)element.GetValue(DropTextProperty);
        }

        public static readonly DependencyProperty DropTextProperty = DependencyProperty.RegisterAttached("DropText",
           typeof(String), typeof(Effects), new PropertyMetadata(null, DropTextChanged));

        private static void DropTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as DropEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new DropEffectComposition(d as UIElement);
                    SetEffectComposition(d as UIElement, effectComposition);
                }
                else
                {
                    effectComposition.Animate(e.NewValue as String);
                }
            }
        }
        #endregion

        #region SlideEffectComposition
        public static void SetSlide(UIElement element, bool value)
        {
            element.SetValue(SlideProperty, value);
        }

        public static bool GetSlide(UIElement element)
        {
            return (bool)element.GetValue(SlideProperty);
        }

        public static readonly DependencyProperty SlideProperty = DependencyProperty.RegisterAttached("Slide",
         typeof(bool), typeof(Effects), new PropertyMetadata(true, SlideChanged));

        private static void SlideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as SlideEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new SlideEffectComposition(d as UIElement, (bool)e.NewValue);
                    SetEffectComposition(d as UIElement, effectComposition);
                }
                else
                {
                    effectComposition.Animate((bool)e.NewValue);
                }
            }
        }
        #endregion

        #region FallEffectComposition
        public static void SetFall(UIElement element, bool value)
        {
            element.SetValue(FallProperty, value);
        }

        public static bool GetFall(UIElement element)
        {
            return (bool)element.GetValue(FallProperty);
        }

        public static readonly DependencyProperty FallProperty = DependencyProperty.RegisterAttached("Fall",
         typeof(bool), typeof(Effects), new PropertyMetadata(DependencyProperty.., OnFallChanged));

        private static void OnFallChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as FallEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new FallEffectComposition(d as UIElement, (bool)e.NewValue);
                    SetEffectComposition(d as UIElement, effectComposition);
                }
                else
                {
                    effectComposition.Animate((bool)e.NewValue);
                }
            }
        }

        #endregion
    }

    public class DropEffectComposition : DependencyObject
    {
        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public TextBlock Element;

        #region Animations
        CompositionAnimation FadeInAnimation;
        CompositionAnimation FadeOutAnimation;
        CompositionAnimation DropInAnimation;
        CompositionAnimation DropOutAnimation;
        private void InitializeAnimations()
        {
            FadeInAnimation = Compositor.FadeIn(250);
            FadeOutAnimation = Compositor.FadeOut(250);
            DropInAnimation = Compositor.DropIn(500);
            DropOutAnimation = Compositor.DropOut(500);
        }

        public async void Animate(string newText)
        {
            AnimateOpacity(newText);
            AnimateOffset();
        }

        private async void AnimateOpacity(string newText)
        {
            ContainerVisual.StartAnimation("Opacity", FadeOutAnimation);
            await Task.Delay(400);
            Element.Text = newText;
            ContainerVisual.StartAnimation("Opacity", FadeInAnimation);
        }

        bool dropout = true;
        private async void AnimateOffset()
        {
            ContainerVisual.StartAnimation("Offset", dropout ? DropOutAnimation : DropInAnimation);
            dropout = !dropout;
        }
        #endregion

        #region Constructor
        public DropEffectComposition(UIElement element)
        {
            ContainerVisual = element.GetContainerVisual();
            Compositor = ContainerVisual.Compositor;
            Element = element as TextBlock;
            InitializeAnimations();
        }
        #endregion
    }

    public class BlendEffectComposition : DependencyObject
    {
        #region Fields
        public Compositor Compositor;
        public ArithmeticCompositeEffect Effect;
        public CompositionEffectBrush Brush;
        public ContainerVisual RootVisual;
        public SpriteVisual SpriteVisual;
        public UIElement Element;
        public Size imageSize;
        public double imageAspectRatio;

        public CompositionSurfaceBrush LastImageBrush;
        #endregion

        #region Effect
        private void InitializeEffect()
        {
            Effect = new ArithmeticCompositeEffect
            {
                Name = "effect",
                ClampOutput = false,
                Source1 = new CompositionEffectSourceParameter("Source1"),
                Source2 = new CompositionEffectSourceParameter("Source2"),
                Source1Amount = 0.0f,
                Source2Amount = 0.0f,
                MultiplyAmount = 0.0f
            };
        }
        #endregion

        #region Brush
        private Uri CreateUri(string input)
        {
            if (input.Contains("ms-appx"))
            {
                return new Uri(input);
            }
            else
            {
                return new Uri($"ms-appx:///{input.TrimStart('/')}");
            }
        }
        private void InitializeBrush(IGraphicsEffect effect, String uri)
        {
            Brush = Compositor.CreateEffectFactory(effect,
               new[]
               {
                    "effect.Source1Amount",
                    "effect.Source2Amount",
               }
           ).CreateBrush();

            SetForeground(uri);
        }
        #endregion

        #region Initialization
        private void Initialize(UIElement element, String uri)
        {
            InitializeEffect();
            InitializeBrush(Effect, uri);
        }

        private void InitializeVisuals(UIElement element)
        {
            RootVisual = Compositor.CreateChildContainer(element);
            SpriteVisual = Compositor.CreateChildSprite(element);

            InvalidateSize(element);
            Compose(element);
        }

        #endregion

        #region Compose
        private void Compose(UIElement element)
        {
            SpriteVisual.Brush = Brush;
            RootVisual.Children.InsertAtBottom(SpriteVisual);
        }
        #endregion

        #region Size
        private void InvalidateSize(UIElement element)
        {
            SpriteVisual.ResizeImage(imageSize, imageAspectRatio);
            (element as FrameworkElement).SizeChanged += ArithmeticEffectComposition_SizeChanged;
            Element = element;
        }

        private void ArithmeticEffectComposition_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SpriteVisual.ResizeImage(e.NewSize, imageAspectRatio);
        }

        public void Discompose()
        {
            (Element as FrameworkElement).SizeChanged -= ArithmeticEffectComposition_SizeChanged;
        }
        #endregion

        #region Constructor
        public BlendEffectComposition(UIElement element, String uri)
        {
            Compositor = element.GetContainerVisual().Compositor;

            InitializeAnimations();
            Initialize(element, uri);
            InitializeVisuals(element);

        }
        #endregion

        #region Animate
        CompositionAnimation FadeInAnimation;
        CompositionAnimation FadeOutAnimation;
        private void InitializeAnimations()
        {
            FadeInAnimation = Compositor.FadeIn();
            FadeOutAnimation = Compositor.FadeOut();
        }
        public void Animate(bool both = true)
        {
            Brush.StopAnimation("effect.Source1Amount");

            Brush.StartAnimation("effect.Source1Amount", FadeInAnimation);

            if (both)
            {
                Brush.StopAnimation("effect.Source2Amount");
                Brush.StartAnimation("effect.Source2Amount", FadeOutAnimation);
            }
        }

        internal void SetForeground(string newUri)
        {
            var newImageBrush = CreateImageBrush(newUri);

            Brush.SetSourceParameter("Source1", newImageBrush);

            if(LastImageBrush == null)
            {
                Brush.SetSourceParameter("Source2", newImageBrush);
                Animate(false);
            }
            else
            {
                Brush.SetSourceParameter("Source2", LastImageBrush);
                Animate();
            }

            LastImageBrush = newImageBrush;
        }

        private CompositionSurfaceBrush CreateImageBrush(string newUri)
        {
            var source = CreateUri(newUri);
            var imageBrush = Compositor.CreateBrushFromAsset(source, out imageSize);
            imageBrush.Stretch = CompositionStretch.UniformToFill;
            imageAspectRatio = imageSize.Width / imageSize.Height;

            return imageBrush;
        }
        #endregion
    }

    public class SlideEffectComposition : DependencyObject
    {
        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public UIElement Element;

        #region Animations
        CompositionAnimation SlideInAnimation;
        CompositionAnimation SlideOutAnimation;
        private void InitializeAnimations()
        {
            SlideInAnimation = Compositor.SlideIn(500,500);
            SlideOutAnimation = Compositor.SlideOut(500,500);
        }

        public void Animate(bool newvalue)
        {
            AnimateOffset(newvalue);
        }

        private void AnimateOffset(bool newvalue)
        {
            ContainerVisual.StartAnimation("Offset", newvalue ? SlideInAnimation : SlideOutAnimation);
        }
        #endregion

        #region Constructor
        public SlideEffectComposition(UIElement element, bool newvalue)
        {
            ContainerVisual = element.GetContainerVisual();
            Compositor = ContainerVisual.Compositor;
            Element = element;
            InitializeAnimations();
            Animate(newvalue);
        }
        #endregion
    }

    public class FallEffectComposition : DependencyObject
    {
        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public UIElement Element;

        #region Animations
        CompositionAnimation FallDownAnimation;
        CompositionAnimation FallUpAnimation;
        private void InitializeAnimations()
        {
            FallDownAnimation = Compositor.FallDown(500, 500);
            FallUpAnimation = Compositor.FallUp(500, 500);
        }

        public void Animate(bool newvalue)
        {
            AnimateOffset(newvalue);
        }

        private void AnimateOffset(bool newvalue)
        {
            ContainerVisual.StartAnimation("Offset", newvalue ? FallUpAnimation : FallDownAnimation);
        }
        #endregion

        #region Constructor
        public FallEffectComposition(UIElement element, bool newvalue)
        {
            ContainerVisual = element.GetContainerVisual();
            Compositor = ContainerVisual.Compositor;
            Element = element;
            InitializeAnimations();
            Animate(newvalue);
        }
        #endregion
    }
}
