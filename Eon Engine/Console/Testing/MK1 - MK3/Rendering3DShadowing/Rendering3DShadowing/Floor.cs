using Eon;
using Eon.Rendering3D;
using Microsoft.Xna.Framework;

namespace Rendering3DShadowing
{
    public sealed class Floor : GameObject
    {
        public Floor(string id, Vector3 position)
            : base(id)
        {
            World = Matrix.CreateTranslation(position);
        }

        protected override void Initialize()
        {
            AttachComponent(new ModelComponent(ID + "Model",
             "Models/Floor/Floor", "Models/Floor/Shaders.Shader"));

            base.Initialize();
        }
    }
}
