using Eon;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering3D.Framework.Billboards;
using Microsoft.Xna.Framework;

namespace Rendering3DBillboards
{
    public class GrassPatch : GameObject
    {
        Billboard bill1;
        LockedAxisBillboard bill2;

        public GrassPatch(string id, Vector3 position)
            : base(id)
        {
            World *= Matrix.CreateTranslation(position);
        }

        protected override void Initialize()
        {
            bill1 = new Billboard(World.Translation + new Vector3(0.02f, 0, 0),
                0.03f, new Vector3(0, 0.2f, 0), "DeadGrass1");
            bill1.Initialize();

            bill2 = new LockedAxisBillboard(World.Translation, 0.03f, Vector3.Zero, "DeadGrass1");
            bill2.Initialize();

            base.Initialize();
        }

        protected override void Update()
        {
            Vector3 movement = new Vector3();

            if (InputManager.IsKeyStroked(Keys.Num4))
                movement.X -= 0.01f;
            else if (InputManager.IsKeyStroked(Keys.Num6))
                movement.X += 0.01f;

            if (InputManager.IsKeyStroked(Keys.Num2))
                movement.Y -= 0.01f;
            else if (InputManager.IsKeyStroked(Keys.Num8))
                movement.Y += 0.01f;

            bill2.Move(movement);

            base.Update();
        }
    }
}
