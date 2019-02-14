/* Created: 11/09/2014
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.Particles.Attachments.Base;
using Eon.Particles.Base;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Shaders;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Particles.D3.RenderMethods
{
    /// <summary>
    /// Used to define a 
    /// </summary>
    public sealed class ModelParticleRenderer : GameObject, IGenerateRenderer, I3DParticleRenderer
    {
        List<ModelComponent> models = new List<ModelComponent>();

        ModelInfo shaders;

        int totalGenerated = 0;

        public ModelParticleRenderer(string id, string shaderFile)
            : base(id)
        {
            Type[] extraTypes = new Type[]
            { 
                typeof(EonDictionary<string, BasicLPPMaterial>),
                typeof(EonDictionary<int, LODModelInfo>),
                typeof(EonDictionary<string, BasicLPPMaterial>),
                typeof(EonDictionary<string, Shader>)
            };

            shaders = SerializationHelper.Deserialize<ModelInfo>(
                 shaderFile, true, "", extraTypes);
        }

        public void GenerateNew()
        {
            ModelComponent mdl = new ModelComponent(ID + totalGenerated, shaders);
            AttachComponent(mdl);

            models.Add(mdl);

            totalGenerated++;
        }

        public void Render(List<PropertySet> properties)
        {
            for (int i = 0; i < models.Count; i++)
            {
                Vector3 rot = properties[i].Rotation;

                Matrix offset = Matrix.CreateScale(properties[i].Scale) *
                    Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z) *
                    Matrix.CreateTranslation(properties[i].Position);

                models[i].Offset = offset;
            }
        }

        public void Remove(int index)
        {
            models[index].Destroy(true);
            models.RemoveAt(index);
        }

        public void Reset()
        {
            foreach (ModelComponent m in models)
                m.Destroy(true);

            models.Clear();

            totalGenerated = 0;
        }
    }
}
