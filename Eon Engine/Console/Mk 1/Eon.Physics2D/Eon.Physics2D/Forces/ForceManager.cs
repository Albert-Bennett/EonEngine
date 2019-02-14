/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Eon.Physics2D.Forces.Volumes;
using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Defines a class which used to manage in game forces.
    /// </summary>
    internal class ForceManager : IUpdate, IDispose
    {
        static List<IWorldForce> forces = new List<IWorldForce>();
        static List<IUpdate> updateForces = new List<IUpdate>();
        static List<IVolumetricForce> volumetricForces = new List<IVolumetricForce>();

        /// <summary>
        /// Adds a force.
        /// </summary>
        /// <param name="force">The force to be applied.</param>
        internal static void AddForce(IWorldForce force)
        {
            IWorldForce f = null;

            f = (from c in forces
                 where c.ID == force.ID
                 select c).FirstOrDefault();

            if (f == null)
            {
                if (force is IUpdate)
                    updateForces.Add(force as IUpdate);

                forces.Add(force);
            }
        }

        /// <summary>
        /// Adds a volumetric force to this.
        /// </summary>
        /// <param name="volume">The volumetric force to be added.</param>
        internal static void AddForce(IVolumetricForce volume)
        {
            IVolumetricForce v = null;

            v = (from c in volumetricForces
                 where c.ID == volume.ID
                 select c).FirstOrDefault();

            if (v == null)
            {
                if (volume is IUpdate)
                    updateForces.Add(volume as IUpdate);

                volumetricForces.Add(volume);
            }
        }

        /// <summary>
        /// Calculates the force to be applied to an object.
        /// </summary>
        /// <param name="particle">The particle to get the applied forces for.</returns>
        internal static Vector2 AccumulateForces(ParticleObject particle)
        {
            Vector2 force = Vector2.Zero;

            for (int i = 0; i < forces.Count; i++)
                force += forces[i].CalculateAppliedForce(particle);

            return force;
        }

        internal static List<Dictionary<string, Vector2>> AccumulateVolumetricForces()
        {
            if (volumetricForces.Count > 0)
            {
                List<Dictionary<string, Vector2>> ids = 
                    new List<Dictionary<string, Vector2>>();

                for (int i = 0; i < volumetricForces.Count; i++)
                    ids.Add(volumetricForces[i].GetForces());

                return ids;
            }

            return null;
        }

        public void _Update()
        {
            for (int i = 0; i < updateForces.Count; i++)
                updateForces[i]._Update();
        }

        /// <summary>
        /// Removes a force.
        /// </summary>
        /// <param name="force">The force to be removed.</param>
        internal static void Remove(IWorldForce force)
        {
            if (forces.Contains(force))
                forces.Remove(force);

            if (force is IUpdate && updateForces.Contains(force as IUpdate))
                updateForces.Remove(force as IUpdate);
        }

        /// <summary>
        /// Removes a volumetric force from this.
        /// </summary>
        /// <param name="force">The volumetric force to be removed.</param>
        internal static void Remove(IVolumetricForce force)
        {
            if (volumetricForces.Contains(force))
                volumetricForces.Remove(force);

            if (force is IUpdate && updateForces.Contains(force as IUpdate))
                updateForces.Remove(force as IUpdate);
        }

        public void Dispose(bool finalize)
        {
            updateForces.Clear();
            updateForces = null;

            forces.Clear();
            forces = null;
        }
    }
}
