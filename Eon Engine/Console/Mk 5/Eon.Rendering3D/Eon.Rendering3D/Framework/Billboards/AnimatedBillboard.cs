/* Created: 25/07/2014
 * Last Updated: 29/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Framework.Billboards
{
    /// <summary>
    /// Used to define a Billboard that has an animated texture.
    /// </summary>
    public sealed class AnimatedBillboard : Billboard
    {
        int rows;
        int columns;
        int totalFrames;

        int currentFrame = 0;
        int currentRow = 0;
        int currentCol = 0;

        float width;
        float height;

        bool started;

        TimeSpan timeBetweenFrames;
        TimeSpan currentTime = TimeSpan.Zero;

        /// <summary>
        /// The width of each frame.
        /// </summary>
        public float Width { get { return width; } }

        /// <summary>
        /// The height of each frame.
        /// </summary>
        public float Height { get { return height; } }

        /// <summary>
        /// Current row.
        /// </summary>
        public int CurrentRow { get { return currentRow; } }

        /// <summary>
        /// Current column.
        /// </summary>
        public int CurrentColumn { get { return currentCol; } }

        /// <summary>
        /// Total amount of rows in the AnimatedBillboard.
        /// </summary>
        public int Rows { get { return rows; } }

        /// <summary>
        /// Total amount of columns in the AnimatedBillboard.
        /// </summary>
        public int Columns { get { return columns; } }

        /// <summary>
        /// Creates a new AnimatedBillboard.
        /// </summary>
        /// <param name="position">The position of the AnimatedBillboard.</param>
        /// <param name="scale">The scale of the AnimatedBillboard.</param>
        /// <param name="rotation">The rotation of the AnimatedBillboard.</param>
        /// <param name="textureFilepath">The filepath for the AnimatedBillboard's texture.</param>
        /// <param name="rows">The number of rows in texture.</param>
        /// <param name="columns">The number of columns in the texture.</param>
        /// <param name="totalFrames">Total frames in the animation.</param>
        /// <param name="timeBetweenFrames">Amount of time (milliseconds) between frames.</param>
        public AnimatedBillboard(Vector3 position, float scale, Vector3 rotation,
            string textureFilepath, int rows, int columns, int totalFrames, float timeBetweenFrames)
            : base(position, scale, rotation, textureFilepath)
        {
            this.rows = rows;
            this.columns = columns;
            this.timeBetweenFrames = TimeSpan.FromMilliseconds(timeBetweenFrames);
            this.totalFrames = totalFrames;

            currentTime = this.timeBetweenFrames;

            width = this.Texture.Width / columns;
            height = this.Texture.Height / rows;
        }

        /// <summary>
        /// Starts the animation at a random frame.
        /// </summary>
        public void Start()
        {
            currentCol = RandomHelper.GetRandom(0, columns);
            currentRow = RandomHelper.GetRandom(0, rows);

            while (rows * columns > totalFrames)
                rows = RandomHelper.GetRandom(0, rows);

            currentFrame = rows * columns;

            started = true;
        }

        public void Update()
        {
            if (started)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime >= timeBetweenFrames)
                {
                    currentTime = TimeSpan.Zero;

                    currentCol++;

                    currentFrame++;

                    if (currentCol >= columns)
                    {
                        currentCol = 0;
                        currentRow++;

                        currentFrame++;

                        if (currentFrame >= totalFrames)
                        {
                            currentCol = 0;
                            currentRow = 0;
                            currentFrame = 0;
                        }
                        else if (currentRow >= rows)
                            currentRow = 0;
                    }
                }
            }
        }
    }
}
