/* Created: 28/09/2014
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to define a model in a ModelComponent.
    /// </summary>
    internal sealed class LODModel
    {
        List<MeshPart> meshes = new List<MeshPart>();
        Matrix[] boneTransforms;

        Vector3 origin;

        Model model;

        bool enabled = true;

        /// <summary>
        /// The MeshParts in this ModelComponent.
        /// </summary>
        internal List<MeshPart> Meshes
        {
            get { return meshes; }
        }

        /// <summary>
        /// The numbre of vertices in the model.
        /// </summary>
        internal int VertexCount
        {
            get
            {
                int vert = 0;

                for (int i = 0; i < model.Meshes.Count; i++)
                    for (int j = 0; j < model.Meshes[i].MeshParts.Count; j++)
                        vert += model.Meshes[i].MeshParts[j].NumVertices;

                return vert;
            }
        }

        /// <summary>
        /// The origin of the LODModel.
        /// </summary>
        internal Vector3 Origin
        {
            get { return origin; }
        }

        /// <summary>
        /// Is the LODModel enabled.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
        }

        public LODModel(LODModelInfo info, Matrix baseTransform, Matrix parentWorld)
        {
            model = Common.ContentBuilder.Load<Model>(info.ModelFilepath);

            boneTransforms = new Matrix[model.Bones.Count()];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            for (int i = 0; i < model.Meshes.Count; i++)
                if (info.Materials.Contains(model.Meshes[i].Name))
                {
                    MeshPart part = new MeshPart(model.Meshes[i], this,
                        info.Materials[model.Meshes[i].Name], baseTransform, parentWorld);

                    meshes.Add(part);
                }

            origin = model.Root.ModelTransform.Translation +
                parentWorld.Translation + baseTransform.Translation;

            ToogleEnabled();
        }

        public void Update(Matrix world)
        {
            for (int i = 0; i < meshes.Count; i++)
                meshes[i].World = world;
        }

        public void ToogleEnabled()
        {
            enabled = !enabled;

            for (int i = 0; i < meshes.Count; i++)
                meshes[i].ToogleEnabled();
        }

        internal void TransformBone(string boneName, Matrix matrix)
        {
            bool found = false;
            int idx = 0;

            while (!found && idx < model.Bones.Count)
            {
                if (model.Bones[idx].Name == boneName)
                {
                    found = true;
                    boneTransforms[idx] *= matrix;
                }

                idx++;
            }
        }

        internal Matrix GetBoneTransform(string boneName)
        {
            int idx = 0;

            while (idx < model.Bones.Count)
            {
                if (model.Bones[idx].Name == boneName)
                   return boneTransforms[idx];

                idx++;
            }

            return Matrix.Identity;
        }

        internal void SetBaseTransform(Matrix transform)
        {
            foreach (MeshPart mesh in meshes)
                mesh.SetBaseTransform(transform);
        }

        internal bool Intersects(Ray ray)
        {
            for (int i = 0; i < meshes.Count; i++)
                if (ray.Intersects(meshes[i].BoundingSphere) != null)
                    return true;

            return false;
        }

        internal void SetParameter(string meshName, string parameterName, Texture2D value)
        {
            bool found = false;
            int i = 0;

            while (i < meshes.Count && !found)
            {
                if (meshes[i].Name == meshName)
                {
                    meshes[i].SetParameter(parameterName, value);
                    found = true;
                }

                i++;
            }
        }

        internal void SetParameter(string meshName, string parameterName, Vector2 value)
        {
            bool found = false;
            int i = 0;

            while (i < meshes.Count && !found)
            {
                if (meshes[i].Name == meshName)
                {
                    meshes[i].SetParameter(parameterName, value);
                    found = true;
                }

                i++;
            }
        }

        internal void SetParameter(string meshName, string parameterName, float value)
        {
            bool found = false;
            int i = 0;

            while (i < meshes.Count && !found)
            {
                if (meshes[i].Name == meshName)
                {
                    meshes[i].SetParameter(parameterName, value);
                    found = true;
                }

                i++;
            }
        }

        internal void SetParameter(string meshName, string parameterName, Vector3 value)
        {
            bool found = false;
            int i = 0;

            while (i < meshes.Count && !found)
            {
                if (meshes[i].Name == meshName)
                {
                    meshes[i].SetParameter(parameterName, value);
                    found = true;
                }

                i++;
            }
        }

        internal void SetParameter(string meshName, string parameterName, string value)
        {
            bool found = false;
            int i = 0;

            while (i < meshes.Count && !found)
            {
                if (meshes[i].Name == meshName)
                {
                    meshes[i].SetParameter(parameterName, value);
                    found = true;
                }

                i++;
            }
        }

        internal Matrix GetMeshWorld(string name)
        {
            bool found = false;
            int idx = 0;

            while (!found && idx < meshes.Count)
            {
                if (meshes[idx].Name == name)
                    return meshes[idx].World;

                idx++;
            }

            return Matrix.Identity;
        }

        internal BoundingSphere GetMeshViewSphere(string name)
        {
            bool found = false;
            int idx = 0;

            while (!found && idx < meshes.Count)
            {
                if (meshes[idx].Name == name)
                    return meshes[idx].BoundingSphere;

                idx++;
            }

            return new BoundingSphere(Vector3.Zero, 0);
        }

        internal string[] GetMeshNames()
        {
            string[] names = new string[meshes.Count];

            for (int i = 0; i < meshes.Count; i++)
                names[i] = meshes[i].Name;

            return names;
        }

        internal string[] GetBoneNames()
        {
            string[] names = new string[model.Bones.Count];

            for (int i = 0; i < model.Bones.Count; i++)
                names[i] = model.Bones[i].Name;

            return names;
        }

        public void Dispose()
        {
            for (int i = 0; i < meshes.Count; i++)
                meshes[i].Dispose();

            meshes.Clear();
        }
    }
}

