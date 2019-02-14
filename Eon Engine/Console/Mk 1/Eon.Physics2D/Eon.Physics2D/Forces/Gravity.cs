﻿/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Used to define gravity in a game.
    /// </summary>
    public class Gravity : IWorldForce
    {
        float gravity = 9.8f;
        Vector2 direction = new Vector2(0, 1);

        string id;

        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// The direction of Gravity.
        /// </summary>
        public Vector2 Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Creates a new gravity force with default values.
        /// </summary>
        public Gravity(string id)
        {
            this.id = id;

            ForceManager.AddForce(this);
        }

        /// <summary>
        /// Creates a new Gravity force.
        /// </summary>
        /// <param name="gravity">The amount of gravity to be applied.</param>
        public Gravity(string id, float gravity) : this(id, gravity, new Vector2(0, 1)) { }

        /// <summary>
        /// Creates a new Gravity force.
        /// </summary>
        /// <param name="gravity">The amount of gravity to be applied.</param>
        /// <param name="direction">The direction of gravity.</param>
        public Gravity(string id, float gravity, Vector2 direction)
        {
            this.id = id;

            this.gravity = gravity;
            this.direction = direction;

            ForceManager.AddForce(this);
        }

        /// <summary>
        /// Calculates the gravity to be applied to an object at a position.
        /// </summary>
        /// <param name="position">The position of the object.</param>
        public Vector2 CalculateAppliedForce(ParticleObject particle)
        {
            return direction * gravity;
        }

        /// <summary>
        /// Removes this gravity force from being applied.
        /// </summary>
        public void Remove()
        {
            ForceManager.Remove(this);
        }
    }
}
