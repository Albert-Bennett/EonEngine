/* Created 25/07/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

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

        public override void Render()
        {
            Effect.Parameters["Row"].SetValue(currentRow);
            Effect.Parameters["Col"].SetValue(currentCol);

            base.Render();
        }

        #endregion
    }
}
