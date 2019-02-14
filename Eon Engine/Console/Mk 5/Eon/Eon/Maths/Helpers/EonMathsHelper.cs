/* Created: 10/06/2013
 * Last Updated: 26/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Maths.Helpers
{
    /// <summary>
    /// Defines a class which can be used to help with certain maths problems.
    /// </summary>
    public static class EonMathsHelper
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
        public static float Epsilon { get { return 0.00001f; } }

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

            return (float)Math.Acos(dot / mag);
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
            return (float)Math.Acos(dot / mag);
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
        /// Generates noise using a pseudo random algorithim. 
        /// </summary>
        /// <param name="value">The starting value for the noise.</param>
        /// <param name="noise">Initial values for noise.</param>
        /// <returns>The generated noise.</returns>
        public static float GetNoise(float value, float[] noise)
        {
            value = Max(value, 0);

            int len = noise.Length;
            int i = ((int)(len * value)) % len;
            int j = (i + 1) % len;

            return MathHelper.SmoothStep(noise[i], noise[j], value - (int)value);
        }

        /// <summary>
        /// Finds the smaller of two numbers. 
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns>The smaller of the two numbers.</returns>
        public static int Min(int value1, int value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Finds the greater of two numbers. 
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns>The greater of the two numbers.</returns>
        public static int Max(int value1, int value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Finds the absolute value of a number.
        /// </summary>
        /// <param name="value">The value to calculate the absolute of.</param>
        /// <returns>The absolute value of the given number.</returns>
        public static int Abs(int value)
        {
            return value < 0 ? -value : value;
        }

        /// <summary>
        /// Finds the smaller of two numbers. 
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns>The smaller of the two numbers.</returns>
        public static float Min(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Finds the greater of two numbers. 
        /// </summary>
        /// <param name="value1">Value 1</param>
        /// <param name="value2">Value 2</param>
        /// <returns>The greater of the two numbers.</returns>
        public static float Max(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
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
            return value < min ? min : (value > max ? max : value);
        }

        /// <summary>
        /// Clamps a value between 0 and 1.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value)
        {
            return value < 0 ? 0 : (value > 1 ? 1 : value);
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
            return value < min ? min : (value > max ? max : value);
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
            return (float)Math.Atan(tangent);
        }

        /// <summary>
        /// Returns the angle whose tangent is the quotient of two numbers in degrees.
        /// </summary>
        /// <param name="x">X co-ordinate.</param>
        /// <param name="y">Y co-ordinate.</param>
        /// <returns>Degrees.</returns>
        public static float ATan(float x, float y)
        {
            return (float)Math.Atan2(x, y);
        }

        /// <summary>
        /// Returns the angle whose cosine is the given number.
        /// </summary>
        /// <param name="value">Value representing cosine.</param>
        /// <returns>Acos value in degrees.</returns>
        public static float ACos(float value)
        {
            return (float)Math.Acos(value);
        }

        /// <summary>
        /// Returns the angle whose sin is the given number.
        /// </summary>
        /// <param name="value">Value representing sine.</param>
        /// <returns>Asin value in degrees.</returns>
        public static float ASin(float value)
        {
            return (float)Math.Asin(value);
        }

        /// <summary>
        /// Returns the sin of an angle in degrees
        /// </summary>
        /// <param name="degrees">Degrees to calculate</param>
        /// <returns>Sin of a degree value in degrees.</returns>
        public static float Sin(float degrees)
        {
            return (float)Math.Sin(degrees * (Pi / 180));
        }

        /// <summary>
        /// Returns the cos of an angle in degrees
        /// </summary>
        /// <param name="degrees">Degrees to calculate</param>
        /// <returns>Cos of a degree value in degrees.</returns>
        public static float Cos(float degrees)
        {
            return (float)Math.Cos(degrees * (Pi / 180));
        }

        /// <summary>
        /// Returns the tan of an angle in degrees
        /// </summary>
        /// <param name="degrees">Degrees to calculate</param>
        /// <returns>Tan of a degree value in degrees.</returns>
        public static float Tan(float degrees)
        {
            return (float)Math.Tan(ToRadians(degrees));
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
        /// Finds the square root of a value.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <returns>The squared root of the value.</returns>
        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
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
        #region Easing Curves

        /// <summary>
        /// Defines a linear easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float LinearCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            return changeInValue * time / duration + startValue;
        }

        /// <summary>
        /// Defines a quadratic in easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuadraticInCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;

            return changeInValue * time * time + startValue;
        }

        /// <summary>
        /// Defines a quadratic out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuadraticOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;

            return -changeInValue * time * (time - 2) + startValue;
        }

        /// <summary>
        /// Defines a qudaratic in/out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuadraticInOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration / 2;

            if (time < 1)
                return changeInValue / 2 * time * time + startValue;
            time--;

            return -changeInValue / 2 * (time * (time - 2) - 1) + startValue;
        }

        /// <summary>
        /// Defines a cubic in easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float CubicInCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;

            return changeInValue * time * time * time + startValue;
        }

        /// <summary>
        /// Defines a cubic out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float CubicOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;
            time--;

            return changeInValue * (time * time * time + 1) + startValue;
        }

        /// <summary>
        /// Defines a cubic in/out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float CubicInOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration / 2;

            if (time < 1)
                return changeInValue / 2 * time * time * time + startValue;

            time -= 2;

            return changeInValue / 2 * (time * time * time + 2) + startValue;
        }

        /// <summary>
        /// Defines a quartic in easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuarticInCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;

            return changeInValue * time * time * time * time + startValue;
        }

        /// <summary>
        /// Defines a quartic out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuarticOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;
            time--;

            return -changeInValue * (time * time * time * time - 1) + startValue;
        }

        /// <summary>
        /// Defines a quartic in/out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuarticInOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration / 2;

            if (time < 1)
                return changeInValue / 2 * time * time * time * time + startValue;

            time -= 2;

            return -changeInValue / 2 * (time * time * time * time - 2) + startValue;
        }

        /// <summary>
        /// Defines a quintic in easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuinticInCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;

            return changeInValue * time * time * time * time * time + startValue;
        }

        /// <summary>
        /// Defines a quintic out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuinticOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;
            time--;

            return changeInValue * (time * time * time * time * time + 1) + startValue;
        }

        /// <summary>
        /// Defines a quintic in/out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float QuinticInOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration / 2;

            if (time < 1)
                return changeInValue / 2 * time * time * time * time * time + startValue;

            time -= 2;

            return changeInValue / 2 * (time * time * time * time * time + 2) + startValue;
        }

        /// <summary>
        /// Defines a sine in easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float SineInCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            return -changeInValue * (float)Math.Cos(time /
                duration * (Pi / 2)) + changeInValue + startValue;
        }

        /// <summary>
        /// Defines a sine out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float SineOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            return changeInValue * (float)Math.Sin(time /
                duration * (Pi / 2)) + startValue;
        }

        /// <summary>
        /// Defines a sine in/out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float SineInOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            return -changeInValue / 2 * (float)(Math.Cos(Pi *
                time / duration) - 1) + startValue;
        }

        /// <summary>
        /// Defines a exponential in easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float ExponentialInCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            return changeInValue * (float)(Math.Pow(2,
                10 * (time / duration - 1))) + startValue;
        }

        /// <summary>
        /// Defines a exponential out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float ExponentialOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            return changeInValue * (float)(-Math.Pow(2,
                -10 * time / duration) + 1) + startValue;
        }

        /// <summary>
        /// Defines a exponential in/out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float ExponentialInOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration / 2;

            if (time < 1)
                return changeInValue / 2 * (float)Math.Pow(2,
                    10 * (time - 1)) + startValue;

            time--;

            return changeInValue / 2 * (float)(-Math.Pow(2,
                -10 * time) + 2) + startValue;
        }

        /// <summary>
        /// Defines a circular in easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float CircularInCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;

            return -changeInValue * (float)(Math.Sqrt(1 -
                time * time) - 1) + startValue;
        }

        /// <summary>
        /// Defines a circular out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float CircularOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration;
            time--;

            return changeInValue * (float)Math.Sqrt(1 -
                time * time) + startValue;
        }

        /// <summary>
        /// Defines a circular in/out easing curve.
        /// </summary>
        /// <param name="time">Current time.</param>
        /// <param name="startValue">Starting value.</param>
        /// <param name="changeInValue">Change in value.</param>
        /// <param name="duration">Duration of the easment.</param>
        /// <returns>The new value.</returns>
        public static float CircularInOutCurve(float time, float duration,
            float startValue, float changeInValue)
        {
            time /= duration/2;

            if (time < 1)
                return -changeInValue / 2 * (float)(Math.Sqrt(1 -
                    time * time) - 1) + startValue;

            time -= 2;

            return changeInValue / 2 * (float)(Math.Sqrt(1 -
                time * time) + 1) + startValue;
        }

        #endregion
        #region Vector2

        /// <summary>
        /// Clamps a vector2 between two values.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The minimum value that can be clamped.</param>
        /// <param name="max">The maximum value that can be clamped.</param>
        /// <returns>The clamped vector2.</returns>
        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            if (IsLessThanOrEqualTo(value, min))
                return min;
            else if (IsGreaterThanOrEqualTo(value, max))
                return max;

            return value;
        }

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
            return vec.X < Epsilon && vec.Y < Epsilon;
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
        /// Rotates a position about an origin.
        /// </summary>
        /// <param name="origin">Origin.</param>
        /// <param name="position">Position.</param>
        /// <param name="angle">Angle in degrees.</param>
        /// <returns>Result of the transformation.</returns>
        public static Vector2 RotateAroundPoint(Vector2 origin,
            Vector2 position, float angle)
        {
            float cos = Cos(angle);
            float sinTheta = Sin(angle);

            return new Vector2()
            {
                X = cos * (position.X - origin.X) - sinTheta * (position.Y - origin.Y) + origin.X,
                Y = sinTheta * (position.X - origin.X) + cos * (position.Y - origin.Y) + origin.X
            };
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

            return x == 0 ? y : y / x;
        }

        #endregion
        #region  Vector3

        /// <summary>
        /// Clamps a vector3 between two values.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The minimum value that can be clamped.</param>
        /// <param name="max">The maximum value that can be clamped.</param>
        /// <returns>The clamped vector3.</returns>
        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            if (IsLessThanOrEqualTo(value, min))
                return min;
            else if (IsGreaterThanOrEqualTo(value, max))
                return max;

            return value;
        }

        /// <summary>
        /// gets the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector to get the magnitude of.</param>
        /// <returns>The magnitude of the gevne vecvtor.</returns>
        public static float Magnitude(Vector3 vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X +
                vector.Y * vector.Y + vector.Z + vector.Z);
        }

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
        /// Gets a point on a circle.
        /// </summary>
        /// <param name="segment">The segment where the point lies.</param>
        /// <param name="segments">The total number of segments.</param>
        /// <returns>The point on a circle.</returns>
        public static Vector3 GetPointOnCircle(int segment, int segments)
        {
            float angle = (segment * Pi2) / segments;

            float dy = (float)Math.Cos(angle);
            float dx = (float)Math.Sin(angle);

            return new Vector3(dy, 0, dx);
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
        #region Rectangle

        /// <summary>
        /// A check to see if a point is inside of a given rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to use.</param>
        /// <param name="position">The position to be checked.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsInsideOf(Rectangle rectangle, Vector2 position)
        {
            return position.X >= rectangle.X && position.X <= rectangle.Right &&
                    position.Y >= rectangle.Y && position.Y <= rectangle.Bottom;
        }

        /// <summary>
        /// Transforms a rectangle using a matrix.
        /// </summary>
        /// <param name="rectangle">The rectangle to be transformed.</param>
        /// <param name="matrix">The matrix to use to transform.</param>
        /// <returns>The transformed rectangle.</returns>
        public static Rectangle Transform(Rectangle rectangle, Matrix matrix)
        {
            Vector3 translation = matrix.Translation;
            Vector3 scale = new Vector3();
            Quaternion rotation = new Quaternion();

            matrix.Decompose(out scale, out rotation, out translation);

            rectangle.X += (int)translation.X;
            rectangle.Y += (int)translation.Y;
            rectangle.Width = (int)((float)rectangle.Width * scale.X);
            rectangle.Height = (int)((float)rectangle.Height * scale.Y);

            return rectangle;
        }

        #endregion
        #region Matrices

        /// <summary>
        /// Finds the absolute value of two matrices.
        /// </summary>
        /// <param name="value">The Matrix to calculate the absolute value of.</param>
        /// <param name="result">The result of the calculation.</param>
        /// <returns>The absolute calculation of the matrix.</returns>
        public static void Abs(ref Matrix value, out Matrix result)
        {
            result = Matrix.Identity;

            result.M11 = Abs(value.M11);
            result.M12 = Abs(value.M12);
            result.M13 = Abs(value.M13);
            result.M14 = Abs(value.M14);

            result.M21 = Abs(value.M21);
            result.M22 = Abs(value.M22);
            result.M23 = Abs(value.M23);
            result.M24 = Abs(value.M24);

            result.M31 = Abs(value.M31);
            result.M32 = Abs(value.M32);
            result.M33 = Abs(value.M33);
            result.M34 = Abs(value.M34);

            result.M41 = Abs(value.M41);
            result.M42 = Abs(value.M42);
            result.M43 = Abs(value.M43);
            result.M44 = Abs(value.M44);
        }

        /// <summary>
        /// Calculates a quaternion from a rotational matrix.
        /// </summary>
        /// <param name="m">The rotational matrix to be used.</param>
        /// <returns>The result of the calculation.</returns>
        public static Quaternion GetFromMatrix(Matrix m)
        {
            Quaternion q = new Quaternion();

            float det = m.Determinant();

            if (Math.Abs(det - 1) < Epsilon)
            {
                float t = m.M11 + m.M22 + m.M33 + m.M44;

                if (t > Epsilon)
                {
                    q.W = (float)Math.Sqrt(t) / 2.0f;
                    q.X = (m.M32 - m.M23) / (4 * q.W);
                    q.Y = (m.M13 - m.M31) / (4 * q.W);
                    q.Z = (m.M21 - m.M12) / (4 * q.W);
                }
                else
                {
                    if ((m.M11 > m.M22) && (m.M11 > m.M33))
                    {
                        float s = (float)Math.Sqrt(1.0f + m.M11 - m.M22 - m.M33) * 2;
                        q.W = (m.M32 - m.M23) / s;
                        q.X = 0.25f * s;
                        q.Y = (m.M12 + m.M21) / s;
                        q.Z = (m.M13 + m.M31) / s;
                    }
                    else if (m.M22 > m.M33)
                    {
                        float s = (float)Math.Sqrt(1.0f + m.M22 - m.M11 - m.M33) * 2;
                        q.W = (m.M13 - m.M31) / s;
                        q.X = (m.M12 + m.M21) / s;
                        q.Y = 0.25f * s;
                        q.Z = (m.M23 + m.M32) / s;
                    }
                    else
                    {
                        float s = (float)Math.Sqrt(1.0f + m.M33 - m.M11 - m.M22) * 2;
                        q.W = (m.M21 - m.M12) / s;
                        q.X = (m.M13 + m.M31) / s;
                        q.Y = (m.M23 + m.M32) / s;
                        q.Z = 0.25f * s;
                    }
                }

                q.Normalize();
            }

            return q;
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
