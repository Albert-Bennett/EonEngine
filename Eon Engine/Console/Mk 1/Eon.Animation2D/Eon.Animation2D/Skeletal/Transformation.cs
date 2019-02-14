/* Created 12/07/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Animation2D.Skeletal
{
    public class Transformation
    {
        public Vector2 Position;
        public Vector2 Scale;
        public float Rotation;

        public static Transformation Identity
        {
            get
            {
                return new Transformation()
                {
                    Position = Vector2.Zero,
                    Rotation = 0,
                    Scale = Vector2.One
                };
            }
        }

        public Transformation()
        {
            Position = Vector2.Zero;
            Rotation = 0;
            Scale = Vector2.One;
        }

        public static Transformation Compose(Transformation transformation1,
            Transformation transformation2)
        {
            Transformation res = Transformation.Identity;

            res.Scale = transformation1.Scale * transformation2.Scale;
            res.Rotation = transformation1.Rotation + transformation2.Rotation;

            res.Position = transformation1.Transform(transformation2.Position);

            return res;
        }

        public Vector2 Transform(Vector2 position)
        {
            Vector2 res = Vector2.Transform(position,
               Matrix.CreateRotationZ(Rotation));

            res *= Scale;
            res += position;

            return res;
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
