/* Created: 25/09/2014
 * Last Updated: 16/10/2014
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
    /// Used to define a cube primative.
    /// </summary>
    internal sealed class CubePrimative : Primative
    {
        /// <summary>
        /// Creates a new CubePrimative.
        /// </summary>
        /// <param name="texture">The texture of the CubePrimative.</param>
        public CubePrimative(Texture2D texture)
            : base(texture)
        {
            CreateVertices();
        }

        /// <summary>
        /// Creates a new CubePrimative.
        /// </summary>
        /// <param name="textureFilepath">The filepath of the texture of the CubePrimative.</param>
        public CubePrimative(string textureFilepath)
            : base(textureFilepath)
        {
            CreateVertices();
        }

        /// <summary>
        /// Creates a new CubePrimative.
        /// </summary>
        /// <param name="colour">The colour of the CubePrimative.</param>
        public CubePrimative(Color colour)
            : base(TextureHelper.CreateTexture(colour, new Point(2, 2)))
        {
            CreateVertices();
        }

        /// <summary>
        /// Creates a new CubePrimative.
        /// </summary>
        /// <param name="colour">The colour of the CubePrimative.</param>
        /// <param name="frustum">The bounding frustum used to generate the CubePrimative.</param>
        public CubePrimative(Color colour, BoundingFrustum frustum)
            : base(TextureHelper.CreateTexture(colour, new Point(2, 2)))
        {
            short[] indices = new short[36];
            VertexPositionTexture[] vertices = new VertexPositionTexture[24];

            Vector3[] points = new Vector3[8];
            frustum.GetCorners(points);

            //Front
            vertices[0] = new VertexPositionTexture(points[0], new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(points[1], new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(points[2], new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(points[3], new Vector2(0, 1));

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 3;

            indices[3] = 1;
            indices[4] = 2;
            indices[5] = 3;

            //Back
            vertices[4] = new VertexPositionTexture(points[4], new Vector2(0, 0));
            vertices[5] = new VertexPositionTexture(points[5], new Vector2(1, 0));
            vertices[6] = new VertexPositionTexture(points[6], new Vector2(1, 1));
            vertices[7] = new VertexPositionTexture(points[7], new Vector2(0, 1));

            indices[6] = 4;
            indices[7] = 7;
            indices[8] = 5;

            indices[9] = 5;
            indices[10] = 6;
            indices[11] = 7;

            //Bottom
            vertices[8] = new VertexPositionTexture(points[2], new Vector2(0, 0));
            vertices[9] = new VertexPositionTexture(points[3], new Vector2(1, 0));
            vertices[10] = new VertexPositionTexture(points[6], new Vector2(1, 1));
            vertices[11] = new VertexPositionTexture(points[7], new Vector2(0, 1));

            indices[12] = 8;
            indices[13] = 9;
            indices[14] = 10;

            indices[15] = 9;
            indices[16] = 10;
            indices[17] = 11;

            //Top
            vertices[12] = new VertexPositionTexture(points[0], new Vector2(0, 0));
            vertices[13] = new VertexPositionTexture(points[1], new Vector2(1, 0));
            vertices[14] = new VertexPositionTexture(points[4], new Vector2(1, 1));
            vertices[15] = new VertexPositionTexture(points[5], new Vector2(0, 1));

            indices[18] = 12;
            indices[19] = 13;
            indices[20] = 14;

            indices[21] = 13;
            indices[22] = 14;
            indices[23] = 15;

            //Left
            vertices[16] = new VertexPositionTexture(points[1], new Vector2(0, 0));
            vertices[17] = new VertexPositionTexture(points[2], new Vector2(1, 0));
            vertices[18] = new VertexPositionTexture(points[5], new Vector2(1, 1));
            vertices[19] = new VertexPositionTexture(points[6], new Vector2(0, 1));

            indices[24] = 19;
            indices[25] = 18;
            indices[26] = 17;

            indices[27] = 18;
            indices[28] = 17;
            indices[29] = 16;

            //Right
            vertices[20] = new VertexPositionTexture(points[0], new Vector2(0, 0));
            vertices[21] = new VertexPositionTexture(points[3], new Vector2(1, 0));
            vertices[22] = new VertexPositionTexture(points[4], new Vector2(1, 1));
            vertices[23] = new VertexPositionTexture(points[7], new Vector2(0, 1));

            indices[30] = 23;
            indices[31] = 22;
            indices[32] = 21;

            indices[33] = 22;
            indices[34] = 21;
            indices[35] = 20;

            SetBuffers(indices, vertices);
        }

        protected override void CreateVertices()
        {
            short[] indices = new short[36];
            VertexPositionTexture[] vertices = new VertexPositionTexture[24];

            Vector3[] faces = new Vector3[]
            {
                 new Vector3(1,0,0),
                 new Vector3(-1,0,0),
                 new Vector3(0,1,0),
                 new Vector3(0,-1,0),
                 new Vector3(0,0,1),
                 new Vector3(0,0,-1)
            };

            int added = 0;
            int index = 0;

            for (int i = 0; i < faces.Length; i++)
            {
                indices[index] = (short)added;
                indices[index + 1] = (short)(1 + added);
                indices[index + 2] = (short)(2 + added);
                indices[index + 3] = (short)added;
                indices[index + 4] = (short)(2 + added);
                indices[index + 5] = (short)(3 + added);

                Vector3 side = new Vector3(faces[i].Y, faces[i].Z, faces[i].X);
                Vector3 facing = Vector3.Cross(faces[i], side);

                int count = added;

                vertices[count] = new VertexPositionTexture(faces[i] - side - facing, new Vector2(0, 0));
                vertices[count + 1] = new VertexPositionTexture(faces[i] - side + facing, new Vector2(1, 0));
                vertices[count + 2] = new VertexPositionTexture(faces[i] + side + facing, new Vector2(1, 1));
                vertices[count + 3] = new VertexPositionTexture(faces[i] + side - facing, new Vector2(0, 1));

                added += 4;
                index += 6;
            }

            SetBuffers(indices, vertices);
        }

        void SetBuffers(short[] indices, VertexPositionTexture[] vertices)
        {
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
