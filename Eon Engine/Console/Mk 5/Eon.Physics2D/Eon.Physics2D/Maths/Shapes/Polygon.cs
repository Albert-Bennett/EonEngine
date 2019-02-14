/* Created: 04/09/2015
 * Last Updated: 04/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Physics2D.Maths.Shapes
{
    /// <summary>
    /// Defines an irregular ConvexShape.
    /// </summary>
    public sealed class Polygon : ConvexShape
    {
        List<Vector2> vertices = new List<Vector2>();

        /// <summary>
        /// The vertices that make up the Polygon.
        /// </summary>
        public override Vector2[] Vertices
        {
            get { return vertices.ToArray(); }
        }

        /// <summary>
        /// Creates a new Polygon.
        /// </summary>
        public Polygon() { }

        /// <summary>
        /// Creates a new Polygon.
        /// </summary>
        /// <param name="vertices">The vertices that make up the Polygon.</param>
        public Polygon(List<Vector2> vertices)
        {
            this.vertices = vertices;
        }

        /// <summary>
        /// Adds a vertex to the Polygon.
        /// </summary>
        /// <param name="vertex">The vertex to be added.</param>
        public void AddVertex(Vector2 vertex)
        {
            vertices.Add(vertex);
        }
    }
}
