/* Created: 19/09/2015
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Engine.Input;
using Eon.Physics2D;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;

namespace LPP2DTest0.PhyObjects
{
    public sealed class CirclePhy : GameObject
    {
        float mass;
        int size = 96;

        Sprite spr;
        PhysicsComponent phy;

        public CirclePhy(string id, float mass)
            : base(id)
        {
            this.mass = mass;
        }

        protected override void Initialize()
        {
            Rectangle rect = new Rectangle((int)InputManager.MousePos.X,
                (int)InputManager.MousePos.Y, size, size);

            spr = new Sprite(ID + "Spr", 1, "TestContent/Circle", Color.White, true, rect);
            AttachComponent(spr);

            phy = new PhysicsComponent(ID + "phy", new Eon.Physics2D.Maths.Shapes.Circle(
                InputManager.MousePos + new Vector2(size / 2), size), Vector2.Zero, mass);

            AttachComponent(phy);

            base.Initialize();
        }

        protected override void Update()
        {
            if (World.Position.Y > Common.TextureQuality.Y + size)
                Destroy();

            base.Update();
        }
    }
}
