using Eon;
using Eon.Helpers;
using Eon.Rendering3D;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
   public sealed class Scutler: GameObject
    {
       public Scutler(string id, Vector3 position)
           : base(id)
       {
          // World = Matrix.CreateTranslation(position); //Matrix.CreateScale(0.5f)* Matrix.CreateTranslation(position);
       }

       protected override void Initialize()
       {
           ModelComponent comp = new ModelComponent(ID + "Model",
            "Models/Enemies/Scutler/Scutler", "Models/Enemies/Scutler/Shaders.Shader");

           AttachComponent(comp);

           base.Initialize();
       }
    }
}
