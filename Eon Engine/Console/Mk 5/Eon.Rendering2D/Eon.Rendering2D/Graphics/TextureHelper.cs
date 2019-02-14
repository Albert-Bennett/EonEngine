/* Created: 16/09/2013
 * Last Updated: 13/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering2D.Graphics
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
            Vector2 size = font.MeasureString(text);

            RenderTarget2D target = new RenderTarget2D(Common.Device, (int)size.X, (int)size.Y, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            Common.Device.SetRenderTarget(target);
            Common.Device.Clear(Color.TransparentBlack);

            Common.Batch.Begin();
            Common.Batch.DrawString(font, text, Vector2.Zero, Color.White);
            Common.Batch.End();

            Common.Device.SetRenderTarget(null);

            return GeneratePoints(scale, density, offset, target);
        }

        /// <summary>
        /// Generates a list of vector2 defining various points in an image.
        /// </summary>
        /// <param name="scale">The scale of the image.</param>
        /// <param name="density">The density of the points.</param>
        /// <param name="offset">Positional offset.</param>
        /// <param name="texture">The texture to generate the points from.</param>
        /// <returns>A list of points defining the given image.</returns>
        public static List<Vector2> GeneratePoints(float scale, int density,
            Vector2 offset, Texture2D texture)
        {
            Point m = texture.Bounds.Size;
            Vector2 size = m.ToVector2();

            List<Vector2> points = new List<Vector2>();

            Color[] data = new Color[m.X * m.Y];
            texture.GetData<Color>(data);

            for (int x = 0; x < m.X; x += density)
                for (int y = 0; y < m.Y; y += density)
                {
                    int x2 = EonMathsHelper.Clamp(x + RandomHelper.GetRandom(-density / 2, density / 2), 0, m.X);
                    int y2 = EonMathsHelper.Clamp(y + RandomHelper.GetRandom(-density / 2, density / 2), 0, m.Y);

                    if (data[m.X * y2 + x2].R != 0)
                        points.Add(offset + ((new Vector2(x2, y2) - size / 2) * scale));
                }

            if (texture is RenderTarget2D)
                texture.Dispose();

            return points;
        }
    }
}
