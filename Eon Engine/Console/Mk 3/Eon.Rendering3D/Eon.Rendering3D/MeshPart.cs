/* Created: 08/05/2014
 * Last Updated: 28/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework;
using Eon.Rendering3D.Framework.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to define a mesh inside of a model.
    /// </summary>
    internal sealed class MeshPart : ITechniqueRenderer
    {
        Shader shader;
        LODModel owner;
        ModelMesh mesh;

        Matrix parentWorld;
        Matrix baseTransform;
        Matrix boneTransform;

        BoundingSphere sphere;

        string name;
        string rootName;
        bool enabled = true;

        /// <summary>
        /// The name of the MeshPart.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Wheather or not the MeshPart is
        /// in view of the current camera.
        /// </summary>
        public bool IsSeen
        {
            get
            {
                if (CameraManager.CurrentCamera != null)
                    return CameraManager.CurrentCamera.IsInView(sphere);

                return false;
            }
        }

        public bool Enabled
        {
            get { return enabled; }
        }

        internal Matrix World
        {
            get { return baseTransform * boneTransform * parentWorld; }
            set
            {
                parentWorld = value;
                boneTransform = owner.GetBoneTransform(rootName);

                ApplyBounds();
            }
        }

        public Vector3 Position
        {
            get { return World.Translation; }
        }

        /// <summary>
        /// The name of the MeshPart's parent bone.
        /// </summary>
        public string ParentBoneName
        {
            get { return mesh.ParentBone.Name; }
        }

        /// <summary>
        /// The way that the MeshPart is rendered.
        /// </summary>
        public RenderTypes RenderType
        {
            get { return shader.RenderType; }
        }

        /// <summary>
        /// The bounding sphere of the MeshPart.
        /// </summary>
        public BoundingSphere BoundingSphere { get { return sphere; } }

        /// <summary>
        /// Creates a new MeshPart.
        /// </summary>
        /// <param name="mesh">The model mesh to be rendered.</param>
        /// <param name="owner">The LODModel that this MeshPart is attached to.</param>
        /// <param name="shader">The shader to use to render the model mesh.</param>
        /// <param name="baseTransform">The base transformation of the parent ModelComponent.</param>
        public MeshPart(ModelMesh mesh, LODModel owner, 
            Shader shader, Matrix baseTransform, Matrix parentWorld)
        {
            this.mesh = mesh;
            this.owner = owner;

            name = mesh.Name;
            sphere = mesh.BoundingSphere;

            this.baseTransform = baseTransform;

            rootName = mesh.ParentBone.Name;
            boneTransform = owner.GetBoneTransform(rootName);

            this.parentWorld = parentWorld;

            ApplyBounds();

            this.shader = shader;
            shader.Load();

            ModelManager.Add(this);
        }

        void ApplyBounds()
        {
            Matrix world = World;

            sphere.Center = Vector3.Transform(sphere.Center, world);

            Vector3 scale = new Vector3(sphere.Radius);
            scale = Vector3.TransformNormal(scale, world);

            sphere.Radius = Math.Max(scale.X, Math.Max(scale.Y, scale.Z));
        }

        internal void SetBaseTransform(Matrix transform)
        {
            baseTransform = transform;
        }

        public void _Render()
        {
            _Render(shader.GetDefaultTechnique());
        }

        public void Render(Effect effect)
        {
            if (effect.Parameters["World"] != null)
                effect.Parameters["World"].SetValue(World);

            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                Common.Device.SetVertexBuffer(part.VertexBuffer);
                Common.Device.Indices = part.IndexBuffer;

                effect.CurrentTechnique.Passes[0].Apply();

                Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                    part.VertexOffset, 0, part.NumVertices,
                    part.StartIndex, part.PrimitiveCount);

                Common.Device.SetVertexBuffer(null);
                Common.Device.Indices = null;
            }
        }

        public void _Render(string technique)
        {
            if (shader.ContainsTechnique(technique))
            {
                shader.SetParameters(World);

                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Common.Device.SetVertexBuffer(part.VertexBuffer);
                    Common.Device.Indices = part.IndexBuffer;

                    shader.SetCurrentTechnique(technique);
                    shader.ApplyPasses();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                        part.VertexOffset, 0, part.NumVertices,
                        part.StartIndex, part.PrimitiveCount);

                    Common.Device.SetVertexBuffer(null);
                    Common.Device.Indices = null;
                }
            }
        }

        /// <summary>
        /// Sets the MeshPart's parent bone.
        /// </summary>
        /// <param name="bone">The bone to be set to.</param>
        public void SetParentBone(Matrix boneTransform)
        {
            mesh.ParentBone.ModelTransform = boneTransform;
        }

        /// <summary>
        /// Applies a transformation matrix to the MeshPart's parent bone.
        /// </summary>
        public void ApplyBoneTransform(Matrix transform)
        {
            mesh.ParentBone.ModelTransform *= transform;
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="image">The parameter's value.</param>
        public void SetParameter(string parameterName, Texture2D image)
        {
            shader.SetParameter(parameterName, image);
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, float value)
        {
            shader.SetParameter(parameterName, value);
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, Vector2 value)
        {
            shader.SetParameter(parameterName, value);
        }

        /// <summary>
        /// Sets a effect parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="value">The parameter's value.</param>
        public void SetParameter(string parameterName, Vector3 value)
        {
            shader.SetParameter(parameterName, value);
        }

        public void ToogleEnabled()
        {
            enabled = !enabled;
        }

        public void Dispose()
        {
            ModelManager.Remove(this);
        }
    }
}
