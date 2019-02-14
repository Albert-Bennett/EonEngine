/* Created 30/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// Used to hold information about 
    /// primatives being rendered.
    /// </summary>
    internal struct PrimativeData : IDispose
    {
        internal VertexBuffer VertexBuffer;
        internal IndexBuffer IndexBuffer;

        internal int VerticesCount;
        internal int IndexCount;

        public void Dispose(bool finalize)
        {
            if (VertexBuffer != null)
                VertexBuffer.Dispose();

            if (IndexBuffer != null)
                IndexBuffer.Dispose();

            if (finalize)
            {
                VertexBuffer = null;
                VerticesCount = 0;

                IndexBuffer = null;
                IndexCount = 0;
            }
        }
    }
}
