using CompositionProToolkit;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace AnimatedControls
{
    /// <summary>
    /// This Element contains the different parts needed when using the Visual Layer (Composition)
    /// </summary>
    public class UniversalElement
    {
        public FrameworkElement Element;

        public Visual ElementVisual;
        public Compositor Compositor { get { return ElementVisual.Compositor; } }


        public ICompositionMaskGenerator Masker;

        public UniversalElement(object sender)
        {
            Element = sender as FrameworkElement;

            ElementVisual = ElementCompositionPreview.GetElementVisual(Element);

            Masker = CompositionMaskFactory.GetCompositionMaskGenerator(Compositor);

        }

        #region Sizes
        public Rect RectSize(double ox = 0, double oy = 0, double fx = 0, double fy = 0)
        {
            return new Rect(0 + ox, 0 + oy, Element.ActualWidth + fx, Element.ActualHeight + fy);
        }
        #endregion

        #region Visual
        public SpriteVisual Visual;
        public void CreateVisual()
        {
            Visual = Compositor.CreateSpriteVisual();
            Visual.Size = Element.RenderSize.ToVector2();
        }
        #endregion

        #region Mask
        public async Task<ICompositionMask> CreateGeometryMask(CanvasGeometry geometry)
        {
            CreateVisual();
            var gMask = await Masker.CreateMaskAsync(Visual.Size.ToSize(), geometry);
            return gMask;
        }
        #endregion

        #region MaskBrush
        /// <summary>
        /// You can call this directly from start
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public async Task CreateMaskBrush(CanvasGeometry geometry, Color color)
        {
            var geometryMask = await CreateGeometryMask(geometry);

            var maskBrush = Compositor.CreateMaskBrush();

            maskBrush.Mask = Compositor.CreateSurfaceBrush(geometryMask.Surface);
            maskBrush.Source = Compositor.CreateColorBrush(color);

            Visual.Brush = maskBrush;
        }
        #endregion
    }
}
