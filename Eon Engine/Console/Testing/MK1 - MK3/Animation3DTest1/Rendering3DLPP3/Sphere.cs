using Eon;
using Eon.Collections;
using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Shaders;
using Microsoft.Xna.Framework;
using System;

namespace Animation3DTest1
{
    public sealed class Sphere : GameObject
    {
        public Sphere(string id, Vector3 position)
            : base(id)
        {
            World.Position = position;
        }

        protected override void Initialize()
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, BasicLPPMaterial>),
                typeof(EonDictionary<int, LODModelInfo>)
            };

            ModelInfo shaders = SerializationHelper.Deserialize<ModelInfo>(
                 "Models/Sphere/Shaders.Shader", true, "", extraTypes);

            AttachComponent(new ModelComponent(ID + "Model", shaders));

            base.Initialize();
        }
    }
}
