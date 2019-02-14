/* Created 20/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Physics2D.Math;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces.Volumes
{
    /// <summary>
    /// Used to define a physics volume used 
    /// to represent attraction forces.
    /// </summary>
    public class MagneticVolume : PhysicsVolume
    {
        float force;
        bool repel;

        /// <summary>
        /// Creates a new MagneticVolume.
        /// </summary>
        /// <param name="id">The ID of the MagneticVolume.</param>
        /// <param name="bounds">The area of effect of the MagneticVolume.</param>
        /// <param name="force">Ther force of the attraction.</param>
        /// <param name="repel">Wheather or not to repel objects.</param>
        public MagneticVolume(string id, BoundingCircle bounds,
            float force, bool repel)
            : base(id, bounds)
        {
            this.repel = repel;
            this.force = force;
        }

        protected override Vector2 CalculateForce(Vector2 position)
        {
            float dist = Vector2.Distance((_AreaOfEffect as BoundingCircle).Center, position);

            float calc = (force / (_AreaOfEffect as BoundingCircle).Radius) * dist;

            Vector2 direct = EonMathHelper.GetDirection(
                (_AreaOfEffect as BoundingCircle).Center, position);

            direct.Normalize();

            if (repel)
                calc = -calc;

            return direct * calc;
        }

        /// <summary>
        /// The point of attraction (the center of the bounding area).
        /// </summary>
        /// <param name="attractionPoint">The new attraction point.</param>
        public void SetAttractionPoint(Vector2 attractionPoint)
        {
            BoundingCircle circle = _AreaOfEffect as BoundingCircle;
            circle.Center = attractionPoint;

            SetBounds(circle);
        }

        /// <summary>
        /// Sets the area of effect for the MagneticVolume.
        /// </summary>
        /// <param name="newRadius">The new radius for the 
        /// force to be applied within.</param>
        public void SetAreaOfEffect(float newRadius)
        {
            BoundingCircle circle = _AreaOfEffect as BoundingCircle;
            circle.Radius = newRadius;

            SetBounds(circle);
        }
    }
}
