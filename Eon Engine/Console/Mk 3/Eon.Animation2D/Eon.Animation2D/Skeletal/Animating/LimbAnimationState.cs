/* Created 16/06/2013
 * Last Updated: 21/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
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
        int aniIndex;
        int nextAniIndex;
        int difference;

        float frameRate;
        float maxLenght;
        bool isPlaying;

        public Transformation CurrentTransformation;
        public LimbKeyFrameCollection Animation { get; set; }

        public LimbAnimationFinishedEvent Finished;

        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        public string LimbName
        {
            get
            {
                return Animation.LimbName;
            }
        }

        public LimbAnimationState(LimbKeyFrameCollection animation, float frameRate)
            : this()
        {
            this.frameRate = frameRate;
            Animation = animation;

            Reset();
        }

        public void Update()
        {
            if (currentIndex >= difference)
            {
                aniIndex++;
                nextAniIndex++;
                currentIndex = 0;

                if (nextAniIndex >= Animation.Count)
                {
                    isPlaying = false;

                    if (Finished != null)
                        Finished();
                }
                else
                {
                    difference = Animation[nextAniIndex].FrameNumber - Animation[aniIndex].FrameNumber;
                    maxLenght = difference * frameRate;
                }
            }

            if (isPlaying)
            {
                currentIndex++;

                Transformation trans1 = Animation[aniIndex].Transform;
                Transformation trans2 = Animation[nextAniIndex].Transform;

                Transformation.Lerp(ref trans1, ref trans2,
                (frameRate * currentIndex) / maxLenght,
                out CurrentTransformation);
            }
        }

        public void Reset()
        {
            currentIndex = 0;

            isPlaying = true;

            if (Animation.Count == 0)
                CurrentTransformation = Transformation.Identity;
            else if (Animation.Count == 1)
                CurrentTransformation = Animation[0].Transform;

            aniIndex = 0;

            if (Animation.Count > 1)
            {
                nextAniIndex = 1;
                difference = Animation[nextAniIndex].FrameNumber - Animation[aniIndex].FrameNumber;
                maxLenght = difference * frameRate;

                Update();
            }
        }
    }
}
