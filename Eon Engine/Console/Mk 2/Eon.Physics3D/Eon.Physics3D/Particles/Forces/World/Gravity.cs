/* Created 05/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics3D.Particles.Forces.World
{
    /// <summary>
    /// Defines a gravitational force.
    /// </summary>
    public sealed class Gravity : IForce
    {
        float gravity = 9.8f;
        Vector3 direction = new Vector3(0, 1, 0);

        string id;

        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// The direction of Gravity.
        /// </summary>
        public Vector3 Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Creates a new Gravity force.
        /// </summary>
        /// <param name="gravity">The amount of gravity to be applied.</param>
        /// <param name="direction">The direction of gravity.</param>
        public Gravity(string id, float gravity, Vector3 direction)
        {
            this.id = id;

            this.gravity = gravity;
            this.direction = direction;

            ForceManager.Add(this);
        }

        /// <summary>
        /// Creates a new Gravity force with default values.
        /// </summary>
        public Gravity(string id)
        {
            this.id = id;

            ForceManager.Add(this);
        }

        /// <summary>
        /// Creates a new Gravity force.
        /// </summary>
        /// <param name="gravity">The amount of gravity to be applied.</param>
        public Gravity(string id, float gravity) : this(id, gravity, new Vector3(0, 1, 0)) { }

        public void ApplyForce(IForceAccumulator accumulator)
        {
            accumulator.AccumulateForces(direction * gravity);
        }

        public void Destroy()
        {
            ForceManager.Remove(this);
        }
    }
}
