/* Created: 17/07/2015
 * Last Updated: 17/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.BoundingVolumes;
using System.Collections.Generic;

namespace Eon.Rendering3D.Culling.ViewData
{
    /// <summary>
    /// Used to define view data for a Model.
    /// </summary>
    public sealed class ViewModelData
    {
        /// <summary>
        /// To be used with either EonBoundingBox or EonBoundingSphere.
        /// </summary>
        public string BoundingVolume;

        /// <summary>
        /// What EonBoundingVolume should be created.
        /// </summary>
        public BoundingVolumeTypes VolumeType;

        /// <summary>
        /// A list of ViewSpheres that make up the Vertex 
        /// group associated with the ViewModelData. 
        /// </summary>
        public List<ViewSphere> ViewSpheres;
    }
}
