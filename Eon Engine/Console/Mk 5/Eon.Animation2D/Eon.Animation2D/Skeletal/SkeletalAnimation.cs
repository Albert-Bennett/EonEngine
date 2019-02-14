/* Created 03/06/2013
 * Last Updated: 22/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using System;
using System.Collections.Generic;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines an animation that can be applied to a 2D Skeleton.
    /// [Serializable]
    /// </summary>
    public sealed class SkeletalAnimation
    {
        internal HasFinishedEvent OnFinished;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan frameRate;

        int count;
        bool isPlaying = false;

        /// <summary>
        /// The name given to the SkeletalAnimation.
        /// </summary>
        public string Name;

        /// <summary>
        /// The amount of time it takes for a frame to be changed.
        /// </summary>
        public float FrameRate;

        /// <summary>
        /// The rate at which animation are blended at.
        /// </summary>
        public float BlendRate;

        /// <summary>
        /// An array that contains all of the individual 
        /// limb animations.
        /// </summary>
        public LimbKeyFrameCollection[] LimbAnimations;

        internal LimbAnimationState[] limbStates;

        internal void Initialize()
        {
            frameRate = TimeSpan.FromMilliseconds(FrameRate);

            limbStates = new LimbAnimationState[LimbAnimations.Length];

            for (int i = 0; i < limbStates.Length; i++)
            {
                LimbAnimationState state = new LimbAnimationState(LimbAnimations[i], (float)frameRate.TotalSeconds);
                state.Finished += new LimbAnimationFinishedEvent(LimbAniFinished);

                limbStates[i] = state;
            }
        }

        void LimbAniFinished()
        {
            count++;
        }

        internal void Update()
        {
            if (count < limbStates.Length && isPlaying)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime >= frameRate)
                {
                    currentTime -= frameRate;

                    for (int i = 0; i < limbStates.Length; i++)
                        if (limbStates[i].Animation.TotalFrames >= 0)
                        {
                            LimbAnimationState state = limbStates[i];

                            if (state.IsPlaying)
                            {
                                state.Update();

                                limbStates[i] = state;
                            }
                        }
                }
            }
            else
                if (OnFinished != null)
                    OnFinished(Name);
        }

        internal void Merge(SkeletalAnimation animation)
        {
            List<LimbAnimationState> states =
                new List<LimbAnimationState>(limbStates);

            List<LimbKeyFrameCollection> nextFrames =
                new List<LimbKeyFrameCollection>(animation.LimbAnimations);

            for (int i = 0; i < LimbAnimations.Length; i++)
                for (int j = 0; j < animation.LimbAnimations.Length; j++)
                    if (LimbAnimations[i].LimbName == animation.LimbAnimations[j].LimbName)
                    {
                        LimbAnimationState state = new LimbAnimationState(animation.LimbAnimations[j], (float)frameRate.TotalSeconds);
                        state.Finished += new LimbAnimationFinishedEvent(LimbAniFinished);

                        states[i] = state;

                        nextFrames.Remove(animation.LimbAnimations[j]);
                    }

            foreach (LimbKeyFrameCollection frame in nextFrames)
            {
                LimbAnimationState state = new LimbAnimationState(frame, (float)frameRate.TotalSeconds);
                state.Finished += new LimbAnimationFinishedEvent(LimbAniFinished);

                states.Add(state);
            }

            limbStates = states.ToArray();
        }

        internal void Play()
        {
            isPlaying = true;
        }

        internal void Reset()
        {
            for (int i = 0; i < limbStates.Length; i++)
                limbStates[i].Reset();

            currentTime = TimeSpan.Zero;
            count = 0;

            isPlaying = false;
        }
    }
}
