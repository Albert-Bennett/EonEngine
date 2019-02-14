/* Created: 05/03/2014
 * Last Updated: 14/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Eon.Engine.Input
{
    /// <summary>
    /// Used to define the touch pad of a windows phone.
    /// </summary>
    public sealed class TouchPad : BaseInputItem
    {
        Vector2 touchLoc;
        Vector2 touchLoc2;
        TouchType touchType = TouchType.None;

        Vector2 prevDelta;
        Vector2 prevDelta2;

        Vector2 direction;
        Vector2 direction2;

        Vector2 delta;
        Vector2 delta2;

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
        public Vector2 Delta { get { return delta; } }

        /// <summary>
        /// The second touch delta.
        /// </summary>
        public Vector2 Delta2 { get { return delta2; } }

        /// <summary>
        /// The direction of the touch gester.
        /// </summary>
        public Vector2 Direction { get { return direction; } }

        /// <summary>
        /// The second direction of the touch gester.
        /// </summary>
        public Vector2 Direction2 { get { return direction2; } }

        /// <summary>
        /// The type of touch that had happened.
        /// </summary>
        public TouchType TouchType { get { return touchType; } }

        public TouchPad()
            : base("TouchPad")
        {
            TouchPanel.EnabledGestures = GestureType.None |
                GestureType.DoubleTap | GestureType.Tap |
                GestureType.Hold | GestureType.FreeDrag| GestureType.Flick;
        }

        protected override void Update()
        {
            touchType = TouchType.None;
            prevDelta = delta;
            delta = delta2 = Vector2.Zero;

            prevDelta2 = Delta2;

            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gester = TouchPanel.ReadGesture();

                switch (gester.GestureType)
                {
                    case GestureType.Hold:
                        touchType = TouchType.Hold;
                        break;

                    case GestureType.FreeDrag:
                        touchType = TouchType.Drag;
                        break;

                    case GestureType.Flick:
                        touchType = TouchType.Flick;
                        break;

                    case GestureType.Tap:
                        touchType = TouchType.Tap;
                        break;

                    case GestureType.DoubleTap:
                        touchType = TouchType.DoubleTap;
                        break;

                    default:
                        touchType = TouchType.None;
                        break;
                }

                delta = gester.Delta;
                touchLoc = gester.Position;
                direction = gester.Delta - prevDelta;
                direction.Normalize();

                delta2 = gester.Delta2;
                touchLoc2 = gester.Position2;
                direction2 = gester.Delta2 - prevDelta2;
                direction2.Normalize();

                base.Update();
            }
        }
    }
}
