/* Created 14/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Animation2D.Skeletal.Blending
{
    /// <summary>
    /// Defines a class that is used to 
    /// blend two D2SkeletalAnimation's together.
    /// </summary>
    internal sealed class Blender
    {
        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan maxTime;

        LimbAnimationState[] prevAnimations;
        LimbAnimationState[] nextAnimations;
        LimbAnimationState[] currentAnimations;

        int animations;

        internal HasFinishedEvent OnFinished;

        internal LimbAnimationState[] _CurrentAnimations
        {
            get { return currentAnimations; }
        }

        public Blender(float blendTime, LimbAnimationState[] prevAnimations,
            LimbAnimationState[] nextAnimations)
        {
            Reset(blendTime, prevAnimations, nextAnimations);
        }

        internal void Reset(float blendTime, LimbAnimationState[] prevAnimations,
            LimbAnimationState[] nextAnimations)
        {
            maxTime = TimeSpan.FromSeconds(blendTime);
            currentTime = TimeSpan.Zero;

            this.nextAnimations = nextAnimations;
            this.prevAnimations = prevAnimations;

            animations = prevAnimations.Length;

            currentAnimations = new LimbAnimationState[animations];

            for (int i = 0; i < animations; i++)
                currentAnimations[i] = CreateKeyFrame(i);
        }

        LimbAnimationState CreateKeyFrame(int index)
        {
            LimbKeyFrameCollection frames = new LimbKeyFrameCollection();

            int totalFrames = (int)(prevAnimations[index].Animation.FrameRate / maxTime.TotalSeconds) + 1;

            LimbKeyFrame frame1 = prevAnimations[index].Animation[prevAnimations[index].Animation.Count - 1];
            frame1.FrameNumber = 0;

            LimbKeyFrame frame2 = nextAnimations[index].Animation[0];
            frame2.FrameNumber = totalFrames;

            frames.AddFrame(frame1);
            frames.AddFrame(frame2);

            frames.LimbName = prevAnimations[index].LimbName;
            frames.FrameRate = (float)TimeSpan.FromSeconds(maxTime.TotalSeconds / totalFrames).TotalSeconds;

            LimbAnimationState frame = new LimbAnimationState(frames);

            return frame;
        }

        internal void Update(SpriteEffects effect)
        {
            if (currentTime < maxTime)
            {
                currentTime += Common.ElapsedTimeDelta;

                for (int i = 0; i < animations; i++)
                {
                    LimbAnimationState state = currentAnimations[i];
                    state.Update(effect);

                    currentAnimations[i] = state;
                }
            }
            else
                if (OnFinished != null)
                    OnFinished("");
        }

        internal void Dispose()
        {
            currentAnimations = null;
            prevAnimations = null;
            nextAnimations = null;
        }
    }
}
