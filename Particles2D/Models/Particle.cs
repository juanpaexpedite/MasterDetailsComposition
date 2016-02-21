using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Particles2D.Models
{
    public class Particle
    {
        private static Random generator = new Random(DateTime.Now.Millisecond);
        public Particle(int x, int y, int idxBitmap, float orientationx, float orientationy, CanvasRenderTarget source, float particlespeed = 0.25f, bool particlefall = false)
        {
            particleBitmap = idxBitmap;
            Radius = (particleBitmap + 1) * 4;
            X = x;
            Y = y;
            OrientationX = orientationx;
            OrientationY = orientationy;

            //Here is all the turbulence, without this is linear
            random = generator.Next(2, 50);
            factor = 0.7f * random / 10.0f;
            var octaves = generator.Next(1, 5);
            turbulence = new Transform2DEffect() { Source = new TurbulenceEffect() { Octaves = octaves } };
            displacement = new DisplacementMapEffect() { Source = source, Displacement = turbulence };

            speed = particlespeed;
            fall = particlefall;
        }

        public int particleBitmap;

        public float Radius;

        public float X;

        public float Y;

        public float OrientationX;

        public float OrientationY;

        private float speed = 0.25f;
        private bool fall = false;
        
        //Here is all the turbulence, without this is linear
        public Matrix3x2 TurbulenceMatrix;
        public float random;
        public float factor;
        private Transform2DEffect turbulence;
        private DisplacementMapEffect displacement;

        #region Update
        public void Update(float elapsedTime,Size bounds)
        {
            UpdatePosition();
            CheckBounds(bounds);
            UpdateTurbulence(elapsedTime);
            
        }

        private void UpdatePosition()
        {
            X += OrientationX * speed;
            Y += OrientationY * speed;
        }

        private void UpdateTurbulence(float elapsedTime)
        {
            TurbulenceMatrix = Matrix3x2.CreateTranslation((float)Math.Cos(elapsedTime + random) * 2 - 2, (float)Math.Sin(elapsedTime) * 2 - 2);
            turbulence.TransformMatrix = TurbulenceMatrix;
            displacement.Amount = (float)Math.Sin(elapsedTime * factor) * random;
        }

        private void CheckBounds(Size bounds)
        {
            var height = bounds.Height;
            var width = bounds.Width;
            var diameter = 2 * Radius;
            if (!fall)
            {
                if (OrientationY > 0)
                {
                    if (Y > height - diameter)
                        OrientationY = -1;
                }
                else if (OrientationY < 0)
                {
                    if (Y < 0)
                        OrientationY = 1;
                }
            }
            else //snow particle to uppersize
            {
                if (Y > height - diameter)
                    Y = 0 - diameter * 4;
            }

            if (OrientationX > 0)
            {
                if (X  > width - diameter)
                    OrientationX = -1;
            }
            else if (OrientationX < 0)
            {
                if (X < 0)
                    OrientationX = 1;
            }
        }
        #endregion

        #region Draw
        public void Draw(CanvasDrawingSession session)
        {
            session.DrawImage(displacement,new Vector2(X, Y));

            //session.DrawImage((ICanvasImage)displacement.Source, new Vector2(X, Y));
        }
        #endregion


    }
}
