/* Created: 24/08/2014
 * Last Updated: 25/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.UIApi.Cursors
{
    /// <summary>
    /// Used to define an on screen cursor.
    /// </summary>
    public class BaseCursor : GameObject
    {
        float size = 24;

        string textureFilepath;

        Vector2 offset = Vector2.Zero;
        Sprite spr;

        /// <summary>
        /// The size of the BaseCursor.
        /// </summary>
        public float Size
        {
            get { return size; }
        }

        /// <summary>
        /// The position of the BaseCUrsor.
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(World.Position.X, World.Position.Y); }
            set
            {
                World.Position = new Vector3(value, 0);

                if (InputManager.IsMouseConnected)
                    InputManager.SetMousePosition(value - offset);
            }
        }

        /// <summary>
        /// Creates a new BaseCursor.
        /// </summary>
        /// <param name="id">The id of the BaseCursor.</param>
        /// <param name="textureFilepath">The texture of the BaseCursor.</param>
        public BaseCursor(string id, string textureFilepath)
            : this(id, textureFilepath, Vector2.Zero, 24) { }

        /// <summary>
        /// Creates a new BaseCursor.
        /// </summary>
        /// <param name="id">The id of the BaseCursor.</param>
        /// <param name="textureFilepath">The texture of the BaseCursor.</param>
        /// <param name="offset">The Cursor image's offset.</param>
        public BaseCursor(string id, string textureFilepath, Vector2 offset)
            : this(id, textureFilepath, offset, 24) { }

        /// <summary>
        /// Creates a new BaseCursor.
        /// </summary>
        /// <param name="id">The id of the BaseCursor.</param>
        /// <param name="textureFilepath">The texture of the BaseCursor.</param>
        /// <param name="offset">The Cursor image's offset.</param>
        public BaseCursor(string id, string textureFilepath, Vector2 offset, float size)
            : base(id)
        {
            this.offset = offset;
            this.size = size;
            this.textureFilepath = textureFilepath;
        }

        protected override void Initialize()
        {
            UpdateWorld();

            World.Size = new Vector3(Size);

            spr = new Sprite(ID + "Sprite", 20, textureFilepath, Color.White, true);
            AttachComponent(spr);

            base.Initialize();
        }

        void UpdateWorld()
        {
            if (InputManager.IsMouseConnected)
                World.Position = new Vector3(InputManager.MousePos + offset, 0);
            else if (InputManager.IsTouchPadConnected)
                World.Position = new Vector3(InputManager.TouchPad.TouchLoc, 0);
            else if (InputManager.IsGamePadConnected())
                World.Position += new Vector3(InputManager.GetThumbStickAmount(TriggerIndex.Left), 0);
        }

        protected override void Update()
        {
            UpdateWorld();

            base.Update();
        }

        /// <summary>
        /// Changes the image of the BaseCursor.
        /// </summary>
        /// <param name="textureIndex">The filepath of the texture to change the BaseCursor to.</param>
        public void ChangeCursorImage(string textureFilepath)
        {
            Texture2D tex = null;

            try
            {
                tex = Common.ContentBuilder.Load<Texture2D>(textureFilepath);

                spr.Texture = tex;
            }
            catch { }
        }

        /// <summary>
        /// Changes the colour of the BaseCursor.
        /// </summary>
        /// <param name="colour">The new color of the BaseCursor.</param>
        public void ChangeCursorColour(Color colour)
        {
            spr.Colour = colour;
        }

        /// <summary>
        /// Sets the position of the cursor.
        /// </summary>
        /// <param name="position">The new position of the cursor.</param>
        public void SetPosition(Vector2 position)
        {
            InputManager.SetMousePosition(position);

            UpdateWorld();
        }

        /// <summary>
        /// Centers the cursor on the screen.
        /// </summary>
        public void Center()
        {
            Vector2 center = new Vector2(Common.DefaultScreenResolution.X / 2,
                Common.DefaultScreenResolution.Y / 2);

            SetPosition(center);
        }

        protected override void TextureQualityChanged()
        {
            World.Position = Common.GetReScaled(World.Position);
            offset = Common.GetReScaled(offset);

            base.TextureQualityChanged();
        }
    }
}
