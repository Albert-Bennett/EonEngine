/* Created 16/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// A class used to help with vertices.
    /// </summary>
    public static class VertexHelper
    {
        /// <summary>
        /// Creates a plane from a set of vertices.
        /// </summary>
        /// <param name="position1">The first vertex position.</param>
        /// <param name="position2">The second vertex position.</param>
        /// <param name="position3">The third vertex position.</param>
        /// <returns>The generated face.</returns>
        public static Plane GetFace(ref Vector3 position1,
            ref Vector3 position2, ref Vector3 position3)
        {
            return new Plane(position1, position2, position3);
        }

        /// <summary>
        /// Calculates the normal of a set of vertices.
        /// </summary>
        /// <param name="position1">The first vertex position.</param>
        /// <param name="position2">The second vertex position.</param>
        /// <param name="position3">The third vertex position.</param>
        /// <returns>The generated normal.</returns>
        public static Vector3 CalculateNormal(ref Vector3 position1,
            ref Vector3 position2, ref Vector3 position3)
        {
            Vector3 norm = Vector3.Cross(position2 - position1,
                position3 - position1);

            return Vector3.Normalize(norm);
        }

        /// <summary>
        /// Calculates a tangent.
        /// </summary>
        /// <param name="vertex1">Vertex one.</param>
        /// <param name="vertex2">Vertex two.</param>
        /// <param name="vertex3">Vertex three.</param>
        /// <param name="normal">The normal of the face.</param>
        /// <returns>The calculate tangent.</returns>
        public static Vector3 CalculateTangent(ref TangentNormalTexture vertex1,
            ref TangentNormalTexture vertex2, ref TangentNormalTexture vertex3, ref Vector3 normal)
        {
            Vector3 tangent = Vector3.Zero;

            Vector3 edge1 = vertex3.Position - vertex1.Position;
            Vector3 edge2 = vertex2.Position - vertex1.Position;

            Vector2 texEdge1 = vertex3.TextureCoord - vertex1.TextureCoord;
            Vector2 texEdge2 = vertex2.TextureCoord - vertex1.TextureCoord;

            edge1.Normalize();
            edge2.Normalize();

            texEdge1.Normalize();
            texEdge2.Normalize();

            float det = (texEdge1.X * texEdge2.Y) - (texEdge1.Y * texEdge2.X);

            Vector3 t;

            if ((float)Math.Abs(det) < 1e-6f)
                t = Vector3.UnitX;
            else
            {
                det = 1.0f / det;

                t.X = (texEdge2.Y * edge1.X - texEdge1.Y * edge2.X) * det;
                t.Y = (texEdge2.Y * edge1.Y - texEdge1.Y * edge2.Y) * det;
                t.Z = (texEdge2.Y * edge1.Z - texEdge1.Y * edge2.Z) * det;

                t.Normalize();
            }

            tangent.X = t.X;
            tangent.Y = t.Y;
            tangent.Z = t.Z;

            return tangent;
        }
    }
}
