using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UniversalComposition
{
    public static class ElementManager
    {
        public static async Task<ScrollViewer> GetScrollViewer(UIElement element)
        {
            var parent = VisualTreeHelper.GetParent(element as DependencyObject);

            while (parent == null)
            {
                await Task.Delay(100);
                parent = VisualTreeHelper.GetParent(element as DependencyObject);
            }

            if (parent is ScrollViewer)
                return parent as ScrollViewer;

            while (parent != null && !(parent is ScrollViewer))
            {
                parent = VisualTreeHelper.GetParent(parent as DependencyObject);
            }

            return (ScrollViewer)parent;

        }

        public static UIElement GetParentInScroller(UIElement element)
        {
            var parent = VisualTreeHelper.GetParent(element as DependencyObject);

            if (parent is ScrollViewer)
                return element;

            while (parent !=null)
            {
                var newparent = VisualTreeHelper.GetParent(parent as DependencyObject);

                if(newparent is ScrollViewer)
                {
                    return parent as UIElement;
                }

                parent = newparent;
            }

            return null;

        }


        public static async Task<FrameworkElement> GetPage(UIElement element)
        {
            var parent = VisualTreeHelper.GetParent(element as DependencyObject);

            while(parent == null)
            {
                await Task.Delay(100);
                parent = VisualTreeHelper.GetParent(element as DependencyObject);
            }

            if (parent is Page)
                return parent as FrameworkElement;

            while(parent != null && !(parent is Page) )
            {
                parent = VisualTreeHelper.GetParent(parent as DependencyObject);
            }

            return (FrameworkElement)parent;

        }

        public static bool IsElementVisible(FrameworkElement element, ScrollViewer container)
        {
            return element.Margin.Left < container.HorizontalOffset;
        }

        public static bool IsElementFullVisible(FrameworkElement element, FrameworkElement container)
        {
            if (element.Visibility == Visibility.Collapsed)
                return false;

            var elementSize = new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight);

            Rect bounds = element.TransformToVisual(container).TransformBounds(elementSize);

            var parentBounds = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);

            return parentBounds.Contains(new Point(bounds.Left, bounds.Top)) && parentBounds.Contains(new Point(bounds.Right, bounds.Bottom));
        }

        public static bool IsElementPartialVisible(FrameworkElement element, FrameworkElement container)
        {
            if (element.Visibility == Visibility.Collapsed)
                return false;

            var elementSize = new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight);

            Rect bounds = element.TransformToVisual(container).TransformBounds(elementSize);

            var parentBounds = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);

            return parentBounds.Contains(new Point(bounds.Left, bounds.Top)) || parentBounds.Contains(new Point(bounds.Right, bounds.Bottom));
        }



    }


}
