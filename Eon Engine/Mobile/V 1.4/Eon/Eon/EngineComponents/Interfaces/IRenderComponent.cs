/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.EngineComponents.Interfaces
{
    /// <summary>
    /// Defines an EngineComponent that can be rendered.
    /// </summary>
    public interface IRenderComponent : ISortable
    {
        Texture2D FinalImage { get; }
        bool RenderFinal { get; }

        void Render();
        void ScreenResolutionChanged();
    }
}
