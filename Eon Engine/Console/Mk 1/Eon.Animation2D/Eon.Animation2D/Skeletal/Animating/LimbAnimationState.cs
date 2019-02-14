/* Created 16/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Animation2D.Skeletal.Animating
{
    /// <summary>
    /// Used to define the state of a Limb in a SkeletalAnimation.
    /// </summary>
    public struct LimbAnimationState
    {
        int currentIndex;
        int nextIndex;

        float currentLenght;
        TimeSpan timePast;

        public Transformation CurrentTransformation;
        public LimbKeyFrameCollection Animation { get; set; }

        public string LimbName
        {
            get
            {
                return Animation.LimbName;
            }
        }

        public LimbAnimationState(LimbKeyFrameCollection animation)
            : this()
        {
            Animation = animation;
            Reset();
        }

        public void Update(SpriteEffects effect)
        {
            if (Animation.Count > 1)
            {
                timePast += Common.ElapsedTimeDelta;

                while (timePast.TotalSeconds > currentLenght)
                {
                    timePast -= TimeSpan.FromSeconds(currentLenght);
                    currentIndex = nextIndex;
                    nextIndex = (nextIndex + 1) % Animation.Count;

                    int frame1 = Animation[currentIndex].FrameNumber;
                    int frame2 = Animation[nextIndex].FrameNumber;
                    int difference = frame2 > frame1 ? frame2 - frame1 : (Animation.Count - frame1) + frame2;

                    currentLenght = difference / (float)Animation.FrameRate.TotalSeconds;
                }

                Transformation trans1 = Animation[currentIndex].Transform;
                Transformation trans2 = Animation[nextIndex].Transform;
                Transformation.Lerp(ref trans1, ref trans2,
                    (float)timePast.TotalSeconds / currentLenght,
                    out CurrentTransformation);
            }
        }

        public void Reset()
        {
            currentIndex = 0;
            nextIndex = 0;

            currentIndex = 0;
            timePast = TimeSpan.Zero;

            if (Animation.Count == 0)
                CurrentTransformation = Transformation.Identity;
            else if (Animation.Count == 1)
                CurrentTransformation = Animation[0].Transform;

            if (Animation.Count > 1)
            {
                currentLenght = (Animation[1].FrameNumber -
                    Animation[0].FrameNumber) / (float)Animation.FrameRate.TotalSeconds;

                nextIndex = 1;
                Update(SpriteEffects.None);
            }
        }
    }
}
