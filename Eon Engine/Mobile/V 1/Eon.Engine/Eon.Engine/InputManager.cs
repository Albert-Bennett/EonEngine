/* Created 05/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Double A Game Studios.
 */

using Eon.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Eon.Engine
{
    /// <summary>
    /// A class that is used to process input from the player
    /// through the use of input devices.
    /// </summary>
    public sealed class InputManager
    {
        static TouchScreen screen = new TouchScreen();

        /// <summary>
        /// The touch screen.
        /// </summary>
        public static TouchScreen Screen { get { return screen; } }

        /// <summary>
        /// A check to see if the back button has been pressed.
        /// </summary>
        /// <returns>The result of the check.</returns>
        public static bool IsBackPressed()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                return true;

            return false;
        }
    }
}
