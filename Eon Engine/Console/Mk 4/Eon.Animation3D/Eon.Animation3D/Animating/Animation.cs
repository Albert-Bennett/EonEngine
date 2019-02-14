/* Created 03/07/2014
 * Last Updated: 03/07/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;

namespace Eon.Animation3D.Animating
{
    /// <summary>
    /// Used to define an animation.
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// The name of the Animation.
        /// </summary>
        public string Name;

        /// <summary>
        /// The time it takes to change frames.
        /// </summary>
        public float FrameRate;

        /// <summary>
        /// The individual bone transforms in the Animation.
        /// </summary>
        public BoneAnimation[] BoneAnimations = new BoneAnimation[0];
    }
}
