/* Created 04/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Game.Audio;
using Eon.Game2D.Objects;
using Eon.Physics2D.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Game2D.Audio
{
    /// <summary>
    /// Defines a trigger that plays a sound when touched. 
    /// </summary>
    public sealed class SoundTrigger : Trigger
    {
        string soundName;
        AudioTypes playType;

        /// <summary>
        /// Creates a new SoundTrigger.
        /// </summary>
        /// <param name="id">The identifaction name to give the Trigger.</param>
        /// <param name="bounds">The collision area of the Trigger.</param>
        /// <param name="delayTime">The amount of time to delay between activations.</param>
        /// <param name="triggerCount">The amount of times the Trigger can be sprung.</param>
        /// <param name="enabled">Wheather or not the Trigger i9s enabled from the start.</param>
        /// <param name="acceptableCollisionTypes">The only types of objects that, when 
        /// collided with the Trigger can activate it.</param>
        /// <param name="soundName">The name of the sound to be played when triggered.</param>
        /// <param name="playType">The way in which the audio will played.</param>
        public SoundTrigger(string id, Eon.Physics2D.Maths.Shapes.Rectangle bounds, TimeSpan delayTime, int triggerCount,
            bool enabled, List<string> acceptableCollisionTypes, string soundName, AudioTypes playType)
            : base(id, bounds, delayTime, triggerCount, enabled, acceptableCollisionTypes)
        {
            this.soundName = soundName;
            this.playType = playType;
        }

        /// <summary>
        /// Creates a new SoundTrigger.
        /// </summary>
        /// <param name="id">The identifaction name to give the Trigger.</param>
        /// <param name="bounds">The collision area of the Trigger.</param>
        /// <param name="delayTime">The amount of time to delay between activations.</param>
        /// <param name="triggerCount">The amount of times the Trigger can be sprung.</param>
        /// <param name="soundName">The name of the sound to be played when triggered.</param>
        /// <param name="playType">The way in which the audio will played.</param>
        public SoundTrigger(string id, Eon.Physics2D.Maths.Shapes.Rectangle bounds, TimeSpan delayTime,
            int triggerCount, string soundName, AudioTypes playType) :
            base(id, bounds, delayTime, triggerCount)
        {
            this.soundName = soundName;
            this.playType = playType;
        }

        protected override void Triggered(CollisionInfo info)
        {
            switch (playType)
            {
                case AudioTypes.Ambient:
                    AudioManager.Play(soundName);
                    break;

                case AudioTypes.World:
                    {
                        Vector3 origin = new Vector3(Bounds.Center.X, Bounds.Center.Y, 0);

                        AudioManager.PlaySound3D(soundName, origin);
                    }
                    break;
            }
        }
    }
}
