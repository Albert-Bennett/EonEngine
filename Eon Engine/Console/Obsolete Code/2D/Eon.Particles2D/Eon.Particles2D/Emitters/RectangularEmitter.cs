/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;

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
        /// <param name="rectangle">Area to spawn particles inside of.</param>
        public RectangularEmitter(int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);

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
            return RandomHelper.GetRandom(
                new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Bottom));
        }
    }
}
