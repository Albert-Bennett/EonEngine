using Eon.Engine;
using Eon.Engine.Input;
using Eon.PostProcessing.Effects;
using Eon.System.Management;
using Eon.Testing;
using Eon.UIApi.Cursors;
using Microsoft.Xna.Framework;

namespace MK4VideoTesting
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();
            new Cursor("Cursor", "Cursor", 16);

            base.Initialize();

            new Testing("TestProps");
            //new StaticModel(Vector3.Zero, "Terrain","Terrain");
            new StaticModel(Vector3.Zero, "Room", "A");
            new StaticModel(Vector3.Zero, "RoomB", "B");
            new StaticModel(Vector3.Zero, "RoomC", "C");
            new StaticModel(Vector3.Zero, "RoomBA", "BA");
            new StaticModel(Vector3.Zero, "RoomCA", "CA");
            new StaticModel(Vector3.Zero, "RoomCB", "CB");
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputHandler.GetInput("ExitStroked"))
                Exit();
            else if (InputManager.IsKeyStroked(Keys.Tab))
            {
                ((ErrorConsole)EngineModuleManager.Find("ErrorConsole")).ToggleHidden();

                IsFixedTimeStep = !IsFixedTimeStep;
            }
            else if (InputManager.IsKeyStroked(Keys.R))
                ((Eon.Rendering3D.Framework.Framework)EngineModuleManager.Find("Render3DFramework")).RenderDebug();

            if (InputManager.IsKeyStroked(Keys.P))
                this.SaveSnapShot();

            if (InputManager.IsKeyStroked(Keys.F))
                new FogEffect(Eon.Rendering3D.Framework.Framework.MainPostProcessing, 
                    Color.Gray.ToVector3(), 1.0f, 0.1f, 10.0f, FogType.Quick);

            if (InputManager.IsKeyStroked(Keys.G))
                new BloomEffect(Eon.Rendering3D.Framework.Framework.MainPostProcessing,
                    0.01f, 5.0f, 0.8f, 2.0f, 1.0f, 3.0f);

            if (InputManager.IsKeyStroked(Keys.H))
                new GausianBlurEffect(Eon.Rendering3D.Framework.Framework.MainPostProcessing, 1.0f);

            base.Update(gameTime);
        }
    }
}
