using Eon;
using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Shaders;
using Microsoft.Xna.Framework;
using System;

namespace Rendering3DLPP3
{
    public sealed class Terrain : GameObject
    {
        ModelComponent mdl;
        ModelComponent sky;

        public Terrain(Vector3 position, string id) : base(id) 
        {
            World.Position = position;
        }

        protected override void Initialize()
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, LPPTerrainMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, Shader>)
            };

            ModelInfo shaders = SerializationHelper.Deserialize<ModelInfo>(
                 "Terrain2/Shaders.Shader", true, "", extraTypes);

            mdl = new ModelComponent(ID + "Model", shaders);
            AttachComponent(mdl);

            extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, BasicMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, Shader>)
            };

            shaders = SerializationHelper.Deserialize<ModelInfo>(
                 "Terrain2/SkyShaders.Shader", true, "", extraTypes);

            sky = new ModelComponent(ID + "SkySphere", shaders);
            AttachComponent(sky);

            base.Initialize();
        }

        protected override void Update()
        {
            if (InputManager.IsKeyStroked(Keys.Space))
                mdl.ToogleEnable();
            else if (InputManager.IsKeyStroked(Keys.Enter))
                sky.ToogleEnable();

            base.Update();
        }
    }
}
