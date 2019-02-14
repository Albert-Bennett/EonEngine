using Microsoft.Xna.Framework;

namespace Eon.Animation3D.Animating
{
    /// <summary>
    /// Used to define a bone transformation.
    /// </summary>
   public class Transform
    {
       /// <summary>
       /// Positional transform.
       /// </summary>
       public Vector3 Position;

       /// <summary>
       /// Rotational transform.
       /// </summary>
       public Vector3 Rotation;

       /// <summary>
       /// Scaleable transform.
       /// </summary>
       public Vector3 Scale;

       /// <summary>
       /// Transform as a matrix.
       /// </summary>
       public Matrix Matrix
       {
           get
           {
               return Matrix.CreateScale(Scale) *
                Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(Rotation.Y),
                MathHelper.ToRadians(Rotation.X),
                MathHelper.ToRadians(Rotation.Z)) *
                Matrix.CreateTranslation(Position);
           }
       }
    }
}
