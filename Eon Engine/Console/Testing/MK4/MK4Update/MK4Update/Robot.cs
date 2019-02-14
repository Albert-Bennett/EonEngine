using Eon;
using Eon.Collections;
using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Materials;
using Microsoft.Xna.Framework;
using System;

namespace MK4Update
{
    public sealed class Robot : GameObject
    {
        ModelComponent mdl;

        public Robot(Vector3 position, string id)
            : base(id)
        {
            World.Position = position;
        }

        protected override void Initialize()
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, BasicLPPMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, Material>)
            };

            ModelInfo shaders = SerializationHelper.Deserialize<ModelInfo>(
                 "Robot/Shaders.Shader", true, "", extraTypes);

            mdl = new ModelComponent(ID + "Model", shaders);
            AttachComponent(mdl);

            base.Initialize();
        }
    }
}
