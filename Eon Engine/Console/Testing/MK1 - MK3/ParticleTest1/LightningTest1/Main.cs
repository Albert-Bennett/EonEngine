using Eon;
using Eon.Engine;
using Eon.Particles.D2.Lightning;
using Eon.System.Management;
using Eon.System.Resolution;
using Eon.Testing;
using Microsoft.Xna.Framework;

namespace LightningTest1
{
    public class Main : Framework
    {
        LightningBolt bolt;

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
                bolt = new LightningBolt(new Vector2(400, 0), new Vector2(400, 550), 1,
                       false, "EndSegment", "MidSection", 8, 100, 0.3f, 50, Color.White, 0.0002f);

            if (bolt != null)
                bolt.Update();

            base.Update(gameTime);
        }
    }
}
