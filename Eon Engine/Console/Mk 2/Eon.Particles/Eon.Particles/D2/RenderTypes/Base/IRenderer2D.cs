/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.Attachments.Base;
using System.Collections.Generic;

namespace Eon.Particles.D2.RenderTypes.Base
{
    /// <summary>
    /// Used to define a means of rendering particles.
    /// </summary>
    public interface IRenderer2D
    {
        void Draw(List<PropertySet> properties);
    }
}
