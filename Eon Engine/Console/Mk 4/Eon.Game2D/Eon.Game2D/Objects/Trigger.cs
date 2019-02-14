/* Created 23/10/2013
 * Last Updated: 23/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game;
using Eon.Physics2D;
using Eon.Physics2D.Collision;
using Eon.Physics2D.Maths.Shapes;
using Eon.System.Interfaces;
using System;
using System.Collections.Generic;

namespace Eon.Game2D.Objects
{
    /// <summary>
    /// Defines an object that when
    /// collided with preforms an action.
    /// </summary>
    public abstract class Trigger : ObjectComponent, ILevelAsset, IUpdate
    {
        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan activationTime = TimeSpan.Zero;

        bool triggerable = true;

        int triggerCount = 1;

        CollisionComponent collide;
        Rectangle bounds;

        List<string> acceptableCollisionTypes;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The amount of times this Trigger can be set of before is get permently disabled.
        /// </summary>
        public int TriggerCount
        {
            get { return triggerCount; }
        }

        protected Rectangle Bounds
        {
            get { return (Rectangle)collide.Bounds; }
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
        public Trigger(string id, Rectangle bounds) :
            this(id, bounds, TimeSpan.FromSeconds(1), -1, true, new List<string>()) { }

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
            : base(id)
        {
            this.bounds = bounds;
            this.triggerCount = triggerCount;
            Enabled = enabled;
            activationTime = delayTime;

            this.acceptableCollisionTypes = acceptableCollisionTypes;
        }

        protected override void Initialize()
        {
            if (Owner != null)
            {
                collide = new CollisionComponent(ID + "Collision", bounds);
                collide.OnCollided += new CollisionEvent(_Collided);

                Owner.AttachComponent(collide);
            }

            base.Initialize();
        }

        public void _Update()
        {
            if (Enabled)
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

        public void _PostUpdate()
        {
            PostUpdate();
        }

        protected virtual void PostUpdate() { }

        void _Collided(CollisionInfo info)
        {
            if (Enabled && triggerable)
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
        /// Enables the Trigger.
        /// </summary>
        public override void Enable()
        {
            if (triggerCount > 0 || triggerCount == -1)
                Enabled = true;
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
