/* Created: 24/11/2014
 * Last Updated: 24/11/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives.Constructs
{
    /// <summary>
    /// Used to define a line.
    /// </summary>
    internal sealed class LinePrimative : Primative
    {
        Vector3[] verts;

        /// <summary>
        /// Creates a new LinePrimative.
        /// </summary>
        /// <param name="texture">The texture of the LinePrimative.</param>
        public LinePrimative(Texture2D texture, Vector3 position,
            Vector3 direct, float length, float thickness)
            : base(texture)
        {
            verts = new Vector3[]
            {
                position,
                position - new Vector3(0,thickness,0),
                position + (direct * length),
                verts[2] - new Vector3(0,thickness,0)
            };

            CreateVertices();
        }

        /// <summary>
        /// Creates a new LinePrimative.
        /// </summary>
        /// <param name="textureFilepath">The filepath of the texture of the LinePrimative.</param>
        public LinePrimative(string textureFilepath, Vector3 position,
            Vector3 direct, float length, float thickness)
            : base(textureFilepath)
        {
            verts = new Vector3[]
            {
                position,
                position - new Vector3(0, thickness, 0),
                position + (direct * length),
                verts[2] - new Vector3(0, thickness, 0)
            };

            CreateVertices();
        }

        /// <summary>
        /// Creates a new LinePrimative.
        /// </summary>
        /// <param name="colour">The colour of the LinePrimative.</param>
        public LinePrimative(Color colour, Vector3 position,
            Vector3 direct, float length, float thickness)
            : base(TextureHelper.CreateTexture(colour, new Point(2, 2)))
        {
            verts = new Vector3[]
            {
                position,
                position - new Vector3(0, thickness, 0),
                position + (direct * length),
                verts[2] - new Vector3(0, thickness, 0)
            };

            CreateVertices();
        }

        protected override void CreateVertices()
        {
            short[] indices = new short[6];
            VertexPositionTexture[] vertices = new VertexPositionTexture[4];

            vertices[0] = new VertexPositionTexture(verts[0], new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(verts[1], new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(verts[2], new Vector2(0, 1));
            vertices[3] = new VertexPositionTexture(verts[3], new Vector2(1, 1));

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 3;
            indices[3] = 0;
            indices[4] = 3;
            indices[5] = 2;

            data.VertexBuffer = new VertexBuffer(Common.Device,
                typeof(VertexPositionTexture), vertices.Length,
                BufferUsage.WriteOnly);

            data.VertexBuffer.SetData<VertexPositionTexture>(vertices);

            data.IndexBuffer = new IndexBuffer(Common.Device, IndexElementSize.SixteenBits,
                indices.Length, BufferUsage.WriteOnly);

            data.IndexBuffer.SetData<short>(indices);
        }
    }
}
