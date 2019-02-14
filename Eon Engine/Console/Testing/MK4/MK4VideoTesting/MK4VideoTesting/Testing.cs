using Eon;
using Eon.Engine.Input;
using Eon.Maths.Helpers;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Microsoft.Xna.Framework;

namespace MK4VideoTesting
{
    public class Testing : GameObject
    {
        PointLight pl0;
        PointLight pl1;
        PointLight pl2;

        SpotLight spot;
        DirectionalLight dl;

        ChaseCamera camera;

        public Testing(string id)
            : base(id) { }

        protected override void Initialize()
        {
            camera = new ChaseCamera("Cam1", 0.05f, 5000, new Vector3(0, 0.10f, -0.1f),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            camera.TargetOffSet = new Vector3(0, 0.10f, 0);

            AttachComponent(camera);

            pl0 = new PointLight(ID + "PointLight0", -0.5f,
                -0.5f, 0.5f, 255, 255, 255, 0.5f, 2);

            pl1 = new PointLight(ID + "PointLight1", 0, 0, 0,
                255, 255, 255, GetIntensity(), 5);

            pl2 = new PointLight(ID + "PointLight2", 1,
                -0.5f, 1, 255, 255, 255, GetIntensity(), 5);

            spot = new SpotLight(ID + "Spot1", 0.0f, 0.07f, 0.0f,
                255, 255, 156, 3.0f, 0.5f, -1.0f, 0.0f, false, 60, 50, 5f);

            dl = new DirectionalLight(ID + "dl", 255, 255, 255,
                0.1f, -1, -0.5f, 0, false);

            base.Initialize();
        }

        float GetPos()
        {
            return RandomHelper.GetRandom(-0.5f, 0.5f);
        }

        float GetIntensity()
        {
            return RandomHelper.GetRandom(0.2f, 1.0f);
        }

        protected override void Update()
        {
            if (InputManager.IsKeyStroked(Keys.Num1))
                pl0.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num2))
                pl1.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num3))
                pl2.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num4))
                if (spot != null)
                    spot.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num5))
                if (dl != null)
                    dl.ToogleEnable();

            CameraUpdate();

            base.Update();
        }

        void CameraUpdate()
        {
            Vector3 movement = Vector3.Zero;

            float rotationSpeed = 0.002f;

            Vector3 rotation = Vector3.Zero;

            if (InputManager.IsMouseButtonPressed(MouseButtons.Left))
                rotation = new Vector3(InputManager.MouseDelta.Y *
                    rotationSpeed, InputManager.MouseDelta.X * rotationSpeed, 0);

            float speed = 0.004f;

            if (InputManager.IsKeyPressed(Keys.A))
                movement.X += speed;
            else if (InputManager.IsKeyPressed(Keys.D))
                movement.X -= speed;

            if (InputManager.IsKeyPressed(Keys.W))
                movement.Y += speed;
            else if (InputManager.IsKeyPressed(Keys.S))
                movement.Y -= speed;

            if (InputManager.IsKeyPressed(Keys.Z))
                movement.Z += speed;
            else if (InputManager.IsKeyPressed(Keys.X))
                movement.Z -= speed;

            if (movement != Vector3.Zero || rotation != Vector3.Zero)
            {
                Vector3 move = Vector3.Transform(movement,
                    Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z));

                camera.Move(move, rotation);
                camera.FollowingPos += movement;

                spot.World.Position = CameraManager.CurrentCamera.Position;
                spot.Direction = CameraManager.CurrentCamera.Direction;
            }
        }
    }
}
