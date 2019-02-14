/* Created: 24/02/2015
 * Last Updated: 24/02/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives.Vertices
{
    /// <summary>
    /// Used to define a vertex that has a
    /// position, colour, bi-normal and tangent.
    /// </summary>
    public struct VertexPositionTangent : IVertexType
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
        /// Creates a new VertexPositionBiNormal.
        /// </summary>
        /// <param name="position">The position of the vertex.</param>
        /// <param name="textureCoord">The texture co-ordinate of the vertex.</param>
        /// <param name="normal">The normal of the vertex.</param>
        /// <param name="tangent">The tangent of the vertex.</param>
        public VertexPositionTangent(Vector3 position,
            Vector2 textureCoord, Vector3 normal,
            Vector3 tangent)
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
