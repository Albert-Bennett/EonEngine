/* Created 04/04/2015
 * Last Updated: 27/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D;
using Eon.System.States;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// Defines a Splash screen in the game.
    /// </summary>
    public abstract class SplashScreen : MenuScreen
    {
        Sprite bg;

        int currentValue = 255;
        float current = 0;

        float time = 500;
        float step;

        string textureFilepath;

        /// <summary>
        /// Creates a new SplashScreen.
        /// </summary>
        /// <param name="id">The ID of the SplashScreen.</param>
        /// <param name="textureFilepath">The image to be shown.</param>
        public SplashScreen(string id, string textureFilepath)
            : base(id, GameStates.MainMenu)
        {
            this.textureFilepath = textureFilepath;
            this.time = 500;
        }

        /// <summary>
        /// Creates a new SplashScreen.
        /// </summary>
        /// <param name="id">The ID of the SplashScreen.</param>
        /// <param name="textureFilepath">The image to be shown.</param>
        /// <param name="displayTime">The length of time for the SplashScreen to fade.</param>
        public SplashScreen(string id, string textureFilepath,
            float displayTime) : base(id, GameStates.MainMenu)
        {
            this.textureFilepath = textureFilepath;
            this.time = displayTime;
        }

        protected override void Initialize()
        {
            bg = new Sprite(ID + "BG", 0, textureFilepath,
                Color.Black, true, Vector2.Zero, Common.TextureQuality);

            AttachComponent(bg);

            step = 255.0f / time;

            base.Initialize();
        }

        protected override void Update()
        {
            current += step;

            if (current >= 1)
            {
                current -= 1;
                currentValue -= 1;

                bg.Colour = new Color(currentValue,
                    currentValue, currentValue, currentValue);

                if (currentValue <= 1)
                    OnFade();
            }

            base.Update();
        }

       protected abstract void OnFade();
    }
}

