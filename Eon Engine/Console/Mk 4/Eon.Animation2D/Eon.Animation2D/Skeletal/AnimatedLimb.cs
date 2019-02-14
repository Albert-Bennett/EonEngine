/* Created 13/05/2015
 * Last Updated: 10/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Maths.Helpers;
using Eon.Rendering2D.Drawing;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Defines an Limb that has a SpriteSheet animation.
    /// </summary>
    public sealed class AnimatedLimb : Limb
    {
        TimeSpan currentTime;
        TimeSpan frameRate;

        int rows;
        int cols;
        int totalFrames;

        int currentCol = 0;
        int currentRow = 0;
        int currentFrame = 0;

        int yMovement;
        int xMovement;

        Rectangle sourceRect;

        /// <summary>
        /// Creates a new AnimatedLimb.
        /// </summary>
        /// <param name="info">The information for the AnimatedLimb.</param>
        /// <param name="postRender">Post render the Limb.</param>
        public AnimatedLimb(LimbInfo info, bool postRender)
            : base(info, postRender)
        {
            frameRate = TimeSpan.FromMilliseconds(info.AnimateData.FrameRate);
            totalFrames = info.AnimateData.TotalFrames;
            rows = info.AnimateData.Rows;
            cols = info.AnimateData.Columns;

            if (info.AnimateData.RandomStart)
            {
                currentRow = RandomHelper.GetRandom(0, rows);

                int c = (totalFrames - (currentRow * cols)) % cols;

                currentCol = RandomHelper.GetRandom(0, c);
            }

            xMovement = Texture.Width / cols;
            yMovement = Texture.Height / rows;

            sourceRect = new Rectangle()
            {
                X = currentRow * xMovement,
                Y = currentCol * yMovement,
                Width = xMovement,
                Height = yMovement
            };
        }

        internal override void Update(Maths.Transformation parentWorld)
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= frameRate)
            {
                currentTime -= frameRate;

                currentFrame++;
                currentCol++;

                if (currentFrame >= totalFrames)
                {
                    currentRow = 0;
                    currentCol = 0;
                    currentFrame = 0;
                }
                else if (currentCol >= cols)
                {
                    currentCol = 0;
                    currentRow++;
                }

                sourceRect.X = currentCol * xMovement;
                sourceRect.Y = currentRow * yMovement;
            }

            base.Update(parentWorld);
        }

        protected override void _Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    Common.Batch.Draw(Texture, Bounds, sourceRect, Colour,
                        MathHelper.ToRadians(Transform.Rotation), RotationalPoint, SpriteEffects.None, 0);
                    break;

                case DrawingStage.Normal:
                    Common.Batch.Draw(NormalMap, Bounds, sourceRect, Color.White,
                       MathHelper.ToRadians(Transform.Rotation), RotationalPoint, SpriteEffects.None, 0);
                    break;

                case DrawingStage.Distortion:
                    Common.Batch.Draw(DistortionMap, Bounds, sourceRect, Color.White,
                        MathHelper.ToRadians(Transform.Rotation), RotationalPoint, SpriteEffects.None, 0);
                    break;
            }
        }

        /// <summary>
        /// Changes the animation.
        /// </summary>
        /// <param name="spriteSheet">The SpriteSheet to be used.</param>
        public void ChangeAnimation(string spriteSheet)
        {
            try
            {
                SpriteSheet.SpriteSheet sheet =
                    SerializationHelper.Deserialize<SpriteSheet.SpriteSheet>(spriteSheet, true, ".SPR");

                sheet.Initalize();

                cols = sheet.Columns;
                rows = sheet.Rows;
                totalFrames = sheet.TotalFrames;

                NormalMap = sheet.NormalMap;
                Texture = sheet.Texture;
                DistortionMap = sheet.DistortionMap;

                sourceRect = sheet.SourceRectangle;
                xMovement = sheet.FrameWidth;
                yMovement = sheet.FrameHeight;

                frameRate = sheet.FrameRate;

                currentTime = TimeSpan.Zero;

                currentFrame = 0;
                currentCol = 0;
                currentRow = 0;
            }
            catch
            {
                new Error("Unable to load SpriteSheet: " +
                    spriteSheet, Seriousness.Error);
            }
        }
    }
}
