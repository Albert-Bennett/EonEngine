using Eon;
using Eon.Engine;
using Eon.System.Management;
using Eon.System.Resolution;
using Eon.Testing;
using Microsoft.Xna.Framework;

namespace LightningTest1
{
    public class Main : Framework
    {
        LightningTest lightninig;

        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            base.Initialize();

            Common.ChangeScreenResolution(ScreenResolutions.Auto);
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Exit();

            if (InputManager.IsKeyStroked(Keys.Tab))
                ((ErrorConsole)EngineComponentManager.Find("ErrorConsole")).ToggleHidden();

            if (InputManager.IsKeyStroked(Keys.A))
            {
                if (lightninig != null)
                    lightninig.Destroy();

                lightninig = new LightningTest();
            }

            base.Update(gameTime);
        }
    }
}
