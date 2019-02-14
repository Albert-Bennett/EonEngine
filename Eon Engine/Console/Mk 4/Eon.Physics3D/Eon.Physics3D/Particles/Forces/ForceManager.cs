/* Created: 05/09/2014
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Eon.Physics3D.Particles.Forces
{
    /// <summary>
    /// Used to define a manager of IForces and IForceAccumulator.
    /// </summary>
    public sealed class ForceManager : EngineModule, IUpdate
    {
        static List<IForce> forces = new List<IForce>();
        static List<IForceAccumulator> accumulators = new List<IForceAccumulator>();

        public int Priority
        {
            get { return 0; }
        }

        public ForceManager() : base("D3ForceManager") { }

        internal static void Add(IForceAccumulator accumulator)
        {
            accumulators.Add(accumulator);
        }

        /// <summary>
        /// Adds an IForce to be managed.
        /// </summary>
        /// <param name="force">The force to be added.</param>
        public static void Add(IForce force)
        {
            IForce f = (from fr in forces
                        where fr.ID == force.ID
                        select fr).FirstOrDefault();

            if (f == null)
                forces.Add(force);
        }

        public void _Update()
        {
            for (int i = 0; i < forces.Count; i++)
                for (int j = 0; j < accumulators.Count; j++)
                    ThreadPool.QueueUserWorkItem(forces[i].ApplyForce, accumulators[j]);
        }

        public void _PostUpdate() { }

        internal static void Remove(IForceAccumulator accumulator)
        {
            if (accumulators.Contains(accumulator))
                accumulators.Remove(accumulator);
        }

        /// <summary>
        /// removes a force from this.
        /// </summary>
        /// <param name="force">The force to be removed.</param>
        public static void Remove(IForce force)
        {
            if (forces.Contains(force))
                forces.Remove(force);
        }
    }
}
