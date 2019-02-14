/* Created: 25/07/2014
 * Last Updated: 24/02/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering3D.Framework.Billboards
{
    /// <summary>
    /// Used to define a Billboard that has an animated texture.
    /// </summary>
    public sealed class AnimatedBillboard : Billboard
    {
        #region Fields

        int rows;
        int columns;
        int totalFrames;

        int currentFrame = 0;
        int currentRow = 0;
        int currentCol = 0;

        TimeSpan timeBetweenFrames;
        TimeSpan currentTime = TimeSpan.Zero;

        public int Priority
        {
            get { return 0; }
        }

        #endregion
        #region Ctor

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
        }

        public override void Initialize()
        {
            effectFilepath = "Eon/Shaders/Materials/AnimatedBillboard";

            base.Initialize();

            Effect.Parameters["Width"].SetValue(1f / (float)columns);
            Effect.Parameters["Height"].SetValue(1f / (float)rows);

            Effect.Parameters["Cols"].SetValue(columns);
            Effect.Parameters["Rows"].SetValue(rows);
        }

        public override void ReInitialize(Vector3 position, float scale, Vector3 rotation)
        {
            currentRow = 0;
            currentCol = 0;

            currentTime = TimeSpan.FromMilliseconds(0);

            base.ReInitialize(position, scale, rotation);
        }

        #endregion
        #region Animating

        public void StartRandom()
        {
            currentCol = RandomHelper.GetRandom(0, columns);
            currentRow = RandomHelper.GetRandom(0, rows);

            while (rows * columns > totalFrames)
                rows = RandomHelper.GetRandom(0, rows);

            currentFrame = rows * columns;
        }

        public void Update()
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

        protected override void Render(string technique)
        {
            Effect.Parameters["Row"].SetValue(currentRow);
            Effect.Parameters["Col"].SetValue(currentCol);

            Effect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);

            base.Render(technique);
        }

        #endregion
    }
}
