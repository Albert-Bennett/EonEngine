﻿/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using Eon.Physics2D.Math;
using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Defines an explosive force.
    /// </summary>
    public class Explosion : IUpdate, IWorldForce
    {
        BoundingCircle impactArea;
        float force;
        float decay;

        int radius;

        string id;

        public int Priority
        {
            get { return 0; }
        }

        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new Explosion force. 
        /// </summary>
        /// <param name="center">The center of the Expolsion force.</param>
        /// <param name="maxForce">The maximum force that 
        /// can be generated by the Expolsion force.</param>
        /// <param name="decay">The amount of decay in the Expolsion force.</param>
        public Explosion(string id, Vector2 center, float maxForce, float decay)
        {
            this.id = id;

            force = maxForce;

            int radius = 0;

            while (maxForce > 0)
            {
                radius++;
                maxForce -= decay;
            }

            this.radius = radius;

            this.decay = decay;

            impactArea = new BoundingCircle(center, radius);

            ForceManager.AddForce(this);
        }

        /// <summary>
        /// Creates a new Expolsion force.
        /// </summary>
        /// <param name="center">The center of the Expolsion force.</param>
        /// <param name="radius">The radius of the Expolsion force.</param>
        /// <param name="maxForce">The maximum force that 
        /// can be generated by the Expolsion force.</param>
        /// <param name="decay">The amount of decay in the Expolsion force.</param>
        public Explosion(Vector2 center, float maxForce, float decay, int radius)
        {
            force = maxForce;
            this.decay = decay;
            this.radius = radius;

            impactArea = new BoundingCircle(center, radius);

            ForceManager.AddForce(this);
        }

        /// <summary>
        /// Calculates the forec of the explosion at the position.
        /// </summary>
        /// <param name="position">The position of the object.</param>
        /// <returns>The calculated force.</returns>
        public Vector2 CalculateAppliedForce(ParticleComponent particle)
        {
            Vector2 apply = Vector2.Zero;

            float dist = Vector2.Distance(impactArea.Center, particle.Position);

            if (dist < impactArea.Radius)
            {
                Vector2 direct = particle.Position - impactArea.Center;
                direct.Normalize();

                float f = (force / radius) * dist;

                apply = direct * f;
            }

            return apply;
        }

        public void _Update()
        {
            impactArea.Radius -= decay;

            if (impactArea.Radius <= 0)
                Remove();
        }

        /// <summary>
        /// Removes this force from the ForceManager.
        /// </summary>
        public void Remove()
        {
            ForceManager.Remove(this);
        }
    }
}