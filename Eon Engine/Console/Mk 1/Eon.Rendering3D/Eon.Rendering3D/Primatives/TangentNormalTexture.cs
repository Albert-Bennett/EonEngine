/* Created 30/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// Used to define a special type of vertex type.
    /// </summary>
    public struct TangentNormalTexture : IVertexType
    {
        public Vector3 Position;
        public Vector2 TextureCoord;
        public Vector3 Tangent;
        public Vector3 Normal;

        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
               new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
                new VertexElement(sizeof(float) * 5, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                new VertexElement(sizeof(float) * 8, VertexElementFormat.Vector3, VertexElementUsage.Tangent, 0)
         );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexDeclaration; }
        }

        /// <summary>
        /// Creates a new TangentNormaltexture vertex.
        /// </summary>
        /// <param name="position">The position of thwe vertex.</param>
        /// <param name="textureCoord">The texture co-ordinate of the vertex.</param>
        /// <param name="normal">The normal of the vertex.</param>
        /// <param name="tangent">The tangent of the vertex.</param>
        public TangentNormalTexture(Vector3 position,
            Vector2 textureCoord, Vector3 normal, Vector3 tangent)
        {
            this.Position = position;
            this.TextureCoord = textureCoord;
            this.Normal = normal;
            this.Tangent = tangent;
        }

        /// <summary>
        /// Finalizes the tangent calculations.
        /// </summary>
        public void EvaluateTangent()
        {
            Tangent = (Tangent - Normal * Vector3.Dot(Normal, Tangent));
            Tangent.Normalize();
        }
    }
}
