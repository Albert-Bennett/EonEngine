/* Created 03/09/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls._2D
{
    /// <summary>
    /// Used to define a 2D MenuItem.
    /// </summary>
    public interface I2DMenuItem
    {
        Rectangle Bounds { get; }

        void SetPosition(Vector2 position);
        void Move(Vector2 movement);
    }
}
