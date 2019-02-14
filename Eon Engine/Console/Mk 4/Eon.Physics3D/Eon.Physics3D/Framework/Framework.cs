/* Created: 05/09/2014
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics3D.Particles.Forces;
using Eon.System.Management;

namespace Eon.Physics3D.Framework
{
    /// <summary>
    /// Defines the 3D Physics framework. 
    /// </summary>
    public sealed class Framework : EngineModule
    {
        public Framework() : base("Physics3DFramework") { }

        protected override void Initialize()
        {
            new ForceManager();

            base.Initialize();
        }
    }
}
