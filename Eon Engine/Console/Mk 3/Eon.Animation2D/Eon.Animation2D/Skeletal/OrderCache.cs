/* Created 19/09/2014
 * Last Updated: 19/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Graphics;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define the oredr in 
    /// which limbs should be drawn in.
    /// </summary>
    internal struct OrderCache
    {
        public int Order;
        public string LimbName;

        public Texture2D Texture; 
        public Texture2D NormalMap;
        public Texture2D DistortionMap;
    }
}
