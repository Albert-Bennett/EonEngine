using Eon;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Materials;
using Microsoft.Xna.Framework;
using System;

namespace MK4VideoTesting
{
    public sealed class StaticModel : GameObject
    {
        ModelComponent mdl;
        string filepath;

        public StaticModel(Vector3 position, string filepath, string id)
            : base(id)
        {
            World.Position = position;

            this.filepath = filepath;
        }

        protected override void Initialize()
        {
            Type[] materialTypes = new Type[]
            {
                typeof(LPPTerrainMaterial),
                typeof(BasicMaterial)
            };

            mdl = new ModelComponent(ID + "Model",
                filepath + "/Shaders.Shader", materialTypes);

            AttachComponent(mdl);

            base.Initialize();
        }
    }
}
