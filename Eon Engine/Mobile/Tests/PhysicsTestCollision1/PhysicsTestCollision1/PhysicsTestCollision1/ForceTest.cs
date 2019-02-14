using Eon;
using Eon.Physics2D;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;

namespace PhysicsTestCollision1
{
    public class ForceTest : GameObject
    {
        Sprite spr;
        PhysicsComponent phy;

        public ForceTest() : base("ForceTest") { }

        protected override void Initialize()
        {
            spr = new Sprite(ID + "Spr", 0, "BlackTexture",
                new Vector2((Common.ScreenResolution.X / 4) - 32, 64),
                new Vector2(64, 64), Color.White, false);

            AttachComponent(spr);

            phy = new PhysicsComponent(ID + "Phy", new Eon.Physics2D.Math.Shapes.Rectangle(
                (Common.ScreenResolution.X / 4) - 32, 0, 64, 64), Vector2.Zero, 0.5f);

            AttachComponent(phy);

            base.Initialize();
        }

        protected override void Update()
        {
            World = Matrix.CreateTranslation(phy.Position.X, phy.Position.Y, 0);

            base.Update();
        }
    }
}
