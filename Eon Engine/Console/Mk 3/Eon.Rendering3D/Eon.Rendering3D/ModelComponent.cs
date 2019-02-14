/* Created: 08/05/2014
 * Last Updated: 02/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Shaders;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to define a 3D model which is comprised of multiple mesh parts.
    /// </summary>
    public class ModelComponent : ObjectComponent, IUpdate
    {
        Dictionary<int, LODModel> models = new Dictionary<int, LODModel>();

        ModelInfo info;

        Matrix shaderOffset;
        Matrix baseTransform = Matrix.Identity;

        int index;
        LODLevels lod = LODLevels.One;

        bool isStatic = false;

        public LODLevels CurrentLOD
        {
            get { return lod; }
        }

        /// <summary>
        /// The world matrix of the ModelComponent.
        /// </summary>
        public Matrix World
        {
            get { return Owner.World.Matrix * shaderOffset * baseTransform; }
            set { baseTransform = value; }
        }

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// An additional baseTransform of the ModelComponent 
        /// outside of shader offsets and parent offsets.
        /// </summary>
        public Matrix BaseTransform
        {
            get { return baseTransform; }
            set { baseTransform = value; }
        }

        /// <summary>
        /// The origin of the current LODModel.
        /// </summary>
        public Vector3 Origin
        {
            get { return models[index].Origin; }
        }

        /// <summary>
        /// Creates a new ModelComponent.
        /// </summary>
        /// <param name="id">The id of the ModelComponent.</param>
        /// <param name="shaderFilepath">The filepath for the shader information.</param>
        public ModelComponent(string id,  ModelInfo info)
            : base(id)
        {
            this.info = info;

            isStatic = info.IsStatic;
        }

        protected override void Initialize()
        {
            shaderOffset = Matrix.CreateScale(info.Scale)
                * Matrix.CreateFromYawPitchRoll(
                    info.RotationY, info.RotationX, info.RotationZ)
                * Matrix.CreateTranslation(new Vector3(info.PositionX, info.PositionY, info.PositionZ));

            for (int i = 0; i < info.Models.Count; i++)
                models.Add(info.Models[i].Key, new LODModel(
                    info.Models[i].Value, shaderOffset, Owner.World.Matrix));

            base.Initialize();
        }

        public void _Update()
        {
            if (models.Count == 1)
            {
                if (lod != LODLevels.Zero)
                {
                    lod = LODLevels.Zero;
                    models[(int)lod].ToogleEnabled();
                }
            }
            else
            {
                LODLevels temp = CameraManager.CurrentCamera.GetLODLevel(World.Translation);

                if (temp != lod)
                    if (models.ContainsKey((int)temp))
                    {
                        index = (int)temp;

                        models[(int)lod].ToogleEnabled();
                        models[index].ToogleEnabled();

                        lod = temp;
                    }
                    else
                    {
                        models[(int)lod].ToogleEnabled();

                        lod = temp;
                    }
            }

            if (!isStatic)
                models[index].Update(World);
        }

        /// <summary>
        /// Transforms a bone.
        /// </summary>
        /// <param name="boneName">The name of the bone to be transformed.</param>
        /// <param name="transformation">The transformation to be applied.</param>
        public void TransformBone(string boneName, Matrix transformation)
        {
            models[index].TransformBone(boneName, transformation);
        }

        /// <summary>
        /// Gets the world matrix of a known MeshPart.
        /// </summary>
        /// <param name="meshName">The name of the mesh.</param>
        /// <returns>The world matrix of the mesh.</returns>
        public Matrix GetMeshWorld(string meshName)
        {
            return models[index].GetMeshWorld(meshName);
        }

        /// <summary>
        /// Sets the base transformation of this ModelComponent.
        /// </summary>
        /// <param name="transformation">The base transformation.</param>
        public void SetBaseTransform(Matrix transformation)
        {
            foreach (LODModel mdl in models.Values)
                mdl.SetBaseTransform(transformation);
        }

        /// <summary>
        /// Sets a parameter of the shader used to render a specific mesh.
        /// </summary>
        /// <param name="meshName">The name of the mesh to have a shader parameter changed.</param>
        /// <param name="parameterName">The name of the parameter to be changed.</param>
        /// <param name="texture">The texture to be set.</param>
        public void SetParameter(string meshName,string parameterName, Texture2D texture)
        {
            foreach (LODModel model in models.Values)
                model.SetParameter(meshName, parameterName, texture);
        }

        /// <summary>
        /// Sets a parameter of the shader used to render a specific mesh.
        /// </summary>
        /// <param name="meshName">The name of the mesh to have a shader parameter changed.</param>
        /// <param name="parameterName">The name of the parameter to be changed.</param>
        /// <param name="value">The value to be set.</param>
        public void SetParameter(string meshName, string parameterName, float value)
        {
            foreach (LODModel model in models.Values)
                model.SetParameter(meshName, parameterName, value);
        }

        /// <summary>
        /// Sets a parameter of the shader used to render a specific mesh.
        /// </summary>
        /// <param name="meshName">The name of the mesh to have a shader parameter changed.</param>
        /// <param name="parameterName">The name of the parameter to be changed.</param>
        /// <param name="value">The value to be set.</param>
        public void SetParameter(string meshName, string parameterName, Vector2 value)
        {
            foreach (LODModel model in models.Values)
                model.SetParameter(meshName, parameterName, value);
        }

        /// <summary>
        /// Sets a parameter of the shader used to render a specific mesh.
        /// </summary>
        /// <param name="meshName">The name of the mesh to have a shader parameter changed.</param>
        /// <param name="parameterName">The name of the parameter to be changed.</param>
        /// <param name="value">The value to be set.</param>
        public void SetParameter(string meshName, string parameterName, Vector3 value)
        {
            foreach (LODModel model in models.Values)
                model.SetParameter(meshName, parameterName, value);
        }

        /// <summary>
        /// Gets the name of all MeshPart in the current LODModel.
        /// </summary>
        /// <returns>The result of the search.</returns>
        public string[] GetMeshNames()
        {
            return models[index].GetMeshNames();
        }

        /// <summary>
        /// Gets the name of all bones in the current LODModel.
        /// </summary>
        /// <returns>The result of the serach.</returns>
        public string[] GetBoneNames()
        {
            return models[index].GetBoneNames();
        }

        /// <summary>
        /// The total number of vertices in the current LODModel.
        /// </summary>
        /// <returns>The number of vertices.</returns>
        public int GetVertexCount()
        {
            return models[index].VertexCount;
        }

        public override void ToogleEnable()
        {
            foreach (LODModel mdl in models.Values)
                mdl.ToogleEnabled();

            base.ToogleEnable();
        }

        public override void Destroy(bool remove)
        {
            for (int i = 0; i < 3; i++)
                if (models.ContainsKey(i))
                    models[i].Dispose();

            models.Clear();

            base.Destroy(remove);
        }
    }
}
