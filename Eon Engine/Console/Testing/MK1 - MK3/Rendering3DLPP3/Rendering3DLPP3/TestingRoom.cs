using Eon;
using Eon.Collections;
using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Shaders;
using System;

namespace Rendering3DLPP3
{
    public sealed class TestingRoom : GameObject
    {
        public TestingRoom() : base("TestingRoom") { }

        protected override void Initialize()
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, BasicLPPMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, Shader>)
            };

            ModelInfo shaders = SerializationHelper.Deserialize<ModelInfo>(
                 "TestingRoom/Shaders.Shader", true, "", extraTypes);

            AttachComponent(new ModelComponent(ID + "Model", shaders));

            base.Initialize();
        }
    }
}
