/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Particles
{
    /// <summary>
    /// Used to define the information required 
    /// to create a particle emitter.
    /// </summary>
    public sealed class ParticleEmitterInfo
    {
       public float MinLifeTime;
       public float MaxLifeTime;

       public float MinMass;
       public float MaxMass;

       public ParameterCollection RenderType;
       public ParameterCollection EmittionType;
       public ParameterCollection CycleType;

       public ParameterCollection[]Attachments;
    }
}
