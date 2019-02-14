/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;

namespace Eon.Rendering2D.Framework
{
    /// <summary>
    /// Defines a framework class that is used to manage the rendering of objects.
    /// </summary>
    public sealed class Framework : EngineComponent
    {
        /// <summary>
        /// Creates a new D2RenderFramework.
        /// </summary>
        public Framework() : base("D2RenderFramework") { }

        protected override void Initialize()
        {
            new CameraManager();
            new DrawingManager();
            new PostRenderManager();

            base.Initialize();
        }
    }
}
