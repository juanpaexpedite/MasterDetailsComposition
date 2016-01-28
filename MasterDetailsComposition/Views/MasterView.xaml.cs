using MasterDetailsComposition.Attached;
using MasterDetailsComposition.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MasterDetailsComposition.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterView : Page
    {
        #region Constructor
        public MasterView()
        {
            this.InitializeComponent();

        }
        #endregion

        #region Game Item Templates
        private List<UIElement> itemElements = new List<UIElement>();
        private void ItemGrid_Loaded(object sender, RoutedEventArgs e)
        {
            itemElements.Add(sender as UIElement);
        }
        #endregion

        private void ItemGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var current = (sender as FrameworkElement).DataContext as Game;

            GotoDetails(sender);
            GotoBackDetails(current);
        }

        private async void GotoDetails(object selecteditem)
        {
            BackButton.Visibility = Visibility.Visible;
            
            foreach (var item in itemElements)
            {
                if (!item.Equals(selecteditem))
                {
                    await Task.Delay(10);
                    item.SetValue(Effects.FallProperty, true);
                }
            }

            await Task.Delay(30);
            (selecteditem as UIElement).SetValue(Effects.FallProperty, true);

            MainScroller.Visibility = Visibility.Collapsed;
        }

        private async void GotoBackDetails(Game current)
        {
            await Task.Delay(60);
            TitleLabel.SetValue(Effects.DropTextProperty, current.Name);
            TitleBackground.SetValue(Effects.BlendForegroundProperty, $"Assets/Backgrounds/{current.Code}.jpg");
            DetailsGrid.SetValue(Effects.SlideProperty, true);

        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BackButton.Visibility = Visibility.Collapsed;
            GotoMaster();

        }

        private async void GotoMaster()
        {

            MainScroller.Visibility = Visibility.Visible;
            TitleLabel.SetValue(Effects.DropTextProperty, "id software games");
            TitleBackground.SetValue(Effects.BlendForegroundProperty, "Assets/Backgrounds/masterbackground.jpg");
            DetailsGrid.SetValue(Effects.SlideProperty, false);

            foreach (var item in itemElements)
            {
                await Task.Delay(30);
                item.SetValue(Effects.FallProperty, false);
            }
        }

        
    }
    
}
