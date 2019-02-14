/* Created 25/07/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Billboards
{
    /// <summary>
    /// Defines a type of Billbioard that faces one direction only.
    /// </summary>
    public sealed class LockedAxisBillboard : Billboard
    {
        Vector3 axis;
        Vector3 up;

        public LockedAxisBillboard(Vector3 position, float scale,
            Vector3 rotation, string textureFilepath)
            : base(position, scale, rotation, textureFilepath)
        {
            Matrix trans = Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);

            axis = Vector3.Transform(Vector3.Right, trans);
            up = Vector3.Transform(Vector3.Up, trans);
        }

        public override void Initialize()
        {
            effectFilepath = "Eon/Shaders/Materials/LABillboard";

            base.Initialize();
        }

        public override void Render()
        {
            StartRender();

            Effect.Parameters["Up"].SetValue(up);

            Effect.Parameters["Scale"].SetValue(Scale);
            Effect.Parameters["Pos"].SetValue(Position);
            Effect.Parameters["Axis"].SetValue(axis);

            EndRender();
        }
    }
}
