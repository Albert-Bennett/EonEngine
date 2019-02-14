/* Created: 29/03/2015
 * Last Updated: 29/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Billboards
{
    /// <summary>
    /// Used to define a Billboard.
    /// </summary>
    public interface IBillboard
    {
        Matrix World { get; }

        Vector3 Position { get; }
        float Scale { get; }

        Texture2D Texture { get; }
    }
}
