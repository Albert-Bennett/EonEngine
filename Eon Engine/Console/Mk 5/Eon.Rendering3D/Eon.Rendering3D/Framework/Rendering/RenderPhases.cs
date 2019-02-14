/* Created: 10/07/2015
 * Last Updated: 10/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering3D.Framework.Rendering
{
    /// <summary>
    /// Defines in what stage will a RenderPass be executed.
    /// </summary>
    public enum RenderPhases : int
    {
        PreLighting = 0,
        PostLighting = 1
    }
}
