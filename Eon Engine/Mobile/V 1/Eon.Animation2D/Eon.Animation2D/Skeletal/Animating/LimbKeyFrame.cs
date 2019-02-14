/* Created 14/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Animation2D.Skeletal.Animating
{
    /// <summary>
    /// Defines a key frame for a limb animation. 
    /// </summary>
    public sealed class LimbKeyFrame
    {
        /// <summary>
        /// The number of the frme that this is.
        /// </summary>
        public int FrameNumber;

        /// <summary>
        /// The Transformation to be applied 
        /// to a Limb when animating.
        /// </summary>
        public Transformation Transform;

        internal void ScreenResolutionChanged()
        {
            Transform.Position = Common.ReCalibrateScreenSpaceVector(Transform.Position);
            Transform.Scale = Common.ReCalibrateScreenSpaceVector(Transform.Scale);
        }
    }
}
