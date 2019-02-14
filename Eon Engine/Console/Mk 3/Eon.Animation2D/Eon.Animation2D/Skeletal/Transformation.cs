/* Created 12/07/2013
 * Last Updated: 21/09/2014
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
        public float X;
        public float Y;

        public float ScaleX;
        public float ScaleY;

        public float Rotation;

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Scale
        {
            get { return new Vector2(ScaleX, ScaleY); }
            set
            {
                ScaleX = value.X;
                ScaleY = value.Y;
            }
        }

        public static Transformation Identity
        {
            get
            {
                return new Transformation()
                {
                    Rotation = 0,
                    ScaleX = 1,
                    ScaleY = 1,
                    Y = 0,
                    X = 0
                };
            }
        }

        public Transformation()
        {
            Rotation = 0;

            X = 0;
            Y = 0;

            ScaleX = 1;
            ScaleY = 1;
        }

        public static Transformation Compose(Transformation transformation1,
            Transformation transformation2)
        {
            Transformation res = Transformation.Identity;

            res.Scale = transformation1.Scale * transformation2.Scale;
            res.Rotation = transformation1.Rotation + transformation2.Rotation;

            res.Position = transformation1.Position + transformation2.Position;
           // res.Position *= res.Scale;

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
