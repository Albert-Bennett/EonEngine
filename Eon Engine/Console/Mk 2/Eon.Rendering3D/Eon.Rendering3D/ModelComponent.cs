/* Created 08/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.Rendering3D.Framework.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to define a 3D model which is comprised of multiple mesh parts.
    /// </summary>
    public class ModelComponent : ObjectComponent
    {
        List<MeshPart> meshes = new List<MeshPart>();

        Matrix[] boneTransforms;

        string modelFilepath;
        string shaderFilepath;

        Matrix offset;

        /// <summary>
        /// The MeshParts in this ModelComponent.
        /// </summary>
        internal List<MeshPart> Meshes
        {
            get { return meshes; }
        }

        /// <summary>
        /// The world matrix of the ModelComponent.
        /// </summary>
        public Matrix World
        {
            get { return offset * Owner.World.Matrix; }
        }

        /// <summary>
        /// Gets the transformation matrix of a bone.
        /// </summary>
        /// <param name="index">The index of the bone to get the transform of.</param>
        /// <returns>The result of the check.</returns>
        public Matrix this[int index]
        {
            get { return boneTransforms[index]; }
        }

        /// <summary>
        /// Creates a new ModelComponent.
        /// </summary>
        /// <param name="id">The id of the ModelComponent.</param>
        /// <param name="modelFilepath">The filepath for the model.</param>
        /// <param name="shaderFilepath">The filepath for the shader information.</param>
        public ModelComponent(string id, string modelFilepath,
            string shaderFilepath)
            : base(id)
        {
            this.modelFilepath = modelFilepath;
            this.shaderFilepath = shaderFilepath;

            ModelManager.Add(this);
        }

        protected override void Initialize()
        {
            ModelDefination shaders = XmlHelper.DeserializeContent<ModelDefination>(shaderFilepath);
            Model model = Common.ContentManager.Load<Model>(modelFilepath);

            boneTransforms = new Matrix[model.Bones.Count()];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            offset = Matrix.CreateScale(shaders.Scale)
                * Matrix.CreateFromYawPitchRoll(
                    shaders.RotationalOffset.Y, shaders.RotationalOffset.X, shaders.RotationalOffset.Z)
                * Matrix.CreateTranslation(shaders.PositionalOffset);

            for (int i = 0; i < model.Meshes.Count; i++)
                if (shaders.Shaders.Contains(model.Meshes[i].Name))
                {
                    MeshPart part = new MeshPart(this, model.Meshes[i],
                        shaders.Shaders[model.Meshes[i].Name]);

                    meshes.Add(part);
                }

            base.Initialize();
        }

        /// <summary>
        /// Used to animate the ModelComponent.
        /// </summary>
        /// <param name="boneTransforms">The transformation matrices of the bones in the animation.</param>
        public void Animate(EonDictionary<string, Matrix> boneTransforms)
        {
            //for (int i = 0; i < boneTransforms.Count; i++)
            //    for (int j = 0; j < meshes.Count; j++)
            //        if (meshes[j].ParentBoneName == boneTransforms.Keys[i])
            //            meshes[j].SetParentBone(boneTransforms[i].Value);
        }

        /// <summary>
        /// Used to render the ModelComponent.
        /// </summary>
        /// <param name="renderType">The way that the ModelComponent is to be rendered.</param>
        public void Render(RenderTypes renderType)
        {
            if (Enabled)
                for (int i = 0; i < meshes.Count; i++)
                    if (meshes[i].RenderType == renderType)
                        meshes[i].Render();
        }

        /// <summary>
        /// Used to render the ModelComponent using a specific technique.
        /// </summary>
        /// <param name="technique">The technique used to render the ModelComponent.</param>
        /// <param name="renderType">The way that the ModelComponent is to be rendered.</param>
        public void Render(RenderTypes renderType, string technique)
        {
            if (Enabled)
                for (int i = 0; i < meshes.Count; i++)
                    if (meshes[i].RenderType == renderType)
                        meshes[i].Render(technique);
        }

        public override void Destroy(bool remove)
        {
            meshes.Clear();

            ModelManager.Remove(this);

            base.Destroy(remove);
        }
    }
}
