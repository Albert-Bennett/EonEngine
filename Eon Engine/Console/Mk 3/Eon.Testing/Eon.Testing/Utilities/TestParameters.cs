/* Created: 08/09/2014
 * Last Updated: 02/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Resolution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Testing.Utilities
{
    /// <summary>
    /// Used to define an object that displys various test parameters 
    /// including screen resolution and texture quality.
    /// </summary>
    internal sealed class TestParameters : Utility
    {
        Vector2 position = new Vector2(15, 45);
        float scale = 1;

        public override void Draw()
        {
            Vector2 pos = position;

            Common.Batch.DrawString(ErrorConsole.Font, "Screen Size: Width = " +
                Common.DeviceManager.PreferredBackBufferWidth + ", Height = " +
                Common.DeviceManager.PreferredBackBufferHeight + ".", pos, Color.White,
                0, Vector2.Zero, scale, SpriteEffects.None, 0);

            pos.Y += 15;

            Common.Batch.DrawString(ErrorConsole.Font, "Screen Resolution: Width = " +
               Common.Device.Viewport.Width + ", Height = " +
               Common.Device.Viewport.Height + ".", pos, Color.White,
                0, Vector2.Zero, scale, SpriteEffects.None, 0);

            pos.Y += 15;

            Common.Batch.DrawString(ErrorConsole.Font, "Texture Quality: Width = " +
                Common.TextureQuality.X + ", Height = " +
                Common.TextureQuality.Y + ".", pos, Color.White,
                0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
