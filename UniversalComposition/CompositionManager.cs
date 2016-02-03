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
using Windows.UI.Xaml.Hosting;

namespace UniversalComposition
{
    public static class CompositionManager
    {
        public static ContainerVisual GetContainerVisual(this UIElement element)
        {
            return ElementCompositionPreview.GetElementVisual(element) as ContainerVisual;
        }


        public static CompositionSurfaceBrush CreateBrushFromAsset(this Compositor compositor, Uri uri, out Size size)
        {
            CompositionImageFactory surfaceFactory = CompositionImageFactory.CreateCompositionImageFactory(compositor);
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

        public static async Task<Vector2> GetSize(this UIElement element)
        {
            var elementFramework = element as FrameworkElement;

            while (elementFramework.ActualWidth == 0)
                await Task.Delay(60);

            return new Vector2((float)elementFramework.ActualWidth, (float)elementFramework.ActualHeight);
        }

        public static void ResizeImage(this SpriteVisual sprite, Size size, double imageAspectRatio)
        {
            sprite.Size = new Vector2((float)size.Width, (float)size.Height);
        }

    }
}
