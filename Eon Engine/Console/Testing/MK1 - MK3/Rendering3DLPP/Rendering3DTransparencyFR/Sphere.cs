using Eon;
using Eon.Helpers;
using Eon.Rendering3D;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
   public sealed class Sphere: GameObject
    {
       Vector3 rot;

       public Sphere(string id, Vector3 position)
           : base(id)
       {
           World = Matrix.CreateTranslation(position);
       }

       protected override void Initialize()
       {
           AttachComponent(new ModelComponent(ID + "Model",
            "Models/Sphere/Sphere", "Models/Sphere/Shaders.Shader"));

           base.Initialize();
       }

       protected override void Update()
       {
           float max = MathHelper.ToRadians(30);

           rot += RandomHelper.GetRandomVector3(
                new Vector3(0f, 0f, 0f),
                new Vector3(max, max, max));

           Vector3 pos = World.Translation;

           World = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z) *
               Matrix.CreateTranslation(pos);

           base.Update();
       }
    }
}
