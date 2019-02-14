using Eon;
using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Materials;
using Microsoft.Xna.Framework;
using System;

namespace MK4Update
{
    public sealed class Model : GameObject
    {
        string filepath;

        public Model(string id, string modelFilepath, Vector3 position)
            : base(id)
        {
            World.Position = position;
            filepath = modelFilepath;
        }

        protected override void Initialize()
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, BasicLPPMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, BasicMaterial>)
            };

            ModelInfo shaders = SerializationHelper.Deserialize<ModelInfo>(
                 filepath + ".Shader", true, "", extraTypes);

            AttachComponent(new ModelComponent(ID + "Model", shaders));

            base.Initialize();
        }

        protected override void Update()
        {
            float rotationRate = 0.01f;
            Vector3 rotation = Vector3.Zero;

            if (InputManager.IsKeyPressed(Keys.Left))
                rotation.Y += rotationRate;
            else if (InputManager.IsKeyPressed(Keys.Right))
                rotation.Y -= rotationRate;

            if (InputManager.IsKeyPressed(Keys.Up))
                rotation.X += rotationRate;
            else if (InputManager.IsKeyPressed(Keys.Down))
                rotation.X -= rotationRate;

            if (rotation != Vector3.Zero)
                World.Rotation += new Quaternion(rotation, 1);

            base.Update();
        }
    }
}
