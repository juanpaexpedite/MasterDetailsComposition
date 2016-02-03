using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace UniversalComposition
{
    public static class AnimationCompositionManager
    {
        public static CompositionAnimation RotateClock(this Compositor compositor, float angle = 3.14159f, double duration = 1000)
        {
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, 0.0f);
            animation.InsertKeyFrame(1.0f, angle);
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation RotateUnClock(this Compositor compositor, float angle = 3.14159f, double duration = 1000)
        {
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, angle);
            animation.InsertKeyFrame(1.0f, 0.0f);
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation Land(this Compositor compositor, double altitude, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(0f, (float)altitude, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(0f, 0f, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation TakeOff(this Compositor compositor, double altitude, double duration = 1000)
        {
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, new Vector3(0f, 0f, 0.0f));
            animation.InsertKeyFrame(1.0f, new Vector3(0f, -1*(float)altitude, 0.0f));
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation FadeOut(this Compositor compositor, double duration = 1000)
        {
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, 0.0f);
            animation.InsertKeyFrame(1.0f, 1.0f);
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation FadeIn(this Compositor compositor, double duration = 1000)
        {
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, 1.0f);
            animation.InsertKeyFrame(1.0f, 0.0f);
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

        public static CompositionAnimation Scroll(this Compositor compositor, double from, double to, double duration = 1000)
        {
            var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, (float)from);
            animation.InsertKeyFrame(1.0f, (float)to);
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            return animation;
        }

    }
}
