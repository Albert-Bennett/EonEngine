/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles2D.Emitters
{
    /// <summary>
    /// Defines a method for spawning Particles.
    /// </summary>
    public interface IEmitterType
    {
        Vector2 Position { get; set; }

        Vector2 CreateEmittionPoint();
    }
}
