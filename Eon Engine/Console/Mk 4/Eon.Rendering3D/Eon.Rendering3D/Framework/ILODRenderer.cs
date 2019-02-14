/* Created: 27/09/2014
 * Last Updated: 27/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework
{
    /// <summary>
    /// Used to define a primative that has lod.
    /// </summary>
    public interface ILODRenderer : IRenderable3D
    {
        Matrix World { get; }

        LODLevels CurrentLOD { get; }
    }
}
