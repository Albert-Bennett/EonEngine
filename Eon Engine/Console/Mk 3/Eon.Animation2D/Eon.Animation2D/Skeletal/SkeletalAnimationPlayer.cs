/* Created 03/06/2013
 * Last Updated: 19/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Eon.Animation2D.Skeletal.Blending;
using Eon.Helpers;
using Eon.System.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines an object that  is used to apply animations to a given Skeleton.
    /// </summary>
    public sealed class SkeletalAnimationPlayer : ObjectComponent, IUpdate
    {
        List<SkeletalAnimation> animations = new List<SkeletalAnimation>();

        SkeletalAnimation currentAnimation = null;
        SkeletalAnimation prevAnimation = null;

        Blender blender;
        bool blending = false;

        Skeleton skeleton;

        public HasFinishedEvent OnFinished;

        /// <summary>
        /// Update order priority.
        /// </summary>
        public int Priority
        {
            get { return 1; }
        }

        /// <summary>
        /// Creates a new D2SkeletalAnimationPlayer.
        /// </summary>
        /// <param name="id">The unique identification 
        /// name to give to this.</param>
        /// <param name="skeleton">The Skeleton that is to be animated.</param>
        public SkeletalAnimationPlayer(string id, Skeleton skeleton)
            : base(id)
        {
            this.skeleton = skeleton;

            skeleton.CalculateTransforms();
        }

        /// <summary>
        /// Used to add an D2SkeletalAnimation to this 
        /// D2SkeletalAnimationPlayer's rooster.
        /// </summary>
        /// <param name="animation">The D2SkeletalAnimation to be added.</param>
        public void AddAnimation(SkeletalAnimation animation)
        {
            SkeletalAnimation ani = null;

            ani = (from a in animations
                   where a.Name == animation.Name
                   select a).FirstOrDefault();

            if (ani == null)
                animations.Add(animation);
        }

        /// <summary>
        /// Used to add an D2SkeletalAnimation to this 
        /// D2SkeletalAnimationPlayer's rooster.
        /// </summary>
        /// <param name="animationFilepath">The location of the animation.</param>
        public void AddAnimation(string animationFilepath)
        {
            AddAnimation(SerializationHelper.Deserialize<SkeletalAnimation>(animationFilepath, true, ".SkelAni"));
        }

        /// <summary>
        /// Used to add an D2SkeletalAnimation to this 
        /// D2SkeletalAnimationPlayer's rooster.
        /// </summary>
        /// <param name="animationFilepaths">The locations of the animations.</param>
        public void AddAnimations(string[] animationFilepaths)
        {
            for (int i = 0; i < animationFilepaths.Length; i++)
                AddAnimation(SerializationHelper.Deserialize<SkeletalAnimation>(animationFilepaths[i], true, ".SkelAni"));
        }

        /// <summary>
        /// Used to apply a D2SkeletalAnimation to
        /// the given skeleton.
        /// </summary>
        /// <param name="animationName">The name of the 
        /// D2SkeletalAnimation to be played.</param>
        public void PlayAnimation(string animationName)
        {
            if (currentAnimation != null && currentAnimation.Name == animationName)
            {
                currentAnimation.Reset();
                currentAnimation.Play();
            }
            else
                for (int i = 0; i < animations.Count; i++)
                    if (animations[i].Name == animationName)
                    {
                        if (currentAnimation != null)
                            prevAnimation = currentAnimation;

                        currentAnimation = animations[i];
                        currentAnimation.Initialize();

                        currentAnimation.OnFinished += new HasFinishedEvent(AnimiationFinished);

                        if (prevAnimation != null)
                            if (blender == null)
                            {
                                blender = new Blender(prevAnimation._LimbAnimations,
                                    currentAnimation._LimbAnimations, prevAnimation.BlendRate, prevAnimation.FrameRate);

                                blender.OnFinished += new HasFinishedEvent(FinishedBlending);

                                blending = true;
                            }
                            else
                            {
                                blender.Reset(prevAnimation._LimbAnimations,
                                    currentAnimation._LimbAnimations);

                                blending = true;
                            }
                        else
                            currentAnimation.Play();
                    }
        }

        void AnimiationFinished(string id)
        {
            if (OnFinished != null)
                OnFinished(id);
        }

        void FinishedBlending(string id)
        {
            currentAnimation.Play();

            blending = false;
        }

        public void _Update()
        {
            if (!blending)
            {
                if (currentAnimation != null)
                {
                    currentAnimation.Update();

                    if (currentAnimation != null)
                        Apply(currentAnimation._LimbAnimations);
                }
            }
            else
                Apply(blender._CurrentAnimations);
        }

        void Apply(LimbAnimationState[] states)
        {
            skeleton.SetLimbTransformation(states);
            skeleton.CalculateTransforms();
        }
    }
}