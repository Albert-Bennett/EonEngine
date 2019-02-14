/* Created 27/07/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to depth order sort all transparent objects.
    /// </summary>
    internal struct DepthSortCache
    {
        public float Distance;
        public int Index;
        public char Identifier;
    }
}
