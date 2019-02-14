/* Created: 06/03/2015
 * Last Updated: 07/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to help manage LOD distances.
    /// </summary>
    public static class LODManager
    {
        static float[] fragments = new float[]
        {
            0.0f,
            0.75f,
            0.83f,
            0.91f,
            1.0f
        };

        /// <summary>
        /// The distances that define various
        /// lod levels in the range of 0.0 - 1.0.
        /// </summary>
        public static float[] LODFragments
        {
            get { return fragments; }
        }
    }
}
