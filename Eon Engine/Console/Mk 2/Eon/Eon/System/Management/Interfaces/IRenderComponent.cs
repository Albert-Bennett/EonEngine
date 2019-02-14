/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.System.Management.Interfaces
{
    /// <summary>
    /// Defines an EngineComponent that can be rendered.
    /// </summary>
    public interface IRenderComponent : ISortable
    {
        Texture2D FinalImage { get; }
        bool RenderFinal { get; }

        void Render();
    }
}
