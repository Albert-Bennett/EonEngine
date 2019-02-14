/* Created: 29/03/2015
 * Last Updated: 29/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Decals
{
    /// <summary>
    /// An interface used to define a type of Decal.
    /// </summary>
    public interface IDecal
    {
        Matrix World { get; }
        Texture2D Texture { get; }
        BoundingSphere Bounds { get; }
    }
}
