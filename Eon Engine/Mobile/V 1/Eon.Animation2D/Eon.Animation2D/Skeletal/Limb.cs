/* Created 03/06/2013
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
    public struct Limb
    {
        public string Name;
        public string ParentLimb;
        public int DrawOrder;

        public string TextureFilepath;

        public Vector2 RotationPoint;
        public Vector2 Offset;
        public Vector2 Size;
        public Rectangle Bounds;

        public Transformation Transform;

        /// <summary>
        /// Used to set the Transformation of this Limb.
        /// </summary>
        /// <param name="transform">The Transformation to use.</param>
        public void SetTransform(Transformation transform)
        {
            Transform = transform;
        }

        /// <summary>
        /// Translates the Limb by a certain amount.
        /// </summary>
        /// <param name="translation">The amount to move the Limb by.</param>
        public void Translate(Vector2 translation)
        {
            Transform.Position += translation;
        }

        /// <summary>
        /// Used to calculate the bounding 
        /// rectangle of this Limb. 
        /// </summary>
        /// <param name="world">The world matrix for the owner of 
        /// the Skeleton that this is a part of.</param>
        public void CalculateBounds(Matrix world)
        {
            Bounds = new Rectangle(
                (int)Transform.Position.X,
                (int)Transform.Position.Y,
                (int)Size.X, (int)Size.Y);

            if (world != null)
            {
                Bounds.X += (int)world.Translation.X;
                Bounds.Y += (int)world.Translation.Y;

                Vector3 scl = Vector3.One;
                Eon.Helpers.EonMathHelper.GetMatrixScale(world, out scl);

                Bounds.Width *= (int)scl.X;
                Bounds.Height *= (int)scl.Y;
            }

            Bounds.Width *= (int)Transform.Scale.X;
            Bounds.Height *= (int)Transform.Scale.Y;
        }

        public void ScreenResolutionChanged()
        {
            Size = Common.ReCalibrateScreenSpaceVector(Size);
            Offset = Common.ReCalibrateScreenSpaceVector(Offset);
        }
    }
}
