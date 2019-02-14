using Eon;
using Eon.Collections;
using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Materials;
using Microsoft.Xna.Framework;
using System;

namespace MK4Update
{
    public sealed class WaterTest : GameObject
    {
        ModelComponent mdl;

        public WaterTest(Vector3 position)
            : base("Water")
        {
            World.Position = position;
        }

        protected override void Initialize()
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, WaterMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, Material>)
            };

            ModelInfo shaders = SerializationHelper.Deserialize<ModelInfo>(
                 "WaterTest/Water_Mat.Shader", true, "", extraTypes);

            mdl = new ModelComponent(ID + "Model", shaders);
            AttachComponent(mdl);

            base.Initialize();
        }
    }
}
