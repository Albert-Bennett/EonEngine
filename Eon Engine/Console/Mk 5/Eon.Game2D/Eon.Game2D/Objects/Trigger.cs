/* Created 23/10/2013
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game.Assets;
using Eon.Physics2D;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Game2D.Objects
{
    /// <summary>
    /// Defines an object that when
    /// collided with preforms an action.
    /// </summary>
    public abstract class Trigger : ObjectComponent, ILevelAsset
    {
        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan activationTime = TimeSpan.Zero;

        bool triggerable = true;

        int triggerCount = 1;

        PhysicsComponent collide;

        List<string> acceptableCollisionTypes;

        /// <summary>
        /// The bounds of the Trigger.
        /// </summary>
        public Eon.Physics2D.Maths.Shapes.ConvexShape Bounds
        {
            get
            {
                if (collide != null)
                    return collide.Bounds;

                return null;
            }
        }

        /// <summary>
        /// The amount of times this Trigger can be set of before is get permently disabled.
        /// </summary>
        public int TriggerCount
        {
            get { return triggerCount; }
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
            this.triggerCount = triggerCount;
            Enabled = enabled;
            activationTime = delayTime;

            this.acceptableCollisionTypes = acceptableCollisionTypes;

            collide = new PhysicsComponent(ID + "Collide",
                Eon.Physics2D.Maths.Shapes.Rectangle.FromRectangle(bounds), Vector2.Zero, -1, true);
        }

        protected override void Initialize()
        {
            if (Owner != null)
            {
               // collide.OnCollided += new CollisionEvent(_Collided);

                Owner.AttachComponent(collide);
            }

            base.Initialize();
        }

        protected override void Update()
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

        //void _Collided(CollisionInfo info)
        //{
        //    if (Enabled && triggerable)
        //    {
        //        triggerCount--;
        //        triggerable = false;

        //        if (triggerCount > 0 || triggerCount == -1)
        //        {
        //            if (Check(info))
        //                Triggered(info);
        //        }
        //        else
        //            Disable();
        //    }
        //}

        //bool Check(CollisionInfo info)
        //{
        //    if (acceptableCollisionTypes.Count > 0)
        //    {
        //        string insta = info.Instigator.GetType().ToString();
        //        string col = info.Collider.GetType().ToString();

        //        if (acceptableCollisionTypes.Contains(insta))
        //            return true;
        //        else if (acceptableCollisionTypes.Contains(col))
        //            return true;

        //        return false;
        //    }

        //    return true;
        //}

        //protected virtual void Triggered(CollisionInfo info) { }

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
