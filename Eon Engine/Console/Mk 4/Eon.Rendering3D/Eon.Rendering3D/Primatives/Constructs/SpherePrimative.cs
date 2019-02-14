/* Created: 25/09/2014
 * Last Updated: 27/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Rendering3D.Primatives.Constructs
{
    /// <summary>
    /// Used to define a sphere primative.
    /// </summary>
    internal sealed class SpherePrimative : Primative
    {
        int tesselation;

        /// <summary>
        /// Creates a new SpherePrimative.
        /// </summary>
        /// <param name="texture">The texture of the SpherePrimative.</param>
        /// <param name="tesselation">The tesselation of the SpherePrimative.</param>
        public SpherePrimative(Texture2D texture, int tesselation)
            : base(texture)
        {
            this.tesselation = EonMathsHelper.Max(3, tesselation);

            CreateVertices();
        }

        /// <summary>
        /// Creates a new SpherePrimative.
        /// </summary>
        /// <param name="textureFilepath">The filepath of the texture of the SpherePrimative.</param>
        /// <param name="tesselation">The tesselation of the SpherePrimative.</param>
        public SpherePrimative(string textureFilepath, int tesselation)
            : base(textureFilepath)
        {
            this.tesselation = EonMathsHelper.Max(3, tesselation);

            CreateVertices();
        }

        /// <summary>
        /// Creates a new SpherePrimative.
        /// </summary>
        /// <param name="colour">The colour of the SpherePrimative.</param>
        /// <param name="tesselation">The tesselation of the SpherePrimative.</param>
        public SpherePrimative(Color colour, int tesselation)
            : base(TextureHelper.CreateTexture(colour, new Point(2, 2)))
        {
            this.tesselation = tesselation;

            CreateVertices();
        }

        protected override void CreateVertices()
        {
            int horizontalSegments = tesselation * 2;

            List<short> indices = new List<short>();
            List<VertexPositionTexture> vertices = new List<VertexPositionTexture>();

            vertices.Add(new VertexPositionTexture(Vector3.Down, Vector2.Zero));

            float x = 1.0f / (float)horizontalSegments;
            float y = 1.0f / (float)tesselation + 1;

            for (int i = 0; i < tesselation - 1; i++)
            {
                float lat = ((i + 1) * MathHelper.Pi /
                    tesselation) - MathHelper.PiOver2;

                float dy = (float)Math.Sin(lat);
                float dxz = (float)Math.Cos(lat);

                for (int j = 0; j < horizontalSegments; j++)
                {
                    float lon = j * MathHelper.TwoPi / horizontalSegments;

                    float dx = (float)Math.Cos(lon) * dxz;
                    float dz = (float)Math.Sin(lon) * dxz;

                    Vector3 pos = new Vector3(dx, dy, dz);

                    vertices.Add(new VertexPositionTexture(pos,
                        new Vector2(x * j, y * i)));
                }
            }

            vertices.Add(new VertexPositionTexture(Vector3.Up, Vector2.One));

            for (int i = 0; i < horizontalSegments; i++)
            {
                indices.Add(0);
                indices.Add((short)(1 + (i + 1) % horizontalSegments));
                indices.Add((short)(i + 1));
            }

            for (int i = 0; i < tesselation - 2; i++)
            {
                for (int j = 0; j < horizontalSegments; j++)
                {
                    int idx = i + 1;
                    int h = (j + 1) % horizontalSegments;

                    indices.Add((short)(1 + i * horizontalSegments + j));
                    indices.Add((short)(1 + i * horizontalSegments + h));
                    indices.Add((short)(1 + idx * horizontalSegments + j));

                    indices.Add((short)(1 + i * horizontalSegments + h));
                    indices.Add((short)(1 + idx * horizontalSegments + h));
                    indices.Add((short)(1 + idx * horizontalSegments + j));
                }
            }

            for (int i = 0; i < horizontalSegments; i++)
            {
                indices.Add((short)(vertices.Count - 1));
                indices.Add((short)(vertices.Count - 2 - (i + 1) % horizontalSegments));
                indices.Add((short)(vertices.Count - 2 - i));
            }

            data.VertexBuffer = new VertexBuffer(Common.Device,
                typeof(VertexPositionTexture), vertices.Count,
                BufferUsage.WriteOnly);

            data.VertexBuffer.SetData<VertexPositionTexture>(vertices.ToArray());

            data.IndexBuffer = new IndexBuffer(Common.Device, IndexElementSize.SixteenBits,
                indices.Count, BufferUsage.WriteOnly);

            data.IndexBuffer.SetData<short>(indices.ToArray());
        }
    }
}
