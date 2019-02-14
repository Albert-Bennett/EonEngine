/* Created: 19/09/2015
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Rendering2D.Text;
using Microsoft.Xna.Framework;

namespace LPP2DTest0.PhyObjects
{
    public sealed class TestPositionReadout : GameObject
    {
        GameObject trace;
        TextItem txt;

        public TestPositionReadout(string id, GameObject trace) : base(id)
        {
            this.trace = trace;
        }

        protected override void Initialize()
        {
            txt = new TextItem(ID + "txt", 1, "", 
                "Eon/Fonts/Arial23", Vector2.Zero, Color.White, true);

            AttachComponent(txt);

            base.Initialize();
        }

        protected override void Update()
        {
            txt.ChangeText("{" + trace.World.Position.X + ", " +
                trace.World.Position.Y + "}");

            base.Update();
        }

        public void ChangeTrace(GameObject trace)
        {
            this.trace = trace;
        }
    }
}
