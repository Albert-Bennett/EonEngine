/* Created 05/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

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
        public Vector3 Translation
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
        public Vector3 Scale
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
