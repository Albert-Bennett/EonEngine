using Eon;
using Eon.AI.Paths;
using Eon.Collections;
using Eon.Engine;
using Eon.Game.LevelManagement;
using Eon.Helpers;
using Eon.Rendering3D.Framework.Shaders;
using Eon.System.Management;
using Eon.System.Resolution;
using Eon.Testing;
using Eon.UIApi.Cursors;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Animation3DTest1
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();
            new BaseCursor("Cursor", "Cursor", new Vector2(-8, -8));

            base.Initialize();

            Common.ChangeScreenResolution(ScreenResolutions.Auto);

            new Testing1("TestProps");

            new Sphere("Sphere", new Vector3(0.13f, 0.05f, 0.0f));

            new TestingRoom();
            new Rings();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Exit();

            if (InputManager.IsKeyStroked(Keys.Tab))
                ((ErrorConsole)EngineComponentManager.Find("ErrorConsole")).ToggleHidden();

            if (InputManager.IsKeyStroked(Keys.Q))
                Common.ChangeTextureQuality(TextureQuality.MediumQuality);

            if (InputManager.IsKeyStroked(Keys.R))
                ((Eon.Rendering3D.Framework.Framework)EngineComponentManager.Find("Render3DFramework")).RenderDebug();

            base.Update(gameTime);
        }
    }
}
