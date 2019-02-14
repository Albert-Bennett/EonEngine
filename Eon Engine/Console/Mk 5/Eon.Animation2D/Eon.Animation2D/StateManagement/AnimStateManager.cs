/* Created 24/09/2015
 * Last Updated: 15/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Animation2D.Skeletal;
using Eon.Animation2D.Skeletal.Animating;
using Eon.Animation2D.SpriteSheet;
using Eon.Helpers;
using Eon.Rendering2D;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Animation2D.StateManagement
{
    /// <summary>
    /// Defines a manager for animation states.
    /// </summary>
    public sealed class AnimStateManager : GameObject
    {
        List<AnimState> states;

        string currentState = "";
        string prevState = "";

        bool isSkeletal = false;

        Skeleton skeleton;
        AnimatedSprite sprite;

        /// <summary>
        /// The current animation state.
        /// </summary>
        public string CurrentState
        {
            get { return currentState; }
        }

        /// <summary>
        /// The previous animation state.
        /// </summary>
        public string PreviousState
        {
            get { return prevState; }
        }

        /// <summary>
        /// Creates a new AnimStateManager.
        /// </summary>
        /// <param name="id">The ID of the AnimStateManager.</param>
        /// <param name="filepath">The filepath of the AnimStateInfo file.</param>
        public AnimStateManager(string id, string filepath)
            : base(id)
        {
            Type[] extraTypes = new Type[]
            {
                typeof(Graphics.ImageOrentation),
                typeof(List<AnimState>),
                typeof(TextureAtlas),
                typeof(Vector2),
                typeof(SkeletalAnimation),
                typeof(LimbKeyFrameCollection[])
            };

            AnimStateInfo info = SerializationHelper.Deserialize<AnimStateInfo>(
                  filepath, true, ".AnimState", extraTypes);

            CheckLoad(info);

            if (info.SkeletonFilepath != "null")
                isSkeletal = true;

            states = info.States;
        }

        /// <summary>
        /// Creates a new AnimStateManager.
        /// </summary>
        /// <param name="id">The ID of the AnimStateManager.</param>
        /// <param name="states">The AnimStateInfo file.</param>
        public AnimStateManager(string id, AnimStateInfo states)
            : base(id)
        {
            CheckLoad(states);

            this.states = states.States;

            if (states.SkeletonFilepath != "null")
                isSkeletal = true;
        }

        void CheckLoad(AnimStateInfo states)
        {
            if (states.isLoaded == false)
            {
                for (int i = 0; i < states.States.Count; i++)
                    if (isSkeletal)
                    {
                        //Fully implement by merging with SkeletalAnimationPlayer. 

                        if (states.States[i].Filepath != "")
                            states.States[i].Animation = SerializationHelper.Deserialize<SkeletalAnimation>(
                                states.States[i].Filepath, true, ".SkelAni");
                    }
                    else
                    {
                        Type[] extraTypes = new Type[]
                        {
                            typeof(Vector2),
                            typeof(int),
                            typeof(string)
                        };

                        if (states.States[i].Filepath != "")
                            states.States[i].Animation = SerializationHelper.Deserialize<TextureAtlas>(
                                states.States[i].Filepath, true, ".SPR", extraTypes);
                    }

                states.isLoaded = true;
            }
        }

        /// <summary>
        /// Attaches a Skeleton to this.
        /// </summary>
        /// <param name="skeleton">The Skeleton to be attached.</param>
        public void AttachSkeleton(Skeleton skeleton)
        {
            if (isSkeletal)
                this.skeleton = skeleton;
        }

        /// <summary>
        /// Attaches a AnimatedSprite to this.
        /// </summary>
        /// <param name="sprite">The AnimatedSprite to be attached.</param>
        public void AttachAnimatedSprite(AnimatedSprite sprite)
        {
            if (!isSkeletal)
                this.sprite = sprite;
        }

        /// <summary>
        /// Changes the current Animation.
        /// </summary>
        /// <param name="state">The new animation state.</param>
        public void ChangeState(string state)
        {
            if (state != currentState)
            {
                bool found = false;
                int idx = 0;

                while (idx < states.Count && !found)
                {
                    if (states[idx].State == state)
                    {
                        prevState = currentState;
                        currentState = states[idx].State;

                        found = true;
                    }
                    else
                        idx++;
                }

                if (found)
                {
                    if (isSkeletal)
                    {

                    }
                    else
                    {
                        switch (states[idx].Orentation)
                        {
                            case Graphics.ImageOrentation.None:
                                sprite.SpriteEffect = SpriteEffects.None;
                                break;

                            case Graphics.ImageOrentation.FlipX:
                                sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                                break;

                            case Graphics.ImageOrentation.FlipY:
                                sprite.SpriteEffect = SpriteEffects.FlipVertically;
                                break;

                            case Graphics.ImageOrentation.FlipXY:
                                sprite.SpriteEffect = SpriteEffects.FlipHorizontally |
                                    SpriteEffects.FlipVertically;
                                break;
                        }

                        sprite.Loop = states[idx].Loop;
                        sprite.ChangeSpriteSheet(states[idx].Animation as TextureAtlas);
                        sprite.Play(false);
                    }
                }
                else
                    new Error("The Animation: " + state + " dosen't exist.", Seriousness.Error);
            }
        }
    }
}
