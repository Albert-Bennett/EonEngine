﻿/* Created: 08/09/2014
 * Last Updated: 29/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Testing.Utilities
{
    /// <summary>
    /// Used to define a counter for the frame rate of the game.
    /// </summary>
    internal sealed class FPSCounter : Utility
    {
        Vector2 position = new Vector2(15, 15);
        float scale = 1.0f;

        public override void Draw()
        {
            Vector2 pos = position;

            Common.Batch.DrawString(ErrorConsole.Font, "FPS: " +
                (int)(1 / Common.ElapsedTimeDelta.TotalSeconds),
                pos, Color.White);

            pos.Y += 15;

            Common.Batch.DrawString(ErrorConsole.Font, "Total Game Time: " +
                (int)Common.TotalGameTime.TotalSeconds, pos, Color.White,
                0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
