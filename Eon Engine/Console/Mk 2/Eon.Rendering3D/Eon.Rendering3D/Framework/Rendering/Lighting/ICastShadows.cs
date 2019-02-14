/* Created 27/07/2014
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
        Matrix View { get; }
        Matrix Proj { get; }

        Vector3 Direction { get; }
        bool CastsShadows { get; }

        bool Shadows(BoundingSphere boundingSphere);
    }
}
