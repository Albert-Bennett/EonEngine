/* Created: 03/10/2013
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Forces.Volumes;
using Eon.System.Interfaces;
using Eon.System.Management;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Defines a class which used to manage in game forces.
    /// </summary>
    internal class ForceManager : EngineComponent, IUpdate
    {
        static List<IForce> forces = new List<IForce>();
        static List<IUpdate> updateForces = new List<IUpdate>();
        static List<IVolumetricForce> volumetricForces = new List<IVolumetricForce>();

        static List<IForceAccumulator> accumulators = new List<IForceAccumulator>();

        public int Priority
        {
            get { return 0; }
        }

        public ForceManager() : base("D2ForceManager") { }

        /// <summary>
        /// Adds a force.
        /// </summary>
        /// <param name="force">The force to be applied.</param>
        internal static void AddForce(IForce force)
        {
            IForce f = (from fr in forces
                        where fr.ID == force.ID
                        select fr).FirstOrDefault();

            if (f == null)
            {
                forces.Add(force);

                if (force is IUpdate)
                    updateForces.Add(force as IUpdate);
            }
        }

        internal static void Add(IForceAccumulator accumulator)
        {
            accumulators.Add(accumulator);
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

        internal static Dictionary<string, Vector2> AccumulateVolumetricForces()
        {
            if (volumetricForces.Count > 0)
            {
                Dictionary<string, Vector2> forces =
                    new Dictionary<string, Vector2>();

                for (int i = 0; i < volumetricForces.Count; i++)
                {
                    Dictionary<string, Vector2> temp = volumetricForces[i].GetForces();

                    foreach (KeyValuePair<string, Vector2> f in temp)
                        if (forces.ContainsKey(f.Key))
                            forces[f.Key] += f.Value;
                        else
                            forces.Add(f.Key, f.Value);
                }

                return forces;
            }

            return null;
        }

        public void _Update()
        {
            for (int i = 0; i < updateForces.Count; i++)
                updateForces[i]._Update();

            for (int i = 0; i < forces.Count; i++)
                for (int j = 0; j < accumulators.Count; j++)
                    ThreadPool.QueueUserWorkItem(forces[i].ApplyForce, accumulators[j]);
        }

        /// <summary>
        /// Removes a force.
        /// </summary>
        /// <param name="force">The force to be removed.</param>
        internal static void Remove(IForce force)
        {
            if (forces.Contains(force))
            {
                forces.Remove(force);

                if (force is IUpdate)
                    updateForces.Remove(force as IUpdate);
            }
        }

        internal static void Remove(IForceAccumulator accumulator)
        {
            if (accumulators.Contains(accumulator))
                accumulators.Remove(accumulator);
        }

        /// <summary>
        /// Removes a volumetric force from this.
        /// </summary>
        /// <param name="force">The volumetric force to be removed.</param>
        internal static void Remove(IVolumetricForce force)
        {
            if (volumetricForces.Contains(force))
            {
                volumetricForces.Remove(force);

                if (force is IUpdate && updateForces.Contains(force as IUpdate))
                    updateForces.Remove(force as IUpdate);
            }
        }

        public void Remove(object obj)
        {
            if (volumetricForces.Contains(obj as IVolumetricForce))
            {
                volumetricForces.Remove(obj as IVolumetricForce);

                if (obj is IUpdate && updateForces.Contains(obj as IUpdate))
                    updateForces.Remove(obj as IUpdate);
            }
        }

        public void Dispose(bool finalize)
        {
            if (updateForces != null)
                updateForces.Clear();

            updateForces = null;

            if (forces != null)
                forces.Clear();

            forces = null;

            accumulators.Clear();
            accumulators = null;
        }
    }
}
