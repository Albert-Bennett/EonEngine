/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Physics2D.Forces.Volumes
{
    /// <summary>
    /// Defines an object that applies a force to objects inside of it.
    /// </summary>
    internal interface IVolumetricForce : IID
    {
        Dictionary<string, Vector2> GetForces();

        void Remove();
    }
}
