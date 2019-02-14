using Eon.Engine;
using Eon.Rendering3D.Framework.Decals;
using Eon.System.Management;
using Eon.Testing;
using Eon.UIApi.Cursors;
using Microsoft.Xna.Framework;

namespace MK4Update
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();
            new BaseCursor("Cursor", "Cursor", new Vector2(-8, -8), 16);

            base.Initialize();

            new Testing("TestProps");

            //new Model("Cube", "Cube/Cube_Mat",new Vector3(0.0f, 0.1f, -0.2f));

            //new LODCube(new Vector3(0, 0.05f, -0.07f),
            //    new Vector3(0.02f), Vector3.Zero, Color.White);

            //new LODCube(new Vector3(0.01f, 0.05f, -0.1f),
            //    new Vector3(0.02f), Vector3.Zero, new Color(0, 108, 255, 128));

            new Robot(Vector3.Zero, "Robot");

            //new TransparentSphere("TSphere0", Vector3.Zero);

            //new WaterTest(Vector3.Zero);

            //new LODCube(new Vector3(0.01f, -0.3f, -0.1f),
            //    new Vector3(0.1f), Vector3.Zero, Color.Red);

            new Decal(new Vector3(0.01f, -0.3f, -0.1f),
                new Vector3(0.1f), Vector3.Zero, "Decals/Element", 
                "Decals/Element_Norm", "Eon/Textures/DefaultSpecularMap");

            new Terrain(Vector3.Zero, "T0");
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

            if (InputManager.IsKeyStroked(Keys.R))
                ((Eon.Rendering3D.Framework.Framework)EngineModuleManager.Find("Render3DFramework")).RenderDebug();

            //bool derpa;

            //if (InputManager.IsKeyStroked(Keys.A))
            //    derpa = true;

            base.Update(gameTime);
        }
    }
}
