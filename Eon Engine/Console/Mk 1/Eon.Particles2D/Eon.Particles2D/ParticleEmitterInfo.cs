/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Collections;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Particles2D
{
    /// <summary>
    /// A class that define the parameters of a ParticleEmitter.
    /// </summary>
    public class ParticleEmitterInfo
    {
        public string ID;

        public ParameterCollection ParticleRenderType;
        public ParameterCollection EmitterType;
        public ParameterCollection CycleType;

        public List<ParameterCollection> Attachments =
             new List<ParameterCollection>();

        public float MinLifeTime;
        public float MaxLifeTime;

        public Vector2 MinAcceleration;
        public Vector2 MaxAcceleration;

        public float Mass;
    }
}
