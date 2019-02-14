/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Particles.D2
{
    /// <summary>
    /// Used to define a 2D ParticleSystem.
    /// </summary>
    public sealed class ParticleSystem2DInfo
    {
        public string AssemblyRef = "Eon.Particles";

        public bool PostRender = false;
        public int DrawLayer = 0;

        public EonDictionary<string, ParticleEmitterInfo> Emitters;
    }
}
