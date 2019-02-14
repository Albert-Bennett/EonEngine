/* Created 16/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.PostProcessing;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Eon.System.Management;

namespace Eon.Rendering3D.Framework
{
    /// <summary>
    /// Defines a framework class that is
    /// used to manage 3D rendering.
    /// </summary>
    public sealed class Framework : EngineComponent
    {
        PostProcessingLocal localPP;

        CameraManager cameraManager;

        public Framework() : base("Render3DFramework") 
        {
            new RenderManager();
        }

        protected override void Initialize()
        {
            cameraManager = new CameraManager();
            new LightManager();
            new ModelManager();

            localPP = new PostProcessingLocal("D3PostProcessing", 1);

            base.Initialize();
        }

        public CameraManager GetCameraManager()
        {
            return cameraManager;
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
