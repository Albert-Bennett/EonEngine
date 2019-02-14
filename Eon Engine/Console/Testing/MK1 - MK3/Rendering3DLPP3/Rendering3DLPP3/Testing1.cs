using Eon;
using Eon.Animation2D.SpriteSheet;
using Eon.Engine;
using Eon.Maths.Helpers;
using Eon.Rendering2D.Text;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Eon.System.Management;
using Microsoft.Xna.Framework;

namespace Rendering3DLPP3
{
    public class Testing1 : GameObject
    {
        PointLight pl0;
        PointLight pl1;
        PointLight pl2;

        SpotLight spot;
        DirectionalLight dl;

        ChaseCamera camera;

        #region Testing

        float bias = 0.08f;

        TextItem txt;

        #endregion

        public Testing1(string id)
            : base(id) { }

        protected override void Initialize()
        {
            camera = new ChaseCamera("Cam1", 0.05f, 10000, new Vector3(0, 0.10f, -0.1f),
                new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            camera.TargetOffSet = new Vector3(0, 0.10f, 0);

            AttachComponent(camera);

            pl0 = new PointLight(ID + "PointLight0", GetPos(),
                Color.White.ToVector3(), GetIntensity(), GetRadius());

            AttachComponent(pl0);

            pl1 = new PointLight(ID + "PointLight1", new Vector3(0, 0.0f, 0),
                Color.White.ToVector3(), GetIntensity(), GetRadius());

            AttachComponent(pl1);

            pl2 = new PointLight(ID + "PointLight2", GetPos(),
                Color.White.ToVector3(), GetIntensity(), 5);

            AttachComponent(pl2);

            spot = new SpotLight(ID + "Spot1", new Vector3(0.0f, 0.07f, 0.0f),
                new Vector3(255, 255, 156) / 255, 1.0f, false,
                new Vector3(1.0f, 0.0f, 0.0f), 60, 30, 0.2f);

            AttachComponent(spot);

            dl = new DirectionalLight(ID + "dl", new Vector3(255, 206, 120),
                0.002f, new Vector3(-1f, 0, 0), false);

            AttachComponent(dl);

            #region Testing

            txt = new TextItem(ID + "BiasTxt", 0, "Depth Bias: " + bias,
                "Eon/Fonts/Arial12", new Vector2(200, 10), Color.White, true);

            AttachComponent(txt);

            //ani = new AnimatedSprite(ID + "Ani", 0, "BlobBounce",
            //    Color.White, 70, 6, 4, true, 0, new Vector2(128, 128), Vector2.Zero);

            //AttachComponent(ani);
            //ani.OnFinished += new HasFinishedEvent(Finished);

            //ani.Play(false);

            #endregion

            base.Initialize();
        }

        Vector3 GetPos()
        {
            return RandomHelper.GetRandom(
                new Vector3(-0.339f, 0, 0.0f),
                new Vector3(0.339f, 0.5f, 0.718f));
        }

        float GetIntensity()
        {
            return RandomHelper.GetRandom(0.2f, 1.0f);
        }

        float GetRadius()
        {
            return RandomHelper.GetRandom(0.1f, 1f);
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

            #region Testing

            float b = bias;

            if (InputManager.IsKeyPressed(Keys.Up))
                b += 0.00001f;
            else if (InputManager.IsKeyPressed(Keys.Down))
                b -= 0.00001f;

            if (b != bias)
            {
                bias = b;

                txt.ChangeText("Depth Bias: " + bias);

                EngineComponentManager.Find("RenderManager3D").SendMessage("ChangeBias", bias);
            }

            //if (InputManager.IsKeyPressed(Keys.Left))
            //    spot.Direction -= new Vector3(0, 0, 0.01f);
            //else if (InputManager.IsKeyPressed(Keys.Right))
            //    spot.Direction += new Vector3(0, 0, 0.01f);

            //if (InputManager.IsKeyPressed(Keys.Up))
            //    spot.Direction += new Vector3(0, 0.01f, 0);
            //else if (InputManager.IsKeyPressed(Keys.Down))
            //    spot.Direction -= new Vector3(0, 0.01f, 0);

            #endregion

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

                spot.Position = CameraManager.CurrentCamera.Position;
                spot.Direction = CameraManager.CurrentCamera.Direction;
            }
        }
    }
}
