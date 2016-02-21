using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Particles2D.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Particles2D
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Constructor and Events
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
        #endregion

        #region Particles
        private ParticlesManager ParticlesManager;
        private void Initialize()
        {
            MainCanvas.CreateResources += MainCanvas_CreateResources;
            MainCanvas.SizeChanged += MainCanvas_SizeChanged;
            MainCanvas.Update += MainCanvas_Update;
            MainCanvas.Draw += MainCanvas_Draw;
        }

        private void MainCanvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            ParticlesManager = new ParticlesManager(MainCanvas,128);
            ParticlesManager.Initialize(blur:true, fall:true); //fall == true is like snow, false like cells
        }
        #endregion

        #region Win2D - XNA Style
        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ParticlesManager.UpdateBounds(e.NewSize);
        }
        private void MainCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            ParticlesManager.Update(args);
        }

        private void MainCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            ParticlesManager.Draw(args.DrawingSession);
        }
        #endregion
    }
}
