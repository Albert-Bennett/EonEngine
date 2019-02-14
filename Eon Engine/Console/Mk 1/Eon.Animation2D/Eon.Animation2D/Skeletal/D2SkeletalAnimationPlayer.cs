/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Animation2D.Skeletal.Animating;
using Eon.Animation2D.Skeletal.Blending;
using Eon.EngineComponents;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines an object that  is used to apply animations to a given Skeleton.
    /// </summary>
    public sealed class D2SkeletalAnimationPlayer : ObjectComponent, IPriorityComponent
    {
        List<D2SkeletalAnimation> animations = new List<D2SkeletalAnimation>();

        D2SkeletalAnimation currentAnimation = null;
        D2SkeletalAnimation prevAnimation = null;

        Blender blender;
        bool blending = false;

        Skeleton skeleton;

        GameStates precidence = GameStates.Game;

        public HasFinishedEvent OnFinished;

        /// <summary>
        /// The GameStae that defines in what state the 
        /// game must be in, inorder to play animations.
        /// </summary>
        public GameStates Precidence
        {
            get { return precidence; }
            set { precidence = value; }
        }

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
        public D2SkeletalAnimationPlayer(string id, Skeleton skeleton)
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
        public void AddAnimation(D2SkeletalAnimation animation)
        {
            D2SkeletalAnimation ani = null;

            ani = (from a in animations
                   where a.Name == animation.Name
                   select a).FirstOrDefault();

            if (ani == null)
            {
                if (Common.PreviousScreenResolution != Vector2.One)
                    ani.ScreenResolutionChanged();

                animations.Add(animation);
            }
        }

        /// <summary>
        /// Used to add an D2SkeletalAnimation to this 
        /// D2SkeletalAnimationPlayer's rooster.
        /// </summary>
        /// <param name="animationFilepath">The location of the animation.</param>
        public void AddAnimation(string animationFilepath)
        {
            AddAnimation(Common.ContentManager.Load<D2SkeletalAnimation>(animationFilepath));
        }

        /// <summary>
        /// Used to add an D2SkeletalAnimation to this 
        /// D2SkeletalAnimationPlayer's rooster.
        /// </summary>
        /// <param name="animationFilepaths">The locations of the animations.</param>
        public void AddAnimations(string[] animationFilepaths)
        {
            for (int i = 0; i < animationFilepaths.Length; i++)
                AddAnimation(Common.ContentManager.Load<D2SkeletalAnimation>(animationFilepaths[i]));
        }

        /// <summary>
        /// Used to apply a D2SkeletalAnimation to
        /// the given skeleton.
        /// </summary>
        /// <param name="animationName">The name of the 
        /// D2SkeletalAnimation to be played.</param>
        public void PlayAnimation(string animationName)
        {
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
                            blender = new Blender(prevAnimation.BlendingTime,
                                prevAnimation._LimbAnimations, currentAnimation._LimbAnimations);

                            blender.OnFinished += new HasFinishedEvent(FinishedBlending);

                            blending = true;
                        }
                        else
                        {
                            blender.Reset(prevAnimation.BlendingTime,
                                prevAnimation._LimbAnimations, currentAnimation._LimbAnimations);

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
            if (GameStateManager.CurrentState == Precidence)
            {
                if (!blending)
                {
                    if (currentAnimation != null)
                    {
                        currentAnimation.Update(skeleton.Effect);

                        Apply(currentAnimation._LimbAnimations);
                        skeleton.CalculateTransforms();
                    }
                }
                else
                {
                    blender.Update(skeleton.Effect);

                    Apply(blender._CurrentAnimations);
                    skeleton.CalculateTransforms();
                }
            }
            else
                currentAnimation.Reset();
        }

        void Apply(LimbAnimationState[] states)
        {
            skeleton.SetLimbTransformation(states);
        }

        /// <summary>
        /// Used to preform an action when the resolution
        /// of the screen has been changed.
        /// </summary>
        public void ScreenResolutionChanged()
        {
            for (int i = 0; i < animations.Count; i++)
                animations[i].ScreenResolutionChanged();
        }
    }
}