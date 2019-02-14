/* Created: 13/06/2013
 * Last Updated: 03/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

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
    public sealed class Framework : EngineModule
    {
        static PostProcessingLocal localPP;
        static PostProcessingLocal postLocalPP;

        static TextureBuffer textureBuffer;
        static TextureBuffer postTextureBuffer;

        /// <summary>
        /// The TextureBuffer for Rendering2D.
        /// </summary>
        public static TextureBuffer TextureBuffer
        {
            get { return textureBuffer; }
        }

        /// <summary>
        /// The TextureBuffer for post rendering.
        /// </summary>
        public static TextureBuffer PostTextureBuffer
        {
            get { return postTextureBuffer; }
        }

        /// <summary>
        /// The PostProcessingLocal for the main rendering pass.
        /// </summary>
        public static PostProcessingLocal MainPostProcessing
        {
            get { return localPP; }
        }

        /// <summary>
        /// The PostProcessingLocal for the post rendering pass.
        /// </summary>
        public static PostProcessingLocal PostPostProcessing
        {
            get { return postLocalPP; }
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

            textureBuffer = new TextureBuffer("Default2D", 2, "Final");
            postTextureBuffer = new TextureBuffer("Post2D", 2, "Final");

            localPP = new PostProcessingLocal(
                "D2PostProcessing", textureBuffer, 3);

            postLocalPP = new PostProcessingLocal(
                "D2PostPostProcessing", postTextureBuffer, 4);

            new LightingManager2D();

            base.Initialize();
        }
    }
}
