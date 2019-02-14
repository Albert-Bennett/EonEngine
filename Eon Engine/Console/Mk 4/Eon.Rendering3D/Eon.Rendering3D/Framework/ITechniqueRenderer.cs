/* Created: 24/09/2014
 * Last Updated: 05/02/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework
{
    /// <summary>
    /// Used to define an interface that allows for shader 
    /// techniques to be changed whilst rendering an object.
    /// </summary>
    public interface ITechniqueRenderer : IRenderable3D
    {
        void Render(Effect effect);
    }
}
