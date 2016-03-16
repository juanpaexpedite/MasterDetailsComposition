using CompPac.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CompPac.Characters
{
    public abstract class Composable : Control
    {
        public ContainerVisual Visual;
        public Compositor Compositor;
        public FrameworkElement MainElement;

        public virtual void InitializeComposition(Vector2 bounds)
        {
            Visual = MainElement.GetContainerVisual();
            Compositor = Visual.Compositor;

            float halfwidth = (float)MainElement.Width / 2.0f;
            float halfheight = (float)MainElement.Height / 2.0f;

            Visual.CenterPoint = new Vector3(halfwidth, halfwidth, 0);
            UpdateComposition(bounds);
        }

        public abstract void UpdateComposition(Vector2 bounds);
    }
}
