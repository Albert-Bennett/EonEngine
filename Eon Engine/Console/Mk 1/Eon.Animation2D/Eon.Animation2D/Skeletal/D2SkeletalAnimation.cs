/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines an animation that can be applied to a 2D Skeleton.
    /// </summary>
    public sealed class D2SkeletalAnimation
    {
        internal HasFinishedEvent OnFinished;
        bool playing = false;

        TimeSpan currentTime = TimeSpan.Zero;

        /// <summary>
        /// The name given to the D2SkeletalAnimation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The total lenght of time of the animation.
        /// </summary>
        public TimeSpan AnimationTime { get; set; }

        /// <summary>
        /// The amount of time to blend between two D2SkeletalAnimations. 
        /// </summary>
        public TimeSpan BlendingTime { get; set; }

        /// <summary>
        /// An array that contains all of the individual 
        /// limb animations.
        /// </summary>
        public LimbKeyFrameCollection[] LimbAnimations { get; set; }

        internal LimbAnimationState[] _LimbAnimations { get; set; }

        internal void Initialize()
        {
            _LimbAnimations = new LimbAnimationState[LimbAnimations.Length];

            for (int j = 0; j < _LimbAnimations.Length; j++)
                _LimbAnimations[j] = new LimbAnimationState(LimbAnimations[j]);
        }

        internal void Play()
        {
            playing = true;
        }

        internal void Update(SpriteEffects effect)
        {
            if (playing)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime < AnimationTime)
                    for (int i = 0; i < _LimbAnimations.Length; i++)
                    {
                        LimbAnimationState state = _LimbAnimations[i];
                        state.Update(effect);

                        _LimbAnimations[i] = state;
                    }
                else
                    if (OnFinished != null)
                        OnFinished(Name);
            }
        }

        internal void Reset()
        {
            for (int i = 0; i < _LimbAnimations.Length; i++)
                _LimbAnimations[i].Reset();

            currentTime = TimeSpan.Zero;
        }

        internal void ScreenResolutionChanged()
        {
            foreach (LimbKeyFrameCollection frames in LimbAnimations)
                frames.ScreenResolutionChanged();
        }
    }
}
