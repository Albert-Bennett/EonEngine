/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering2D.Drawing
{
    /// <summary>
    /// An interface that enables an object tyo be drawn
    /// by either the PostRenderManager or the DrawManager.
    /// </summary>
    public interface IDrawItem
    {
        int DrawLayer { get; }

        void Draw(DrawingStage stage);
    }
}
