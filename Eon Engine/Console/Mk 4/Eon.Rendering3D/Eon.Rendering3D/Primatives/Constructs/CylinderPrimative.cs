/* Created: 26/09/2014
 * Last Updated: 27/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Primatives.Constructs
{
    /// <summary>
    /// Used to define a cylinder primative.
    /// </summary>
    internal sealed class CylinderPrimative : Primative
    {
        List<short> indices = new List<short>();
        List<VertexPositionTexture> vertices = new List<VertexPositionTexture>();

        int tesselation;

        /// <summary>
        /// Creates a new CylinderPrimative.
        /// </summary>
        /// <param name="texture">The texture of the CylinderPrimative.</param>
        /// <param name="tesselation">The tesselation of the CylinderPrimative.</param>
        public CylinderPrimative(Texture2D texture, int tesselation)
            : base(texture)
        {
            this.tesselation = EonMathsHelper.Max(3, tesselation);

            CreateVertices();
        }

        /// <summary>
        /// Creates a new CylinderPrimative.
        /// </summary>
        /// <param name="textureFilepath">The filepath of the texture of the CylinderPrimative.</param>
        /// <param name="tesselation">The tesselation of the CylinderPrimative.</param>
        public CylinderPrimative(string textureFilepath, int tesselation)
            : base(textureFilepath)
        {
            this.tesselation = EonMathsHelper.Max(3, tesselation);

            CreateVertices();
        }

        /// <summary>
        /// Creates a new CylinderPrimative.
        /// </summary>
        /// <param name="colour">The colour of the CylinderPrimative.</param>
        /// <param name="tesselation">The tesselation of the CylinderPrimative.</param>
        public CylinderPrimative(Color colour, int tesselation)
            : base(TextureHelper.CreateTexture(colour, new Point(2, 2)))
        {
            this.tesselation = tesselation;

            CreateVertices();
        }

        protected override void CreateVertices()
        {
            int segments = tesselation * 2;

            float x = 1.0f / (float)segments;
            float y = 1.0f / 4.0f;

            for (int i = 0; i < segments; i++)
            {
                Vector3 point = EonMathsHelper.GetPointOnCircle(i, segments);

                vertices.Add(new VertexPositionTexture(point + Vector3.Up, new Vector2(x * i, y * 2)));
                vertices.Add(new VertexPositionTexture(point + Vector3.Down, new Vector2(x * i, y * 3)));

                indices.Add((short)(i * 2));
                indices.Add((short)(i * 2 + 1));
                indices.Add((short)((i * 2 + 2) % (segments * 2)));

                indices.Add((short)(i * 2 + 1));
                indices.Add((short)((i * 2 + 2) % (segments * 2)));
                indices.Add((short)((i * 2 + 3) % (segments * 2)));
            }

            CapEnd(Vector3.Up, segments);
            CapEnd(Vector3.Down, segments);

            data.VertexBuffer = new VertexBuffer(Common.Device,
                typeof(VertexPositionTexture), vertices.Count,
                BufferUsage.WriteOnly);

            data.VertexBuffer.SetData<VertexPositionTexture>(vertices.ToArray());

            data.IndexBuffer = new IndexBuffer(Common.Device, IndexElementSize.SixteenBits,
                indices.Count, BufferUsage.WriteOnly);

            data.IndexBuffer.SetData<short>(indices.ToArray());

            indices.Clear();
            indices = null;

            vertices.Clear();
            vertices = null;
        }

        void CapEnd(Vector3 baseTransform, int segments)
        {
            vertices.Add(new VertexPositionTexture(baseTransform, new Vector2(baseTransform.Y, baseTransform.Y)));

            int idx = 1 + (int)baseTransform.Y;

            for (int i = 0; i < segments; i++)
            {
                indices.Add((short)(vertices.Count - 1));
                indices.Add((short)(i * idx));
                indices.Add((short)(((i + 1) * idx) % (segments * 2)));
            }
        }
    }
}
