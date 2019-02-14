/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Eon.Engine.Input
{
    /// <summary>
    /// Defines a GamePad input device.
    /// </summary>
    public class GamePad : BaseInputItem
    {
        #region Variables

        bool vibrate = true;
        float controllerSensitivity = 0.5f;
        int index;

        float[] triggers = new float[(int)TriggersIdx.Count];
        float[] prevTriggers = new float[(int)TriggersIdx.Count];
        Vector2[] thumbStick = new Vector2[(int)TriggersIdx.Count];
        Vector2[] prevThumbStick = new Vector2[(int)TriggersIdx.Count];

        GamePadState currentState;
        GamePadState prevState;
        GamePadState emptyState;

        float[] vibrationIntencity = new float[(int)TriggersIdx.Count];
        TimeSpan vibrationDurationAccTime = TimeSpan.Zero;
        TimeSpan vibrationDuration = TimeSpan.Zero;

        #endregion
        #region Fields

        /// <summary>
        /// Wheather or not this GamePad's vibration function is enabled.
        /// </summary>
        public bool VibrateEnabled
        {
            get { return vibrate; }
        }

        /// <summary>
        /// The Player Index of the GamePad.
        /// </summary>
        public PlayerIndex PlayerIndex
        {
            get { return (PlayerIndex)Enum.GetValues(typeof(PlayerIndex)).GetValue(index); }
        }

        /// <summary>
        /// The sensitivity setting for this GamePad.
        /// </summary>
        public float Sensitivity { get { return controllerSensitivity; } }

        /// <summary>
        /// Returns the values of the GamePad's triggers as an array.
        /// </summary>
        public float[] Triggers
        {
            get { return triggers; }
        }

        /// <summary>
        /// Returns the values of this GamePad's Thumbsticks.
        /// </summary>
        public Vector2[] ThumbStick
        {
            get { return thumbStick; }
        }

        /// <summary>
        /// Retuns an array representing this GamePad's vibration per moter.
        /// </summary>
        public float[] VibrationAmount
        {
            get { return vibrationIntencity; }
        }

        /// <summary>
        /// Returns true if this GamePad is connected.
        /// </summary>
        public bool IsPadConnected
        {
            get { return currentState.IsConnected; }
        }

        #endregion
        #region Updates

        /// <summary>
        /// Creates a new GamePad.
        /// </summary>
        public GamePad(PlayerIndex playerIndex)
            : base("GamePad" + playerIndex)
        {
            index = (int)playerIndex;
            emptyState = Microsoft.Xna.Framework.Input.GamePad.GetState((Microsoft.Xna.Framework.PlayerIndex)index);
        }

        protected override void PreUpdate()
        {
            prevState = currentState;

            prevTriggers[(int)TriggersIdx.Left] = Triggers[(int)TriggersIdx.Left];
            prevTriggers[(int)TriggersIdx.Right] = Triggers[(int)TriggersIdx.Right];

            prevThumbStick[(int)TriggersIdx.Left] = thumbStick[(int)TriggersIdx.Left];
            prevThumbStick[(int)TriggersIdx.Right] = thumbStick[(int)TriggersIdx.Right];
        }

        protected override void Update()
        {
            PreUpdate();

            if (VibrateEnabled)
                if (vibrationDurationAccTime < vibrationDuration)
                {
                    vibrationDurationAccTime += Common.ElapsedTimeDelta;

                    Microsoft.Xna.Framework.Input.GamePad.SetVibration((Microsoft.Xna.Framework.PlayerIndex)index,
                        VibrationAmount[(int)TriggersIdx.Left],
                        VibrationAmount[(int)TriggersIdx.Right]);
                }
                else if (vibrationDurationAccTime >= vibrationDuration)
                {
                    vibrationDurationAccTime = TimeSpan.Zero;
                    vibrationDuration = TimeSpan.Zero;

                    Microsoft.Xna.Framework.Input.GamePad.SetVibration((Microsoft.Xna.Framework.PlayerIndex)index, 0, 0);
                }

            PostUpdate();
        }

        protected override void _PostUpdate()
        {
            currentState = Microsoft.Xna.Framework.Input.GamePad.GetState((Microsoft.Xna.Framework.PlayerIndex)index);

            Triggers[(int)TriggersIdx.Left] = currentState.Triggers.Left;
            Triggers[(int)TriggersIdx.Right] = currentState.Triggers.Right;

            thumbStick[(int)TriggersIdx.Left] = currentState.ThumbSticks.Left;
            thumbStick[(int)TriggersIdx.Right] = currentState.ThumbSticks.Right;
        }

        #endregion
        #region Checks

        bool IsPadStateEquals(GamePadState targetState, ControlPadBtns pad, ButtonState state)
        {
            switch (pad)
            {
                case ControlPadBtns.Start:
                    return (targetState.Buttons.Start == state);

                case ControlPadBtns.Back:
                    return (targetState.Buttons.Back == state);

                case ControlPadBtns.A:
                    return (targetState.Buttons.A == state);

                case ControlPadBtns.B:
                    return (targetState.Buttons.B == state);

                case ControlPadBtns.X:
                    return (targetState.Buttons.X == state);

                case ControlPadBtns.Y:
                    return (targetState.Buttons.Y == state);

                case ControlPadBtns.LeftShoulder:
                    return (targetState.Buttons.LeftShoulder == state);

                case ControlPadBtns.LeftStick:
                    return (targetState.Buttons.LeftStick == state);

                case ControlPadBtns.RightShoulder:
                    return (targetState.Buttons.RightShoulder == state);

                case ControlPadBtns.RightStick:
                    return (targetState.Buttons.RightStick == state);

                case ControlPadBtns.LeftPad:
                    return (targetState.DPad.Left == state);

                case ControlPadBtns.RightPad:
                    return (targetState.DPad.Right == state);

                case ControlPadBtns.UpPad:
                    return (targetState.DPad.Up == state);

                case ControlPadBtns.DownPad:
                    return (targetState.DPad.Down == state);

                case ControlPadBtns.LeftTrigger:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetPadTriggerValue(TriggersIdx.Left) > 0.0f);
                        else if (state == ButtonState.Released)
                            return (GetPadTriggerValue(TriggersIdx.Left) == 0.0f);

                        return false;
                    }

                case ControlPadBtns.RightTrigger:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetPadTriggerValue(TriggersIdx.Right) > 0.0f);
                        else if (state == ButtonState.Released)
                            return (GetPadTriggerValue(TriggersIdx.Right) == 0.0f);

                        return false;
                    }

                case ControlPadBtns.LeftThumbStickUp:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Left).Y > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Left).Y == controllerSensitivity);

                        return false;
                    }

                case ControlPadBtns.LeftThumbStickDown:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Left).Y < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Left).Y == -controllerSensitivity);

                        return false;
                    }

                case ControlPadBtns.LeftThumbStickLeft:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Left).X < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Left).X == -controllerSensitivity);

                        return false;
                    }

                case ControlPadBtns.LeftThumbStickRight:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Left).X > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Left).X == controllerSensitivity);

                        return false;
                    }

                case ControlPadBtns.RightThumbStickUp:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Right).Y > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Right).Y == controllerSensitivity);

                        return false;
                    }

                case ControlPadBtns.RightThumbStickDown:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Right).Y < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Right).Y == -controllerSensitivity);

                        return false;
                    }

                case ControlPadBtns.RightThumbStickLeft:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Right).X < -controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Right).Y == -controllerSensitivity);

                        return false;
                    }

                case ControlPadBtns.RightThumbStickRight:
                    {
                        if (state == ButtonState.Pressed)
                            return (GetThumbStickAmount(TriggersIdx.Right).X > controllerSensitivity);
                        else if (state == ButtonState.Released)
                            return (GetThumbStickAmount(TriggersIdx.Right).Y == controllerSensitivity);

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
        public bool IsPadPressed(ControlPadBtns button)
        {
            return IsPadStateEquals(currentState, button, ButtonState.Pressed);
        }

        /// <summary>
        /// A check to see if a button on the game pad has been pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>Th result of the check.</returns>
        public bool IsPadReleased(ControlPadBtns button)
        {
            return IsPadStateEquals(currentState, button, ButtonState.Released);
        }

        /// <summary>
        /// A check to see if a button on the game pad has been pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns>Th result of the check.</returns>
        public bool IsPadStroked(ControlPadBtns button)
        {
            switch (button)
            {
                case ControlPadBtns.LeftThumbStickUp:
                    return IsThumbStickUpStroked(TriggersIdx.Left);

                case ControlPadBtns.LeftThumbStickDown:
                    return IsThumbStickDownStroked(TriggersIdx.Left);

                case ControlPadBtns.LeftThumbStickLeft:
                    return IsThumbStickLeftStroked(TriggersIdx.Left);

                case ControlPadBtns.LeftThumbStickRight:
                    return IsThumbStickRightStroked(TriggersIdx.Left);

                case ControlPadBtns.RightThumbStickUp:
                    return IsThumbStickUpStroked(TriggersIdx.Right);

                case ControlPadBtns.RightThumbStickDown:
                    return IsThumbStickDownStroked(TriggersIdx.Right);

                case ControlPadBtns.RightThumbStickLeft:
                    return IsThumbStickLeftStroked(TriggersIdx.Right);

                case ControlPadBtns.RightThumbStickRight:
                    return IsThumbStickRightStroked(TriggersIdx.Right);

                case ControlPadBtns.LeftTrigger:
                    return IsTriggerStroked(TriggersIdx.Left);

                case ControlPadBtns.RightTrigger:
                    return IsTriggerStroked(TriggersIdx.Right);
            };

            return (!IsPadStateEquals(prevState, button, ButtonState.Pressed) &&
                        IsPadPressed(button));
        }
        public bool IsPadTriggersStroked(TriggersIdx index)
        {
            return (Triggers[(int)index] > 0.0f && prevTriggers[(int)index] <= 0.0f);
        }

        #endregion
        #region Helpers

        public bool IsThumbStickUpStroked(TriggersIdx index)
        {
            return (ThumbStick[(int)index].Y > controllerSensitivity &&
                    prevThumbStick[(int)index].Y <= controllerSensitivity);
        }
        public bool IsThumbStickDownStroked(TriggersIdx index)
        {
            return (ThumbStick[(int)index].Y < -controllerSensitivity &&
                    prevThumbStick[(int)index].Y >= -controllerSensitivity);
        }
        public bool IsThumbStickLeftStroked(TriggersIdx index)
        {
            return (ThumbStick[(int)index].X < -controllerSensitivity &&
                    prevThumbStick[(int)index].X >= -controllerSensitivity);
        }
        public bool IsThumbStickRightStroked(TriggersIdx index)
        {
            return (ThumbStick[(int)index].X > controllerSensitivity &&
                    prevThumbStick[(int)index].X <= controllerSensitivity);
        }
        public bool IsTriggerStroked(TriggersIdx index)
        {
            return (Triggers[(int)index] > controllerSensitivity &&
                    prevTriggers[(int)index] <= controllerSensitivity);
        }

        public float GetPadTriggerValue(TriggersIdx index)
        {
            return Triggers[(int)index];
        }
        public Vector2 GetThumbStickAmount(TriggersIdx index)
        {
            return ThumbStick[(int)index];
        }

        public override void Reset()
        {
            currentState = prevState = emptyState;

            if (vibrate)
            {
                vibrationDurationAccTime = TimeSpan.Zero;
                vibrationDuration = TimeSpan.Zero;

                Microsoft.Xna.Framework.Input.GamePad.SetVibration((Microsoft.Xna.Framework.PlayerIndex)index, 0.0f, 0.0f);
            }
        }

        public void ToggleVibrate()
        {
            if (vibrate)
                vibrate = false;
            else
                vibrate = true;
        }
        public void SetVibration(float leftMotor, float rightMotor, float duration)
        {
            if (IsPadConnected)
                if (VibrateEnabled)
                {
                    leftMotor = EonMathHelper.Clamp(leftMotor, 0.0f, 1.0f);
                    rightMotor = EonMathHelper.Clamp(rightMotor, 0.0f, 1.0f);

                    vibrationDuration = TimeSpan.FromMilliseconds(duration);
                    vibrationIntencity[(int)TriggersIdx.Left] = leftMotor;
                    vibrationIntencity[(int)TriggersIdx.Right] = rightMotor;
                }
        }
        public void ChangeControllerSensitivity(float amount)
        {
            if (controllerSensitivity != amount)
                controllerSensitivity = EonMathHelper.Clamp(controllerSensitivity, 0.1f, 0.9f);
        }

        #endregion
    }
}
