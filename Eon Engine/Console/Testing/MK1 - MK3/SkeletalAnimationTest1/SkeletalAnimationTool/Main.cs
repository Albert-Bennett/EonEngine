using Eon;
using Eon.Engine;
using Eon.System.Management;
using Eon.System.Resolution;
using Eon.Testing;

namespace SkeletalAnimationTool
{
    public class Main : Framework
    {
        ArmAnimation ani;

        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            base.Initialize();

            Common.ChangeScreenResolution(ScreenResolutions.Auto);
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Exit();

            if (InputManager.IsKeyStroked(Keys.Tab))
                ((ErrorConsole)EngineComponentManager.Find("ErrorConsole")).ToggleHidden();

            if (InputManager.IsKeyStroked(Keys.A))
                ani = new ArmAnimation();

            if (InputManager.IsKeyStroked(Keys.S))
                ani.Save();

            if (InputManager.IsKeyStroked(Keys.P))
                ani.PlayAnimation();

            base.Update(gameTime);
        }
    }
}
