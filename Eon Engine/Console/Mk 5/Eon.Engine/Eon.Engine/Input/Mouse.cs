/* Created: 09/06/2013
 * Last Updated: 25/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Eon.Engine.Input
{
    /// <summary>
    /// Defines the mouse input item.
    /// </summary>
    public class Mouse : BaseInputItem
    {
        const float MaxSensitivity = 1.0f;
        const float MinSensitivity = 0.1f;

        float sensitivity = 0.5f;

        Vector2 defaultPos;
        MouseState currentState;
        MouseState prevState;
        MouseState emptyState;

        Vector2 pos;

        public Vector2 Pos
        {
            get { return pos * Common.UpScale; }
        }

        /// <summary>
        /// Finds out the delta of the mouse.
        /// </summary>
        internal Vector2 MouseDelta
        {
            get
            {
                return (currentState.Position -
                    prevState.Position).ToVector2() * Common.UpScale;
            }
        }

        /// <summary>
        /// The value of the Mouse's scrollwheel.
        /// </summary>
        internal float ScrollWheel { get { return currentState.ScrollWheelValue; } }

        /// <summary>
        /// Creates a new Mouse input device.
        /// </summary>
        public Mouse()
            : base("Mouse")
        {
            defaultPos = new Vector2(Common.Device.Viewport.Width / 2,
                Common.Device.Viewport.Height / 2);

            Reset();
        }

        protected override void Update()
        {
            prevState = currentState;

            SetMousePos();

            currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            base.Update();
        }

        void SetMousePos()
        {
            Vector2 delta = (currentState.Position -
                prevState.Position).ToVector2();

            delta *= sensitivity;
            pos = new Vector2(currentState.X + delta.X, currentState.Y + delta.Y);
        }

        /// <summary>
        /// Sets the position of the Mouse.
        /// </summary>
        /// <param name="position">The new position of the mouse.</param>
        internal void SetPosition(Vector2 position)
        {
            pos = position;

            Microsoft.Xna.Framework.Input.Mouse.SetPosition(
                (int)position.X, (int)position.Y);
        }

        /// <summary>
        /// Calulates a screen space ray using the mouses position.
        /// </summary>
        /// <param name="view">The view matrix to use.</param>
        /// <param name="proj">The projection matrix to use.</param>
        /// <returns>The calculated ray.</returns>
        internal Ray GetScreenSpaceRay(Matrix view, Matrix proj)
        {
            Vector3 near = new Vector3(pos, 0);
            Vector3 far = new Vector3(pos, 1);

            Vector3 nearPoint = Common.Device.Viewport.Unproject(near,
                proj, view, Matrix.Identity);

            Vector3 farPoint = Common.Device.Viewport.Unproject(far,
                proj, view, Matrix.Identity);

            Vector3 direct = farPoint - nearPoint;
            direct.Normalize();

            return new Ray(nearPoint, direct);
        }

        ButtonState GetBtnState(MouseButtons btn, MouseState state)
        {
            switch (btn)
            {
                case MouseButtons.Left:
                    return state.LeftButton;

                case MouseButtons.Middle:
                    return state.MiddleButton;

                case MouseButtons.Right:
                    return state.RightButton;

                case MouseButtons.Alt1:
                    return state.XButton1;

                default:
                    return state.XButton2;
            }
        }

        /// <summary>
        /// A check to see if a mouse button is being held down.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        internal bool IsButtonPressed(MouseButtons button)
        {
            return GetBtnState(button, currentState) == ButtonState.Pressed ? true : false;
        }
        bool IsButtonPressed(MouseButtons button, MouseState state)
        {
            return GetBtnState(button, state) == ButtonState.Pressed ? true : false;
        }

        /// <summary>
        /// A check to see if a mouse button has being released.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        internal bool IsButtonReleased(MouseButtons button)
        {
            return GetBtnState(button, currentState) == ButtonState.Released ? true : false;
        }
        bool IsButtonReleased(MouseButtons button, MouseState state)
        {
            return GetBtnState(button, state) == ButtonState.Released ? true : false;
        }

        /// <summary>
        /// A check to see if a mouse button has been stroked.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>The result of the check.</returns>
        internal bool IsButtonStroked(MouseButtons button)
        {
            if (IsButtonReleased(button, prevState) && IsButtonPressed(button, currentState))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Used to set the sensitivity of the Mouse.
        /// </summary>
        /// <param name="value">The value to set the sensitivity to.</param>
        internal void SetMouseSensitivity(float value)
        {
            if (sensitivity != value)
                sensitivity = EonMathsHelper.Clamp(sensitivity, MinSensitivity, MaxSensitivity);
        }

        /// <summary>
        /// Resets the Mouse.
        /// </summary>
        internal override void Reset()
        {
            Microsoft.Xna.Framework.Input.Mouse.SetPosition((int)defaultPos.X, (int)defaultPos.Y);

            emptyState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            currentState = prevState = emptyState;
        }
    }
}
