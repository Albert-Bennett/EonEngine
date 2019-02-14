/* Created 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Game;
using Eon.Interfaces;
using Eon.Physics2D.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Game2D.Objects
{
    /// <summary>
    /// Defines an object that when
    /// collided with preforms an action.
    /// </summary>
    public abstract class Trigger : ILevelAsset, IEnabled, IUpdate
    {
        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan activationTime = TimeSpan.Zero;

        bool triggerable = true;

        int triggerCount = 1;
        bool enabled = true;
        string id;

        CollisionObject collide;

        List<string> acceptableCollisionTypes;

        /// <summary>
        /// The amount of times this Trigger can be set of before is get permently disabled.
        /// </summary>
        public int TriggerCount
        {
            get { return triggerCount; }
        }

        /// <summary>
        /// Wheather or not this Trigger is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
        }

        protected Rectangle Bounds
        {
            get { return (Rectangle)collide.Bounds; }
        }

        /// <summary>
        /// The unique identifaction name for the Trigger
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new Trigger.
        /// </summary>
        /// <param name="id">The identifaction name to give the Trigger.</param>
        /// <param name="bounds">The collision area of the Trigger.</param>
        /// <param name="delayTime">The amount of time to delay between activations.</param>
        /// <param name="triggerCount">The amount of times the Trigger can be sprung.</param>
        /// <param name="enabled">Wheather or not the Trigger i9s enabled from the start.</param>
        /// <param name="acceptableCollisionTypes">The only types of objects that, when 
        /// collided with the Trigger can activate it.</param>
        public Trigger(string id, Rectangle bounds, TimeSpan delayTime, int triggerCount,
            bool enabled, List<string> acceptableCollisionTypes)
        {
            this.id = id;

            this.triggerCount = triggerCount;
            this.enabled = enabled;
            activationTime = delayTime;

            this.acceptableCollisionTypes = acceptableCollisionTypes;

            collide = new CollisionObject(id + "Collision", bounds);
            collide.OnCollided += new CollisionEvent(_Collided);
        }

        /// <summary>
        /// Creates a new Trigger.
        /// </summary>
        /// <param name="id">The identifaction name to give the Trigger.</param>
        /// <param name="bounds">The collision area of the Trigger.</param>
        /// <param name="delayTime">The amount of time to delay between activations.</param>
        /// <param name="triggerCount">The amount of times the Trigger can be sprung.</param>
        public Trigger(string id, Rectangle bounds, TimeSpan delayTime, int triggerCount) :
            this(id, bounds, delayTime, triggerCount, true, new List<string>()) { }

        /// <summary>
        /// Creates a new Trigger.
        /// </summary>
        /// <param name="id">The identifaction name to give the Trigger.</param>
        /// <param name="bounds">The collision area of the Trigger.</param>
        public Trigger(string id, Rectangle bounds) : this(id, bounds, TimeSpan.FromSeconds(1), -1, true, new List<string>()) { }

        public void _Update()
        {
            if (enabled)
                if (!triggerable)
                {
                    currentTime += Common.ElapsedTimeDelta;

                    if (currentTime >= activationTime)
                    {
                        triggerable = true;
                        currentTime = TimeSpan.Zero;
                    }
                }
        }

        void _Collided(CollisionInfo info)
        {
            if (enabled && triggerable)
            {
                triggerCount--;
                triggerable = false;

                if (triggerCount > 0 || triggerCount == -1)
                {
                    if (Check(info))
                        Triggered(info);
                }
                else
                    Disable();
            }
        }

        bool Check(CollisionInfo info)
        {
            if (acceptableCollisionTypes.Count > 0)
            {
                string insta = info.Instigator.GetType().ToString();
                string col = info.Collider.GetType().ToString();

                if (acceptableCollisionTypes.Contains(insta))
                    return true;
                else if (acceptableCollisionTypes.Contains(col))
                    return true;

                return false;
            }

            return true;
        }

        protected virtual void Triggered(CollisionInfo info) { }

        /// <summary>
        /// Disables the Trigger.
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Enables the Trigger.
        /// </summary>
        public void Enable()
        {
            if (triggerCount > 0 || triggerCount == -1)
                enabled = true;
        }

        /// <summary>
        /// Toggles the whether or not this Trigger shound be enabled.
        /// </summary>
        public void ToogleEnable()
        {
            if (enabled)
                Disable();
            else
                Enable();
        }

        public void LevelTransitionOn(string levelID)
        {
            _LevelTransitionOn(levelID);
        }

        public void LevelTransitionOff(string levelID)
        {
            _LevelTransitionOff(levelID);
        }

        protected virtual void _LevelTransitionOn(string levelID) { }
        protected virtual void _LevelTransitionOff(string levelID) { }
    }
}
