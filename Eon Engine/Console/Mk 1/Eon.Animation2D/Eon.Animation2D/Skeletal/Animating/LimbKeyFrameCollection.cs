/* Created 14/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using System;
using System.Collections.Generic;

namespace Eon.Animation2D.Skeletal.Animating
{
    /// <summary>
    /// Defines a collection of key frames 
    /// for a specific limb in a 2D Skeleton.
    /// </summary>
    public sealed class LimbKeyFrameCollection : List<LimbKeyFrame>
    {
        /// <summary>
        /// The name of the Limb that the
        /// LimbKeyFrames are going to be applied to.
        /// </summary>
        public string LimbName { get; set; }

        /// <summary>
        /// The time between each LimbKeyFrame.
        /// </summary>
        public TimeSpan FrameRate { get; set; }

        internal void ScreenResolutionChanged()
        {
            foreach (LimbKeyFrame frame in this)
                frame.ScreenResolutionChanged();
        }
    }
}
