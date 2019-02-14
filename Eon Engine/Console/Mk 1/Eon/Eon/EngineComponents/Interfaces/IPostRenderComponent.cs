/* Created 09/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.EngineComponents.Interfaces
{
    /// <summary>
    /// Defines a inteface which is used to describe 
    /// objects that are to be rendered after the main scene has been rendered.
    /// </summary>
    public interface IPostRenderComponent : ISortable
    {
        Texture2D FinalImage { get; }

        void PostRender();
        void ScreenResolutionChanged();
    }
}
