using Eon;
using Eon.Engine;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
    public class Game1 : Framework
    {
        public Game1() : base() { }

        protected override void Initialize()
        {
            //Common.FullScreen();

            base.Initialize();

            new Testing1("Test1");
            new Sphere("Sphere1", new Vector3(0.03f, 0, 0));
            new Cube("Cube1", new Vector3(-0.03f, 0, 0));
            new Sphere("Sphere2", new Vector3(0, 0, -0.03f));
            new Cube("Cube2", new Vector3(0, 0, 0.03f));
            new Floor("Floor1", new Vector3(0, -0.01f, 0));
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsButtonPressed(ControlPadBtns.Back) ||
                InputManager.IsKeyPressed(Keys.Esc))
                Exit();

            base.Update(gameTime);
        }
    }
}
