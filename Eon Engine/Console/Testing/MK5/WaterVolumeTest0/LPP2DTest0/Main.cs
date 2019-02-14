/* Created: 05/09/2015
 * Last Updated: 05/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Engine.Input;
using Eon.System.Management;
using Eon.System.States;
using Eon.Testing;
using Microsoft.Xna.Framework;

namespace LPP2DTest0
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            new TestMap();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Exit();

            if (InputManager.IsKeyStroked(Keys.Tab))
            {
                ((ErrorConsole)EngineModuleManager.Find("ErrorConsole")).ToggleHidden();

                IsFixedTimeStep = !IsFixedTimeStep;
            }

            base.Update(gameTime);
        }
    }
}
