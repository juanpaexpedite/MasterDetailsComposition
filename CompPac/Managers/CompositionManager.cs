using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace CompPac.Managers
{
    public static class CompositionManager
    {
        public static ContainerVisual GetContainerVisual(this UIElement element)
        {
            return ElementCompositionPreview.GetElementVisual(element) as ContainerVisual;
        }
    }
}
