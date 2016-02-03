using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UniversalComposition;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace SquaresComposition.Attached
{
    public class Effects : DependencyObject
    {
        #region Tag
        public static void SetEffectComposition(UIElement element, DependencyObject value)
        {
            element.SetValue(EffectCompositionProperty, value);
        }
        public static DependencyObject GetEffectComposition(UIElement element)
        {
            return (DependencyObject)element.GetValue(EffectCompositionProperty);
        }

        public static readonly DependencyProperty EffectCompositionProperty = DependencyProperty.RegisterAttached("EffectComposition",
          typeof(DependencyObject), typeof(Effects), new PropertyMetadata(null));

        #endregion

        #region SquaresEffectComposition
        public static void SetSquares(UIElement element, bool value)
        {
            element.SetValue(SquaresProperty, value);
        }

        public static bool GetSquares(UIElement element)
        {
            return (bool)element.GetValue(SquaresProperty);
        }

        public static readonly DependencyProperty SquaresProperty = DependencyProperty.RegisterAttached("Squares",
         typeof(bool), typeof(Effects), new PropertyMetadata(false, OnSquaresChanged));

        private static void OnSquaresChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement)
            {
                var element = d as UIElement;

                var effectComposition = GetEffectComposition(element) as SquaresEffectComposition;

                if (effectComposition == null)
                {
                    effectComposition = new SquaresEffectComposition(d as UIElement, (bool)e.NewValue);
                    SetEffectComposition(d as UIElement, effectComposition);
                }
                else
                {
                    effectComposition.Animate((bool)e.NewValue);
                }
            }
        }

        #endregion
    }

    public class SquaresEffectComposition : DependencyObject
    {
        public static int SquareSize = 120;

        public Compositor Compositor;
        public ContainerVisual ContainerVisual;
        public UIElement Element;
        public ContainerVisual SquaresVisual;
        #region Animations
        CompositionAnimation SquaresDownAnimation;
        CompositionAnimation SquaresUpAnimation;
        private void InitializeAnimations()
        {
            SquaresDownAnimation = Compositor.RotateClock();
            SquaresUpAnimation = Compositor.RotateUnClock();
        }

        public void Animate(bool newvalue)
        {
            AnimateOffset(newvalue);
        }

        private void AnimateOffset(bool newvalue)
        {
            var animation = newvalue ? SquaresDownAnimation : SquaresUpAnimation;

            foreach (var square in squares)
            {
                square.StartAnimation("RotationAngle", animation);
            }
        }
        #endregion

        #region Constructor
        public SquaresEffectComposition(UIElement element, bool newvalue)
        {
            ContainerVisual = element.GetContainerVisual();
            Compositor = ContainerVisual.Compositor;
            Element = element;
            SquaresVisual = Compositor.CreateChildContainer(Element);


            CreateSquares();
            InitializeAnimations();
            Animate(newvalue);
        }
        #endregion

        #region CreateSquares
        List<Visual> squares = new List<Visual>();
        private async void CreateSquares()
        {
            var elementFramework = Element as FrameworkElement;

            while (elementFramework.ActualWidth == 0)
                await Task.Delay(60);

            var horizontal = elementFramework.ActualWidth / SquareSize;
            var vertical = elementFramework.ActualHeight / SquareSize;

            for(int i=0;i<horizontal;i++)
            {
                for(int j=0;j<vertical;j++)
                {
                    var visual = CreateRandomSquare(i,j);

                    squares.Add(visual.Children.First());
                    squares.Add(visual.Children.Last());

                    SquaresVisual.Children.InsertAtTop(visual);
                }
            }
        }

        private Random Generator = new Random(DateTime.Now.Millisecond);

        

        private ContainerVisual CreateRandomSquare(int x, int y)
        {
            ContainerVisual squareVisual = Compositor.CreateContainerVisual();

            var factor = Generator.Next(1, 5);
            var posX = x * SquareSize;
            var posY = y * SquareSize;
            SpriteVisual visual = Compositor.CreateSpriteVisual();
            visual.Brush = Compositor.CreateColorBrush(Colors.White);
            visual.Size = new Vector2(SquareSize);
            visual.Offset = new Vector3(posX, posY, 0);
            visual.CenterPoint = new Vector3(SquareSize * 0.5f);


            SpriteVisual shadowVisual = Compositor.CreateSpriteVisual();
            shadowVisual.Brush = Compositor.CreateColorBrush(Colors.Gray);
            shadowVisual.Size = new Vector2(SquareSize + 8);
            shadowVisual.Offset = new Vector3(posX - 2, posY - 2, 0);
            shadowVisual.CenterPoint = new Vector3(SquareSize * 0.5f + 4);

            squareVisual.Children.InsertAtTop(shadowVisual);
            squareVisual.Children.InsertAtTop(visual);

            return squareVisual;
        }
     
        #endregion
    }
}
