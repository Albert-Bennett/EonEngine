/* Created 23/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Shadowing
{
    /// <summary>
    /// Defines the edge of a ShadowVolume. 
    /// </summary>
    internal struct Edge
    {
        Vector3 point1;
        Vector3 point2;

        /// <summary>
        /// Point 1.
        /// </summary>
        public Vector3 Point1
        {
            get { return point1; }
        }

        /// <summary>
        /// Point 2.
        /// </summary>
        public Vector3 Point2
        {
            get { return point2; }
        }

        /// <summary>
        /// Creates an Edge. Used to define the edge of ShadowVolume.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        public Edge(Vector3 point1, Vector3 point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public static bool operator !=(Edge edge1, Edge edge2)
        {
            return edge1.Point1 != edge2.Point1 ||
                edge1.Point2 != edge2.Point2;
        }

        public static bool operator ==(Edge edge1, Edge edge2)
        {
            return edge1.Point1 == edge2.Point1 &&
                edge1.Point2 == edge2.Point2;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Edge))
                return false;
            else
            {
                Edge other = (Edge)obj;

                return Point1 == other.Point1 &&
                    Point2 == other.Point2;
            }
        }

        public override int GetHashCode()
        {
            return point1.GetHashCode() ^ point2.GetHashCode();
        }

        public override string ToString()
        {
            return "Point 1 { X " + point1.X + ", Y " + point1.Y + ", Z " + point1.Z + " }" +
                ", Point 2 { X " + point2.X + ", Y " + point2.Y + ", Z " + point2.Z + " }";
        }
    }
}
