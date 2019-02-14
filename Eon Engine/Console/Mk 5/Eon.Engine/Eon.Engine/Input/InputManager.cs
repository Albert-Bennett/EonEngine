/* Created: 04/06/2013
 * Last Updated: 13/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces.Base;
using Eon.System.Management;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Eon.Engine.Input
{
    /// <summary>
    /// A class that is used to process input from the player
    /// through the use of input devices.
    /// </summary>
    public sealed class InputManager : EngineModule, IUpdate
    {
        #region Input devices

        static Eon.Engine.Input.Keyboard keyboard = null;
        static Eon.Engine.Input.Mouse mouse = null;
        static Eon.Engine.Input.GamePad gamePad = null;
        static Eon.Engine.Input.TouchPad touchPad = null;

        static bool wasGamePadConnected = false;

        public static OnGamePadDisconnectedEvent OnGamePadDisconnected;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The game's TouchPad.
        /// </summary>
        public static TouchPad TouchPad
        {
            get { return touchPad; }
        }

        #endregion
        #region Ctor/ Updating

        /// <summary>
        /// Creates a new InputManager.
        /// </summary>
        public InputManager() : base("InputManager") { }

        public void _Update()
        {
            if (gamePad == null && wasGamePadConnected)
            {
                int idx = 0;
                bool connected = false;

                while (idx < 4 && !connected)
                {
                    connected = Microsoft.Xna.Framework.Input.GamePad.GetState(
                        (Microsoft.Xna.Framework.PlayerIndex)idx).IsConnected;

                    idx++;
                }

                gamePad = new Input.GamePad((Eon.Engine.Input.PlayerIndex)idx);
            }
        }

        public void _PostUpdate() { }

        #endregion
        #region Keyboard

        /// <summary>
        /// A check to see if the keyboard has been created.
        /// </summary>
        /// <returns>The result of the check.</returns>
        public static bool IsKeyboardConnected()
        {
            return keyboard != null;
        }

        /// <summary>
        /// Creates a new KeyBoard and Mouse.
        /// </summary>
        public static void CreateKeyboard()
        {
            if (keyboard == null)
            {
                keyboard = new Eon.Engine.Input.Keyboard();
                mouse = new Eon.Engine.Input.Mouse();
            }
        }

        /// <summary>
        /// Finds out if a key on the Keyboard is being held down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsKeyPressed(Keys key)
        {
            if (keyboard != null)
                return keyboard.IsKeyPressed(key);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a key on the Keyboard has been released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsKeyReleased(Keys key)
        {
            if (keyboard != null)
                return keyboard.IsKeyReleased(key);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a key on the Keyboard has been stroked.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsKeyStroked(Keys key)
        {
            if (keyboard != null)
                return keyboard.IsKeyStroked(key);
            else
                return false;
        }

        /// <summary>
        /// Gets the value of a key.
        /// </summary>
        /// <returns>The value of all keys currently being pressed.</returns>
        public static string[] GetKeyValue()
        {
            if (keyboard != null)
                return keyboard.GetKeyInput();

            return null;
        }

        #endregion
        #region Gamepad

        /// <summary>
        /// Changes the sensitivity of both thumb sticks.
        /// </summary>
        /// <param name="sensitivity">The sensitivity of the thumbsticks.</param>
        public static void ChangeThumbStickSensitivity(float sensitivity)
        {
            if (gamePad != null)
                gamePad.ChangeControllerSensitivity(sensitivity);
        }

        /// <summary>
        /// A check to see if a gamepad is connected.
        /// </summary>
        /// <returns>Is a GamePad is Connected.</returns>
        public static bool IsGamePadConnected()
        {
            if (gamePad != null)
                return gamePad.IsPadConnected;

            return false;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being held down.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickHeldDown(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.GetThumbStickAmount(index).Y <= -gamePad.Sensitivity;
            else
                return false;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being held up.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickHeldUp(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.GetThumbStickAmount(index).Y >= gamePad.Sensitivity;
            else
                return false;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being held left.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickHeldLeft(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.GetThumbStickAmount(index).X <= -gamePad.Sensitivity;
            else
                return false;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being held right.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickHeldRight(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.GetThumbStickAmount(index).X >= gamePad.Sensitivity;
            else
                return false;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being pushed down.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickDown(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.IsThumbStickDownStroked(index);
            else
                return false;
        }

        internal static void CurrentGamePadDisconnected(int index)
        {
            gamePad.Destroy();
            gamePad = null;

            wasGamePadConnected = true;

            if (OnGamePadDisconnected != null)
                OnGamePadDisconnected(index);
        }

        /// <summary>
        /// Check if the player has a game pad connected if 
        /// so this method creates the code for one.
        /// </summary>
        /// <returns>The result of the check.</returns>
        public static bool CheckCreateGamePad()
        {
            if (gamePad == null)
            {
                GamePadState state = default(GamePadState);
                bool connected = false;

                int playerIndex = 0;

                while (!connected && playerIndex < 4)
                {
                    state = Microsoft.Xna.Framework.Input.GamePad.GetState((Microsoft.Xna.Framework.PlayerIndex)playerIndex);

                    if (state.IsConnected)
                    {
                        connected = true;
                        gamePad = new Input.GamePad((Eon.Engine.Input.PlayerIndex)playerIndex);
                    }
                    else
                        playerIndex++;
                }

                return connected;
            }

            return true;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being pushed up.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickUp(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.IsThumbStickUpStroked(index);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being pushed left.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickLeft(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.IsThumbStickLeftStroked(index);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a thumbstick on a connected gamepad is being pushed right.
        /// </summary>
        /// <param name="index">The thumbstick index.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsThumbStickRight(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.IsThumbStickRightStroked(index);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a gamepad button is being held down.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsButtonPressed(GamePadButtons button)
        {
            if (gamePad != null)
                return gamePad.IsPadPressed(button);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a gamepad button has been released.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsButtonReleased(GamePadButtons button)
        {
            if (gamePad != null)
                return gamePad.IsPadReleased(button);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a gamepad button has been pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsButtonStroked(GamePadButtons button)
        {
            if (gamePad != null)
                return gamePad.IsPadStroked(button);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a gamepad trigger hase been stroked.
        /// </summary>
        /// <param name="index">The trigger to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsTriggerStroked(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.IsPadTriggerStroked(index);

            return false;
        }

        /// <summary>
        /// Finds out if a gamepad trigger hase been pressed.
        /// </summary>
        /// <param name="index">The trigger to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsTriggerPressed(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.IsPadTriggerPressed(index);

            return false;
        }

        /// <summary>
        /// Finds out if a gamepad trigger hase been released.
        /// </summary>
        /// <param name="index">The trigger to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsTriggerReleased(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.IsPadTriggerReleased(index);

            return false;
        }

        /// <summary>
        /// Finds the amount of movement in a thumbstick as a Vector2.
        /// </summary>
        /// <param name="index">The thumbstick to check.</param>
        /// <returns>The result of the check.</returns>
        public static Vector2 GetThumbStickAmount(TriggerIndex index)
        {
            if (gamePad != null)
                return gamePad.GetThumbStickAmount(index);
            else
                return Vector2.Zero;
        }

        #endregion
        #region Mouse

        /// <summary>
        /// The position of the Mouse on the screen.
        /// </summary>
        public static Vector2 MousePos
        {
            get
            {
                if (mouse != null)
                    return mouse.Pos;
                else if (touchPad != null)
                    return touchPad.TouchLoc;
                else
                    return Vector2.Zero;
            }
        }

        /// <summary>
        /// Whether or not a Mouse has been created. 
        /// </summary>
        public static bool IsMouseConnected { get { return mouse != null; } }

        /// <summary>
        /// Changes the mouse position.
        /// </summary>
        /// <param name="position">The new position of the mouse.</param>
        public static void SetMousePosition(Vector2 position)
        {
            if (mouse != null)
                mouse.SetPosition(position);
        }

        /// <summary>
        /// Changes the mouses sensitivity.
        /// </summary>
        /// <param name="sensitivity">The sensitivity of the mouse.</param>
        public static void ChangeMouseSensitivity(float sensitivity)
        {
            if (mouse != null)
                mouse.SetMouseSensitivity(sensitivity);
        }

        /// <summary>
        /// A check to see if the mouse is on the screen.
        /// </summary>
        public static bool MouseIsOnScreen
        {
            get
            {
                if (MousePos.X < Common.Device.Viewport.Width && MousePos.X > Common.Device.Viewport.X)
                    if (MousePos.Y < Common.Device.Viewport.Height && MousePos.Y > Common.Device.Viewport.Y)
                        return true;

                return false;
            }
        }

        /// <summary>
        /// The delta between mouse positions.
        /// </summary>
        public static Vector2 MouseDelta
        {
            get
            {
                if (mouse != null)
                    return mouse.MouseDelta;
                else if (touchPad != null)
                    return touchPad.Delta;
                else
                    return Vector2.Zero;
            }
        }

        /// <summary>
        /// Finds the Mouse's scroll wheel value.
        /// </summary>
        public static float ScrollWheelValue
        {
            get
            {
                if (mouse != null)
                    return mouse.ScrollWheel;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Finds out if a button on the Mouse is being held down.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsMouseButtonPressed(MouseButtons button)
        {
            if (mouse != null)
                return mouse.IsButtonPressed(button);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a button on the Mouse has been released.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsMouseButtonReleased(MouseButtons button)
        {
            if (mouse != null)
                return mouse.IsButtonReleased(button);
            else
                return false;
        }

        /// <summary>
        /// Finds out if a button on the Mouse has been stroked.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsMouseButtonStroked(MouseButtons button)
        {
            if (mouse != null)
                return mouse.IsButtonStroked(button);
            else
                return false;
        }

        public static Ray GetScreenSpaceRay(Matrix view, Matrix projection)
        {
            return mouse.GetScreenSpaceRay(view, projection);
        }

        #endregion
        #region TouchPad

        /// <summary>
        /// Creates the TouchPad. 
        /// </summary>
        public static void CreateTouchPad()
        {
            if (touchPad == null)
                touchPad = new TouchPad();
        }

        /// <summary>
        /// Whether or not a TouchPad has been created. 
        /// </summary>
        public static bool IsTouchPadConnected { get { return touchPad != null; } }

        /// <summary>
        /// Has the gesture been made on the TouchPad.
        /// </summary>
        /// <param name="gesture">The gesture to check for.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsGestured(TouchType gesture)
        {
            if (touchPad != null)
                return touchPad.TouchType == gesture;

            return false;
        }

        #endregion
    }
}
