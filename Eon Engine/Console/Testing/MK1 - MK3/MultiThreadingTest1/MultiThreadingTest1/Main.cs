using Eon.Engine;
using Eon.System.Management;
using Eon.Testing;

namespace MultiThreadingTest1
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            base.Initialize();
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Esc))
                Exit();

            if (InputManager.IsKeyStroked(Keys.Tab))
            {
                ((ErrorConsole)EngineComponentManager.Find("ErrorConsole")).ToggleHidden();

                new Error("Test Error", Seriousness.Error);
            }

            base.Update(gameTime);
        }
    }
}
