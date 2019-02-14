/* Created 12/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Used to define a special type of Sprite 
    /// that is used to render a frame of an image.
    /// </summary>
    public sealed class FrameSprite : Sprite
    {
        #region Variables

        int movementX;
        int movementY;

        int maxRows;
        int maxColumns;

        int currentRow = 0;
        int currentColumn = 0;

        Rectangle sourceRect;

        #endregion
        #region Ctors

        /// <summary>
        /// Creates a new FrameSprite.
        /// </summary>
        /// <param name="id">The id of the FrameSprite.</param>
        /// <param name="drawLayer">The layer to draw the FrameSprite on.</param>
        /// <param name="textureFilepath">The filepath of FrameSprite's texture.</param>
        /// <param name="colour">The colour to draw the FrameSprite.</param>
        /// <param name="maxRows">The maximum amount of rows in the image.</param>
        /// <param name="maxCols">The maximum amount of columns in the image.</param>
        public FrameSprite(string id, int drawLayer, string textureFilepath,
            Color colour, int maxRows, int maxCols)
            : base(id, drawLayer, textureFilepath, colour, false, Vector2.Zero, Vector2.One, 0, SpriteEffects.None)
        {
            this.maxRows = maxRows;
            this.maxColumns = maxCols;
        }

        /// <summary>
        /// Creates a new FrameSprite.
        /// </summary>
        /// <param name="id">The id of the FrameSprite.</param>
        /// <param name="drawLayer">The layer to draw the FrameSprite on.</param>
        /// <param name="textureFilepath">The filepath of FrameSprite's texture.</param>
        /// <param name="colour">The colour to draw the FrameSprite.</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="offset">The positional offset of the FrameSprite.</param>
        /// <param name="scale">The scale of the FrameSprite.</param>
        /// <param name="maxRows">The maximum amount of rows in the image.</param>
        /// <param name="maxCols">The maximum amount of columns in the image.</param>
        public FrameSprite(string id, int drawLayer, string textureFilepath,
            Vector2 offset, Vector2 scale, Color colour,
            bool postRender, int maxRows, int maxCols)
            : base(id, drawLayer, textureFilepath, colour, postRender, offset, scale, 0, SpriteEffects.None)
        {
            this.maxRows = maxRows;
            this.maxColumns = maxCols;
        }

        #endregion
        #region Init

        protected override void Initialize()
        {
            base.Initialize();

            movementX = Texture.Width / maxColumns;
            movementY = Texture.Height / maxRows;

            sourceRect = new Rectangle(0, 0, movementX, movementY);
        }

        /// <summary>
        /// Moves the current frame to the next one.
        /// </summary>
        public void MoveFrame()
        {
            int tr = currentRow;

            if (tr++ >= maxRows)
            {
                currentRow = 0;

                int tc = currentColumn;

                if (tc++ >= maxColumns)
                    currentColumn = 0;
                else
                    currentColumn++;
            }
            else
                currentRow++;

            UpdateSourceRectangle();
        }

        /// <summary>
        /// Moves the current frame to a specific one.
        /// </summary>
        /// <param name="frameNumber">The frame number to move to.</param>
        public void MoveFrame(int frameNumber)
        {
            if (frameNumber > maxColumns * maxRows)
                frameNumber = maxColumns * maxRows;

            int col = 0;
            int row = 0;

            while (frameNumber > 0)
            {
                int temp = frameNumber;

                if (temp - maxRows < 0)
                    row = frameNumber;
                else
                    col++;

                frameNumber = temp;
            }

            currentColumn = col;
            currentRow = row;

            UpdateSourceRectangle();
        }

        void UpdateSourceRectangle()
        {
            sourceRect.X = currentRow * movementX;
            sourceRect.Y = currentColumn * movementY;
        }

        #endregion
        #region Rendering

        public override void Draw()
        {
            if (!Destroyed && Initialized)
            {
                Rectangle rect = new Rectangle((int)Offset.X, (int)Offset.Y, (int)Scale.X, (int)Scale.Y);

                if (Owner != null)
                {
                    Vector3 s = Vector3.One;
                    Helpers.EonMathHelper.GetMatrixScale(Owner.World, out s);

                    rect.X += (int)Owner.World.Translation.X;
                    rect.Y += (int)Owner.World.Translation.Y;
                    rect.Width *= (int)s.X;
                    rect.Height *= (int)s.Y;
                }

                Bounds = rect;

                if (Rotation > 0)
                    Common.Batch.Draw(Texture, Bounds, sourceRect, Colour, Rotation,
                        new Vector2(sourceRect.Center.X, sourceRect.Center.Y), SpriteEffect, 0);
                else
                    Common.Batch.Draw(Texture, rect, sourceRect, Colour, 0, Vector2.Zero, SpriteEffect, 0);
            }
        }

        #endregion
    }
}
