/* Created: 05/09/2014
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics3D.Particles.Forces.World
{
    /// <summary>
    /// Used to define the forces produced by a wind.
    /// </summary>
    public sealed class Wind : IForce
    {
        Vector3 direction;
        Vector3 pos;

        float force;
        float range;

        string id;

        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a Wind force.
        /// </summary>
        /// <param name="force">The force of the Wind.</param>
        /// <param name="direction">The direction of the Wind.</param>
        public Wind(string id, float force, Vector3 direction,
            Vector3 position, float range)
        {
            this.id = id;
            this.force = force;
            this.direction = direction;

            this.pos = position;
            this.range = range;

            ForceManager.Add(this);
        }

        public void ApplyForce(object accumulator)
        {
            IForceAccumulator acc = accumulator as IForceAccumulator;

            Vector3 apply = Vector3.Zero;

            if (range > 0)
            {
                float dist = Vector3.Distance(pos,
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

        public void Destroy()
        {
            ForceManager.Remove(this);
        }
    }
}
