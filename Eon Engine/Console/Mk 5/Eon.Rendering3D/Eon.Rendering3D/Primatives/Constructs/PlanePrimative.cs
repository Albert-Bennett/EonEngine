/* Created: 25/09/2014
 * Last Updated: 27/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives.Constructs
{
    /// <summary>
    /// Used to define a Plane primative.
    /// </summary>
    internal sealed class PlanePrimative : Primative
    {
        /// <summary>
        /// Creates a new PlanePrimative.
        /// </summary>
        /// <param name="texture">The texture of the PlanePrimative.</param>
        public PlanePrimative(Texture2D texture)
            : base(texture)
        {
            CreateVertices();
        }

        /// <summary>
        /// Creates a new PlanePrimative.
        /// </summary>
        /// <param name="textureFilepath">The filepath of the texture of the PlanePrimative.</param>
        public PlanePrimative(string textureFilepath)
            : base(textureFilepath)
        {
            CreateVertices();
        }

        /// <summary>
        /// Creates a new PlanePrimative.
        /// </summary>
        /// <param name="colour">The colour of the PlanePrimative.</param>
        public PlanePrimative(Color colour)
            : base(TextureHelper.CreateTexture(colour, new Point(2, 2)))
        {
            CreateVertices();
        }

        protected override void CreateVertices()
        {
            short[] indices = new short[6];
            VertexPositionTexture[] vertices = new VertexPositionTexture[4];

            vertices[0] = new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(1, 0, 0), new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(new Vector3(1, 1, 0), new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2(0, 1));

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

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
