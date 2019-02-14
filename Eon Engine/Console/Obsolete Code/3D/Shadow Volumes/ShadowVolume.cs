/* Created: 23/05/2014
 * Last Updated: 02/10/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework.Rendering.Lighting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Rendering.Shadowing
{
    /// <summary>
    /// Used to define a ShadowVolume.
    /// </summary>
    internal sealed class ShadowVolume
    {
        List<VertexPositionColor> shadowVertices = new List<VertexPositionColor>();

        int total = 0;

        /// <summary>
        /// Used to build a shadow map.
        /// </summary>
        /// <param name="model">The model to be shadowed.</param>
        /// <param name="light">The light causing the shadow.</param>
        public void Build(MeshPart model, ICastShadows light)
        {
            Construct(model, light.Direction);
        }

        void Construct(MeshPart mesh, Vector3 lightDirection)
        {
            List<Edge> edges = new List<Edge>();

            short[][] totalIndices = mesh.Indices;
            VertexPositionNormalTexture[][] totalVertices = mesh.Vertices;

            for (int i = 0; i < totalVertices.Length; i++)
            {
                short[] indices = totalIndices[i];
                VertexPositionNormalTexture[] vertices = totalVertices[i];

                int faces = vertices.Length / 3;

                for (int f = 0; f < faces; f++)
                {
                    short p0 = indices[3 * f];
                    short p1 = indices[3 * f + 1];
                    short p2 = indices[3 * f + 2];

                    Vector3 vert0 = vertices[p0].Position;
                    Vector3 vert1 = vertices[p1].Position;
                    Vector3 vert2 = vertices[p2].Position;

                    Vector3 normal = Vector3.Cross(vert2 - vert1, vert1 - vert0);
                    normal.Normalize();

                    if (Vector3.Dot(normal, lightDirection) >= 0.0f)
                    {
                        Add(edges, new Edge(vert0, vert1));
                        Add(edges, new Edge(vert1, vert2));
                        Add(edges, new Edge(vert2, vert0));
                    }
                }

                for (int k = 0; k < edges.Count; k++)
                {
                    Edge e = edges[k];

                    Vector3 vert0 = e.Point1;
                    Vector3 vert1 = e.Point2;
                    Vector3 vert2 = vert0 + lightDirection * 100;
                    Vector3 vert3 = vert1 + lightDirection * 100;

                    shadowVertices.Add(new VertexPositionColor(vert0, Color.Transparent));
                    shadowVertices.Add(new VertexPositionColor(vert1, Color.Transparent));
                    shadowVertices.Add(new VertexPositionColor(vert2, Color.Transparent));
                    shadowVertices.Add(new VertexPositionColor(vert0, Color.Transparent));
                    shadowVertices.Add(new VertexPositionColor(vert2, Color.Transparent));
                    shadowVertices.Add(new VertexPositionColor(vert3, Color.Transparent));

                    total += 6;
                }
            }
        }

        void Add(List<Edge> edges, Edge edge)
        {
            if (edges.Count > 0)
            {
                bool added = false;
                int idx = 0;

                while (!added && idx < edges.Count)
                {
                    Edge e = edges[idx];

                    if (e == edge)
                        added = true;

                    idx++;
                }

                if (!added)
                    edges.Add(edge);
            }
            else
                edges.Add(edge);
        }

        public void Render()
        {
            Common.Device.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList, shadowVertices.ToArray(), 0, total / 3);
        }
    }
}
