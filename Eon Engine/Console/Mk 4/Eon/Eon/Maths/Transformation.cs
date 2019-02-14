/* Created: 05/09/2014
 * Last Updated: 23/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Maths
{
    /// <summary>
    /// Used to define a transformation.
    /// </summary>
    public sealed class Transformation
    {
        Vector3 translation;
        Quaternion rotation;
        Vector3 scale;

        /// <summary>
        /// Positional transformation.
        /// </summary>
        public Vector3 Position
        {
            get { return translation; }
            set { translation = value; }
        }

        /// <summary>
        /// Rotational transform.
        /// </summary>
        public Quaternion Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Scaleable transform.
        /// </summary>
        public Vector3 Size
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Transform as a matrix.
        /// </summary>
        public Matrix Matrix
        {
            get
            {
                return Matrix.CreateScale(scale) *
                 Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) *
                 Matrix.CreateTranslation(translation);
            }
        }

        /// <summary>
        /// The rotation as a matrix.
        /// </summary>
        public Matrix Rotational
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(
                    rotation.Y, rotation.X, rotation.Z);
            }
            set { rotation = EonMathsHelper.GetFromMatrix(value); }
        }

        /// <summary>
        /// The translation as a matrix.
        /// </summary>
        public Matrix Translation
        {
            get
            {
                return Matrix.CreateTranslation(translation);
            }
        }

        /// <summary>
        /// The scale as a matrix.
        /// </summary>
        public Matrix Scale
        {
            get
            {
                return Matrix.CreateScale(scale);
            }
        }

        /// <summary>
        /// A zero based Transformation.
        /// </summary>
        public static Transformation Identity
        {
            get
            {
                return new Transformation()
                    {
                        translation = Vector3.Zero,
                        rotation = Quaternion.Identity,
                        scale = Vector3.One
                    };
            }
        }
    }
}
