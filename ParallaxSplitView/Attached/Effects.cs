using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UniversalComposition;
using Windows.Foundation;
using Windows.Graphics.Effects;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace ParallaxSplitView.Attached
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

        #region Column
        public static void SetColumn(UIElement element, int value)
        {
            element.SetValue(ColumnProperty, value);
        }
        public static int GetColumn(UIElement element)
        {
            return (int)element.GetValue(ColumnProperty);
        }

        public static readonly DependencyProperty ColumnProperty = DependencyProperty.RegisterAttached("Column",
          typeof(int), typeof(Effects), new PropertyMetadata(0));
        #endregion

        #region Delay
        public static void SetDelay(UIElement element, double value)
        {
            element.SetValue(DelayProperty, value);
        }
        public static double GetDelay(UIElement element)
        {
            return (double)element.GetValue(DelayProperty);
        }

        /// <summary>
        /// Delay in seconds
        /// </summary>
        public static readonly DependencyProperty DelayProperty = DependencyProperty.RegisterAttached("Delay",
          typeof(double), typeof(Effects), new PropertyMetadata(DependencyProperty.UnsetValue));

        #endregion

        #region BoolEffectParameter
        public static void SetBoolEffectParameter(UIElement element, bool value)
        {
            element.SetValue(BoolEffectParameterProperty, value);
        }

        public static bool GetBoolEffectParameter(UIElement element)
        {
            return (bool)element.GetValue(BoolEffectParameterProperty);
        }

        public static readonly DependencyProperty BoolEffectParameterProperty = DependencyProperty.RegisterAttached("BoolEffectParameter",
          typeof(bool), typeof(Effects), new PropertyMetadata(true));
        #endregion

        #region DoubleEffectParameter
        public static void SetDoubleEffectParameter(UIElement element, Double value)
        {
            element.SetValue(DoubleEffectParameterProperty, value);
        }

        public static Double GetDoubleEffectParameter(UIElement element)
        {
            return (Double)element.GetValue(DoubleEffectParameterProperty);
        }

        public static readonly DependencyProperty DoubleEffectParameterProperty = DependencyProperty.RegisterAttached("DoubleEffectParameter",
          typeof(Double), typeof(Effects), new PropertyMetadata(0.0));
        #endregion

        #region GradientEffectComposition
        public static void SetGradient(UIElement element, LinearGradientBrush value)
        {
            element.SetValue(Gradientroperty, value);
        }
        public static LinearGradientBrush GetGradient(UIElement element)
        {
            return (LinearGradientBrush)element.GetValue(Gradientroperty);
        }

        public static readonly DependencyProperty Gradientroperty = DependencyProperty.RegisterAttached("Gradient",
           typeof(LinearGradientBrush), typeof(Effects), new PropertyMetadata(null, GradientForegroundChanged));

        private static void GradientForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newGradient = e.NewValue as LinearGradientBrush;

            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as GradientEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new GradientEffectComposition(d as UIElement, newGradient);
                    SetEffectComposition(d as UIElement, effectComposition);

                }
                else
                {
                    effectComposition.SetGradient(newGradient);
                }
            }

        }
        #endregion

        #region AppearEffectComposition
        public static void SetAppear(UIElement element, bool value)
        {
            element.SetValue(AppearProperty, value);
        }
        public static bool GetAppear(UIElement element)
        {
            return (bool)element.GetValue(AppearProperty);
        }

        public static readonly DependencyProperty AppearProperty = DependencyProperty.RegisterAttached("Appear",
           typeof(bool), typeof(Effects), new PropertyMetadata(false, AppearChanged));

        private static void AppearChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as AppearEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new AppearEffectComposition(d as UIElement);
                    SetEffectComposition(d as UIElement, effectComposition);

                }
                else
                {
                    effectComposition.Animate();
                }
            }

        }
        #endregion

        #region FadeOutEffectComposition
        public static void SetFadeOut(UIElement element, bool value)
        {
            element.SetValue(FadeOutProperty, value);
        }
        public static bool GetFadeOut(UIElement element)
        {
            return (bool)element.GetValue(FadeOutProperty);
        }

        public static readonly DependencyProperty FadeOutProperty = DependencyProperty.RegisterAttached("FadeOut",
           typeof(bool), typeof(Effects), new PropertyMetadata(false, FadeOutChanged));

        private static void FadeOutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as FadeOutEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new FadeOutEffectComposition(d as UIElement);
                    SetEffectComposition(d as UIElement, effectComposition);

                }
                else
                {
                    effectComposition.Animate();
                }
            }

        }
        #endregion

        #region DisappearEffectComposition
        public static void SetDisappear(UIElement element, bool value)
        {
            element.SetValue(DisappearProperty, value);
        }
        public static bool GetDisappear(UIElement element)
        {
            return (bool)element.GetValue(DisappearProperty);
        }

        public static readonly DependencyProperty DisappearProperty = DependencyProperty.RegisterAttached("Disappear",
           typeof(bool), typeof(Effects), new PropertyMetadata(false, DisappearChanged));

        private static void DisappearChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as DisappearEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new DisappearEffectComposition(d as UIElement);
                    SetEffectComposition(d as UIElement, effectComposition);

                }
                else
                {
                    effectComposition.Animate();
                }
            }

        }
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

            if (d is UIElement)
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

        #region UIElementParameter
        public static void SetElementParameter(UIElement element, UIElement value)
        {
            element.SetValue(ElementParameterProperty, value);
        }

        public static UIElement GetElementParameter(UIElement element)
        {
            return (UIElement)element.GetValue(ElementParameterProperty);
        }

        public static readonly DependencyProperty ElementParameterProperty = DependencyProperty.RegisterAttached("ElementParameter",
          typeof(UIElement), typeof(Effects), new PropertyMetadata(null));
        #endregion

        #region ParallaxEffectComposition
        public static void SetParallax(UIElement element, double value)
        {
            element.SetValue(ParallaxProperty, value);
        }
        public static double GetParallax(UIElement element)
        {
            return (double)element.GetValue(ParallaxProperty);
        }

        public static readonly DependencyProperty ParallaxProperty = DependencyProperty.RegisterAttached("Parallax",
           typeof(double), typeof(Effects), new PropertyMetadata(0.0, ParallaxChanged));

        private static void ParallaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parallax = (double)e.NewValue;

            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as ParallaxEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new ParallaxEffectComposition(d as UIElement, parallax);
                    SetEffectComposition(d as UIElement, effectComposition);

                }
            }

        }
        #endregion
    }

    public class FadeOutEffectComposition : DependencyObject
    {
        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public UIElement Element;

        #region Animations
        CompositionAnimation FadeOutAnimation;
        double delay = 0;
        double duration = 1500;
        private void InitializeAnimations()
        {
            FadeOutAnimation = Compositor.FadeOut(duration);
        }

        public async void Animate()
        {
            if (delay > 0)
            {
                await Task.Delay((int)(delay * 1000.0));
            }

            AnimateOpacity();
        }

        private void AnimateOpacity()
        {
            ContainerVisual.StartAnimation("Opacity", FadeOutAnimation);
        }

        #endregion

        #region Constructor
        public FadeOutEffectComposition(UIElement element)
        {
            ContainerVisual = element.GetContainerVisual();

            ContainerVisual.Opacity = 0f;

            Compositor = ContainerVisual.Compositor;
            Element = element as UIElement;
            CheckParameters();


            InitializeAnimations();
            Animate();

        }
        #endregion

        public void CheckParameters()
        {
            var newduration = Effects.GetDoubleEffectParameter(Element);
            if (newduration > 0)
                duration = newduration * 1000;

            delay = Effects.GetDelay(Element);
        }
    }

    public class GradientEffectComposition : DependencyObject
    {
        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public Panel Element;
        public LinearGradientBrush Gradient;

        #region Animations
        CompositionAnimation LandAnimation;
        private void InitializeAnimations()
        {
            //LandAnimation = Compositor.Land();
        }
        #endregion

        public GradientEffectComposition(UIElement element, LinearGradientBrush gradient)
        {
            Element = element as Panel;
            Gradient = gradient;

            ContainerVisual = element.GetContainerVisual();
            Compositor = ContainerVisual.Compositor;

            InitializeAnimations();
            SetGradient(gradient);
        }

        public SpriteVisual GradientVisual;
        public ContainerVisual ChildContainerVisual;
        public async void SetGradient(LinearGradientBrush gradient)
        {
            GradientVisual = Compositor.CreateChildSprite(Element);
            var size = await Element.GetSize();

            ChildContainerVisual = Compositor.CreateChildContainer(Element);
            ChildContainerVisual.Children.InsertAtTop(GradientVisual);

            GradientVisual.Size = size;

            //To implement LinearGradientCompositionBrush

        }

    }

    public class AppearEffectComposition : DependencyObject
    {
        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public UIElement Element;

        #region Animations
        CompositionAnimation FadeOutAnimation;
        CompositionAnimation LandAnimation;
        bool useOpacity = true;
        double delay = 0;
        double duration = 1500;
        private void InitializeAnimations()
        {
            FadeOutAnimation = Compositor.FadeOut(1500);
            LandAnimation = Compositor.Land(500,duration);
        }

        public async void Animate()
        {
            var column = Effects.GetColumn(Element);

            if (column > 0)
            {
                var scroller = await ElementManager.GetScrollViewer(Element);
                if (scroller != null)
                {
                    var page = await ElementManager.GetPage(scroller as FrameworkElement);

                    while(scroller.HorizontalOffset < page.ActualWidth * column)
                    {
                        await Task.Delay(250);
                    }
                }
            }

            if (delay > 0)
            {
                await Task.Delay((int)(delay * 1000.0));
            }

            if (useOpacity)
            {
                AnimateOpacity();
            }

            AnimateOffset();
        }

        private void AnimateOpacity()
        {
            ContainerVisual.StartAnimation("Opacity", FadeOutAnimation);
        }

        private void AnimateOffset()
        {
            ContainerVisual.StartAnimation("Offset", LandAnimation);
        }

        #endregion

        #region Constructor
        public AppearEffectComposition(UIElement element)
        {
            ContainerVisual = element.GetContainerVisual();
            
            Compositor = ContainerVisual.Compositor;
            Element = element as UIElement;
            CheckParameters();

            InitializeAnimations();
            Animate();

        }
        #endregion

        public void CheckParameters()
        {
            #region Set Opacity Animation
            var effectParameter = Effects.GetBoolEffectParameter(Element);
            if (effectParameter == false)
            {
                useOpacity = false;
                ContainerVisual.Opacity = 1f;
                ContainerVisual.Offset = new Vector3(0f, 2000f, 0.0f);
            }
            else
            {
                ContainerVisual.Opacity = 0f;
                ContainerVisual.Offset = new Vector3(0f, 500f, 0.0f);
            }
            #endregion

            if(Effects.GetDoubleEffectParameter(Element)>0)
            {
                duration = Effects.GetDoubleEffectParameter(Element) * 1000;
            }

            delay = Effects.GetDelay(Element);
        }
    }

    public class DisappearEffectComposition : DependencyObject
    {
        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public UIElement Element;

        #region Animations
        CompositionAnimation FadeInAnimation;
        CompositionAnimation TakeOffAnimation;
        bool useOpacity = true;
        double delay = 0;
        private async Task InitializeAnimations()
        {
            FadeInAnimation = Compositor.FadeIn(1500);

            var elementFramework = Element as FrameworkElement;

            while (elementFramework.ActualHeight <= 0)
                await Task.Delay(60);

            var altitude = elementFramework.ActualHeight + elementFramework.Margin.Top;

            TakeOffAnimation = Compositor.TakeOff(500 + altitude,1500);
        }

        public async void Animate()
        {
            if (delay > 0)
            {
                await Task.Delay((int)(delay * 1000.0));
            }

            if (useOpacity)
            {
                AnimateOpacity();
            }

            AnimateOffset();
        }

        private void AnimateOpacity()
        {
            ContainerVisual.StartAnimation("Opacity", FadeInAnimation);
        }

        private void AnimateOffset()
        {
            ContainerVisual.StartAnimation("Offset", TakeOffAnimation);
        }

        #endregion

        #region Constructor
        public DisappearEffectComposition(UIElement element)
        {
            ContainerVisual = element.GetContainerVisual();

            Compositor = ContainerVisual.Compositor;
            Element = element as UIElement;

            CheckParameters();

        }
        #endregion

        public async void CheckParameters()
        {
            #region Set Opacity Animation
            var effectParameter = Effects.GetBoolEffectParameter(Element);
            if (effectParameter == false)
            {
                useOpacity = false;
                ContainerVisual.Opacity = 1f;
            }
            else
            {
                ContainerVisual.Opacity = 0f;
            }
            #endregion

            delay = Effects.GetDelay(Element);

            await InitializeAnimations();
            Animate();
        }
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
            else if(input.Contains("http"))
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

            Brush.StartAnimation("effect.Source1Amount", FadeOutAnimation);

            if (both)
            {
                Brush.StopAnimation("effect.Source2Amount");
                Brush.StartAnimation("effect.Source2Amount", FadeInAnimation);
            }
        }

        internal void SetForeground(string newUri)
        {
            var newImageBrush = CreateImageBrush(newUri);

            Brush.SetSourceParameter("Source1", newImageBrush);

            if (LastImageBrush == null)
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

    public class ParallaxEffectComposition : DependencyObject
    {
        private UIElement scroller;
        private CompositionPropertySet scrollerViewerManipulation;
        private Compositor scrollerCompositor;

        public ParallaxEffectComposition(UIElement element, double value)
        {
            Initialize(element, value);
        }

        private async void Initialize(UIElement element,double value)
        {
            //1.- Find its scrollviewer and compositor 
            while(scroller == null)
            {
                scroller = Effects.GetElementParameter(element);
                await Task.Delay(500);
            }
            scrollerViewerManipulation = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(scroller as ScrollViewer);
            scrollerCompositor = scrollerViewerManipulation.Compositor;

            //2.- Apply Effect
            CreateScrollExpression(element, (float)value);
        }

        private void CreateScrollExpression(UIElement element, float factor)
        {
            ExpressionAnimation expression = scrollerCompositor.CreateExpressionAnimation("ScrollManipulation.Translation.X * ParallaxMultiplier");
            expression.SetScalarParameter("ParallaxMultiplier", factor);
            expression.SetReferenceParameter("ScrollManipulation", scrollerViewerManipulation);

            Visual imageVisual = ElementCompositionPreview.GetElementVisual(element);
            imageVisual.StartAnimation("Offset.X", expression);
        }
    }
}
