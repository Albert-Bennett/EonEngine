/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Defines a quad renderer.
    /// </summary>
    public class QuadRenderer
    {
        VertexPositionColorTexture[] vertices;
        VertexBuffer vb = new VertexBuffer(Common.Device, typeof(VertexPositionColorTexture), 4, BufferUsage.None);
        short[] ids;

        /// <summary>
        /// Creates a new QuadRenderer.
        /// </summary>
        public QuadRenderer()
        {
            vertices = new VertexPositionColorTexture[]
            {
                            new VertexPositionColorTexture(
                                new Vector3(-1,1,1),
                                Color.White,
                                new Vector2(0,0)),
                            new VertexPositionColorTexture(
                                new Vector3(1,1,1),
                                Color.White,
                                new Vector2(1,0)),
                            new VertexPositionColorTexture(
                                new Vector3(-1,-1,1),
                                Color.White,
                                new Vector2(0,1)),
                            new VertexPositionColorTexture(
                                new Vector3(1,-1,1),
                                Color.White,
                                new Vector2(1,1))
            };

            vb.SetData(vertices);
            ids = new short[] { 0, 3, 2, 0, 1, 3 };
        }

        /// <summary>
        /// Sets the vertex buffer of this to the graphics device.
        /// </summary>
        public void BindBuffer()
        {
            Common.Device.SetVertexBuffer(vb);
        }

        /// <summary>
        /// Renders a quad.
        /// </summary>
        public void Draw()
        {
            Common.Device.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices, 0, 2);
        }

        /// <summary>
        /// Renders a quad.
        /// </summary>
        /// <param name="vector1">Top point.</param>
        /// <param name="vector2">Bottom point.</param>
        public void Render(Vector2 vector1, Vector2 vector2)
        {
            vertices[0].Position.X = vector1.X;
            vertices[0].Position.Y = vector2.Y;

            vertices[1].Position.X = vector2.X;
            vertices[1].Position.Y = vector2.Y;

            vertices[2].Position.X = vector1.X;
            vertices[2].Position.Y = vector1.Y;

            vertices[3].Position.X = vector2.X;
            vertices[3].Position.Y = vector1.Y;

            Common.Device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                 PrimitiveType.TriangleList, vertices, 0, 4, ids, 0, 2);
        }
    }
}
