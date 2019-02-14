/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Particles2D.Renders
{
    /// <summary>
    /// Defines a simple animiated sprite.
    /// </summary>
    public struct AnimatedFrame
    {
        int columns;
        int rows;
        int totalFrames;

        int width;
        int height;

        TimeSpan currentTime;
        TimeSpan frameRate;

        public Rectangle SourceRect;
        public Vector2 Origin;

        public int Column;
        public int Row;

        public AnimatedFrame(int columns, int rows,
            int totalFrames, float frameRate, int width, int height)
        {
            this.columns = columns;
            this.rows = rows;

            this.totalFrames = totalFrames;
            this.frameRate = TimeSpan.FromSeconds(frameRate);

            currentTime = TimeSpan.Zero;

            Column = RandomHelper.GetRandomInt(0, columns);
            Row = RandomHelper.GetRandomInt(0, rows);

            this.width = width;
            this.height = height;

            SourceRect = new Rectangle(Column * width, Row * height, width, height);
            Origin = new Vector2(width / 2, height / 2);
        }

        public void Update()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= frameRate)
            {
                Column++;

                if (Column > columns)
                {
                    Column = 0;
                    Row++;

                    if (Row > rows)
                        Row = 0;
                }

                currentTime = TimeSpan.Zero;
                SourceRect = new Rectangle(Column * width, Row * height, width, height);
            }
        }
    }
}
