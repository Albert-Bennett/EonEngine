/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Forces.Volumes;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces.Volumes
{
    /// <summary>
    /// Defines a physics volume that applies 
    /// gravity to object touching it.
    /// </summary>
    public class GravityVolume : PhysicsVolume
    {
        float gravity;
        Vector2 direction;

        /// <summary>
        /// Creates a new GravityVolume.
        /// </summary>
        /// <param name="id">The id of the GravityVolume.</param>
        /// <param name="bounds">The bounds of the GravityVolume.</param>
        /// <param name="gravity">The intensity of the gravity.</param>
        /// <param name="direction">The direction of the Gravity.</param>
        public GravityVolume(string id, Rectangle bounds,
            float gravity, Vector2 direction)
            : base(id, bounds)
        {
            this.direction = direction;
            this.gravity = gravity;
        }

        /// <summary>
        /// Creates a new GravityVolume.
        /// </summary>
        /// <param name="id">The id of the GravityVolume.</param>
        /// <param name="bounds">The bounds of the GravityVolume.</param>
        /// <param name="gravity">The intensity of the gravity.</param>
        public GravityVolume(string id, Rectangle bounds, float gravity) : this(id, bounds, gravity, new Vector2(0, 1)) { }

        /// <summary>
        /// Creates a new GravityVolume.
        /// </summary>
        /// <param name="id">The id of the GravityVolume.</param>
        /// <param name="bounds">The bounds of the GravityVolume.</param>
        public GravityVolume(string id, Rectangle bounds) : this(id, bounds, 9.8f, new Vector2(0, 1)) { }

        protected override Vector2 CalculateForce(Vector2 position)
        {
            return direction * gravity;
        }
    }
}
