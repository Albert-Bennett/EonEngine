/* Created: 27/07/2014
 * Last Updated: 12/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// An interface used to determine which 
    /// LightComponent3D's can cast shadows.
    /// </summary>
    public interface ICastShadows
    {
        Matrix[] SplitViewProjection { get; }
        Matrix ViewProjection { get; }

        bool CastsShadows { get; }
        float FallOff { get; }

        void CalcFrustum(float[] splitDepths);
    }
}
