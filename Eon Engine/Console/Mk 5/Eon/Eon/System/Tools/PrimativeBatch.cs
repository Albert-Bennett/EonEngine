/* Created: 18/09/2015
 * Last Updated: 18/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.System.Tools
{
    /// <summary>
    /// Defines an object that is used to drawing simple primatives in 2D space.
    /// </summary>
    public sealed class PrimativeBatch
    {
        const int DEFAULT_SIZE = 1024;

        List<VertexPositionColor> vertices =
            new List<VertexPositionColor>(DEFAULT_SIZE);

        BasicEffect effect;

        int currentNumber = 0;
        bool destroyed = false;

        /// <summary>
        /// The number of vertices present in the PrimativeBatch.
        /// </summary>
        public int VertexCount
        {
            get { return currentNumber; }
        }

        /// <summary>
        /// Creates a new PrimativeBatch.
        /// </summary>
        public PrimativeBatch()
        {
            effect = new BasicEffect(Common.Device);
            effect.VertexColorEnabled = true;

            effect.Projection = Matrix.CreateOrthographicOffCenter(0,
                Common.TextureQuality.X, Common.TextureQuality.Y, 0, 0, 1);
        }

        /// <summary>
        /// Adds a vertex to the PrimativeBuffer.
        /// </summary>
        /// <param name="position">The position of the Vertex.</param>
        /// <param name="colour">The colour of the Vertex.</param>
        public void AddVertex(Vector2 position, Color colour)
        {
            if (currentNumber > DEFAULT_SIZE)
                throw new ArgumentOutOfRangeException("Vertex buffer to large.");

            vertices[currentNumber] = new VertexPositionColor(
                new Vector3(position, 0), colour);

            currentNumber++;
        }

        /// <summary>
        /// Starts the rendering process.
        /// </summary>
        /// <param name="renderType">The way to render the vertices.</param>
        /// <param name="primativeType">The type of vertices.</param>
        public void Render(RenderType renderType)
        {
            if (!destroyed && currentNumber > 0)
            {
                int verticesPerPrimative = (int)renderType + 2;

                PrimitiveType primativeType = PrimitiveType.TriangleList;

                if (verticesPerPrimative < 3)
                    primativeType = PrimitiveType.LineList;

                effect.CurrentTechnique.Passes[0].Apply();

                int primatives = currentNumber / verticesPerPrimative;

                Common.Device.DrawUserPrimitives<VertexPositionColor>(
                    primativeType, vertices.ToArray(), 0, primatives);

                Clear();
            }
        }

        void Clear()
        {
            vertices = new List<VertexPositionColor>();
            currentNumber = 0;
        }

        /// <summary>
        /// Destroys the PrimativeBatch.
        /// </summary>
        public void Destroy()
        {
            if (!destroyed)
            {
                effect.Dispose();
                vertices = null;

                destroyed = true;
            }
        }
    }
}
