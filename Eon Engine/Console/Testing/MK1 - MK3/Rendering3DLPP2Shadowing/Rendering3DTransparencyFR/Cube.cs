using Eon;
using Eon.Helpers;
using Eon.Rendering3D;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
   public sealed class Cube: GameObject
    {
       public Cube(string id, Vector3 position)
           : base(id)
       {
           World = Matrix.CreateTranslation(position);
       }

       protected override void Initialize()
       {
           ModelComponent comp = new ModelComponent(ID + "Model",
            "Models/Cube/Cube", "Models/Cube/Shaders.Shader");

           AttachComponent(comp);

           base.Initialize();
       }
    }
}
