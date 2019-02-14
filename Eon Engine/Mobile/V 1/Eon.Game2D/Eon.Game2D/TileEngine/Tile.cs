/* Created 15/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Defines a Tile in a TileLayer.
    /// </summary>
    public struct Tile
    {
        Rectangle sourceRect;
        Rectangle bounds;

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public Tile(Rectangle bounds, Rectangle source)
        {
            this.bounds = bounds;
            this.sourceRect = source;
        }

        internal void ScreenResolutionChanged()
        {
            bounds.X = (int)Common.ReCalibrateScale(bounds.X);
            bounds.Y = (int)Common.ReCalibrateScale(bounds.Y);
            bounds.Width = (int)Common.ReCalibrateScale(bounds.Width);
            bounds.Height = (int)Common.ReCalibrateScale(bounds.Height);
        }
    }
}
