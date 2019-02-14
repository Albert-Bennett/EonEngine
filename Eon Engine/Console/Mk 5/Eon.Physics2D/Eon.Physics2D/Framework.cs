/* Created: 03/10/2013
 * Last Updated: 08/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Forces;
using Eon.System.Management;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines the class used to manage physics for the game.
    /// </summary>
    public class Framework : EngineModule
    {
        ForceManager forceManager;

        BroadPhase broadPhase;

        /// <summary>
        /// Creates a new PhysicsD2Framework.
        /// </summary>
        public Framework() : base("PhysicsD2Framework") { }

        protected override void Initialize()
        {
            forceManager = new ForceManager();
            broadPhase = new BroadPhase();

            base.Initialize();
        }
    }
}
