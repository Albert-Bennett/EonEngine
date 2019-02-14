/* Created: 02/09/2014
 * Last Updated: 17/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Maths.BoundingVolumes
{
    /// <summary>
    /// Defines a bounding sphere which has a center and a radius.
    /// </summary>
    public struct EonBoundingSphere : IEquatable<EonBoundingSphere>, IBounding
    {
        #region Properties

        /// <summary>
        /// Center point of the BoundingSphere.
        /// </summary>
        public Vector3 Center;

        /// <summary>
        /// The radius of the Bounding Sphere.
        /// </summary>
        public float Radius;

        #endregion
        #region Constructors

        /// <summary>
        /// Creates a new BoundingSphere.
        /// </summary>
        /// <param name="center">The BoundingSphere's center position.</param>
        /// <param name="radius">The radius of the BoundingSphere.</param>
        public EonBoundingSphere(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Creates a new BoundingSphere.
        /// </summary>
        /// <param name="center">The BoundingSphere's center position.</param>
        /// <param name="radius">The radius of the BoundingSphere.</param>
        public EonBoundingSphere(float center, float radius) : this(new Vector3(center), radius) { }

        /// <summary>
        /// Creates a new BoundingSphere from a formatted string. 
        /// </summary>
        /// <param name="value">The string to use.</param>
        /// <returns>The generated BoundingSphere.</returns>
        public EonBoundingSphere CreateFromValueString(string value)
        {
            value = value.Replace("(", "");
            value = value.Replace(")", "");
            value = value.Replace("{", "");
            value = value.Replace("}", "");

            string[] values = value.Split(new[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length == 4)
                return new EonBoundingSphere(new Vector3(Convert.ToSingle(values[0]), Convert.ToSingle(values[1]),
                    Convert.ToSingle(values[2])), Convert.ToSingle(values[0]));

            return new EonBoundingSphere(0, 0);
        }

        /// <summary>
        /// Creates a new BoundingSphere.
        /// </summary>
        /// <param name="min">Minimum values to use.</param>
        /// <param name="max">Maximum values to use.</param>
        /// <returns>The generated BoundingSphere.</returns>
        public static EonBoundingSphere CreateFromMinMax(Vector3 min, Vector3 max)
        {
            Vector3 center = (min + max) / 2;
            float radius = ((min - max) / 2).Length();

            return new EonBoundingSphere(center, radius);
        }

        #endregion
        #region Base Methods

        /// <summary>
        /// Converts the BoundingSphere into a formatted string safe for loading in.
        /// [For serialization purposes].
        /// </summary>
        /// <returns>The BoundingSphere as a formatted string.</returns>
        public string ToValueString()
        {
            return Center.X + "," + Center.Y + ","
                + Center.Z + "," + "," + Radius;
        }

        /// <summary>
        /// A check to see if an object is equal to this BoundingSphere.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>The Result.</returns>
        public override bool Equals(object obj)
        {
            return (obj is EonBoundingSphere) ? Equals((EonBoundingSphere)obj) : base.Equals(obj);
        }

        /// <summary>
        /// Gets the hash code for this BoundingSphere.
        /// </summary>
        /// <returns>The BoundingSphere's hash code.</returns>
        public override int GetHashCode()
        {
            return Center.GetHashCode() ^ Radius.GetHashCode();
        }

        /// <summary>
        /// Converts this BoundingSphere into a string.
        /// [For display purposes].
        /// </summary>
        /// <returns>This BoundingSphere as a string.</returns>
        public override string ToString()
        {
            return "Center = " + Center.ToString() +
                ", Radius = " + Radius.ToString();
        }

        /// <summary>
        /// A check to see if two BoundingSphere's are equal. 
        /// </summary>
        /// <param name="other">The other BoundingSphere.</param>
        /// <returns>The Result.</returns>
        public bool Equals(EonBoundingSphere other)
        {
            return this == other;
        }

        /// <summary>
        /// A check to see if the given position is inside this BoundingSphere.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>The result of the check.</returns>
        public ContainableTypes Contains(Vector3 position)
        {
            float dist = Vector3.Distance(Center, position);

            if ((dist - Radius) > 0)
                return ContainableTypes.None;
            else
                return ContainableTypes.Fully;
        }

        /// <summary>
        /// A check to see if the given BoundingSphere is inside this BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check.</param>
        /// <returns>The result of the check.</returns>
        public ContainableTypes Contains(EonBoundingSphere sphere)
        {
            float dist = Vector3.Distance(Center, sphere.Center);

            if ((dist - Radius) > 0)
                return ContainableTypes.None;
            else if (dist + sphere.Radius < Radius)
                return ContainableTypes.Fully;
            else
                return ContainableTypes.Partial;
        }

        /// <summary>
        /// A check to see if the given BoundingBox is inside this BoundingSphere.
        /// </summary>
        /// <param name="box">The BoundingBox to check.</param>
        /// <returns>The result of the check.</returns>
        public ContainableTypes Contains(EonBoundingBox box)
        {
            Vector3 closestPos = Vector3.Clamp(Center, box.Min, box.Max);
            float dist = Vector3.DistanceSquared(Center, closestPos);
            float radiSquared = Radius * Radius;

            if (dist > radiSquared)
                return ContainableTypes.None;
            else
            {
                Vector3 objDist = new Vector3(Center.X - box.Min.X,
                    Center.Y - box.Max.Y, Center.Z - box.Max.Z);

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                objDist.X = Center.X - box.Max.X;
                objDist.Y = Center.Y - box.Max.Y;
                objDist.Z = Center.Z - box.Max.Z;

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                objDist.X = Center.X - box.Min.X;
                objDist.Y = Center.Y - box.Min.Y;
                objDist.Z = Center.Z - box.Min.Z;

                objDist.X = Center.X - box.Max.X;
                objDist.Y = Center.Y - box.Min.Y;
                objDist.Z = Center.Z - box.Max.Z;

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                objDist.X = Center.X - box.Min.X;
                objDist.Y = Center.Y - box.Max.Y;
                objDist.Z = Center.Z - box.Min.Z;

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                objDist.X = Center.X - box.Min.X;
                objDist.Y = Center.Y - box.Max.Y;
                objDist.Z = Center.Z - box.Max.Z;

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                objDist.X = Center.X - box.Min.X;
                objDist.Y = Center.Y - box.Min.Y;
                objDist.Z = Center.Z - box.Max.Z;

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                objDist.X = Center.X - box.Max.X;
                objDist.Y = Center.Y - box.Max.Y;
                objDist.Z = Center.Z - box.Min.Z;

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                objDist.X = Center.X - box.Max.X;
                objDist.Y = Center.Y - box.Min.Y;
                objDist.Z = Center.Z - box.Min.Z;

                if (objDist.LengthSquared() > radiSquared)
                    return ContainableTypes.Partial;

                return ContainableTypes.Fully;
            }
        }

        #endregion
        #region Operators

        /// <summary>
        /// A check to see if two BoundingSphere's are equal.
        /// </summary>
        /// <param name="sphere1">BoundingSphere 1.</param>
        /// <param name="sphere2">BoundingSphere 2.</param>
        /// <returns>The result of the check.</returns>
        public static bool operator ==(EonBoundingSphere sphere1, EonBoundingSphere sphere2)
        {
            return sphere1.Center == sphere2.Center && sphere1.Radius == sphere2.Radius;
        }

        /// <summary>
        /// A check to see if two BoundingSphere's are not equal.
        /// </summary>
        /// <param name="sphere1">BoundingSphere 1.</param>
        /// <param name="sphere2">BoundingSphere 2.</param>
        /// <returns>The result of the check.</returns>
        public static bool operator !=(EonBoundingSphere sphere1, EonBoundingSphere sphere2)
        {
            return sphere1.Center != sphere2.Center || sphere1.Radius != sphere2.Radius;
        }

        #endregion
    }
}
