/* Created: 04/09/2015
 * Last Updated: 04/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Materials.Special
{
    /// <summary>
    /// Used to define the basic lpp shader.
    /// At Eon/Shaders/Materials/BasicLPPMaterial.
    /// </summary>
    public sealed class RainGlassMaterial : Material
    {
        Texture2D distortionMap;

        /// <summary>
        /// Filepath for the material's texture.
        /// </summary>
        public string DistortionMap = "Eon/Textures/DefaultTexture";

        protected override void _Load()
        {
            shaderFilepath = "Eon/Shaders/Materials/Special/RainGlass";

            distortionMap = LoadTexture(DistortionMap);

            defaultTechnique = "Render";

            renderType = RenderTypes.LPP;

            base._Load();
        }

        public override void SetParameters(Matrix worldMatrix)
        {
            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["Distortion"].SetValue(distortionMap);
            Effect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));
        }
    }
}
