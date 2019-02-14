/* Created 23/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Used to define a sprite that is rendered
    /// in part by using a source rectangle.
    /// </summary>
    public sealed class SourceSprite : Sprite
    {
        #region Fields

        Rectangle sourceRect;

        #endregion
        #region Ctors

        /// <summary>
        /// Creates a new SourceSprite.
        /// </summary>
        /// <param name="id">The id of the SourceSprite.</param>
        /// <param name="drawLayer">The layer to draw the SourceSprite on.</param>
        /// <param name="textureFilepath">The filepath of SourceSprite's texture.</param>
        /// <param name="colour">The colour to draw the SourceSprite.</param>
        /// <param name="sourceRectangle">The source rectangle of the SourceSprite.</param>
        /// <param name="postRender">Should the SourceSprire be post rendered.</param>
        public SourceSprite(string id, int drawLayer, string textureFilepath,
            Color colour, Rectangle sourceRectangle, bool postRender)
            : base(id, drawLayer, textureFilepath, colour, postRender)
        {
            sourceRect = sourceRectangle;
        }

        /// <summary>
        /// Creates a new SourceSprite.
        /// </summary>
        /// <param name="id">The id of the SourceSprite.</param>
        /// <param name="drawLayer">The layer to draw the SourceSprite on.</param>
        /// <param name="textureFilepath">The filepath of SourceSprite's texture.</param>
        /// <param name="colour">The colour to draw the SourceSprite.</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="offset">The positional offset of the SourceSprite.</param>
        /// <param name="scale">The scale of the SourceSprite.</param>
        /// <param name="sourceRectangle">The source rectangle of the SourceSprite.</param>
        public SourceSprite(string id, int drawLayer, string textureFilepath,
            Color colour, bool postRender, Vector2 offset, Vector2 scale,
            Rectangle sourceRectangle)
            : base(id, drawLayer, textureFilepath, colour,
            postRender, offset, scale, 0, SpriteEffects.None)
        {
            sourceRect = sourceRectangle;
        }

        /// <summary>
        /// Creates a new SourceSprite.
        /// </summary>
        /// <param name="id">The id of the SourceSprite.</param>
        /// <param name="bounds">The bounds of the SourceSprite.</param>
        /// <param name="textureFilepath">The filepath of SourceSprite's texture.</param>
        /// <param name="colour">The colour to draw the SourceSprite.</param>
        /// <param name="drawLayer">The layer to draw the SourceSprite on.</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="sourceRectangle">The source rectangle of the SourceSprite.</param>
        public SourceSprite(string id, Rectangle bounds, string textureFilepath,
            Color colour, int drawLayer, bool postRender, Rectangle sourceRectangle)
            : base(id, bounds, textureFilepath, colour, drawLayer, postRender)
        {
            sourceRect = sourceRectangle;
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
                    rect.Width = sourceRect.Width;
                    rect.Height = sourceRect.Height;
                }

                rect.Width = sourceRect.Width;
                Bounds = rect;

                if (Rotation > 0)
                    Common.Batch.Draw(Texture, Bounds, sourceRect, Colour, Rotation,
                        Vector2.Zero, SpriteEffect, 0);
                else
                    Common.Batch.Draw(Texture, rect, sourceRect, Colour, 0, Vector2.Zero, SpriteEffect, 0);
            }
        }

        #endregion
        #region  Helpers

        /// <summary>
        /// Changes the source rectangle to a different one.
        /// </summary>
        /// <param name="newSourceRectangle">The new source rectangle.</param>
        public void ChangeSourceRectangle(Rectangle newSourceRectangle)
        {
            sourceRect = newSourceRectangle;
        }

        /// <summary>
        /// Changes the source rectangle width and height.
        /// </summary>
        /// <param name="movement">The new width and height of the source rectangle.</param>
        public void ChangeSourceRectangle(Vector2 movement)
        {
            sourceRect.Width = (int)movement.X;
            sourceRect.Height = (int)movement.Y;
        }

        /// <summary>
        /// Changes the source rectangle by moving the end of it.
        /// </summary>
        /// <param name="movement">The amount to move the source rect by.</param>
        public void MoveSourceRectangle(Vector2 movement)
        {
            sourceRect.Width += (int)movement.X;
            sourceRect.Height += (int)movement.Y;
        }

        #endregion
    }
}
