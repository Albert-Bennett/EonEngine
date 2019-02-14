﻿/* Created: 03/10/2013
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Defines a force caused by wind.
    /// </summary>
    public class Wind : IForce
    {
        Vector2 direction;
        Vector2 position;

        float range;
        float force;

        string id;

        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a Wind force.
        /// </summary>
        /// <param name="force">The force of the Wind.</param>
        /// <param name="position">The origin of the Wind.</param>
        /// <param name="direction">The direction of the Wind.</param>
        /// <param name="range">The range of the Wind.</param>
        public Wind(string id, float force, Vector2 position,
            Vector2 direction, float range)
        {
            this.id = id;
            this.force = force;
            this.position = position;
            this.direction = direction;
            this.range = range;

            ForceManager.AddForce(this);
        }

        /// <summary>
        /// Creates a Wind force.
        /// </summary>
        /// <param name="force">The force of the Wind.</param>
        /// <param name="position">The origin of the Wind.</param>
        /// <param name="direction">The direction of the Wind.</param>
        public Wind(string id, float force, Vector2 position,
            Vector2 direction)
            : this(id, force, position, direction, -1) { }

        public void ApplyForce(object accumulator)
        {
            IForceAccumulator acc = accumulator as IForceAccumulator;

            Vector2 apply = Vector2.Zero;

            if (range > 0)
            {
                float dist = Vector2.Distance(position,
                    acc.Position);

                if (dist < range)
                {
                    float f = (force / range) * dist;

                    apply = direction * f;
                }
            }
            else
                apply = direction * force;

            acc.AccumulateForces(apply);
        }

        public void Remove()
        {
            ForceManager.Remove(this);
        }
    }
}