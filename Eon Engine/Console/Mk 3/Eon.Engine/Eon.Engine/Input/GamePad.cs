/* Created: 09/06/2013
 * Last Updated: 14/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Eon.Engine.Input
{
    /// <summary>
    /// Defines a GamePad input device.
    /// </summary>
    public class GamePad : BaseInputItem
    {
        #region Variables

        float controllerSensitivity = 0.5f;
        int index;

        float[] triggers = new float[(int)TriggerIndex.Count];
        float[] prevTriggers = new float[(int)TriggerIndex.Count];
        Vector2[] thumbStick = new Vector2[(int)TriggerIndex.Count];
        Vector2[] prevThumbStick = new Vector2[(int)TriggerIndex.Count];

        GamePadState currentState;
        GamePadState prevState;
        GamePadState emptyState;

        #endregion
        #region Fields

        /// <summary>
        /// The default GamePad Sensitivity.
        /// </summary>
        internal const float DefaultSensitivity = 0.5f;

        /// <summary>
        /// The Player Index of the GamePad.
        /// </summary>
        internal Eon.Engine.Input.PlayerIndex PlayerIndex
        {
            get { return (Eon.Engine.Input.PlayerIndex)index; }
        }

        /// <summary>
        /// The sensitivity setting for this GamePad.
        /// </summary>
        internal float Sensitivity { get { return controllerSensitivity; } }

        /// <summary>
        /// Returns the values of the GamePad's triggers as an array.
        /// </summary>
        internal float[] Triggers
        {
            get { return triggers; }
        }

        /// <summary>
        /// Returns the values of this GamePad's Thumbsticks.
        /// </summary>
        internal Vector2[] ThumbStick
        {
            get { return thumbStick; }
        }

        /// <summary>
        /// Returns true if this GamePad is connected.
        /// </summary>
        internal bool IsPadConnected
        {
            get { return currentState.IsConnected; }
        }

        #endregion
        #region Updates

        /// <summary>
        /// Creates a new GamePad.
        /// </summary>
        public GamePad(Eon.Engine.Input.PlayerIndex playerIndex)
            : base("GamePad" + playerIndex)
        {
            index = (int)playerIndex;

            emptyState = Microsoft.Xna.Framework.Input.GamePad.GetState((Microsoft.Xna.Framework.PlayerIndex)index);
        }

        protected override void PreUpdate()
        {
            prevState = currentState;

            prevTriggers[(int)TriggerIndex.Left] = Triggers[(int)TriggerIndex.Left];
            prevTriggers[(int)TriggerIndex.Right] = Triggers[(int)TriggerIndex.Right];

            prevThumbStick[(int)TriggerIndex.Left] = thumbStick[(int)TriggerIndex.Left];
            prevThumbStick[(int)TriggerIndex.Right] = thumbStick[(int)TriggerIndex.Right];
        }

        protected override void Update()
        {
            PreUpdate();

            PostUpdate();
        }

        protected override void _PostUpdate()
        {
            currentState = Microsoft.Xna.Framework.Input.GamePad.GetState((Microsoft.Xna.Framework.PlayerIndex)index);

            Triggers[(int)TriggerIndex.Left] = currentState.Triggers.Left;
            Triggers[(int)TriggerIndex.Right] = currentState.Triggers.Right;

            thumbStick[(int)TriggerIndex.Left] = currentState.ThumbSticks.Left;
            thumbStick[(int)TriggerIndex.Right] = currentState.ThumbSticks.Right;

            if (!currentState.IsConnected)
                InputManager.CurrentGamePadDisconnected();
        }

        #endregion
        #region Checks

        bool IsPadStateEquals(GamePadState targetState, GamePadButtons pad, ButtonState state)
        {
            switch (pad)
            {
                case GamePadButtons.Start:
                    return (targetState.Buttons.Start == state);

                case GamePadButtons.Back:
                    return (targetState.Buttons.Back == state);

                case GamePadButtons.Guide:
                    return (targetState.Buttons.BigButton == state);

                case GamePadButtons.A:
                    return (targetState.Buttons.A == state);

                case GamePadButtons.B:
                    return (targetState.Buttons.B == state);

                case GamePadButtons.X:
                    return (targetState.Buttons.X == state);

                case GamePadButtons.Y:
                    return (targetState.Buttons.Y == state);

                case GamePadButtons.LeftShoulder:
                    return (targetState.Buttons.LeftShoulder == state);

                case GamePadButtons.LeftStick:
                    return (targetState.Buttons.LeftStick == state);

                case GamePadButtons.RightShoulder:
                    return (targetState.Buttons.RightShoulder == state);

                case GamePadButtons.RightStick:
                    return (targetState.Buttons.RightStick == state);

                case GamePadButtons.LeftPad:
                    return (targetState.DPad.Left == state);

                case GamePadButtons.RightPad:
                    return (targetState.DPad.Right == state);

                case GamePadButtons.UpPad:
                    return (targetState.DPad.Up == state);

                case GamePadButtons.DownPad:
                    return (targetState.DPad.Down == state);

                case GamePadButtons.LeftTrigger:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetPadTriggerValue(TriggerIndex.Left) > 0.0f);
                        else if (state == ButtonState.Released)
                            return (GetPadTriggerValue(TriggerIndex.Left) == 0.0f);

                        return false;
                    }

                case GamePadButtons.RightTrigger:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetPadTriggerValue(TriggerIndex.Right) > 0.0f);
                        else if (state == ButtonState.Released)
                            return (GetPadTriggerValue(TriggerIndex.Right) == 0.0f);

                        return false;
                    }

                case GamePadButtons.LeftThumbStickUp:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Left).Y > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Left).Y == controllerSensitivity);

                        return false;
                    }

                case GamePadButtons.LeftThumbStickDown:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Left).Y < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Left).Y == -controllerSensitivity);

                        return false;
                    }

                case GamePadButtons.LeftThumbStickLeft:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Left).X < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Left).X == -controllerSensitivity);

                        return false;
                    }

                case GamePadButtons.LeftThumbStickRight:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Left).X > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Left).X == controllerSensitivity);

                        return false;
                    }

                case GamePadButtons.RightThumbStickUp:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Right).Y > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Right).Y == controllerSensitivity);

                        return false;
                    }

                case GamePadButtons.RightThumbStickDown:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Right).Y < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Right).Y == -controllerSensitivity);

                        return false;
                    }

                case GamePadButtons.RightThumbStickLeft:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Right).X < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Right).Y == -controllerSensitivity);

                        return false;
                    }

                case GamePadButtons.RightThumbStickRight:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggerIndex.Right).X > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggerIndex.Right).Y == controllerSensitivity);

                        return false;
                    }

                default: return false;
            }
        }

        /// <summary>
        /// A check to see if a button on the game pad has been pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>Th result of the check.</returns>
        internal bool IsPadPressed(GamePadButtons button)
        {
            return IsPadStateEquals(currentState, button, ButtonState.Pressed);
        }

        /// <summary>
        /// A check to see if a button on the game pad has been pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>Th result of the check.</returns>
        internal bool IsPadReleased(GamePadButtons button)
        {
            return IsPadStateEquals(currentState, button, ButtonState.Released);
        }

        /// <summary>
        /// A check to see if a button on the game pad has been pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>Th result of the check.</returns>
        internal bool IsPadStroked(GamePadButtons button)
        {
            switch (button)
            {
                case GamePadButtons.LeftThumbStickUp:
                    return IsThumbStickUpStroked(TriggerIndex.Left);

                case GamePadButtons.LeftThumbStickDown:
                    return IsThumbStickDownStroked(TriggerIndex.Left);

                case GamePadButtons.LeftThumbStickLeft:
                    return IsThumbStickLeftStroked(TriggerIndex.Left);

                case GamePadButtons.LeftThumbStickRight:
                    return IsThumbStickRightStroked(TriggerIndex.Left);

                case GamePadButtons.RightThumbStickUp:
                    return IsThumbStickUpStroked(TriggerIndex.Right);

                case GamePadButtons.RightThumbStickDown:
                    return IsThumbStickDownStroked(TriggerIndex.Right);

                case GamePadButtons.RightThumbStickLeft:
                    return IsThumbStickLeftStroked(TriggerIndex.Right);

                case GamePadButtons.RightThumbStickRight:
                    return IsThumbStickRightStroked(TriggerIndex.Right);

                case GamePadButtons.LeftTrigger:
                    return IsTriggerStroked(TriggerIndex.Left);

                case GamePadButtons.RightTrigger:
                    return IsTriggerStroked(TriggerIndex.Right);
            };

            return (!IsPadStateEquals(prevState, button, ButtonState.Pressed) &&
                        IsPadPressed(button));
        }
        internal bool IsPadTriggersStroked(TriggerIndex index)
        {
            return (Triggers[(int)index] > 0.0f && prevTriggers[(int)index] <= 0.0f);
        }

        #endregion
        #region Helpers

        internal bool IsThumbStickUpStroked(TriggerIndex index)
        {
            return (ThumbStick[(int)index].Y > controllerSensitivity &&
                    prevThumbStick[(int)index].Y <= controllerSensitivity);
        }
        internal bool IsThumbStickDownStroked(TriggerIndex index)
        {
            return (ThumbStick[(int)index].Y < -controllerSensitivity &&
                    prevThumbStick[(int)index].Y >= -controllerSensitivity);
        }
        internal bool IsThumbStickLeftStroked(TriggerIndex index)
        {
            return (ThumbStick[(int)index].X < -controllerSensitivity &&
                    prevThumbStick[(int)index].X >= -controllerSensitivity);
        }
        internal bool IsThumbStickRightStroked(TriggerIndex index)
        {
            return (ThumbStick[(int)index].X > controllerSensitivity &&
                    prevThumbStick[(int)index].X <= controllerSensitivity);
        }
        internal bool IsTriggerStroked(TriggerIndex index)
        {
            return (Triggers[(int)index] > controllerSensitivity &&
                    prevTriggers[(int)index] <= controllerSensitivity);
        }

        internal float GetPadTriggerValue(TriggerIndex index)
        {
            return Triggers[(int)index];
        }
        internal Vector2 GetThumbStickAmount(TriggerIndex index)
        {
            return ThumbStick[(int)index];
        }

        internal override void Reset()
        {
            currentState = prevState = emptyState;
        }

        internal void ChangeControllerSensitivity(float amount)
        {
            if (controllerSensitivity != amount)
                controllerSensitivity = EonMathsHelper.Clamp(controllerSensitivity, 0.1f, 0.9f);
        }

        #endregion
    }
}
