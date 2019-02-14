/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.PostProcessing;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;
using Eon.Rendering2D.Lighting;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D.Framework
{
    /// <summary>
    /// Defines a framework class that is used to manage the rendering of objects.
    /// </summary>
    public sealed class Framework : EngineComponent
    {
        PostProcessingLocal localPostProcessing;

        /// <summary>
        /// Creates a new D2RenderFramework.
        /// </summary>
        public Framework() : base("D2RenderFramework") { }

        protected override void Initialize()
        {
            new CameraManager();
            new DrawingManager();
            new PostRenderManager();
            localPostProcessing = new PostProcessingLocal("D2PostProcessing", 4);
            new LightingManager();

            base.Initialize();
        }

        /// <summary>
        /// Sets various textures for any post
        /// processing that needs to be done.
        /// </summary>
        /// <param name="scene">The scene texture.</param>
        /// <param name="opaque">The opaque texture.</param>
        /// <param name="distortion">The distortion texture.</param>
        public void SetPostProcessing(Texture2D scene,
            Texture2D opaque, Texture2D distortion)
        {
            localPostProcessing.SetTextures(scene, opaque, null, distortion);
        }

        /// <summary>
        /// Get the local processing for 2D rendering.
        /// </summary>
        /// <returns>PostProcessingLocal for 2D rendering.</returns>
        public PostProcessingLocal GetLocalPostProcessing()
        {
            return localPostProcessing;
        }

        /// <summary>
        /// A check to see if a PostProcessingLocal for 2D exists.
        /// </summary>
        /// <returns>The result of the check.</returns>
        public bool PostProcessingExists()
        {
            if (localPostProcessing.Processes > 0)
                return true;
            else
                return false;
        }
    }
}
