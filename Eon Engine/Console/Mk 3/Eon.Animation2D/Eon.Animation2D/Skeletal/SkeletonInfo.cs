/* Created 03/06/2013
 * Last Updated: 18/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define a Skeleton.
    /// </summary>
    public class SkeletonInfo
    {
        public int R;
        public int G;
        public int B;
        public int A;

        /// <summary>
        /// The draw layer of the Skeleton.
        /// </summary>
        public int DrawLayer = 0;

        /// <summary>
        /// Should the Skeleton be post rendered.
        /// </summary>
        public bool PostRender = false;

        /// <summary>
        /// The Limb that make up the Skeleton.
        /// </summary>
        public Limb[] Limbs;
    }
}
