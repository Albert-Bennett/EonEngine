/* Created 28/07/2015
 * Last Updated: 28/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D;
using Eon.System.States;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Game2D.Objects.Misc
{
    /// <summary>
    /// Defines a stationary 2D image.
    /// </summary>
    public sealed class Image : GameObject
    {
        string textureFilepath;

        int drawLayer;
        bool postRender;

        Rectangle bounds;

        /// <summary>
        /// Creates a new Image.
        /// </summary>
        /// <param name="id">The ID of the Image.</param>
        /// <param name="textureFilepath">The filepath of the Image.</param>
        /// <param name="drawLayer">The layer to draw the image on.</param>
        /// <param name="postRender">Should the Image be rendered after everything.</param>
        /// <param name="bounds">The bounding area of the Image.</param>
        public Image(string id, string textureFilepath,
            int drawLayer, bool postRender,
            int x, int y, int width, int height, string presidence)
            : base(id)
        {
            this.textureFilepath = textureFilepath;
            this.drawLayer = drawLayer;
            this.postRender = postRender;

            this.bounds = new Rectangle(x, y, width, height);

            try
            {
                this.Presidence = (GameStates)Enum.Parse(
                    typeof(GameStates), presidence);
            }
            catch
            {
                this.Presidence = GameStateManager.CurrentState;
            }
        }

        protected override void Initialize()
        {
            AttachComponent(new Sprite(ID + "Spr", drawLayer,
                textureFilepath, Color.White, postRender, bounds));

            base.Initialize();
        }
    }
}
