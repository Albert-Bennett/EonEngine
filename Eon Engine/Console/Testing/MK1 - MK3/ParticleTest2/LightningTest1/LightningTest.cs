using Eon;
using Eon.Particles.D2.Lightning;
using Microsoft.Xna.Framework;

namespace LightningTest1
{
    public class LightningTest : GameObject
    {
        LightningBolt bolt;

        public LightningTest()
            : base("Lightning")
        {
            AttachComponent(new LightningText(ID + "Text", new Vector2(400, 200),
                4f, 2, "Eon/Fonts/Arial23", "Test Text", 1, false, "EndSegment",
                "MidSection", 2, 6, 10, 0.05f, Color.White));

            AttachComponent(new FracturedLightning(ID + "Fractured", new Vector2(400, 0),
                new Vector2(400, 300), 1, true, "EndSegment", "MidSection", 15, 110, 0.1f, 0,
                Color.White, 0.00002f, 4, 0.5f));

            bolt = new LightningBolt(new Vector2(200, 0), new Vector2(200, 200),
                3, true, "EndSegment", "MidSection", 5, 110, 0.1f, 0, Color.White, 0.002f);
        }

        protected override void Update()
        {
            if (bolt != null)
                bolt.Update();

            base.Update();
        }
    }
}
