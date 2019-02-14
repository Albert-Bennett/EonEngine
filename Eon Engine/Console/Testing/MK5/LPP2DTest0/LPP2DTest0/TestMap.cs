/* Created: 05/09/2015
 * Last Updated: 18/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Engine.Input;
using Eon.Game2D.TileEngine;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Lighting;
using Microsoft.Xna.Framework;

namespace LPP2DTest0
{
    public sealed class TestMap : GameObject
    {
        BaseCamera2D camera;

        PointLight2D pl0;
        DominateLight2D dl0;

        public TestMap() : base("TestMap") { }

        protected override void Initialize()
        {
            camera = new BaseCamera2D("TestCamera");
            AttachComponent(camera);

            TileMap map = new TileMap("TileMap", "TestContent/Level1-1");

           // pl0 = new PointLight2D("pl0", new Vector3(255, 255, 168), 1, 0, new Vector3(950, 1950, 5), 400);
            dl0 = new DominateLight2D("dl0", new Vector3(56, 56, 56), 0.8f, 32, new Vector3(0.0f, -1.0f, 0.015f));

            base.Initialize();

            map.ConstrictCamera();
        }

        protected override void Update()
        {
            Vector2 movement = new Vector2();
            float speed = 5f;

            if (InputManager.IsKeyPressed(Keys.W))
                movement.Y -= speed;
            else if (InputManager.IsKeyPressed(Keys.S))
                movement.Y += speed;

            if (InputManager.IsKeyPressed(Keys.A))
                movement.X -= speed;
            else if (InputManager.IsKeyPressed(Keys.D))
                movement.X += speed;

            if (movement.X != 0 || movement.Y != 0)
                camera.Move(movement);

            base.Update();
        }
    }
}
