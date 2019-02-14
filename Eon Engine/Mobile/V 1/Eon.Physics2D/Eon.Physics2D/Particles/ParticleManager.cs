/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Forces;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Physics2D.Particles
{
    /// <summary>
    /// Used to manage ParticleComponents.
    /// </summary>
    internal class ParticleManager : IUpdate, IDispose
    {
        static List<ParticleComponent> particles = new List<ParticleComponent>();

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Adds a ParticleComponent to be managed.
        /// </summary>
        /// <param name="particle">The Particle to be managed.</param>
        internal static void Add(ParticleComponent particle)
        {
            particles.Add(particle);
        }

        public void _Update()
        {
            for (int i = 0; i < particles.Count; i++)
                particles[i].WorldForces += ForceManager.AccumulateForces(particles[i]);

            Dictionary<string, Vector2> volumetricForces =
                ForceManager.AccumulateVolumetricForces();

            if (volumetricForces != null)
                foreach (KeyValuePair<string, Vector2> force in volumetricForces)
                {
                    bool found = false;
                    int i = 0;

                    while (!found)
                    {
                        if (particles[i].ID == force.Key)
                        {
                            particles[i].WorldForces += force.Value;
                            found = true;
                        }
                        else
                            i++;
                    }
                }
        }

        /// <summary>
        /// Removes a Particle. 
        /// </summary>
        /// <param name="particle">The Particle to be removed.</param>
        internal static void Remove(ParticleComponent particle)
        {
            if (particles.Contains(particle))
            {
                particles.Remove(particle);
                particle.Destroy(false);
            }
        }

        public void Dispose(bool finalize)
        {
            particles.Clear();
            particles = null;
        }
    }
}
