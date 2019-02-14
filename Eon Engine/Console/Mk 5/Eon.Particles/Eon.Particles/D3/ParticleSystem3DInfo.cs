/* Created: 01/09/2014
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Particles.D3
{
    /// <summary>
    /// Used to define a 3D ParticleSystem.
    /// </summary>
    public sealed class ParticleSystem3DInfo
    {
        public string[] AssemblyRefrences = new string[]
        {
            "Eon.Particles"
        };

        public EonDictionary<string, ParticleEmitterInfo> Emitters;
    }
}
