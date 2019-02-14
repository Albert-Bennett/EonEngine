/* Created: 13/06/2013
 * Last Updated: 02/01/2015
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
using Eon.System.Tools;

namespace Eon.Rendering2D.Framework
{
    /// <summary>
    /// Defines a framework class that is used to manage the rendering of objects.
    /// </summary>
    public sealed class Framework : EngineComponent
    {
        PostProcessingLocal localPostProcessing;

        static TextureBuffer textureBuffer;

        /// <summary>
        /// The TextureBuffer for Rendering2D.
        /// </summary>
        public static TextureBuffer TextureBuffer
        {
            get { return textureBuffer; }
        }

        /// <summary>
        /// The ID of the output texture.
        /// </summary>
        public static string OutputTextureID
        {
            get { return "Final"; }
        }

        /// <summary>
        /// Creates a new D2RenderFramework.
        /// </summary>
        public Framework() : base("D2RenderFramework") { }

        protected override void Initialize()
        {
            new CameraManager2D();
            new DrawingManager();
            new PostRenderManager();

            localPostProcessing = new PostProcessingLocal(
                "D2PostProcessing", "Default2D", "Final", 3);

            new LightingManager2D();

            textureBuffer = new TextureBuffer("Default2D", 1, "Final");

            base.Initialize();
        }
    }
}
