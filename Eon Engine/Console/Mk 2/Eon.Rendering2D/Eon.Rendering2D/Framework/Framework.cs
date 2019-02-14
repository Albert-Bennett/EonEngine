/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.PostProcessing;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Drawing;
using Eon.Rendering2D.Lighting;
using Eon.System.Management;

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
            new CameraManager2D();
            new DrawingManager();
            new PostRenderManager();
            localPostProcessing = new PostProcessingLocal("D2PostProcessing", 4);
            new LightingManager2D();

            try
            {
                RenderSettings settings = XmlHelper.Deserialize<
                    RenderSettings>("RenderSettings2D", ".xml", true);

                for (int i = 0; i < settings.PostProcesses.Length; i++)
                    AssemblyManager.CreateInstance(settings.PostProcesses[i]);
            }
            catch { }

            base.Initialize();
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
