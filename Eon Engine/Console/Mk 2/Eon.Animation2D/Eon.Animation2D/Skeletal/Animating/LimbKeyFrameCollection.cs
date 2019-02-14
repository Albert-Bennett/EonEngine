/* Created 14/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using System;
namespace Eon.Animation2D.Skeletal.Animating
{
    /// <summary>
    /// Defines a collection of key frames 
    /// for a specific limb in a 2D Skeleton.
    /// </summary>
    public sealed class LimbKeyFrameCollection// : List<LimbKeyFrame>
    {
        /// <summary>
        /// The name of the Limb that the
        /// LimbKeyFrames are going to be applied to.
        /// </summary>
        public string LimbName;

        /// <summary>
        /// The LimbKeyFrames in the animation.
        /// </summary>
        public LimbKeyFrame[] LimbFrames;

        /// <summary>
        /// The amount of LimbKeyFrames in this.
        /// </summary>
        public int Count
        {
            get { return LimbFrames.Length; }
        }

        /// <summary>
        /// Finds a LimbKeyFrame at a given index.
        /// </summary>
        /// <param name="index">The index of the item to get.</param>
        /// <returns>The LimbKeyFrame at the given index.</returns>
        public LimbKeyFrame this[int index]
        {
            get
            {
                if (index < Count)
                    return LimbFrames[index];

                throw new ArgumentOutOfRangeException("The index: " + index +
                    " must be less than the size of the collection: " + Count);
            }
        }

        /// <summary>
        /// Adds a LimbKeyFrame to this.
        /// </summary>
        /// <param name="frame">The LimbKeyFrame to be added.</param>
        public void AddFrame(LimbKeyFrame frame)
        {
            LimbFrames = ArrayHelper.AddItem<LimbKeyFrame>(frame, LimbFrames);
        }

        /// <summary>
        /// The time between each LimbKeyFrame.
        /// </summary>
        public float FrameRate;
    }
}
