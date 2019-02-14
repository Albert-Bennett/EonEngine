/* Created 01/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// Defines a 3D triangle.
    /// </summary>
    public struct Triangle
    {
        int[] indices;

        /// <summary>
        /// Finds an index of a vertex at a given index.
        /// </summary>
        /// <param name="index">The index of the vertex index to get.</param>
        /// <returns>The result of the search.</returns>
        public int this[int index]
        {
            get
            {
                if (index < indices.Length && index > -1)
                    return indices[index];
                else
                    throw new ArgumentOutOfRangeException("The given index must" +
                        " be between 0 and 2. Given index: " + index);
            }
        }

        /// <summary>
        /// Creates a new Triangle.
        /// </summary>
        /// <param name="index1">First index.</param>
        /// <param name="index2">Second index.</param>
        /// <param name="index3">Third index.</param>
        public Triangle(int index1, int index2, int index3)
        {
            indices = new int[]
            {
                index1,
                index2,
                index3
            };
        }

        /// <summary>
        /// Used to set both the noprm and the tangent of the face.
        /// </summary>
        /// <param name="vertices">The vertices that make up the face.</param>
        public void SetNormalTangent(List<TangentNormalTexture> vertices)
        {
            TangentNormalTexture[] vert = new TangentNormalTexture[]
            {
                vertices[indices[0]],
                vertices[indices[1]],
                vertices[indices[2]]
            };

            Vector3 normal = VertexHelper.CalculateNormal(
                ref vert[0].Position, ref vert[1].Position, ref vert[2].Position);

            Vector3 tangent = VertexHelper.CalculateTangent(
                ref vert[0], ref vert[1], ref vert[2], ref normal);

            for (int i = 0; i < vert.Length; i++)
            {
                vert[i].Normal += normal;
                vert[i].Normal.Normalize();
                vert[i].Tangent += tangent;
                vert[i].EvaluateTangent();
                vertices[indices[i]] = vert[i];
            }
        }
    }
}
