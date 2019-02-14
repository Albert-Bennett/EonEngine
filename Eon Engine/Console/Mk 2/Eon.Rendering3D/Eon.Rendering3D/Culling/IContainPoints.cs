/* Created 09/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Culling
{
    /// <summary>
    /// An interface used to define an 
    /// object that contains points.
    /// </summary>
    public interface IContainPoints
    {
        Vector2[] Points { get; }
        int Count { get; }
    }
}
