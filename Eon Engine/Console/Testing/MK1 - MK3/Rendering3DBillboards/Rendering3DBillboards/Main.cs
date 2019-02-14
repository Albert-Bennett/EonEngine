using Eon;
using Eon.Engine;
using Microsoft.Xna.Framework;

namespace Rendering3DBillboards
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            new Testing("Testing");

            new Floor("Floor", new Vector3(0, -0.01f, 0));
            new GrassPatch("Patch1", new Vector3(0, 0.0025f, 0));

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Common.ExitGame();

            base.Update(gameTime);
        }
    }
}
