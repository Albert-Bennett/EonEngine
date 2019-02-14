/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using System.Collections.Generic;

namespace Eon.Particles2D
{
    /// <summary>
    /// A class that is used to define the parameters of a ParticleSystem.
    /// </summary>
    public class ParticleSystemInfo
    {
        public bool PostDraw = false;
        public int DrawLayer;

        public List<ParticleEmitterInfo> Emitters =
            new List<ParticleEmitterInfo>();
    }
}
