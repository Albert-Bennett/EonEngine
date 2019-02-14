/* Created 14/09/2013
 * Last Updated: 13/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using System;

namespace Eon.Animation2D.Skeletal.Animating
{
    /// <summary>
    /// Defines a collection of key frames 
    /// for a specific limb in a 2D Skeleton.
    /// </summary>
    public sealed class LimbKeyFrameCollection
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
        /// Defines a set of messages that are 
        /// to be sent at a certain frame.
        /// </summary>
        public EonDictionary<int, Message> MessageList;

        /// <summary>
        /// The amount of LimbKeyFrames in this.
        /// </summary>
        public int Count
        {
            get { return LimbFrames.Length; }
        }

        /// <summary>
        /// The total number of frames in the LimbKeyFrameAnimation.
        /// </summary>
        public int TotalFrames
        {
            get
            {
                return LimbFrames[LimbFrames.Length - 1].FrameNumber;
            }
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
    }
}
