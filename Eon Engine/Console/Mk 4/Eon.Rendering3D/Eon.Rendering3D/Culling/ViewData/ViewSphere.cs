/* Created: 17/07/2015
 * Last Updated: 17/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering3D.Culling.ViewData
{
    /// <summary>
    /// Defines an object that contains an 
    /// EonBoundingSphere and index data. 
    /// </summary>
    public sealed class ViewSphere
    {
        /// <summary>
        /// Used with EonBoundingSphere
        /// </summary>
        public string BoundingSphere;

        /// <summary>
        /// A total of 3 indices are to be held.
        /// </summary>
        public int[] Indices;
    }
}
