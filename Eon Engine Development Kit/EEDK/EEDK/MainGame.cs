/* Created: 01/01/2015
 * Last Updated: 01/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Engine;
using Eon.System.Management;
using Eon.System.Resolution;
using Eon.Testing;
using Microsoft.Xna.Framework;

namespace EEDK
{
    public class MainGame : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Exit();

            base.Update(gameTime);
        }
    }
}
