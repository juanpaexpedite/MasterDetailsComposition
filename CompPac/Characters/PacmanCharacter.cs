using CompPac.Common;
using CompPac.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace CompPac.Characters
{
    public sealed class PacmanCharacter : Composable
    {
        #region Constructor and Events
        public PacmanCharacter()
        {
            this.DefaultStyleKey = typeof(PacmanCharacter);
        }

        protected override void OnApplyTemplate()
        {
            MainElement = (FrameworkElement)GetTemplateChild("PacCanvas");
            
            size = (float)MainElement.Width;

            InitializeComposition(WindowManager.Bounds());
        }
        #endregion

        #region Interop
        private Directions direction;
        private float halfPi = (float)Math.PI / 2.0f;
        private float size = 0;
        private float angle = 0f;

        private float x = 0f;
        private float y = 0f;

        private float displacement = 5f;

        private float dx = 0;
        private float dy = 0;
        public void ChangeTo(Directions newdirection)
        {
            direction = newdirection;
            angle = (float)direction * halfPi;
        }
        #endregion

        #region Composable
        private void UpdatePosition(Vector2 bounds)
        {
            if (direction == Directions.Up)
            {
                dx = 0;
                dy = -displacement;
                
            }
            else if (direction == Directions.Down)
            {
                dx = 0;
                dy = displacement;

            }
            else if (direction == Directions.Left)
            {
                dx = -displacement;
                dy = 0;
            }
            else if (direction == Directions.Right)
            {
                dx =displacement;
                dy = 0;
            }

            if (CheckBounds(bounds))
            {
                y = y + dy;
                x = x + dx;
            }
        }

        private bool CheckBounds(Vector2 bounds)
        {
            foreach(var rect in MapManager.Map)
            {
                var rectPac = new Rect(x + dx, y + dy, size, size);
                rectPac.Intersect(rect);
                if(!rectPac.IsEmpty)
                    return false;
            }

            return true;

            //if (x < -size)
            //{
            //    x = bounds.X;
            //}
            //else if (x > bounds.X)
            //{
            //    x = 0;
            //}

            //if (y < -size)
            //{
            //    y = bounds.Y;
            //}
            //else if (y > bounds.Y)
            //{
            //    y = 0;
            //}
        }

        public override void UpdateComposition(Vector2 bounds)
        {
            UpdatePosition(bounds);
            Visual.RotationAngle = angle;
            Visual.Offset = new Vector3(x, y, 0);
        }
        #endregion
    }
}
