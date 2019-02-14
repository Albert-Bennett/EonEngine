/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles2D
{
    /// <summary>
    /// A class that is used to define the parameters of a ParticleSystem.
    /// </summary>
    public class ParticleSystemInfo
    {
        public string Name;
        public bool PostDraw = false;
        public int DrawLayer;

        public ParticleEmitterInfo[] Emitters;
    }
}
