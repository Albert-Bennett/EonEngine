/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Forces;
using Eon.Physics2D.Particles;
using Eon.Interfaces;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines the class used to manage physics for the game.
    /// </summary>
    public class Framework : EngineComponent, IUpdate, IDispose
    {
        ForceManager forceManager;
        ParticleManager particleManager;

        BroadPhase broadPhase;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new PhysicsD2Framework.
        /// </summary>
        public Framework() : base("PhysicsD2Framework") { }

        protected override void Initialize()
        {
            forceManager = new ForceManager();
            particleManager = new ParticleManager();
            broadPhase = new BroadPhase();

            base.Initialize();
        }

        public void _Update()
        {
            forceManager._Update();
            particleManager._Update();
            broadPhase._Update();
        }

        public void Dispose(bool finalize)
        {
            forceManager.Dispose(finalize);
            particleManager.Dispose(finalize);
            broadPhase.Dispose(finalize);
        }
    }
}
