/* Created 24/08/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Rendering2D;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.UIApi.Cursors
{
    /// <summary>
    /// Used to define an on screen cursor.
    /// </summary>
    public class BaseCursor : GameObject
    {
        const float size = 24;

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
        /// Creates a new BaseCursor.
        /// </summary>
        /// <param name="id">The id of the BaseCursor.</param>
        /// <param name="textureFilepath">The texture of the BaseCursor.</param>
        /// <param name="offset">The Cursor image's offset.</param>
        public BaseCursor(string id, string textureFilepath,Vector2 offset)
            : base(id)
        {
            this.offset = offset;

            this.textureFilepath = textureFilepath;
        }

        /// <summary>
        /// Creates a new BaseCursor.
        /// </summary>
        /// <param name="id">The id of the BaseCursor.</param>
        /// <param name="textureFilepath">The texture of the BaseCursor.</param>
        public BaseCursor(string id, string textureFilepath)
            : this(id, textureFilepath, Vector2.Zero) { }

        public override void Initialize()
        {
            UpdateWorld();

            World.Scale = new Vector3(Size);

            spr = new Sprite(ID + "Sprite", DrawingManager.MaximumLayer + 1, textureFilepath, Color.White, true);
            AttachComponent(spr);

            base.Initialize();
        }

        void UpdateWorld()
        {
            World.Translation = new Vector3(InputManager.MousePos + offset, 0);
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
                tex = Common.ContentManager.Load<Texture2D>(textureFilepath);

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
        /// Sets teh position of the cursor.
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
    }
}
