/* Created 24/09/2015
 * Last Updated: 24/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;
using System.Linq;

namespace Eon.Animation2D.StateManagement
{
    /// <summary>
    /// Defines information about various 
    /// AnimStates contained.
    /// [Serializable]
    /// </summary>
    public sealed class AnimStateInfo
    {
        public string SkeletonFilepath = "null";

        public List<AnimState> States = new List<AnimState>();

        internal bool isLoaded = false;

        /// <summary>
        /// Adds an AnimState to this.
        /// </summary>
        /// <param name="state">The AnimState to be added.</param>
        public void AddState(AnimState state)
        {
            AnimState s = (from a in States
                           where a.State == state.State
                           select a).FirstOrDefault();

            if (s == null)
                States.Add(state);
        }
    }
}
