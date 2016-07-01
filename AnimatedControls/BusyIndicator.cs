using CompositionProToolkit;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace AnimatedControls
{
    public class BusyIndicator : Control
    {
        public BusyIndicator()
        {
            this.Loaded += BusyIndicator_Loaded;
        }

        #region Visual Layer
        private Compositor compositor;
        private SpriteVisual ellipse_0;
        private SpriteVisual ellipse_1;
        private SpriteVisual ellipse_2;
        private SpriteVisual ellipse_3;
        private ICompositionMaskGenerator generator;
        private ContainerVisual container;
        private List<CompositionAnimation> animations = new List<CompositionAnimation>();
        #endregion

        public bool IsReady = false;



        private async void BusyIndicator_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            generator = CompositionMaskFactory.GetCompositionMaskGenerator(compositor);

            container = compositor.CreateContainerVisual();
            container.Size = new Vector2(60, 60);
            container.Offset = new Vector3(-30, -30, 0);

            float size = 30;

            var color = Color.FromArgb(255, 0xD2, 0x42, 0x29);

            ellipse_0 = await CreateEllipseVisual(color, size, size);
            ellipse_1 = await CreateEllipseVisual(color, size, size);
            ellipse_2 = await CreateEllipseVisual(color, size, size);
            ellipse_3 = await CreateEllipseVisual(color, size, size);

            container.Children.InsertAtBottom(ellipse_0);
            container.Children.InsertAtBottom(ellipse_1);
            container.Children.InsertAtBottom(ellipse_2);
            container.Children.InsertAtBottom(ellipse_3);

            ElementCompositionPreview.SetElementChildVisual(this, container);

            animations.Add(AnimateScale(ellipse_0));
            animations.Add(AnimateScale(ellipse_1, true));
            animations.Add(AnimateScale(ellipse_2));
            animations.Add(AnimateScale(ellipse_3, true));

            animations.Add(AnimateRotation(ellipse_0, 0));
            animations.Add(AnimateRotation(ellipse_1, 0.25f * (float)Math.PI));
            animations.Add(AnimateRotation(ellipse_2, 0.5f * (float)Math.PI));
            animations.Add(AnimateRotation(ellipse_3, 0.75f * (float)Math.PI));

            IsReady = true;
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(BusyIndicator), new PropertyMetadata(false, (d,e)=> (d as BusyIndicator).Invalidate()));


        private async void Invalidate()
        {
            while(!IsReady)
            {
                await Task.Delay(200);
            }

            if(IsBusy)
            {
                ellipse_0.StartAnimation("Scale", animations[0]);
                ellipse_0.StartAnimation("RotationAngle", animations[0 + 4]);

                ellipse_1.StartAnimation("Scale", animations[1]);
                ellipse_1.StartAnimation("RotationAngle", animations[1 + 4]);

                ellipse_2.StartAnimation("Scale", animations[2]);
                ellipse_2.StartAnimation("RotationAngle", animations[2 + 4]);

                ellipse_3.StartAnimation("Scale", animations[3]);
                ellipse_3.StartAnimation("RotationAngle", animations[3 + 4]);
            }
            else
            {
                ellipse_0.StopAnimation("Scale");
                ellipse_1.StopAnimation("Scale");
                ellipse_2.StopAnimation("Scale");
                ellipse_3.StopAnimation("Scale");


                ellipse_0.StopAnimation("RotationAngle");
                ellipse_1.StopAnimation("RotationAngle");
                ellipse_2.StopAnimation("RotationAngle");
                ellipse_3.StopAnimation("RotationAngle");
            }
        }

        #region Animate
        private CompositionAnimation AnimateScale(SpriteVisual visual, bool horizontal = true)
        {
            float max = 1.1f;
            float min = 0.5f;
            var linear = compositor.CreateLinearEasingFunction();
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(1, 1, 1), linear);
            animation.InsertKeyFrame(0.5f, new Vector3(horizontal ? max : min, horizontal ? min : max, 1), linear);
            animation.InsertKeyFrame(1.0f, new Vector3(1, 1, 1), linear);
            animation.Duration = TimeSpan.FromMilliseconds(2000);
            animation.IterationBehavior = AnimationIterationBehavior.Forever;
            animation.StopBehavior = AnimationStopBehavior.SetToInitialValue;
            return animation;
        }

        private CompositionAnimation AnimateRotation(SpriteVisual visual, float offset)
        {
            var linear = compositor.CreateLinearEasingFunction();
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, 0f + offset, linear);
            animation.InsertKeyFrame(1.0f, 6.2831853f + offset, linear);
            animation.Duration = TimeSpan.FromMilliseconds(2000);
            animation.IterationBehavior = AnimationIterationBehavior.Forever;
            animation.StopBehavior = AnimationStopBehavior.SetToInitialValue;
            return animation;
        }
        #endregion

        private async Task<SpriteVisual> CreateEllipseVisual(Color color, float radiusX = 20, float radiusY = 20, float strokeWidth = 2)
        {
            var ellipse = compositor.CreateSpriteVisual();
            ellipse.Size = new Vector2(2 * (radiusX + strokeWidth), 2 * (radiusY + strokeWidth));

            var ellipseGeometry = CreateOutlinedEllipseGeometry(new Vector2(radiusX + strokeWidth, radiusY + strokeWidth), radiusX, radiusY, strokeWidth);

            ellipse.Brush = await CreateMask(color, ellipseGeometry, ellipse.Size);
            ellipse.CenterPoint = new Vector3(radiusX + strokeWidth, radiusY + strokeWidth, 0);
            return ellipse;
        }

        private async Task<CompositionMaskBrush> CreateMask(Color color, CanvasGeometry geometry, Vector2 size)
        {
            var compositionMask = await generator.CreateMaskAsync(size.ToSize(), geometry);

            var mask = compositor.CreateSurfaceBrush(compositionMask.Surface);
            var source = compositor.CreateColorBrush(color);

            var maskBrush = compositor.CreateMaskBrush();
            maskBrush.Mask = mask;
            maskBrush.Source = source;

            return maskBrush;
        }

        private CanvasGeometry CreateOutlinedEllipseGeometry(Vector2 center, float radiusX, float radiusY, float strokewidth)
        {
            var inner = CanvasGeometry.CreateEllipse(generator.Device, center, radiusX, radiusY);
            var outer = CanvasGeometry.CreateEllipse(generator.Device, center, radiusX + strokewidth, radiusY + strokewidth);

            return outer.CombineWith(inner, Matrix3x2.Identity, CanvasGeometryCombine.Exclude);
        }
    }
}
