/* Created: 05/09/2015
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Engine.Input;
using Eon.Maths.Helpers;
using Eon.Physics2D.Forces;
using Eon.UIApi.Cursors;
using LPP2DTest0.PhyObjects;
using Microsoft.Xna.Framework;

namespace LPP2DTest0
{
    public sealed class TestMap : GameObject
    {
        int numOfCircles = 0;

        TestPositionReadout txt;

        public TestMap() : base("TestMap") { }

        protected override void Initialize()
        {
            new Gravity("Gravity", 0.0098f);

            RectanglePhy phy = new RectanglePhy("Rect0",
                new Rectangle(-100, 200, 450, 12), true);

            new Cursor("Cursor", "Cursor", 24);

            base.Initialize();
        }

        protected override void Update()
        {
            if (InputManager.IsMouseButtonPressed(MouseButtons.Left))
            {
                CirclePhy circle = new CirclePhy("CirclePhy" + numOfCircles, RandomHelper.GetRandom(1, 50));

                numOfCircles++;

                if (txt == null)
                    txt = new TestPositionReadout("TextPos", circle);
                else
                    txt.ChangeTrace(circle);
            }

            base.Update();
        }
    }
}
