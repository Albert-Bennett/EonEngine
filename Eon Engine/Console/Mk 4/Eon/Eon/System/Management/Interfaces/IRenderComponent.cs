/* Created: 10/06/2013
 * Last Updated: 07/09/2014
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
    public interface IRenderComponent
    {
        int Order { get; }

        void Render();
    }
}
