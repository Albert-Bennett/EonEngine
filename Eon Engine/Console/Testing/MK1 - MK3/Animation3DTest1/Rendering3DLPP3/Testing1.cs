using Eon;
using Eon.Engine;
using Eon.Maths.Helpers;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Microsoft.Xna.Framework;

namespace Animation3DTest1
{
    public class Testing1 : GameObject
    {
        PointLight pl0;
        PointLight pl1;
        PointLight pl2;

        SpotLight spot;

        ChaseCamera camera;

        public Testing1(string id)
            : base(id) { }

        protected override void Initialize()
        {
            camera = new ChaseCamera("Cam1", 0.01f, 1000, new Vector3(0, 0.10f, -0.1f),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            camera.TargetOffSet = new Vector3(0, 0.10f, 0);

            AttachComponent(camera);

            spot = new SpotLight(ID + "Spot1", new Vector3(0.0f, 0.5f, 0.0f),
                new Vector3(255, 255, 156) / 255, 1.0f, true,
                new Vector3(0.0f, -1.0f, 0.0001f), 60, 30, 0.75f);

            AttachComponent(spot);

            base.Initialize();
        }

        Vector3 GetPos()
        {
            return RandomHelper.GetRandom(
                new Vector3(-0.113f, 0, 0.0f),
                new Vector3(0.113f, 0, 0.226f));
        }

        float GetIntensity()
        {
            return RandomHelper.GetRandom(0.2f, 1.0f);
        }

        float GetRadius()
        {
            return RandomHelper.GetRandom(0.1f, 0.4f);
        }

        protected override void Update()
        {
            CameraUpdate();

            base.Update();
        }

        void CameraUpdate()
        {
            Vector3 movement = Vector3.Zero;

            float rotationSpeed = 0.002f;

            Vector3 rotation = Vector3.Zero;

            if (InputManager.IsButtonPressed(MouseButtons.Left))
                rotation = new Vector3(InputManager.MouseDelta.Y *
                    rotationSpeed, InputManager.MouseDelta.X * rotationSpeed, 0);

            float speed = 0.002f;

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
            }
        }
    }
}
