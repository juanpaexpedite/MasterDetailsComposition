using Microsoft.Graphics.Canvas;
using Particles2D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.Foundation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI.Xaml.Media;
using Microsoft.Graphics.Canvas.Effects;

namespace Particles2D.Managers
{


    public class ParticlesManager
    {
        #region Constructor and Interop
        private CanvasAnimatedControl parent;
        public ParticlesManager(CanvasAnimatedControl canvas, int amount = 10)
        {
            parent = canvas;
            device = canvas.Device;
            bounds = new Size(canvas.ActualWidth, canvas.ActualHeight);
            clearColor = (canvas.Background as SolidColorBrush).Color;
            amountparticles = amount;
        }

        public void Initialize(bool blur = false, bool fall = false)
        {
            //1.- Create Bitmaps (4 sizes)
            CreateParticleBitmaps(blur);

            //2.- Create Particles
            CreateParticles(fall);

        }

        public void UpdateBounds(Size newSize)
        {
            bounds = newSize;
        }
        #endregion

        //Definitions
        private int minRadius = 4;
        private int sizes = 4;
        private Color baseColor = Colors.White;
        private Color clearColor = Colors.Black;

        //Fields
        private CanvasDevice device = null;
        private Color outerColor;
        private Color innerColor;
        private CanvasRenderTarget[] particleBitmaps;
        private int amountparticles = 10;
        private List<Particle> particles;
        private Size bounds = new Size(100, 100);
        

        //Sets the inner and outer colors with transparency
        private void SetColors(Color basecolor)
        {
            outerColor = ColorManager.AlphaColor(basecolor, 0x6F);
            innerColor = ColorManager.AlphaColor(basecolor, 0x9F);
        }

        //Creates textures of different sizes
        public void CreateParticleBitmaps(bool blur = false)
        {
            SetColors(baseColor);

            particleBitmaps = new CanvasRenderTarget[sizes];

            int i = -1;
            var nextRadius = 0;
            var nextSize =0;
            var transparent = Color.FromArgb(0, 0, 0, 0);

            float viewportsize = 100; //Here is the trick, if this value is too small appears the displacement and the original image
            
            for (int r = 1; r < sizes + 1; r += 1)
            {
                nextRadius = (r * minRadius);
                nextSize = nextRadius * 2;
                CanvasRenderTarget canvas = new CanvasRenderTarget(device, viewportsize, viewportsize, parent.Dpi);
                var center = new Vector2((viewportsize - nextRadius) / 2);

                //The following is like a 'drawing graph', the output of the first is the input of the second one;
                using (CanvasDrawingSession targetSession = canvas.CreateDrawingSession())
                {
                    targetSession.Clear(transparent);
                    targetSession.FillCircle(center, nextRadius, outerColor);
                    targetSession.FillCircle(center, nextRadius - 6, innerColor);
                }

                if (!blur)
                {
                    particleBitmaps[++i] = canvas;
                }
                else //Add blur just one time
                {
                    var blurEffect = new GaussianBlurEffect() { BlurAmount = 2f };
                    CanvasRenderTarget blurredcanvas = new CanvasRenderTarget(device, viewportsize, viewportsize, parent.Dpi);
                    blurEffect.Source = canvas;
                    using (CanvasDrawingSession targetSession = blurredcanvas.CreateDrawingSession())
                    {
                        targetSession.Clear(transparent);
                        targetSession.DrawImage(blurEffect);
                    }
                    particleBitmaps[++i] = blurredcanvas;
                }
            }
        }

        internal void Update(CanvasAnimatedUpdateEventArgs args)
        {
            float elapsedTime = (float)args.Timing.TotalTime.TotalSeconds;

            foreach (var particle in particles)
            {
                particle.Update(elapsedTime, bounds);
            }
        }

        private Random generator = new Random(DateTime.Now.Millisecond);
        //Create Particles
        public void CreateParticles(bool fall)
        {
            particles = new List<Particle>();

            int i = -1;
            int minpos = minRadius * sizes * 2;
            while (++i < amountparticles)
            {
                var x = generator.Next(minpos, (int)bounds.Width);
                var y = generator.Next(minpos, (int)bounds.Height);
                var s = generator.Next(0, sizes);
                var ox = generator.NextDouble() > 0.5 ? 1 : -1;
                var oy = fall ? 1 : generator.NextDouble() > 0.5 ? 1 : -1;

                var particle = new Particle(x, y,s,ox,oy, particleBitmaps[s], particlefall:fall);
                particles.Add(particle);
            }
        }

        
        public void Draw(CanvasDrawingSession session)
        {
            session.Clear(clearColor);
            foreach (var particle in particles)
            {
                //Testing
                //session.FillCircle(new Vector2(particle.X, particle.Y), 20f, Colors.Red);

                particle.Draw(session);
                
            }
        }
    }

    
}
