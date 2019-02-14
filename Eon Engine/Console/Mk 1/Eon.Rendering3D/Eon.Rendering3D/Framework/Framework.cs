/* Created 16/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.PostProcessing;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Lighting;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework
{
    /// <summary>
    /// Defines a framework class that is
    /// used to manage 3D rendering.
    /// </summary>
    public sealed class Framework : EngineComponent
    {
        PostProcessingLocal localPP;

        public Framework() : base("D3RenderFramework") { }

        protected override void Initialize()
        {
            new CameraManager();
            new LightManager();
            localPP = new PostProcessingLocal("D3PostProcessing", 2);

            base.Initialize();
        }

        /// <summary>
        /// Sets various textures for any post
        /// processing that needs to be done.
        /// </summary>
        /// <param name="scene">The scene texture.</param>
        /// <param name="opaque">The opaque texture.</param>
        /// <param name="depth">The depth map for the scene.</param>
        /// <param name="distortion">The distortion texture.</param>
        public void SetPostProcessing(Texture2D scene,
            Texture2D opaque, Texture2D depth, Texture2D distortion)
        {
            localPP.SetTextures(scene, opaque, depth, distortion);
        }

        /// <summary>
        /// Get the local processing for 3D rendering.
        /// </summary>
        /// <returns>PostProcessingLocal for 3D rendering.</returns>
        public PostProcessingLocal GetLocalPostProcessing()
        {
            return localPP;
        }

        /// <summary>
        /// A check to see if a PostProcessingLocal for 3D exists.
        /// </summary>
        /// <returns>The result of the check.</returns>
        public bool PostProcessingExists()
        {
            if (localPP.Processes > 0)
                return true;
            else
                return false;
        }
    }
}
