/* Created 13/08/2015
 * Last Updated: 13/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Engine.Input;
using Microsoft.Xna.Framework;

namespace EEDK.TilemapEditor
{
    public class Game1 : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Esc))
                Exit();

            base.Update(gameTime);
        }
    }
}
