/* Created 05/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics3D.Particles.Forces.World
{
    /// <summary>
    /// Used to define an attractive force
    /// </summary>
    public sealed class Attractor : IForce
    {
        float range;
        Vector3 position;

        float force;
        bool repel;

        string id;

        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Wheater the Attractor repels.
        /// </summary>
        public bool Repel
        {
            get { return repel; }
        }

        /// <summary>
        /// Cretes a new Attraction force.
        /// </summary>
        /// <param name="id">The ID of the Attraction force.</param>
        /// <param name="force">The force to be applied.</param>
        /// <param name="position">The position of the force's origin.</param>
        /// <param name="range">The range of the force.</param>
        /// <param name="repel">Wheather or not the Attraction force should
        /// repel instead of attact.</param>
        public Attractor(string id, float force,
            Vector3 position, float range, bool repel)
        {
            this.id = id;

            this.position = position;
            this.range = range;
            this.force = force;

            this.repel = repel;

            ForceManager.Add(this);
        }

        public void ApplyForce(IForceAccumulator accumulator)
        {
            Vector3 direction = position - accumulator.Position;

            if (range > 0)
            {
                float dist = Vector3.Distance(position, accumulator.Position);

                if (dist > range)
                {
                    float f = force;
                    f -= (f / range) * dist;

                    if (repel)
                        f -= f;

                    accumulator.AccumulateForces(direction * f);
                }
            }
            else
                accumulator.AccumulateForces(direction * force);
        }

        /// <summary>
        /// Toogles repel.
        /// </summary>
        public void ToogleRepel()
        {
            repel = !repel;
        }

        public void Destroy()
        {
            ForceManager.Remove(this);
        }
    }
}
