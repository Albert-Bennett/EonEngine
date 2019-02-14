/* Created: 18/09/2015
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Maths.Shapes;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces.Volumes
{
    /// <summary>
    /// Defines a volume of water.
    /// </summary>
    public sealed class WaterVolume : PhysicsVolume
    {
        float tension;
        float dampening;
        float spread;

        /// <summary>
        /// Creates a new WaterVolume.
        /// </summary>
        /// <param name="id">The ID of the WaterVolume.</param>
        /// <param name="area">The bounds of the WaterVolume.</param>
        /// <param name="tension">Surface tension.</param>
        /// <param name="dampening">The amount of dampening to be applied.</param>
        /// <param name="spread">The spread of the wave.</param>
        public WaterVolume(string id, ConvexShape area,
            float tension, float dampening, float spread) :
            base(id, area)
        {
            this.tension = tension;
            this.dampening = dampening;
            this.spread = spread;
        }

        //protected override Vector2 CalculateForce(PhysicsComponent component)
        //{
        //    return Vector2.Zero;
        //}

        protected override Vector2 CalculateForce(Vector2 position)
        {
            throw new global::System.NotImplementedException();
        }
    }
}
