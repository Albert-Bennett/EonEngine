/* Created: 12/05/2014
 * Last Updated: 12/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.System.Tools
{
    /// <summary>
    /// Used to define a quad that fills the screen.
    /// </summary>
    public sealed class ScreenQuad
    {
        VertexBuffer vBuffer;
        IndexBuffer iBuffer;

        /// <summary>
        /// Creates a new ScreenQuad.
        /// </summary>
        public ScreenQuad()
        {
            VertexPositionTexture[] vertices =
           {
                new VertexPositionTexture(
                    new Vector3(1,-1,0),new Vector2(1,1)),
                new VertexPositionTexture(
                    new Vector3(-1,-1,0), new Vector2(0,1)),
                new VertexPositionTexture(
                    new Vector3(-1,1,0), new Vector2(0,0)),
                new VertexPositionTexture(
                    new Vector3(1,1,0), new Vector2(1,0))
           };

            vBuffer = new VertexBuffer(Common.Device, VertexPositionTexture.VertexDeclaration, vertices.Length, BufferUsage.None);
            vBuffer.SetData<VertexPositionTexture>(vertices);

            ushort[] indices = new ushort[] { 0, 1, 2, 2, 3, 0 };

            iBuffer = new IndexBuffer(Common.Device, IndexElementSize.SixteenBits,
                indices.Length, BufferUsage.None);

            iBuffer.SetData<ushort>(indices);
        }

        /// <summary>
        /// Renders the ScreenQuad.
        /// </summary>
        public void Render()
        {
            Common.Device.SetVertexBuffer(vBuffer);
            Common.Device.Indices = iBuffer;

            Common.Device.DrawIndexedPrimitives(
                PrimitiveType.TriangleList, 0, 0, 4, 0, 2);

            Common.Device.SetVertexBuffer(null);
            Common.Device.Indices = null;
        }
    }
}
