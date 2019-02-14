/* Created: 30/11/2013
 * Last Updated: 25/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives.Constructs
{
    /// <summary>
    /// Used to hold information about 
    /// primatives being rendered.
    /// </summary>
    public struct PrimativeData : IDispose
    {
        public VertexBuffer VertexBuffer;
        public IndexBuffer IndexBuffer;

        /// <summary>
        /// The number of vertices in the Vertex buffer.
        /// </summary>
        public int VerticesCount
        {
            get { return VertexBuffer.VertexCount; }
        }

        /// <summary>
        /// The number of indices in the index buffer.
        /// </summary>
        public int IndexCount
        {
            get { return IndexBuffer.IndexCount; }
        }

        /// <summary>
        /// Disposes of the PrimativeData.
        /// </summary>
        /// <param name="finalize">Finalize the disposal of the PrimativeData.</param>
        public void Dispose(bool finalize)
        {
            if (VertexBuffer != null)
                VertexBuffer.Dispose();

            if (IndexBuffer != null)
                IndexBuffer.Dispose();

            if (finalize)
            {
                VertexBuffer = null;
                IndexBuffer = null;
            }
        }
    }
}
