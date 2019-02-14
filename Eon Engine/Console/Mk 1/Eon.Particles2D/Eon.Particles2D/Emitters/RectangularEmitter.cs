/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Particles2D.Emitters
{
    /// <summary>
    /// Used to spawn Particles inside of a rectangle.
    /// </summary>
    public class RectangularEmitter : IEmitterType
    {
        Vector2 pos;
        Rectangle rect;

        /// <summary>
        /// The center position of the rectangle
        /// used to generate the spawn points.
        /// </summary>
        public Vector2 Position
        {
            get { return pos; }
            set
            {
                pos = value;

                SetRectangle();
            }
        }

        /// <summary>
        /// Creates a new RectangularEmitter.
        /// </summary>
        /// <param name="rectangle">The rectangle to use 
        /// to create spawn points.</param>
        public RectangularEmitter(Rectangle rectangle)
        {
            rect = rectangle;

            pos = new Vector2(rect.Center.X, rect.Center.Y);
        }

        void SetRectangle()
        {
            rect.X = (int)pos.X - (rect.Width / 2);
            rect.Y = (int)pos.Y + (rect.Height / 2);
        }

        /// <summary>
        /// Creates a spawn point from inside of a rectangle.
        /// </summary>
        /// <returns>The generated spawn point.</returns>
        public Vector2 CreateEmittionPoint()
        {
            return RandomHelper.GetRandomVector2(
                new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Bottom));
        }

        public void ScreenResolutionChanged()
        {
            pos = Common.ReCalibrateScreenSpaceVector(pos);

            SetRectangle();
        }
    }
}
