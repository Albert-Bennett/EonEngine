using Eon;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Lighting;
using Microsoft.Xna.Framework;

namespace Rendering3DTransparencyFR
{
    public class Testing1 : GameObject
    {
        //HemiLight hemi1;
        PointLight p1;
        PointLight p2;
        PointLight p3;

        ChaseCamera camera;

        public Testing1(string id)
            : base(id) { }

        protected override void Initialize()
        {
            camera = new ChaseCamera("Camera", 0.01f, 10000, new Vector3(0, 0, -0.1f),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            AttachComponent(camera);

            //hemi1 = new HemiLight(ID + "Hemi1", new Vector3(0, 60, 0), Color.White.ToVector3(), 1);
            //AttachComponent(hemi1);

            p1 = new PointLight(ID + "PointLight1", new Vector3(0, 0, 0), Color.White.ToVector3(), 1, 0.3f);
            AttachComponent(p1);

            p2 = new PointLight(ID + "PointLight2", new Vector3(0, 1, 0), Color.Red.ToVector3(), 1, 1.2f);
            AttachComponent(p2);

            p3 = new PointLight(ID + "PointLight3", new Vector3(0, -1, 0), Color.White.ToVector3(), 1, 1.3f);
            AttachComponent(p3);

            AttachComponent(new PointLight(
                ID + "PointLight4",
                new Vector3(0, 0, -1),
                RandomHelper.GetRandomVector3(
                Vector3.Zero, Vector3.One),
                RandomHelper.GetRandomFloat(0.1f, 1)
                , 1.3f));

            AttachComponent(new PointLight(
                ID + "PointLight5",
                new Vector3(0, 0, 1),
                RandomHelper.GetRandomVector3(
                Vector3.Zero, Vector3.One),
                RandomHelper.GetRandomFloat(0.1f, 1)
                , 1.3f));

            base.Initialize();
        }

        protected override void Update()
        {
            if (InputManager.IsKeyStroked(Keys.Num1))
                p1.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num2))
                p2.ToogleEnable();

            if (InputManager.IsKeyStroked(Keys.Num3))
                p3.ToogleEnable();

            //if (InputManager.IsKeyStroked(Keys.Num0))
            //    hemi1.ToogleEnable();

            //if (InputManager.IsKeyPressed(Keys.Enter))
                //p1.Intensity -= 0.001f;
            //    p2.Colour = RandomHelper.GetRandomVector3(
            //        Vector3.Zero, Vector3.One);

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
