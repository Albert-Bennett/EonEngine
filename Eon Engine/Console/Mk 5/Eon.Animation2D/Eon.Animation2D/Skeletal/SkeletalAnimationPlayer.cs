/* Created 03/06/2013
 * Last Updated: 07/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Eon.Animation2D.Skeletal.Blending;
using Eon.Helpers;
using Eon.System.Interfaces.Base;
using Eon.Testing;
using System.Collections.Generic;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines an object that  is used to apply animations to a given Skeleton.
    /// </summary>
    public sealed class SkeletalAnimationPlayer : ObjectComponent
    {
        Dictionary<string, string> animations = new Dictionary<string, string>();

        SkeletalAnimation currentAnimation = null;
        SkeletalAnimation prevAnimation = null;

        Blender blender;
        bool blending = false;

        Skeleton skeleton;

        public HasFinishedEvent OnFinished;

        /// <summary>
        /// The name of the current SkeletalAnimation.
        /// </summary>
        public string CurrentAnimation
        {
            get
            {
                if (currentAnimation != null)
                    return currentAnimation.Name;
                else
                    return "";
            }
        }

        /// <summary>
        /// Creates a new SkeletalAnimationPlayer.
        /// </summary>
        /// <param name="id">The unique identification 
        /// name to give to this.</param>
        /// <param name="skeleton">The Skeleton that is to be animated.</param>
        public SkeletalAnimationPlayer(string id, Skeleton skeleton)
            : base(id)
        {
            this.skeleton = skeleton;

            Priority = 2;
        }

        /// <summary>
        /// Used to add an SkeletalAnimation to this 
        /// SkeletalAnimationPlayer's rooster.
        /// </summary>
        /// <param name="animationFilepath">The location of the SkeletalAnimation.</param>
        /// <param name="name">The name of the SkeletalAnimation.</param>
        public void AddAnimation(string animationFilepath, string name)
        {
            if (!animations.ContainsKey(name))
                animations.Add(name, animationFilepath);
        }

        /// <summary>
        /// Used to apply a SkeletalAnimation to
        /// the given skeleton.
        /// </summary>
        /// <param name="animationName">The name of the 
        /// SkeletalAnimation to be played.</param>
        public void PlayAnimation(string animationName)
        {
            if (currentAnimation != null && currentAnimation.Name == animationName)
            {
                currentAnimation.Reset();
                currentAnimation.Play();
            }
            else
            {
                SkeletalAnimation ani = FindAnimation(animationName);

                if (ani != null)
                    if (currentAnimation != null)
                    {
                        prevAnimation = currentAnimation;

                        float blendRate = ani.BlendRate + currentAnimation.BlendRate / 2;

                        if (blendRate > 0)
                        {
                            if (blender == null)
                            {
                                blender = new Blender(prevAnimation.limbStates,
                                    currentAnimation.limbStates, prevAnimation.BlendRate, prevAnimation.FrameRate);

                                blender.OnFinished += new HasFinishedEvent(FinishedBlending);

                                blending = true;
                            }
                            else
                            {
                                blender.Reset(prevAnimation.limbStates,
                                    currentAnimation.limbStates);

                                blending = true;
                            }
                        }
                        else
                            currentAnimation.Merge(ani);
                    }
                    else
                    {
                        currentAnimation = ani;
                        currentAnimation.Initialize();

                        currentAnimation.OnFinished += new HasFinishedEvent(AnimiationFinished);

                        currentAnimation.Play();
                    }
            }
        }

        SkeletalAnimation FindAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
                return SerializationHelper.Deserialize<SkeletalAnimation>(
                    animations[animationName], true, ".SkelAni");

            new Error("The SkeletalAnimation: " + animationName +
                " dosn't exist in: " + ID, Seriousness.Warning);

            return null;
        }

        void AnimiationFinished(string id)
        {
            currentAnimation = null;

            if (OnFinished != null)
                OnFinished(id);
        }

        void FinishedBlending(string id)
        {
            currentAnimation.Play();

            blending = false;
        }

        protected override void Update()
        {
            if (!blending)
            {
                if (currentAnimation != null)
                {
                    currentAnimation.Update();

                    if (currentAnimation != null)
                        Apply(currentAnimation.limbStates);
                }
            }
            else
                Apply(blender._CurrentAnimations);
        }

        void Apply(LimbAnimationState[] states)
        {
            skeleton.SetLimbTransformation(states);
        }
    }
}