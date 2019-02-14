/* Created: 10/06/2013
 * Last Updated: 14/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;

namespace Eon.Rendering2D.Drawing
{
    /// <summary>
    /// An interface that enables an object tyo be drawn
    /// by either the PostRenderManager or the DrawManager.
    /// </summary>
    public interface IDrawItem: IEnabled
    {
        int DrawLayer { get; }
        bool RenderDisabled { get;}

        void Draw(DrawingStage stage);
    }
}
