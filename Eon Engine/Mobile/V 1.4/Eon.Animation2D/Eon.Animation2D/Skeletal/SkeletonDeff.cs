/* Created 03/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define a Skeleton.
    /// </summary>
    public class SkeletonDeff
    {
        /// <summary>
        /// The draw colour of the Skeleton.
        /// </summary>
        public Color Colour;

        /// <summary>
        /// The Limb that make up the Skeleton.
        /// </summary>
        public Limb[] Limbs;
    }
}
