/* Created: 16/09/2013
 * Last Updated: 26/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Used to define a helper for text rendering.
    /// </summary>
    public static class TextureHelper
    {
        /// <summary>
        /// Creates a Texture2D of a specific colour.
        /// </summary>
        /// <param name="colour">The colour of the new Texture2D.</param>
        /// <param name="size">The size of the new Texture2D.</param>
        /// <returns>The created Texture2D.</returns>
        public static Texture2D CreateTexture(Color colour, Point size)
        {
            Texture2D tex = new Texture2D(Common.Device, size.X, size.Y);

            Color[] data = new Color[size.X * size.Y];

            for (int i = 0; i < data.Length; i++)
                data[i] = colour;

            tex.SetData<Color>(data);

            return tex;
        }

        /// <summary>
        /// Gets a set of points in a rendered piece of text.
        /// </summary>
        /// <param name="font">The font that is to be used.</param>
        /// <param name="text">The text to be analised.</param>
        /// <param name="scale">The scale of the text.</param>
        /// <param name="density">How dense the points are. A lower value generates more points.</param>
        /// <param name="offset">The positional offset for the generated points.</param>
        /// <returns>The text as a set of points.</returns>
        public static List<Vector2> GenerateTextPoints(SpriteFont font,
            string text, float scale, int density, Vector2 offset)
        {
            List<Vector2> points = new List<Vector2>();

            Vector2 m = font.MeasureString(text);
            Point size = new Point((int)m.X, (int)m.Y);

            RenderTarget2D target = new RenderTarget2D(Common.Device, size.X, size.Y);

            Common.Device.SetRenderTarget(target);
            Common.Device.Clear(Color.TransparentBlack);

            Common.Batch.Begin();
            Common.Batch.DrawString(font, text, Vector2.Zero, Color.White);
            Common.Batch.End();

            Common.Device.SetRenderTarget(null);

            Color[] data = new Color[size.X * size.Y];
            target.GetData<Color>(data);

            for (int x = 0; x < size.X; x += density)
                for (int y = 0; y < size.Y; y += density)
                {
                    int x2 = EonMathsHelper.Clamp(x + RandomHelper.GetRandom(-density / 2, density / 2), 0, size.X);
                    int y2 = EonMathsHelper.Clamp(y + RandomHelper.GetRandom(-density / 2, density / 2), 0, size.Y);

                    if (data[size.X * y2 + x2].R != 0)
                        points.Add(offset + ((new Vector2(x2, y2) - m / 2) * scale));
                }

            target.Dispose();

            return points;
        }
    }
}
