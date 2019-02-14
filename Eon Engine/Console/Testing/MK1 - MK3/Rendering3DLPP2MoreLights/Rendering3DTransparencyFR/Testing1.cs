using Eon;
using Eon.Engine;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Lighting;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
    public class Testing1 : GameObject
    {
        PointLight point;
        DirectionalLight direct;
        SpotLight spot;

        ChaseCamera camera;

        public Testing1(string id)
            : base(id) { }

        protected override void Initialize()
        {
            camera = new ChaseCamera("Camera", 0.01f, 10000, new Vector3(0, 0, -0.1f),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            AttachComponent(camera);

            point = new PointLight(ID + "PointLight1", new Vector3(0, 0, 0), Color.White.ToVector3(), 0.8f, false, 0.2f);
            AttachComponent(point);

            point.ToogleEnable();

            direct = new DirectionalLight(ID + "Direct1", Color.White.ToVector3(), 0.3f, false, new Vector3(-1, -1, 0));
            AttachComponent(direct);

            direct.ToogleEnable();

            spot = new SpotLight(ID + "Spot1", new Vector3(0, 0.05f, 0), Color.LightYellow.ToVector3(), 1.0f,
                false, new Vector3(0, -1, 0), 50, 30, 2f);

            AttachComponent(spot);

            base.Initialize();
        }

        protected override void Update()
        {
            if (InputManager.IsKeyStroked(Keys.Num1))
                point.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num2))
                direct.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num3))
                spot.ToogleEnable();

            Vector3 pos = new Vector3();

            if (InputManager.IsKeyPressed(Keys.Num8))
                pos.Y += 0.01f;
            else if (InputManager.IsKeyPressed(Keys.Num5))
                pos.Y -= 0.01f;

            spot.Position += pos;

            if (InputManager.IsKeyStroked(Keys.Num4))
                spot.Intensity -= 0.1f;
            else if (InputManager.IsKeyStroked(Keys.Num6))
                spot.Intensity += 0.1f;

            CameraUpdate();

            base.Update();
        }

        void CameraUpdate()
        {
            Vector3 movement = Vector3.Zero;
            Vector3 rotation = Vector3.Zero;

            float speed = 0.002f;
            float rotationSpeed = 0.02f;

            if (InputManager.IsKeyPressed(Keys.A))
                movement.X -= speed;
            else if (InputManager.IsKeyPressed(Keys.D))
                movement.X += speed;

            if (InputManager.IsKeyPressed(Keys.W))
                movement.Y += speed;
            else if (InputManager.IsKeyPressed(Keys.S))
                movement.Y -= speed;

            if (InputManager.IsKeyPressed(Keys.Up))
                rotation.X += rotationSpeed;
            else if (InputManager.IsKeyPressed(Keys.Down))
                rotation.X -= rotation.X += rotationSpeed;

            if (InputManager.IsKeyPressed(Keys.Left))
                rotation.Y -= rotationSpeed;
            else if (InputManager.IsKeyPressed(Keys.Right))
                rotation.Y += rotationSpeed;

            if (InputManager.IsKeyPressed(Keys.Z))
                movement.Z += speed;
            else if (InputManager.IsKeyPressed(Keys.X))
                movement.Z -= speed;

            if (movement != Vector3.Zero || rotation != Vector3.Zero)
            {
                Vector3 move = Vector3.Transform(movement, Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z));
                camera.Move(move, rotation);
            }
        }
    }
}
