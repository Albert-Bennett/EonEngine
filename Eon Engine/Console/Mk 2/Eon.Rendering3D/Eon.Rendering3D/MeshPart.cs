/* Created 08/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to define a mesh inside of a model.
    /// </summary>
    internal sealed class MeshPart
    {
        Shader shader;
        ModelMesh mesh;

        ModelComponent owner;

        VertexPositionNormalTexture[][] vertices;
        short[][] indices;

        string name;

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
        public bool IsInView
        {
            get { return CameraManager.CurrentCamera.IsInView(mesh.BoundingSphere); }
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

        internal short[][] Indices{ get{ return indices;}}

        internal VertexPositionNormalTexture[][] Vertices{get { return vertices; }}

        /// <summary>
        /// The bounding sphere of the MeshPart.
        /// </summary>
        public BoundingSphere BoundingSphere
        {
            get
            {
                BoundingSphere sphere = mesh.BoundingSphere;

                Vector3 offset = Vector3.Zero;
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion();

                if (owner != null)
                    owner.World.Decompose(out scale, out rotation, out offset);

                sphere.Center += offset;
                sphere.Radius *= (scale.X + scale.Y + scale.Z) / 3;

                return sphere;
            }
        }

        /// <summary>
        /// Creates a new MeshPart.
        /// </summary>
        /// <param name="owner">The owner of the mesh part.</param>
        /// <param name="mesh">The model mesh to be rendered.</param>
        /// <param name="shader">The shader to use to render the model mesh.</param>
        public MeshPart(ModelComponent owner, ModelMesh mesh, Shader shader)
        {
            this.owner = owner;
            this.mesh = mesh;
            this.shader = shader;

            name = mesh.Name;

            shader.Load();

            Initialize();
        }

        void Initialize()
        {
            vertices =
            new VertexPositionNormalTexture[mesh.MeshParts.Count][];

            indices = new short[mesh.MeshParts.Count][];

            for (int i = 0; i < mesh.MeshParts.Count; i++)
            {
                vertices[i] = GetVertices(i);
                indices[i] = GetIndices(i);
            }
        }

        VertexPositionNormalTexture[] GetVertices(int index)
        {
            int lenght = mesh.MeshParts[index].VertexBuffer.VertexCount;

            VertexPositionNormalTexture[] verts =
                new VertexPositionNormalTexture[lenght];

            mesh.MeshParts[index].VertexBuffer.GetData<VertexPositionNormalTexture>(verts);

            return verts;
        }

        short[] GetIndices(int index)
        {
            int length = mesh.MeshParts[index].IndexBuffer.IndexCount;
            short[] idx = new short[length];

            mesh.MeshParts[index].IndexBuffer.GetData<short>(idx);

            return idx;
        }

        internal void Render()
        {
            if (shader.DefaultTechnique != "NULL")
                Render(shader.GetDefaultTechnique());
        }

        internal void Render(string technique)
        {
            if (IsInView)
                if (shader.ContainsTechnique(technique))
                {
                    Matrix localWorld = mesh.ParentBone.ModelTransform * owner.World;

                    shader.SetParameters(localWorld);

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
    }
}
