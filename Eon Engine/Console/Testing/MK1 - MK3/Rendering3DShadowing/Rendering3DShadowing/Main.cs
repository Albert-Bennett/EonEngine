/* Created 27/07/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Engine;
using Microsoft.Xna.Framework;

namespace Rendering3DShadowing
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            new Testing();
            new Floor("Floor", new Vector3(0, -0.022f, 0));
            new Sphere("Sphere1", new Vector3(-0.02f, 0.02f, 0));

            base.Initialize();
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Common.ExitGame();

            base.Update(gameTime);
        }
    }
}
