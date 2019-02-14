/* Created: 24/09/2015
 * Last Updated: 25/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Maths
{
    /// <summary>
    /// Defines a 2D bounding volume.  
    /// </summary>
    public interface IBounding2D
    {
        Vector2 Position { get; set; }
        Vector2 Center { get; set; }
        Vector2 Size { get; }

        bool Contains(Vector2 position);
        MTV Contains(BoundingCircle circle);
        MTV Contains(BoundingRectangle rectangle);

        Rectangle GenerateRectangle();
    }
}
