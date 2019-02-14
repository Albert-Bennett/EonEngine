using Eon;
using Eon.Rendering3D;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
   public sealed class SkySphere: GameObject
    {
       public SkySphere(string id, Vector3 position)
           : base(id)
       {
           World =  Matrix.CreateScale(3000) *
               Matrix.CreateTranslation(position);
       }

       protected override void Initialize()
       {
           ModelComponent comp = new ModelComponent(ID + "Model",
            "Models/SkySphere/Sphere", "Models/SkySphere/Shaders.Shader");

           AttachComponent(comp);

           base.Initialize();
       }
    }
}
