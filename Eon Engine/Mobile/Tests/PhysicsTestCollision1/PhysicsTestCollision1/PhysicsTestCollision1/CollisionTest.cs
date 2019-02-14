using Eon;
using Eon.Physics2D;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;

namespace PhysicsTestCollision1
{
    public sealed class CollisionTest : GameObject
    {
        Sprite spr;
        CollisionComponent collide;

        public CollisionTest() : base("Collision") { }

        protected override void Initialize()
        {
            spr = new Sprite(ID + "Spr", 0, "BlackTexture",
                new Vector2(50, Common.ScreenResolution.Y - 72),
                new Vector2(700, 64), Color.White, false);

            AttachComponent(spr);

            collide = new CollisionComponent(ID + "Collide",
                new Eon.Physics2D.Math.Shapes.Rectangle(
                50, Common.ScreenResolution.Y - 72, 700, 64));

            AttachComponent(collide);

            base.Initialize();
        }
    }
}
