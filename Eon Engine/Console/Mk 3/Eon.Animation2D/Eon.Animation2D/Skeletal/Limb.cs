/* Created 03/06/2013
 * Last Updated: 05/10/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines a Limb. ie an animatable part of a Skeleton.
    /// </summary>
    public class Limb
    {
        Rectangle bounds;
        Transformation initialTransform = Transformation.Identity;
        Transformation transform = Transformation.Identity;

        public string Name;
        public string ParentLimb;

        public int Order = 0;

        public string TextureFilepath;
        public string NormalMapFilepath = "Eon/Textures/DefaultTexture";
        public string DistortionMapFilepath = "Eon/Textures/DefaultTexture";

        public float RotationX;
        public float RotationY;

        public float OffsetX;
        public float OffsetY;

        public float SizeX;
        public float SizeY;

        /// <summary>
        /// The bounding area of the Limb.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// The offset of the Limb.
        /// </summary>
        public Vector2 Offset
        {
            get { return new Vector2(OffsetX, OffsetY); }
        }

        /// <summary>
        /// The rotational point of the Limb.
        /// </summary>
        public Vector2 RotationalPoint
        {
            get { return new Vector2(RotationX, RotationY); }
        }

        /// <summary>
        /// The size of the Limb.
        /// </summary>
        public Vector2 Size
        {
            get { return new Vector2(SizeX, SizeY); }
        }

        public Transformation Transform
        {
            get { return Transformation.Compose(initialTransform, transform); }
            set { initialTransform = value; }
        }

        /// <summary>
        /// Used to set the Transformation of this Limb.
        /// </summary>
        /// <param name="transform">The Transformation to use.</param>
        public void SetTransform(Transformation transform)
        {
            this.transform = transform;
        }

        /// <summary>
        /// Translates the Limb by a certain amount.
        /// </summary>
        /// <param name="translation">The amount to move the Limb by.</param>
        public void Translate(Vector2 translation)
        {
            transform.Position += translation;
        }

        /// <summary>
        /// Used to calculate the bounding 
        /// rectangle of this Limb. 
        /// </summary>
        /// <param name="world">The world matrix for the owner of 
        /// the Skeleton that this is a part of.</param>
        public void CalculateBounds(Eon.Maths.Transformation world)
        {
            Transformation transform = Transform;

            if (world != null)
            {
                transform.X += world.Position.X;
                transform.Y += world.Position.Y;

                transform.ScaleX *= world.Size.X;
                transform.ScaleY *= world.Size.Y;
            }

            bounds = new Rectangle(
                (int)((transform.Position.X * transform.Scale.X)),
                (int)((transform.Position.Y * transform.Scale.Y)),
                (int)((Size.X * transform.Scale.X)),
                (int)((Size.Y * transform.Scale.Y)));
        }
    }
}
