using Eon;
using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Maths.Helpers;
using Eon.Rendering3D.Framework.Billboards;
using Eon.Rendering3D.Framework.Shaders;
using Eon.Rendering3D.Primatives;
using Eon.System.Management;
using Eon.System.Resolution;
using Eon.Testing;
using Eon.UIApi.Cursors;
using Microsoft.Xna.Framework;
using System;

namespace Rendering3DLPP3
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();
            new BaseCursor("Cursor", "Cursor", new Vector2(-8, -8));

            base.Initialize();

            new Testing1("TestProps");

            new Sphere("Sphere", new Vector3(0.0f, 0.05f, -0.2f));

            //new TestingRoom();
            //new Rings();

            //new LODCube(new Vector3(0, 0.05f, -0.07f),
            //    new Vector3(0.02f), Vector3.Zero, Color.White);

            //new LODCube(new Vector3(0.01f, 0.05f, -0.1f),
            //    new Vector3(0.02f), Vector3.Zero, new Color(1, 1, 1, 126));

            new TransparentSphere("TSphere0", Vector3.Zero);

            //int num = RandomHelper.GetRandom(5, 50);

            //for (int i = 0; i < 10; i++)
            //    new TransparentSphere("TSphere" + (i + 1),
            //        new Vector3(RandomHelper.GetRandom(-10f, 10f),
            //        RandomHelper.GetRandom(-10f, 10f),
            //        RandomHelper.GetRandom(-10f, 10f)));

            new Billboard(new Vector3(0.1f,
                0.0f, 0), 0.05f, Vector3.Zero, "DeadGrass");

            new Terrain(Vector3.Zero, "T0");
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyStroked(Keys.Esc))
                Exit();

            if (InputManager.IsKeyStroked(Keys.Tab))
                ((ErrorConsole)EngineComponentManager.Find("ErrorConsole")).ToggleHidden();

            if (InputManager.IsKeyStroked(Keys.R))
                ((Eon.Rendering3D.Framework.Framework)EngineComponentManager.Find("Render3DFramework")).RenderDebug();

            //bool derpa;

            //if (InputManager.IsKeyStroked(Keys.A))
            //    derpa = true;

            base.Update(gameTime);
        }
    }
}
