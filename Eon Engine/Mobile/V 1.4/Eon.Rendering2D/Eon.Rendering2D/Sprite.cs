﻿/* Created 16/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Used to define a versatile object that is used 
    /// to render a/set of 2D image(s) in order to 
    /// achieve a particular effect.
    /// </summary>
    public class Sprite : ObjectComponent, IDrawItem, IDispose
    {
        #region Graphicial Variables

        Texture2D texture;

        Color colour;

        /// <summary>
        /// The colour that the Sprite will be rendered with.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// The texture of the Sprite.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        #endregion
        #region Calculous Variables

        Rectangle rect;
        SpriteEffects spriteEffect = SpriteEffects.None;

        float rotation = 0;

        Vector2 offset;
        Vector2 scale;

        bool postRender;

        string textureFilepath;

        int drawLayer;

        public int DrawLayer { get { return drawLayer; } }

        /// <summary>
        /// The rotation of the Sprite.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// The sprite effect to be applied 
        /// to the sprite when rendered.
        /// </summary>
        public SpriteEffects SpriteEffect
        {
            get { return spriteEffect; }
            set { spriteEffect = value; }
        }

        /// <summary>
        /// The bounds of the Sprite.
        /// </summary>
        public Rectangle Bounds
        {
            get { return rect; }
            protected set { rect = value; }
        }

        /// <summary>
        /// The offset of the Sprite in relation to it's owner (if any). 
        /// </summary>
        public Vector2 Offset 
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// The scale of the Sprite in relation to it's owner (if any). 
        /// </summary>
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        #endregion
        #region  Ctors

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The id of the Sprite.</param>
        /// <param name="drawLayer">The layer to draw the Sprite on.</param>
        /// <param name="textureFilepath">The filepath of Sprite's texture.</param>
        /// <param name="colour">The colour to draw the Sprite.</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="offset">The positional offset of the Sprite.</param>
        /// <param name="scale">The scale of the Sprite.</param>
        /// <param name="rotation">The rotation of the Sprite.</param>
        /// <param name="spriteEffect">The particular Sprite effect to be applied to the Sprite.</param>
        public Sprite(string id, int drawLayer, string textureFilepath,
            Color colour, bool postRender, Vector2 offset, Vector2 scale,
            float rotation, SpriteEffects spriteEffect)
            : base(id)
        {
            if (drawLayer == -1)
                if (postRender && PostRenderManager.MaximumLayer > 0)
                    drawLayer = PostRenderManager.MaximumLayer;
                else if (DrawingManager.MaximumLayer > 0)
                    drawLayer = DrawingManager.MaximumLayer;
                else
                    drawLayer = 0;

            this.drawLayer = drawLayer;

            this.textureFilepath = textureFilepath;

            this.offset = offset;
            this.scale = scale;

            this.colour = colour;
            this.rotation = rotation;
            this.spriteEffect = spriteEffect;

            if (Common.PreviousScreenResolution != Vector2.One)
                ScreenResolutionChanged();

            this.postRender = postRender;

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The id of the Sprite.</param>
        /// <param name="drawLayer">The layer to draw the Sprite on.</param>
        /// <param name="textureFilepath">The filepath of Sprite's texture.</param>
        /// <param name="colour">The colour to draw the Sprite.</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="offset">The positional offset of the Sprite.</param>
        /// <param name="scale">The scale of the Sprite.</param>
        public Sprite(string id, int drawLayer, string textureFilepath, Vector2 offset, Vector2 scale, Color colour, bool postRender)
            : this(id, drawLayer, textureFilepath, colour, postRender, offset, scale, 0, SpriteEffects.None) { }

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The ID of the Sprite.</param>
        /// <param name="bounds">The bounds of the Sprite.</param>
        /// <param name="textureFilepath">The filepath of Sprite's texture.</param>
        /// <param name="colour">The colour to draw the Sprite.</param>
        /// <param name="drawLayer">The layer to draw the Sprite on.</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        public Sprite(string id, Rectangle bounds, string textureFilepath, Color colour,
            int drawLayer, bool postRender)
            : this(id, drawLayer, textureFilepath, colour, postRender,
            new Vector2(bounds.X, bounds.Y), new Vector2(bounds.Width, bounds.Height), 0, SpriteEffects.None) { }

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The id of the Sprite.</param>
        /// <param name="colour">The colour to draw the Sprite.</param>
        /// <param name="postRender">wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="offset">The positional offset of the Sprite.</param>
        /// <param name="scale">The scale of the Sprite.</param>
        public Sprite(string id, Vector2 offset, Vector2 scale, Color colour, bool postRender) :
            this(id, 1, "Eon/Textures/DefaultTexture", offset, scale, colour, postRender) { }

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The id of the Sprite.</param>
        /// <param name="drawLayer">The layer to draw the Sprite on.</param>
        /// <param name="textureFilepath">The filepath of Sprite's texture.</param>
        /// <param name="colour">The colour to draw the Sprite.</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        public Sprite(string id, int drawLayer, string textureFilepath, Color colour, bool postRender)
            : this(id, drawLayer, textureFilepath, Vector2.Zero, Vector2.One, colour, postRender) { }

        protected override void Initialize()
        {
            try
            {
                texture = Common.ContentManager.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                throw new NullReferenceException(textureFilepath);
            }

            base.Initialize();
        }

        #endregion
        #region Misc

        public virtual void Draw()
        {
            if (!Destroyed && Initialized)
            {
                rect = new Rectangle((int)offset.X, (int)offset.Y, (int)scale.X, (int)scale.Y);

                if (Owner != null)
                {
                    Vector3 s = Vector3.One;
                    Helpers.EonMathHelper.GetMatrixScale(Owner.World, out s);

                    rect.X += (int)Owner.World.Translation.X;
                    rect.Y += (int)Owner.World.Translation.Y;
                    rect.Width *= (int)s.X;
                    rect.Height *= (int)s.Y;
                }

                if (rotation > 0)
                    Common.Batch.Draw(texture, rect, null, colour, rotation, GetTextureCenter(texture), spriteEffect, 0);
                else
                    Common.Batch.Draw(texture, rect, null, colour, 0, Vector2.Zero, spriteEffect, 0);
            }
        }

        protected Vector2 GetTextureCenter(Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void ScreenResolutionChanged()
        {
            offset = Common.ReCalibrateScreenSpaceVector(offset);
            scale = Common.ReCalibrateScreenSpaceVector(scale);
        }

        /// <summary>
        /// Disposes of the Sprite.
        /// </summary>
        /// <param name="finalize">Wheather or not to completly
        /// remove every aspect of the Sprite.</param>
        public void Dispose(bool finalize)
        {
            if (finalize)
                texture = null;
        }

        public override void Destroy(bool remove)
        {
            Dispose(true);

            base.Destroy(remove);
        }

        #endregion
    }
}
