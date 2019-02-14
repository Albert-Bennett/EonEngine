/* Created: 18/09/2014
 * Last Updated: 18/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Rendering2D.Cameras;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Helpers
{
    /// <summary>
    /// Defines a helper for determining input related items.
    /// </summary>
    public static class InputChecker
    {
        /// <summary>
        /// A check to see if either the mouse is inside of an 
        /// item or if the player tapped on an item.
        /// </summary>
        /// <param name="bounds">The bounding area of the item to be checked.</param>
        /// <param name="ignoreCamera">Does the item ignore the any kind of Camera2D.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsInside(Rectangle bounds, bool ignoreCamera)
        {
            bool inside = false;

            if (InputManager.IsMouseConnected)
                inside = IsInside(InputManager.MousePos, bounds, ignoreCamera);

            if (!inside)
                if (InputManager.TouchPad != null)
                    inside = IsInside(InputManager.TouchPad.TouchLoc, bounds, ignoreCamera);

            return inside;
        }

        static bool IsInside(Vector2 position, Rectangle bounds, bool ignoreCamera)
        {
            if (!ignoreCamera && CameraManager2D.CurrentCamera != null)
                position = Vector2.Transform(position, Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix));

            return bounds.Contains((int)position.X, (int)position.Y);
        }
    }
}
