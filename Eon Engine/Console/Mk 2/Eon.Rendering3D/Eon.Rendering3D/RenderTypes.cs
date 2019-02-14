/* Created 26/07/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to define the specifics of 
    /// rendering ModelComponents.
    /// </summary>
    public enum RenderTypes : byte
    {
        LightingPrePass = 0,
        ForwardRender = 1,
        Transparency = 2
    }
}
