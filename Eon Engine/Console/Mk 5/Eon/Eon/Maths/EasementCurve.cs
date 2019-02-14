/* Created: 18/09/2015
 * Last Updated: 18/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;

namespace Eon.Maths
{
    /// <summary>
    /// Used to define an easement method.
    /// </summary>
    public struct EasementCurve
    {
        CurveTypes curveType;

        float duration;
        float currentTime;
        float delta;
        float weight;

        /// <summary>
        /// The type of easment curve to be used.
        /// </summary>
        public CurveTypes CurveType
        {
            get { return curveType; }
        }

        /// <summary>
        /// Creates a new AnimCurve.
        /// </summary>
        /// <param name="weight">The weight of the blending.</param>
        /// <param name="delta">The change in weight over time.</param>
        /// <param name="duration">The duration of the blend (in miliseconds).</param>
        /// <param name="curveType">The type of easment curve to be used.</param>
        public EasementCurve(int weight, float delta, float duration, CurveTypes curveType)
        {
            this.curveType = curveType;
            this.weight = weight;
            this.delta = delta;

            currentTime = 0;
            this.duration = duration;
        }

        /// <summary>
        /// Gets the weight based on the curve.
        /// </summary>
        /// <returns>The new weight.</returns>
        public float GetWeight()
        {
            if (currentTime < duration)
            {
                float result = 0;

                switch (curveType)
                {
                    case CurveTypes.CircularIn:
                        result = EonMathsHelper.CircularInCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.CircularInOut:
                        result = EonMathsHelper.CircularInOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.CircularOut:
                        result = EonMathsHelper.CircularOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.CubicIn:
                        result = EonMathsHelper.CubicInCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.CubicInOut:
                        result = EonMathsHelper.CubicInOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.CubicOut:
                        result = EonMathsHelper.CubicOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.ExponentialIn:
                        result = EonMathsHelper.ExponentialInCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.ExponentialInOut:
                        result = EonMathsHelper.ExponentialInOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.ExponentialOut:
                        result = EonMathsHelper.ExponentialOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.Linear:
                        result = EonMathsHelper.LinearCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuadraticIn:
                        result = EonMathsHelper.QuadraticInCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuadraticInOut:
                        result = EonMathsHelper.QuadraticInOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuadraticOut:
                        result = EonMathsHelper.QuadraticOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuarticIn:
                        result = EonMathsHelper.QuarticInCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuarticInOut:
                        result = EonMathsHelper.QuarticInOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuarticOut:
                        result = EonMathsHelper.QuarticOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuinticIn:
                        result = EonMathsHelper.QuinticInCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuinticInOut:
                        result = EonMathsHelper.QuinticInOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.QuinticOut:
                        result = EonMathsHelper.QuinticOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.SineIn:
                        result = EonMathsHelper.SineInCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.SineInOut:
                        result = EonMathsHelper.SineInOutCurve(currentTime, duration, weight, delta);
                        break;

                    case CurveTypes.SineOut:
                        result = EonMathsHelper.SineOutCurve(currentTime, duration, weight, delta);
                        break;
                }

                currentTime += (float)Common.ElapsedTimeDelta.TotalMilliseconds;

                return result;
            }

            return weight;
        }

        /// <summary>
        /// Resets the AnimCurve.
        /// </summary>
        public void Reset()
        {
            currentTime = 0;
        }
    }
}
