/* Created 20/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Maths.Helpers;
using Eon.Physics2D.Maths.Shapes;
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
        public MagneticVolume(string id, Circle bounds,
            float force, bool repel)
            : base(id, bounds)
        {
            this.repel = repel;
            this.force = force;
        }

        protected override Vector2 CalculateForce(Vector2 position)
        {
            float dist = Vector2.Distance((_AreaOfEffect as Circle).Center, position);

            float calc = (force / (_AreaOfEffect as Circle).Radius) * dist;

            Vector2 direct = EonMathsHelper.GetDirection(
                (_AreaOfEffect as Circle).Center, position);

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
            Circle circle = _AreaOfEffect as Circle;
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
            Circle circle = _AreaOfEffect as Circle;
            circle.Radius = newRadius;

            SetBounds(circle);
        }
    }
}
