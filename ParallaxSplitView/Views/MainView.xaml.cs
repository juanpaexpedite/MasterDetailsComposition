using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using ParallaxSplitView.Attached;
using ParallaxSplitView.Models;
using ParallaxSplitView.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UniversalComposition;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ParallaxSplitView.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        #region Photos
        public ObservableCollection<String> Photos
        {
            get { return (ObservableCollection<String>)GetValue(PhotosProperty); }
            set { SetValue(PhotosProperty, value); }
        }

        public static readonly DependencyProperty PhotosProperty =
            DependencyProperty.Register(nameof(Photos), typeof(ObservableCollection<String>), typeof(MainView), new PropertyMetadata(null));

        private void InitializePhotos()
        {
            Photos = new ObservableCollection<String>()
            {
                "Assets/Photos/_05A4768_th.jpg",
                "Assets/Photos/_20A7121_th.jpg",
                "Assets/Photos/_20A7185_th.jpg",
                "Assets/Photos/_20A7187_th.jpg",
                "Assets/Photos/_K7A4966_th.jpg",
                "Assets/Photos/build000220150429_th.jpg",
                "Assets/Photos/build00220150429_th.jpg",
                "Assets/Photos/build0220150429_th.jpg",
                "Assets/Photos/build0120150429_th.jpg",
                "Assets/Photos/build1320150429_th.jpg",
                "Assets/Photos/build1220150429_th.png",
                "Assets/Photos/build1020150429_th.jpg",
                "Assets/Photos/build420150428_th.jpg",
                "Assets/Photos/build320150428_th.jpg",
                "Assets/Photos/build720150428_th.jpg",
                "Assets/Photos/build920150428_th.jpg",
                "Assets/Photos/build820150428_th.jpg",
                "Assets/Photos/build520150428_th.jpg",
                "Assets/Photos/build120150428_th.jpg",
                "Assets/Photos/build220150428_th.jpg",
                "Assets/Photos/build620150428_th.jpg"
            };

            this.Loaded += (s, e) =>
             {
                 InvalidatePhoto(Photos.First());
             };

            
        }

        private void InvalidatePhoto(string newuri)
        {
            GalleryImage.SetValue(Effects.BlendForegroundProperty, newuri);
        }
        #endregion

        #region Sessions
        public ObservableCollection<Session> Sessions
        {
            get { return (ObservableCollection<Session>)GetValue(SessionsProperty); }
            set { SetValue(SessionsProperty, value); }
        }

        public static readonly DependencyProperty SessionsProperty =
            DependencyProperty.Register(nameof(Sessions), typeof(ObservableCollection<Session>), typeof(MainView), new PropertyMetadata(null));

        private async void InitializeSessions()
        {
            Sessions = new ObservableCollection<Session>(await Ch9Service.GetBuild2015Sessions());
        }
        #endregion

        #region Session
        public Session CurrentSession
        {
            get { return (Session)GetValue(CurrentSessionProperty); }
            set { SetValue(CurrentSessionProperty, value); }
        }

        public static readonly DependencyProperty CurrentSessionProperty =
            DependencyProperty.Register(nameof(CurrentSession), typeof(Session), typeof(MainView), new PropertyMetadata(null, (d, e) => (d as MainView).InitializeSession()));

        private void SessionButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CurrentSession = (sender as FrameworkElement).DataContext as Session;
        }

        private async void InitializeSession()
        {
            if (CurrentSession.VideoUri == null)
            {
                await Launcher.LaunchUriAsync(new Uri(CurrentSession.WebUri));
            }
            else
            {
                SessionWaiting.IsActive = true;
                SessionMedia.Source = new Uri(CurrentSession.VideoUri);
                currentgrid = 4;
                Scroll();
            }
        }

        private void InitializeMedia()
        {
            SessionMedia.MediaOpened += (s, e) =>
            {
                SessionWaiting.IsActive = false;
            };
            
        }
        #endregion

        public MainView()
        {
            this.InitializeComponent();

            InitializePhotos();
            
            InitializeMedia();
            InitializeEvents();

            this.SizeChanged += MainView_SizeChanged;

            this.Loaded += async (s, e) =>
            {
               await Task.Delay(5000);
               InitializeSessions();
            };
        }

        
        #region Scroller
        private void Scroll()
        {
            Scroller.ChangeView(screenWidth * currentgrid, null, null);
        }
        #endregion

        #region Button Events
        private void InitializeEvents()
        {
            FirstButton.Tapped += ScrollButton_Tapped;
            SecondButton.Tapped += ScrollButton_Tapped;
            ThirdButton.Tapped += ScrollButton_Tapped;
        }

        int currentgrid = 0;
        private void ScrollButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender.Equals(FirstButton))
                currentgrid = 1;
            else if (sender.Equals(SecondButton))
                currentgrid = 2;
            else if (sender.Equals(ThirdButton))
                currentgrid = 3;
            Scroll();
        }

     
        #endregion

        #region Stretch Border Elements
        double screenWidth = 480;
        double screenHeight = 800;
        private async void MainView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            screenWidth = e.NewSize.Width;
            screenHeight = e.NewSize.Height;
            foreach (var definition in ContentRoot.ColumnDefinitions)
            {
                definition.Width = new GridLength(screenWidth);
            }

            //To Keep current grid
            await Task.Delay(30);
            Scroll();
        }
        #endregion

        #region Image Tiles
        private void Image_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var uri = (String)(sender as FrameworkElement).DataContext;
            InvalidatePhoto(uri);
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var uri = (String)(sender as FrameworkElement).DataContext;
            InvalidatePhoto(uri);
        }



        #endregion

        private void SessionMedia_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (SessionMedia.CurrentState == MediaElementState.Paused)
            {
                SessionMedia.Play();
            }
            else if(SessionMedia.CanPause)
            {
                SessionMedia.Pause();
            }
        }

        private void SessionMedia_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            SessionMedia.IsFullWindow = !SessionMedia.IsFullWindow;
        }
    }
}
