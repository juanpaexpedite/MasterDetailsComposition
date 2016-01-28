using Microsoft.UI.Composition.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace MasterDetailsComposition.Managers
{
    public static class CompositionManager
    {
        public static ContainerVisual GetContainerVisual(this UIElement element)
        {
            return ElementCompositionPreview.GetElementVisual(element) as ContainerVisual;
        }

       
        public static CompositionSurfaceBrush CreateBrushFromAsset(this Compositor compositor, Uri uri, out Size size)
        {
            CompositionImageFactory surfaceFactory  = CompositionImageFactory.CreateCompositionImageFactory(compositor);
            var image = surfaceFactory.CreateImageFromUri(uri);

            bool isCompleted = false;
            var waitHandle = new AutoResetEvent(false);
            image.ImageLoaded += (CompositionImage sender, CompositionImageLoadStatus status) =>
            {
                isCompleted = true;
                waitHandle.Set();
            };

            while (!isCompleted)
            {
                waitHandle.WaitOne();
            }

            size = image.Size;
            return compositor.CreateSurfaceBrush(image.Surface);
        }

        public static ContainerVisual CreateChildContainer(this Compositor compositor, UIElement element)
        {
            var root = compositor.CreateContainerVisual();
            ElementCompositionPreview.SetElementChildVisual(element, root);
            return root;
        }

        public static SpriteVisual CreateChildSprite(this Compositor compositor, UIElement element)
        {
            var sprite = compositor.CreateSpriteVisual();
            return sprite;
        }

        public static void ResizeImage(this SpriteVisual sprite, Size size, double imageAspectRatio)
        {
            sprite.Size = new Vector2((float)size.Width,(float)size.Height);
        }

    }

    public static class AnimationCompositionManager
    {
        #region Fall
        public static CompositionAnimation FallDown(this Compositor compositor, float slide = 1000, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(0f, slide, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(0f, 0f, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation FallUp(this Compositor compositor, float slide = 1000, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(0f, 0f, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(0f, slide, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }
        #endregion

        #region Slide
        public static CompositionAnimation SlideOut(this Compositor compositor, float slide = 1000, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(0f, 0f, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(slide, 0f, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation SlideIn(this Compositor compositor,float slide = 1000, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(slide, 0f, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(0f, 0f, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }
        #endregion

        #region Drop
        public static float DropFactor = 1.5f;

        public static CompositionAnimation DropOut(this Compositor compositor, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(0f, 0f, 0.0f));
            animation.InsertKeyFrame(0.5f, new Vector3(0f, DropFactor * 100f, 0.0f));
            animation.InsertKeyFrame(0.51f, new Vector3(0f, DropFactor* -100f, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(0f, 0f, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);

            return animation;
        }

        public static CompositionAnimation DropIn(this Compositor compositor, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(0f, 0f, 0.0f));
            animation.InsertKeyFrame(0.5f, new Vector3(0f, DropFactor* -100f, 0.0f));
            animation.InsertKeyFrame(0.51f, new Vector3(0f, DropFactor *100f, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(0f, 0f, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);

            return animation;
        }
        #endregion

        #region Fade
        public static CompositionAnimation FadeIn(this Compositor compositor, double duration = 1000)
        {
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertExpressionKeyFrame(0.0f, "0.0f");
            animation.InsertExpressionKeyFrame(1.0f, "1.0f");
            animation.Duration = TimeSpan.FromMilliseconds(duration);

            return animation;
        }

        public static CompositionAnimation FadeOut(this Compositor compositor, double duration = 1000)
        {
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertExpressionKeyFrame(0.0f, "1.0f");
            animation.InsertExpressionKeyFrame(1.0f, "0.0f");
            animation.Duration = TimeSpan.FromMilliseconds(duration);

            return animation;
        }
        #endregion
    }
}
