using Eon;
using Eon.Engine;
using Eon.Physics2D.Forces;
using System;

namespace PhysicsTestCollision1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Framework
    {
        public Game1()
        {
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            Common.EnabledScreenOrientationModes(true);
        }

        protected override void Initialize()
        {
            new Gravity("Grav");

            new ForceTest();
            new CollisionTest();

            base.Initialize();
        }
    }
}
