/* Created 12/07/2013
 * Last Updated: 07/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// defines the transformation of a Limb.
    /// </summary>
    public sealed class Transformation
    {
        Vector2 scale;
        Vector2 position;

        public float Rotation;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public static Transformation Identity
        {
            get
            {
                return new Transformation()
                {
                    Rotation = 0,
                    scale = Vector2.One,
                    position = Vector2.Zero
                };
            }
        }

        public Transformation()
        {
            Rotation = 0;

            scale = Vector2.One;
            position = Vector2.Zero;
        }

        public static Transformation Compose(Transformation transformation1,
            Transformation transformation2)
        {
            Transformation res = Transformation.Identity;

            res.Position = TransformPosition(
                transformation2.Position, transformation1);

            res.Scale = transformation1.Scale * transformation2.Scale;
            res.Rotation = transformation1.Rotation + transformation2.Rotation;

            res.Position *= res.Scale;

            return res;
        }

        static Vector2 TransformPosition(Vector2 position, Transformation transform)
        {
            Vector2 pos = Vector2.Transform(position,
                Matrix.CreateRotationZ(MathHelper.ToRadians(transform.Rotation)));

            pos *= transform.scale;
            pos += transform.position;

            return pos;
        }

        public static void Lerp(ref Transformation transformation1,
            ref Transformation transformation2, float amount,
            out Transformation result)
        {
            result = Transformation.Identity;

            result.Position = Vector2.Lerp(transformation1.Position,
                transformation2.Position, amount);

            result.Scale = Vector2.Lerp(transformation1.Scale,
                transformation2.Scale, amount);

            result.Rotation = MathHelper.Lerp(transformation1.Rotation,
                transformation2.Rotation, amount);
        }

        public Transformation Translate(Vector2 translation)
        {
            Transformation res = this;
            res.Position += translation;

            return res;
        }
    }
}
