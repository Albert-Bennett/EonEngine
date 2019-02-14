/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Particles2D
{
    /// <summary>
    /// A class that define the parameters of a ParticleEmitter.
    /// </summary>
    public class ParticleEmitterInfo
    {
        public string ID;

        public float MinLifeTime;
        public float MaxLifeTime;

        public float Mass;

        public ParameterCollection ParticleRenderType;
        public ParameterCollection EmitterType;
        public ParameterCollection CycleType;

        public ParameterCollection[] Attachments;
    }
}
