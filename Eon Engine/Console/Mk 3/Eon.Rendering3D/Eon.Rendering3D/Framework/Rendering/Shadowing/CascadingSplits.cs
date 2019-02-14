/* Created: 12/03/2015
 * Last Updated: 12/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Framework.Rendering.Shadowing
{
    /// <summary>
    /// Defines a class to manage 
    /// splits in cascading shadow maps.
    /// </summary>
    internal static class CascadingSplits
    {
        internal static readonly int Splits = 4;
        static readonly float SplitConst = 0.95f;

        static Vector2[] clipPlanes = new Vector2[4];
        static float[] splitDepths = new float[5];

        static float near = 0.0f;
        static float far = 0.0f;

        public static Vector2[] ClipPlanes { get { return clipPlanes; } }
        public static float[] SplitDepths { get { return splitDepths; } }

        internal static void CalculateSpitDepths()
        {
            if (CameraManager.CurrentCamera.NearPlane != near ||
                CameraManager.CurrentCamera.FarPlane != far)
            {
                near = CameraManager.CurrentCamera.NearPlane;
                far = CameraManager.CurrentCamera.FarPlane;

                splitDepths[0] = near;
                splitDepths[Splits] = far;

                for (int i = 1; i < Splits; i++)
                    splitDepths[i] = SplitConst * near * (float)Math.Pow(
                        far / near, (float)i / Splits) + (1.0f - SplitConst) *
                        ((near + ((float)i / Splits)) * (far - near));

                for (int i = 0; i < Splits; i++)
                    clipPlanes[i] = new Vector2(-splitDepths[i],
                        -splitDepths[i + 1]);
            }
        }
    }
}
