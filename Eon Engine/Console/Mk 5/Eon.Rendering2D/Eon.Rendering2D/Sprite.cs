/* Created: 16/06/2013
 * Last Updated: 17/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Used to define a versatile object that is used 
    /// to render a/set of 2D image(s) in order to 
    /// achieve a particular effect.
    /// </summary>
    public class Sprite : ObjectComponent, IDrawItem, ITextureQualityChanged
    {
        #region Graphicial Variables

        Texture2D texture;
        Texture2D normalMap;

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

        /// <summary>
        /// The normal map to be applied to the Sprite.
        /// </summary>
        public Texture2D NormalMap
        {
            get { return normalMap; }
            set { normalMap = value; }
        }

        #endregion
        #region Calculous Variables

        Rectangle rect;
        SpriteEffects spriteEffect = SpriteEffects.None;

        float rotation = 0;
        float actualRotation = 0;

        Vector2 offset = Vector2.Zero;
        Vector2 scale = Vector2.One;
        Vector2 origin = Vector2.Zero;

        bool postRender;
        bool renderDisabled = false;

        string textureFilepath;
        string normalMapFilepath;

        int drawLayer;

        public int DrawLayer { get { return drawLayer; } }

        /// <summary>
        /// Render the object even if Disabled.
        /// </summary>
        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        /// <summary>
        /// The rotation of the Sprite.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// The true rotation of the Sprite.
        /// </summary>
        protected float ActualRotation
        {
            get { return actualRotation; }
        }

        /// <summary>
        /// The point of rotation for the Sprite.
        /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set
            {
                origin = value;
                CalculateBounds();
            }
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
        /// <param name="colour">The colour to draw the Sprite in (Texture map only).</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="normalMapFilepath">The filepath of the Sprite's normal map.</param>
        /// <param name="offset">The positional offset of the Sprite.</param>
        /// <param name="scale">The scale of the sprite.</param>
        /// <param name="rotation">The rotation of the Sprite.</param>
        /// <param name="spriteEffect">The particular Sprite effect to be applied to the Sprite.</param>
        public Sprite(string id, int drawLayer, string textureFilepath,
            Color colour, bool postRender, string normalMapFilepath,
            Vector2 offset, Vector2 scale, float rotation, SpriteEffects spriteEffect)
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
            this.normalMapFilepath = normalMapFilepath;

            LoadTextures();

            this.offset = offset;
            this.scale = scale;

            this.colour = colour;
            this.rotation = rotation;
            this.spriteEffect = spriteEffect;

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
        /// <param name="colour">The colour to draw the Sprite in (Texture map only).</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="offset">The positional offset of the Sprite.</param>
        /// <param name="scale">The scale of the Sprite.</param>
        public Sprite(string id, int drawLayer, string textureFilepath, Color colour, bool postRender, Vector2 offset, Vector2 scale)
            : this(id, drawLayer, textureFilepath, colour, postRender, "Eon/Textures/DefaultNormalMap",
              offset, scale, 0, SpriteEffects.None) { }

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The id of the Sprite.</param>
        /// <param name="colour">The colour to draw the Sprite in (Texture map only).</param>
        /// <param name="postRender">wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="offset">The positional offset of the Sprite.</param>
        /// <param name="scale">The scale of the Sprite.</param>
        public Sprite(string id, Color colour, bool postRender, Vector2 offset, Vector2 scale) :
            this(id, 1, "Eon/Textures/Pixel", colour, postRender, offset, scale) { }

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The id of the Sprite.</param>
        /// <param name="drawLayer">The layer to draw the Sprite on.</param>
        /// <param name="textureFilepath">The filepath of Sprite's texture.</param>
        /// <param name="colour">The colour to draw the Sprite in (Texture map only).</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        public Sprite(string id, int drawLayer, string textureFilepath, Color colour, bool postRender)
            : this(id, drawLayer, textureFilepath, colour, postRender, Vector2.Zero, Vector2.One) { }

        /// <summary>
        /// Creates a new Sprite.
        /// </summary>
        /// <param name="id">The id of the Sprite.</param>
        /// <param name="drawLayer">The layer to draw the Sprite on.</param>
        /// <param name="textureFilepath">The filepath of Sprite's texture.</param>
        /// <param name="colour">The colour to draw the Sprite in (Texture map only).</param>
        /// <param name="postRender">Wheather or not this Sprite sholud be post rendered.</param>
        /// <param name="bounds">The bounding area of the Sprite.</param>
        public Sprite(string id, int drawLayer, string textureFilepath, Color colour, bool postRender, Rectangle bounds) :
            this(id, drawLayer, textureFilepath, colour, postRender, new Vector2(bounds.X, bounds.Y), new Vector2(bounds.Width, bounds.Height)) { }

        protected override void Initialize()
        {
            LoadTextures();
            CalculateBounds();

            base.Initialize();
        }

        void CalculateBounds()
        {
            rect = new Rectangle((int)offset.X,
                (int)offset.Y,
                (int)scale.X, (int)scale.Y);

            if (Owner != null)
            {
                rect.X += (int)Owner.World.Position.X;
                rect.Y += (int)Owner.World.Position.Y;

                rect.Width *= (int)Owner.World.Size.X;
                rect.Height *= (int)Owner.World.Size.Y;

                actualRotation = rotation + Owner.World.Rotation.Z;
            }
        }

        void LoadTextures()
        {
            try
            {
                texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                new Error(ID + ": " + textureFilepath + " wasn't found.", Seriousness.Warning);

                texture = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/Pixel");
            }

            try
            {
                normalMap = Common.ContentBuilder.Load<Texture2D>(normalMapFilepath);
            }
            catch
            {
                new Error(ID + ": " + normalMapFilepath + " wasn't found.", Seriousness.Warning);

                normalMap = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/DefaultNormalMap");
            }
        }

        #endregion
        #region Misc

        public virtual void Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    {
                        CalculateBounds();

                        Common.Batch.Draw(texture, rect, null, colour,
                            actualRotation, origin, spriteEffect, 0);
                    }
                    break;

                default:
                    Common.Batch.Draw(normalMap, rect, null, Color.White,
                        actualRotation, origin, spriteEffect, 0);
                    break;
            }
        }

        public void SetOrigin()
        {
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void TextureQualityChanged()
        {
            offset = Common.GetReScaled(offset);
            scale = Common.GetReScaled(scale);
        }

        protected override void _Destroy()
        {
            normalMap = null;
            texture = null;

            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);

            base._Destroy();
        }

        #endregion
    }
}
