using Eon;
using Eon.Engine;
using Eon.Utilities;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
    public class Game1 : Framework
    {
        public Game1() : base() { }

        protected override void Initialize()
        {
            //common.ChangeScreenResolution(AcceptedScreenResolutions.SVGA);

            InputManager.CreateKeyboard();

            new Testing1("Test1");

            //new Sphere("Sphere1", new Vector3(0.03f, 0, 0));
            new Cube("Cube1", new Vector3(-0.03f, 0, 0));
            new Sphere("Sphere2", new Vector3(0, 0, -0.03f));
            new Cube("Cube2", new Vector3(0, 0, 0.03f));
            new Cube("Cube3", new Vector3(0.03f, 0, 0));
            new Floor("Floor1", new Vector3(0, -0.012f, 0));
            //new SkySphere("Sky", Vector3.Zero);
            //new Scutler("Scutler", Vector3.Zero);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsButtonPressed(ControlPadBtns.Back) ||
                InputManager.IsKeyPressed(Keys.Esc))
                Exit();

            if (InputManager.IsKeyStroked(Keys.F))
                common.ChangeScreenResolution(ScreenResolutions.Auto);

            if (InputManager.IsKeyStroked(Keys.Q))
                common.ChangeTextureQuality(TextureQuality.MediumQuality);

            base.Update(gameTime);
        }
    }
}
