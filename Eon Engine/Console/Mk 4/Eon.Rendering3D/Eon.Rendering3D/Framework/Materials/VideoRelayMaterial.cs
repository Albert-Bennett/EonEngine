/* Created 01/04/2015
 * Last Updated: 01/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Media;
using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Materials
{
    /// <summary>
    /// Used to define a material that sets the texture
    /// of the model to the current frame in a video.
    /// At Eon/Shaders/Materials/BasicLPPMaterial.
    /// </summary>
    public sealed class VideoRelayMaterial : Material
    {
        Texture2D normalMap;
        Texture2D specMap;

        Video vid;

        /// <summary>
        /// Filepath for the material's normal map.
        /// </summary>
        public string NormalMapFilepath = "Eon/Textures/DefaultNormalMap";

        /// <summary>
        /// Filepath for the material's specular map.
        /// </summary>
        public string SpecularMapFilepath = "Eon/Textures/DefaultSpecularMap";

        /// <summary>
        /// Filepath for the material's video.
        /// </summary>
        public string VideoFilepath = "";

        protected override void _Load()
        {
            shaderFilepath = "Eon/Shaders/Materials/BasicLPPMaterial";

            normalMap = LoadTexture(NormalMapFilepath);
            specMap = LoadTexture(SpecularMapFilepath);

            vid = new Eon.Engine.Media.Video(VideoFilepath);
            vid.IsLooping = true;
            vid.Play();

            defaultTechnique = "Render";

            renderType = RenderTypes.LPP;

            base._Load();
        }

        public override void SetParameters(Matrix worldMatrix)
        {
            vid.Update();

            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["Texture"].SetValue(vid.CurrentFrame);
            Effect.Parameters["NormalMap"].SetValue(normalMap);
            Effect.Parameters["SpecularMap"].SetValue(specMap);
        }
    }
}
