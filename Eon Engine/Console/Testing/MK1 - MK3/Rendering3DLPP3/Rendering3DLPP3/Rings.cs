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
    public sealed class Rings : GameObject
    {
        ModelComponent mdl;

        public Rings() : base("Rings") { }

        protected override void Initialize()
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, BasicLPPMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, Shader>)
            };

            ModelInfo shaders = SerializationHelper.Deserialize<ModelInfo>(
                 "Rings/Shaders.Shader", true, "", extraTypes);

            mdl = new ModelComponent(ID + "Model", shaders);

            AttachComponent(mdl);

            base.Initialize();
        }

        protected override void Update()
        {
            mdl.TransformBone("C0", Matrix.CreateRotationY(0.1f));
            mdl.TransformBone("C1", Matrix.CreateRotationX(0.1f));

            base.Update();
        }
    }
}
