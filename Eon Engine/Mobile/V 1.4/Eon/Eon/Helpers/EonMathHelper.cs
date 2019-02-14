/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Helpers
{
    /// <summary>
    /// Defines a class which can be used to help with certain maths problems.
    /// </summary>
    public static class EonMathHelper
    {
        #region General Calculations

        /// <summary>
        /// Returns 1/3.
        /// </summary>
        public static float AThird { get { return 1f / 3f; } }

        /// <summary>
        /// 45 degrees in sin form.
        /// </summary>
        public static float Sin45 { get { return 0.70710678f; } }

        /// <summary>
        /// Pi = 3.1415926535897932384626433832795 or 180 degrees.
        /// </summary>
        public static float Pi { get { return 3.141593f; } }

        /// <summary>
        /// Pi / 2 or 90 degrees.
        /// </summary>
        public static float HalfPi { get { return Pi / 2; } }

        /// <summary>
        /// Pi / 4 or 45 degrees.
        /// </summary>
        public static float QuaterPi { get { return Pi / 4; } }

        /// <summary>
        /// Pi * 2 or 360 degrees.
        /// </summary>
        public static float Pi2 { get { return Pi * 2; } }

        /// <summary>
        /// E as a float.
        /// </summary>
        public static float E { get { return 2.7182824f; } }

        /// <summary>
        /// Used for numerical inprecissions with floats.
        /// </summary>
        public static float Epsilon { get { return 0.000001f; } }

        #endregion
        #region General

        /// <summary>
        /// Gets the value of a float after the decimal point.
        /// </summary>
        /// <param name="value">The float to get the decimal value of.</param>
        /// <returns>The decimals of the number.</returns>
        public static float GetDecimals(float value)
        {
            int r = (int)value;

            return value - r;
        }

        /// <summary>
        /// Restricts a given angle between 0 and 360 in radians.
        /// </summary>
        /// <param name="rads">The angle in radians.</param>
        /// <returns>The restricted angle.</returns>
        public static float RestrictAngle(float rads)
        {
            while (rads < -Pi)
                rads += Pi2;

            while (rads > Pi)
                rads -= Pi2;

            return rads;
        }

        /// <summary>
        /// Used to compare two floats.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="value2">Value 2.</param>
        /// <returns>Zero if both are equal, 
        /// a positive number if the first float is greater,
        /// a negative number if the last is greater.
        /// </returns>
        public static int Compare(float value1, float value2)
        {
            if (value1 == value2)
                return 0;

            float d = Abs(value1 - value2);

            if (d < Epsilon)
                return 0;

            if (value2 != 0 && d / Abs(value2) < Epsilon)
                return 0;

            return value1 > value2 ? 1 : -1;
        }

        /// <summary>
        /// A check to see if two floats are equal.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="value2">Value 2.</param>
        /// <returns>Whether or not they are equal.</returns>
        public static bool Equals(float value1, float value2)
        {
            return Compare(value1, value2) == 0;
        }

        /// <summary>
        /// Finds the angle between two Vector2's.
        /// </summary>
        /// <param name="value1">Value1.</param>
        /// <param name="value2">Value2.</param>
        /// <returns>The resultant angle.</returns>
        public static float AngleBetween(Vector2 value1, Vector2 value2)
        {
            float dot = Vector2.Dot(value1, value2);
            float mag = value1.Length() * value2.Length();
            return (float)System.Math.Acos(dot / mag);
        }

        /// <summary>
        /// Finds the angle between two Vector3's.
        /// </summary>
        /// <param name="value1">Value1.</param>
        /// <param name="value2">Value2.</param>
        /// <returns>The resultant angle.</returns>
        public static float AngleBetween(Vector3 value1, Vector3 value2)
        {
            float dot = Vector3.Dot(value1, value2);
            float mag = value1.Length() * value2.Length();
            return (float)System.Math.Acos(dot / mag);
        }

        /// <summary>
        /// Used to smooth out a value.
        /// </summary>
        /// <param name="value">The value to be smoothed.</param>
        /// <returns>The result.</returns>
        public static float Smooth(float value)
        {
            return value * value * (3 - 2 * value);
        }

        /// <summary>
        /// Finds the smaller of two numbers. 
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns>The smaller of the two numbers.</returns>
        public static float Min(float value1, float value2)
        {
            if (value1 > value2)
                return value2;
            else
                return value1;
        }

        /// <summary>
        /// Finds the greater of two numbers. 
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns>The greater of the two numbers.</returns>
        public static float Max(float value1, float value2)
        {
            if (value1 > value2)
                return value1;
            else
                return value2;
        }

        /// <summary>
        /// Finds the absolute value of a number.
        /// </summary>
        /// <param name="value">The value to calculate the absolute of.</param>
        /// <returns>The absolute value of the given number.</returns>
        public static float Abs(float value)
        {
            return value < 0 ? -value : value;
        }

        /// <summary>
        /// Clamps a value between 0.0f and 1.0f.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value)
        {
            return Clamp(value, 0, 1);
        }

        /// <summary>
        /// Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="min">Minimun value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            return LessThan(GreaterThan(value, min), max);
        }

        /// <summary>
        /// Clamps a value between 0 and 1.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value)
        {
            return Clamp(value, 0, 1);
        }

        /// <summary>
        /// Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="min">Minimun value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            return LessThan(GreaterThan(value, min), max);
        }

        /// <summary>
        /// Rounds up a float to an int.
        /// </summary>
        /// <param name="value">The value to be rounded up.</param>
        /// <returns>The value rounded.</returns>
        public static int Round(float value)
        {
            return (int)(value > 0 ? value + 0.5 : value - 0.5);
        }

        /// <summary>
        /// Returns the angle whose tangent is the number in degrees.
        /// </summary>
        /// <param name="tangent">The tangent to find the angle of.</param>
        /// <returns>Degrees.</returns>
        public static float ATan(float tangent)
        {
            return (float)System.Math.Atan(tangent);
        }

        /// <summary>
        /// Returns the angle whose tangent is the quotient of two numbers in degrees.
        /// </summary>
        /// <param name="x">X co-ordinate.</param>
        /// <param name="y">Y co-ordinate.</param>
        /// <returns>Degrees.</returns>
        public static float ATan(float x, float y)
        {
            return (float)System.Math.Atan2(x, y);
        }

        /// <summary>
        /// Returns the angle whose cosine is the given number.
        /// </summary>
        /// <param name="value">Value representing cosine.</param>
        /// <returns>Acos value in degrees.</returns>
        public static float ACos(float value)
        {
            return (float)System.Math.Acos(value);
        }

        /// <summary>
        /// Returns the angle whose sin is the given number.
        /// </summary>
        /// <param name="value">Value representing sine.</param>
        /// <returns>Asin value in degrees.</returns>
        public static float ASin(float value)
        {
            return (float)System.Math.Asin(value);
        }

        /// <summary>
        /// Returns the sin of an angle in degrees
        /// </summary>
        /// <param name="degrees">Degrees to calculate</param>
        /// <returns>Sin of a degree value in degrees.</returns>
        public static float Sin(float degrees)
        {
            return (float)System.Math.Sin(degrees * (Pi / 180));
        }

        /// <summary>
        /// Returns the cos of an angle in degrees
        /// </summary>
        /// <param name="degrees">Degrees to calculate</param>
        /// <returns>Cos of a degree value in degrees.</returns>
        public static float Cos(float degrees)
        {
            return (float)System.Math.Cos(degrees * (Pi / 180));
        }

        /// <summary>
        /// Returns the tan of an angle in degrees
        /// </summary>
        /// <param name="degrees">Degrees to calculate</param>
        /// <returns>Tan of a degree value in degrees.</returns>
        public static float Tan(float degrees)
        {
            return (float)System.Math.Tan(ToRadians(degrees));
        }

        /// <summary>
        /// Used to preform a linear interpolation between two floats.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="value2">Value 2.</param>
        /// <param name="amount">The amount to interpolate by.[Must be kept between 0.0 and 1.0]</param>
        /// <returns>The interpolated value.</returns>
        public static float LinearInterpolate(float value1,
            float value2, float amount)
        {
            return value1 * (1 - amount) + value2 * amount;
        }

        /// <summary>
        /// Used to preform a linear interpolation between two Vector2's.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="value2">Value 2.</param>
        /// <param name="amount">The amount to interpolate by.[Must be kept between 0.0 and 1.0]</param>
        /// <returns>The interpolated vector.</returns>
        public static Vector2 LinearInterpolate(Vector2 value1,
            Vector2 value2, float amount)
        {
            return new Vector2(LinearInterpolate(value1.X, value2.X, amount),
                LinearInterpolate(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Used to preform a linear interpolation between two Vector3's.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="value2">Value 2.</param>
        /// <param name="amount">The amount to interpolate by.[Must be kept between 0.0 and 1.0]</param>
        /// <returns>The interpolated vector.</returns>
        public static Vector3 LinearInterpolate(Vector3 value1,
            Vector3 value2, float amount)
        {
            return new Vector3(LinearInterpolate(value1.X, value2.X, amount),
                LinearInterpolate(value1.Y, value2.Y, amount),
                LinearInterpolate(value1.Z, value2.Z, amount));
        }

        /// <summary>
        /// Used to preform a linear interpolation between two Color's.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="value2">Value 2.</param>
        /// <param name="amount">The amount to interpolate by.[Must be kept between 0.0 and 1.0]</param>
        /// <returns>The interpolated color.</returns>
        public static Color LinearInterpolate(Color value1,
            Color value2, float amount)
        {
            return new Color(LinearInterpolate(value1.R, value2.R, amount),
                LinearInterpolate(value1.G, value2.G, amount),
                LinearInterpolate(value1.B, value2.B, amount),
                LinearInterpolate(value1.A, value2.A, amount));
        }

        /// <summary>
        /// Returns the bigger of the two values.
        /// </summary>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns></returns>
        public static float GreaterThan(float value1, float value2)
        {
            if (value1 > value2)
                return value1;
            else
                return value2;
        }

        /// <summary>
        /// Returns the smaller of the two values.
        /// </summary>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns></returns>
        public static float LessThan(float value1, float value2)
        {
            if (value1 < value2)
                return value1;
            else
                return value2;
        }

        /// <summary>
        /// Returns the bigger of the two values.
        /// </summary>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns></returns>
        public static int GreaterThan(int value1, int value2)
        {
            if (value1 > value2)
                return value1;
            else
                return value2;
        }

        /// <summary>
        /// Returns the smaller of the two values.
        /// </summary>
        /// <param name="value1">The first value to compare.</param>
        /// <param name="value2">The second value to compare.</param>
        /// <returns></returns>
        public static int LessThan(int value1, int value2)
        {
            if (value1 < value2)
                return value1;
            else
                return value2;
        }

        /// <summary>
        /// Finds the square root of a value.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <returns>The squared root of the value.</returns>
        public static float Sqrt(float value)
        {
            return (float)System.Math.Sqrt(value);
        }

        /// <summary>
        /// A check to see if two floats are nearly equal.
        /// </summary>
        /// <param name="value1">Float 1.</param>
        /// <param name="value2">Float 2.</param>
        /// <returns>The result of the check.</returns>
        public static bool EqualEnough(float value1, float value2)
        {
            float difference = Abs(value1 - value2);
            return (difference <= Epsilon);
        }

        #endregion
        #region Vector2

        /// <summary>
        /// Gets a vector perpendicular to the edge given.
        /// </summary>
        /// <param name="edge">The edge to get the perpendicualar vector of.</param>
        /// <returns>The perpendicular vector of the given edge.</returns>
        public static Vector2 Perpendicular(Vector2 edge)
        {
            return new Vector2(-edge.Y, edge.X);
        }

        /// <summary>
        /// Finds the midpoint between two vectors.
        /// </summary>
        /// <param name="start">A starting point.</param>
        /// <param name="end">An end point.</param>
        /// <returns>The mid distance between the two points.</returns>
        public static Vector2 Midpoint(Vector2 start, Vector2 end)
        {
            Vector2 direct = GetDirection(start, end);

            return start - (direct * 0.5f);
        }

        /// <summary>
        /// Calculates the angle of a line in degrees.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <returns>The angle of the line.</returns>
        public static float LineAngle(Vector2 point1, Vector2 point2)
        {
            return MathHelper.ToDegrees((float)Math.Abs(
                Math.Atan2(point2.Y - point1.Y, point2.X - point1.X)));
        }

        /// <summary>
        /// Calculates a normal along an edge.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <returns>The calculate normal.</returns>
        public static Vector2 GetEdgeNormal(Vector2 point1, Vector2 point2)
        {
            Vector2 edge = point1 - point2;
            edge.Normalize();

            return new Vector2(edge.Y, -edge.X);
        }

        /// <summary>
        /// A check to see if two vectors are equal.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>Result of the check.</returns>
        public static bool IsEqualTo(Vector2 a, Vector2 b)
        {
            if (a.X == b.X && a.Y == b.Y)
                return true;
            else
                return false;
        }

        /// <summary>
        /// A check to see if one vector is less than or equal to another.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>>Result of the check.</returns>
        public static bool IsLessThanOrEqualTo(Vector2 a, Vector2 b)
        {
            if (a.X <= b.X && a.Y <= b.Y)
                return true;
            else
                return IsEqualTo(a, b);
        }

        /// <summary>
        /// A check to see if one vector is greater than or equal to another.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>>Result of the check.</returns>
        public static bool IsGreaterThanOrEqualTo(Vector2 a, Vector2 b)
        {
            if (a.X >= b.X && a.Y >= b.Y)
                return true;
            else
                return IsEqualTo(a, b);
        }

        /// <summary>
        /// A check to see if a vector is less than zero.
        /// </summary>
        /// <param name="vec">Vector to check</param>
        /// <returns>Result of the check.</returns>
        public static bool VectorLessThanZero(Vector2 vec)
        {
            if (vec.X < Epsilon || vec.Y < Epsilon)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the direction between two Vector2's.
        /// </summary>
        /// <param name="a">Position.</param>
        /// <param name="b">Location.</param>
        /// <returns>The direction to travel between a -> b.</returns>
        public static Vector2 GetDirection(Vector2 a, Vector2 b)
        {
            Vector2 direct = b - a;
            direct.Normalize();

            return direct;
        }

        /// <summary>
        /// Gets the slope of a line.
        /// </summary>
        /// <param name="pointA">Point A.</param>
        /// <param name="pointB">Point B.</param>
        /// <returns>The slope of the line A-B.</returns>
        public static float Slope(Vector2 pointA, Vector2 pointB)
        {
            float y = pointB.Y - pointA.Y;
            float x = pointB.X - pointA.X;

            if (x == 0)
                return y;
            else
                return y / x;
        }

        #endregion
        #region  Vector3

        /// <summary>
        /// Finds the closest point between a position and a bounding box.
        /// </summary>
        /// <param name="bounding">The Bounding box to use.</param>
        /// <param name="position">The position to check.</param>
        /// <returns>The result of the equation.</returns>
        public static Vector3 GetClosestPoint(BoundingBox bounding, Vector3 position)
        {
            float x, y, z;

            if (position.X > bounding.Max.X)
                x = bounding.Max.X;
            else if (position.X < bounding.Min.X)
                x = bounding.Min.X;
            else
                x = position.X;

            if (position.Y > bounding.Max.Y)
                y = bounding.Max.Y;
            else if (position.Y < bounding.Min.Y)
                y = bounding.Min.Y;
            else
                y = position.Y;

            if (position.Z > bounding.Max.Z)
                z = bounding.Max.Z;
            else if (position.Z < bounding.Min.Z)
                z = bounding.Min.Z;
            else
                z = position.Z;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// A check to see if two vectors are equal.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>Result of the check.</returns>
        public static bool IsEqualTo(Vector3 a, Vector3 b)
        {
            if (a.X == b.X && a.Y == b.Y && a.Z == b.Z)
                return true;
            else
                return false;
        }

        /// <summary>
        /// A check to see if one vector is less than or equal to another.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>>Result of the check.</returns>
        public static bool IsLessThanOrEqualTo(Vector3 a, Vector3 b)
        {
            if (a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z)
                return true;
            else
                return false;
        }

        /// <summary>
        /// A check to see if one vector is greater than or equal to another.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>>Result of the check.</returns>
        public static bool IsGreaterThanOrEqualTo(Vector3 a, Vector3 b)
        {
            if (a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z)
                return true;
            else
                return false;
        }

        /// <summary>
        /// A check to see if a vector is less than zero.
        /// </summary>
        /// <param name="vec">Vector to check</param>
        /// <returns>Result of the check.</returns>
        public static bool VectorLessThanZero(Vector3 vec)
        {
            if (vec.X < Epsilon || vec.Y < Epsilon || vec.Z < Epsilon)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the direction between two Vector3's.
        /// </summary>
        /// <param name="a">Position.</param>
        /// <param name="b">Location.</param>
        /// <returns>The direction to travel between a -> b.</returns>
        public static Vector3 GetDirection(Vector3 a, Vector3 b)
        {
            Vector3 direct = b - a;
            direct.Normalize();

            return direct;
        }

        /// <summary>
        /// Finds the midpoint between two vectors.
        /// </summary>
        /// <param name="start">A starting point.</param>
        /// <param name="end">An end point.</param>
        /// <returns>The mid distance between the two points.</returns>
        public static Vector3 Midpoint(Vector3 start, Vector3 end)
        {
            Vector3 direction = GetDirection(start, end);

            return start - (direction * 0.5f);
        }

        /// <summary>
        /// Calculates a line from a known y co-ordinate.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <param name="y">Y.</param>
        /// <returns>The calculated line.</returns>
        public static Vector3 GetLineFromY(Vector3 point1,
            Vector3 point2, float y)
        {
            Vector3 direct = GetDirection(point1, point2);

            float acent = (point1.Y - y) / direct.Y;
            float x = point1.X - (direct.X * acent);
            float z = point1.Z - (direct.Z * acent);

            return new Vector3(x, acent, z);
        }

        #endregion
        #region Matrices

        /// <summary>
        /// Extracts the value representing scale in a given Matrix.
        /// </summary>
        /// <param name="matrix">The Matrix to get the scale from.</param>
        /// <param name="scale">The value representing scale.</param>
        public static void GetMatrixScale(Matrix matrix, out float scale)
        {
            scale = (matrix.M11 + matrix.M22 + matrix.M33) / 3;
        }

        /// <summary>
        /// Extracts the value representing scale in a given Matrix.
        /// </summary>
        /// <param name="matrix">The Matrix to get the scale from.</param>
        /// <param name="scale">The value representing scale.</param>
        public static void GetMatrixScale(Matrix matrix, out Vector3 scale)
        {
            scale = new Vector3(matrix.M11, matrix.M22, matrix.M33);
        }

        #endregion
        #region Converters

        /// <summary>
        /// Converts Degrees into Radians
        /// </summary>
        /// <param name="degrees">Degrees.</param>
        /// <returns>Radians.</returns>
        public static float ToRadians(float degrees)
        {
            return degrees * (Pi / 180);
        }

        #endregion
    }
}
