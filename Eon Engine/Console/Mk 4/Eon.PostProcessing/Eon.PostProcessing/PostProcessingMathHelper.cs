/* Created: 19/10/2013
 * Last Updated: 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.PostProcessing
{
    /// <summary>
    /// Defines a math helper that is used to help 
    /// with maths related to post processing effects.
    /// </summary>
    public static class PostProcessingMathHelper
    {
        /// <summary>
        /// Used to calculate a set of value representing blur weights.
        /// </summary>
        /// <param name="samples">The amount of samples in the blur effect.</param>
        /// <param name="blurAmount">The amount of blur to be applied.</param>
        /// <returns>The calculated blur weights.</returns>
        public static float[] FindBlurWeights(int samples, float blurAmount)
        {
            float[] weights = new float[samples];

            weights[0] = FindGausianElement(0, blurAmount);

            float total = weights[0];

            for (int i = 0; i < samples / 2; i++)
            {
                float w = FindGausianElement(i + 1, blurAmount);

                weights[i * 2 + 1] = w;
                weights[i * 2 + 2] = w;

                total += w * 2;
            }

            for (int i = 0; i < weights.Length; i++)
                weights[i] /= total;

            return weights;
        }

        static float FindGausianElement(int samples, float blurAmount)
        {
            return (float)((1.0 / Math.Sqrt(2 * Math.PI * blurAmount)) *
                Math.Exp(-(samples * samples) / (2 * blurAmount * blurAmount)));
        }

        /// <summary>
        /// Calculates a set of blur offsets.
        /// </summary>
        /// <param name="samples">The amount of samples per blur.
        /// (Must be an uneven number).</param>
        /// <param name="dx">Dx.</param>
        /// <param name="dy">Dy.</param>
        /// <returns>A set of calculated blur offsets.</returns>
        public static Vector2[] FindBlurOffsets(int samples, float dx, float dy)
        {
            Vector2[] offsets = new Vector2[samples];

            offsets[0] = Vector2.Zero;

            for (int i = 0; i < samples / 2; i++)
            {
                float baseTransform = i * 2 + 1.5f;

                Vector2 delta = new Vector2(dx, dy) * baseTransform;

                offsets[i * 2 + 1] = delta;
                offsets[i * 2 + 2] = -delta;
            }

            return offsets;
        }
    }
}
