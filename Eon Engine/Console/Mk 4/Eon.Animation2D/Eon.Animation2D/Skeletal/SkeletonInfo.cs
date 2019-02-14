/* Created 03/06/2013
 * Last Updated: 12/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define a Skeleton.
    /// </summary>
    public class SkeletonInfo
    {
        /// <summary>
        /// Should the Skeleton be post rendered.
        /// </summary>
        public bool PostRender = false;

        /// <summary>
        /// A varable used to signify if the draw layer of
        /// each limb is top be used or is their draw layer 
        /// to be used to order them so that the Skeleton can
        /// be rendered on one draw layer instead of many.
        /// </summary>
        public int Coordinate = -1;

        /// <summary>
        /// The Limb that make up the Skeleton.
        /// </summary>
        public LimbInfo[] Limbs;
    }
}
