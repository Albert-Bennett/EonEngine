/* Created: 09/08/2013
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
    /// Defines a inteface which is used to describe 
    /// objects that are to be rendered after the main scene has been rendered.
    /// </summary>
    public interface IPostRenderComponent
    {
        int Order { get; }

        void PostRender();
    }
}
