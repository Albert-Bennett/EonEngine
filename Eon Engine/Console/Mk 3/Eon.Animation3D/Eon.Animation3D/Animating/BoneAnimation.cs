/* Created 03/07/2014
 * Last Updated: 03/07/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Animation3D.Animating
{
    /// <summary>
    /// Defines the frame animations to be applied to a bone.
    /// </summary>
    public class BoneAnimation
    {
        /// <summary>
        /// The name of the bone that this animation is of.
        /// </summary>
        public string BoneName;

        /// <summary>
        /// The transformations in the current bone animation.
        /// </summary>
        public AnimationTransform[] Transforms = new AnimationTransform[0];

        /// <summary>
        /// The number of bone transforms in this BoneAnimation.
        /// </summary>
        public int Count
        {
            get { return Transforms.Length; }
        }

        /// <summary>
        /// Adds a new frame to the BoneAnimation.
        /// </summary>
        /// <param name="transform">The bone transform to be added.</param>
        public void Add(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            AnimationTransform transform = new AnimationTransform()
            {
                Position = position,
                Rotation = rotation,
                Scale = scale
            };

            Transforms = ArrayHelper.AddItem<AnimationTransform>(transform, Transforms);
        }
    }
}
