using Eon;
using Eon.Helpers;
using Eon.Rendering3D;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
   public sealed class Sphere: GameObject
    {
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
    }
}
