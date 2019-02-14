/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Forces;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Physics2D.Particles
{
    /// <summary>
    /// Used to manage ParticleObjects.
    /// </summary>
    internal class ParticleManager : IUpdate, IDispose
    {
        static List<ParticleObject> particles = new List<ParticleObject>();

        /// <summary>
        /// Adds a ParticleObject to be managed.
        /// </summary>
        /// <param name="particle">The Particle to be managed.</param>
        internal static void Add(ParticleObject particle)
        {
            particles.Add(particle);
        }

        public void _Update()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].WorldForces += ForceManager.AccumulateForces(particles[i]);
                particles[i].Update();
            }

            List<Dictionary<string, Vector2>> volumetricForces =
                ForceManager.AccumulateVolumetricForces();

            if (volumetricForces != null)
                foreach (Dictionary<string, Vector2> accum in volumetricForces)
                    if (accum != null)
                        for (int i = 0; i < accum.Count; i++)
                            for (int j = 0; j < particles.Count; i++)
                                if (particles[j].AttachedToID != "None")
                                {
                                    Vector2 vec = (from s in accum.Keys
                                                   where s == particles[j].AttachedToID
                                                   select accum[s]).FirstOrDefault();

                                    if (vec != null)
                                        particles[j].WorldForces += vec;
                                }
        }

        /// <summary>
        /// Removes a Particle. 
        /// </summary>
        /// <param name="particle">The Particle to be removed.</param>
        internal static void Remove(ParticleObject particle)
        {
            if (particles.Contains(particle))
                particles.Remove(particle);
        }

        public void Dispose(bool finalize)
        {
            particles.Clear();
            particles = null;
        }
    }
}
