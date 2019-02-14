using Eon;
using Eon.Engine;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Billboards;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Microsoft.Xna.Framework;

namespace Rendering3DBillboards
{
    public class Testing : GameObject
    {
        PointLight point;

        ChaseCamera camera;

        public Testing(string id)
            : base(id) { }

        protected override void Initialize()
        {
            camera = new ChaseCamera("Camera", 0.01f, 10000, new Vector3(0, 0, -0.1f),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            AttachComponent(camera);

            point = new PointLight(ID + "PointLight1", new Vector3(0, 0, 0), Color.White.ToVector3(), 0.5f, 0.1f);
            AttachComponent(point);

            base.Initialize();
        }

        protected override void Update()
        {
            if (InputManager.IsKeyStroked(Keys.Num1))
                point.ToogleEnable();

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
