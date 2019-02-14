/* Created: 23/04/2015
 * Last Updated: 23/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Input;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.UIApi.Cursors
{
    /// <summary>
    /// Used to define an on screen cursor.
    /// </summary>
    public sealed class Cursor : BaseCursor
    {
        Sprite spr;
        string textureFilepath;

        /// <summary>
        /// Creates a new Cursor.
        /// </summary>
        /// <param name="id">The id of the Cursor.</param>
        /// <param name="textureFilepath">The texture of the Cursor.</param>
        public Cursor(string id, string textureFilepath, float size)
            : base(id, size)
        {
            this.textureFilepath = textureFilepath;

            Presidence = System.States.GameStates.None;
        }

        protected override void Initialize()
        {
            spr = new Sprite(ID + "Sprite", 20, textureFilepath, Color.White, true);
            AttachComponent(spr);

            Center();

            base.Initialize();
        }

        protected override void UpdateWorld()
        {
            if (InputManager.IsMouseConnected)
                World.Position = new Vector3(InputManager.MousePos, 0);
            else if (InputManager.IsTouchPadConnected)
                World.Position = new Vector3(InputManager.TouchPad.TouchLoc, 0);
            else if (InputManager.IsGamePadConnected())
                World.Position += new Vector3(InputManager.GetThumbStickAmount(TriggerIndex.Left), 0);
        }

        /// <summary>
        /// Changes the image of the Cursor.
        /// </summary>
        /// <param name="textureIndex">The filepath of the texture to change the Cursor to.</param>
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
        /// Changes the colour of the Cursor.
        /// </summary>
        /// <param name="colour">The new color of the Cursor.</param>
        public void ChangeCursorColour(Color colour)
        {
            spr.Colour = colour;
        }
    }
}
