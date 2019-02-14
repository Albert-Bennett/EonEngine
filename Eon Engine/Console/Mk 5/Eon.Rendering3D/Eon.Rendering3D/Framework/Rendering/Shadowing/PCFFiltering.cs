/* Created: 05/10/2014
 * Last Updated: 05/10/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering3D.Framework.Rendering.Shadowing
{
    /// <summary>
    /// Used to signify what type of shadow filtering is to be applied.
    /// </summary>
    internal enum PCFFiltering : byte
    {
        X2 = 2,
        X3 = 3,
        X4 = 4,
        X5 = 5,
        X6 = 6,
        X7 = 7
    }
}
