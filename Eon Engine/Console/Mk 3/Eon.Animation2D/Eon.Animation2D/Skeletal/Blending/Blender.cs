/* Created 14/09/2013
 * Last Updated: 19/09/2014
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

        float blendRate;

        internal HasFinishedEvent OnFinished;

        internal LimbAnimationState[] _CurrentAnimations
        {
            get { return currentAnimations; }
        }

        public Blender(LimbAnimationState[] prevAnimations,
            LimbAnimationState[] nextAnimations, float blendRate, float frameRate)
        {
            this.blendRate = blendRate;
            maxTime = TimeSpan.FromSeconds(frameRate);

            Reset(prevAnimations, nextAnimations);
        }

        internal void Reset(LimbAnimationState[] prevAnimations,
            LimbAnimationState[] nextAnimations)
        {
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

            int totalFrames = (int)(prevAnimations[index].Animation.Count / blendRate);

            LimbKeyFrame frame1 = prevAnimations[index].Animation[prevAnimations[index].Animation.Count - 1];
            frame1.FrameNumber = 0;

            LimbKeyFrame frame2 = nextAnimations[index].Animation[0];
            frame2.FrameNumber = totalFrames;

            frames.AddFrame(frame1);
            frames.AddFrame(frame2);

            frames.LimbName = prevAnimations[index].LimbName;

            LimbAnimationState frame = new LimbAnimationState(frames, (float)maxTime.TotalSeconds);
            frame.Finished += new LimbAnimationFinishedEvent(AniFinished);

            return frame;
        }

        void AniFinished()
        {
            if (OnFinished != null)
                OnFinished("");
        }

        internal void Update()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime < maxTime)
            {
                currentTime = TimeSpan.Zero;

                for (int i = 0; i < animations; i++)
                {
                    LimbAnimationState state = currentAnimations[i];
                    state.Update();

                    currentAnimations[i] = state;
                }
            }     
        }

        internal void Dispose()
        {
            currentAnimations = null;
            prevAnimations = null;
            nextAnimations = null;
        }
    }
}
