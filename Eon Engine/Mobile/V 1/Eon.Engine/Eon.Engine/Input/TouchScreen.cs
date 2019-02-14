
/* Created 05/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Double A Game Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Eon.Engine.Input
{
    /// <summary>
    /// The different types of touch inputs.
    /// </summary>
    public enum TouchType
    {
        None,

        /// <summary>
        /// Defines a Tap action.
        /// </summary>
        Tap,

        /// <summary>
        /// Defines a Drag action.
        /// </summary>
        Drag,

        /// <summary>
        /// Defines a Double Tap action.
        /// </summary>
        DoubleTap,

        /// <summary>
        /// Defines if the player is hold a single point.
        /// </summary>
        Hold,
         
        /// <summary>
        /// Determines a flick motion was preformed.
        /// </summary>
        Flick
    }

    /// <summary>
    /// Used to define the touch pad of a windows phone.
    /// </summary>
    public sealed class TouchScreen : BaseInputItem
    {
        static Vector2 touchLoc;
        static Vector2 touchLoc2;
        static TouchType touchType = Input.TouchType.None;

        Vector2 prevDelta;
        Vector2 prevDelta2;

        /// <summary>
        /// The location of where the touch pad was pressed.
        /// </summary>
        public Vector2 TouchLoc { get { return touchLoc; } }

        /// <summary>
        /// The second loacation where the touch pad was pressed.
        /// </summary>
        public Vector2 TouchLoc2 { get { return touchLoc2; } }

        /// <summary>
        /// The touch delta.
        /// </summary>
        public Vector2 Delta { get; private set; }

        /// <summary>
        /// The second touch delta.
        /// </summary>
        public Vector2 Delta2 { get; private set; }

        /// <summary>
        /// The direction of the touch gester.
        /// </summary>
        public Vector2 Direction { get; private set; }

        /// <summary>
        /// The second direction of the touch gester.
        /// </summary>
        public Vector2 Direction2 { get; private set; }

        /// <summary>
        /// The type of touch that had happened.
        /// </summary>
        public TouchType TouchType { get { return touchType; } }

        public TouchScreen()
            : base("TouchScreen")
        {
            TouchPanel.EnabledGestures = GestureType.None |
                GestureType.DoubleTap | GestureType.Tap |
                GestureType.Hold | GestureType.FreeDrag| GestureType.Flick;
        }

        protected override void Update()
        {
            touchType = TouchType.None;
            prevDelta = Delta;
            Delta = Delta2 = Vector2.Zero;

            prevDelta2 = Delta2;

            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gester = TouchPanel.ReadGesture();

                switch (gester.GestureType)
                {
                    case GestureType.Hold:
                        touchType = Input.TouchType.Hold;
                        break;

                    case GestureType.FreeDrag:
                        touchType = Input.TouchType.Drag;
                        break;

                    case GestureType.Flick:
                        touchType = Input.TouchType.Flick;
                        break;

                    case GestureType.Tap:
                        touchType = Input.TouchType.Tap;
                        break;

                    case GestureType.DoubleTap:
                        touchType = Input.TouchType.DoubleTap;
                        break;

                    default:
                        touchType = Input.TouchType.None;
                        break;
                }

                Delta = gester.Delta;
                touchLoc = gester.Position;
                Direction = gester.Delta - prevDelta;
                Direction.Normalize();

                Delta2 = gester.Delta2;
                touchLoc2 = gester.Position2;
                Direction2 = gester.Delta2 - prevDelta2;
                Direction2.Normalize();

                base.Update();
            }
        }
    }
}
