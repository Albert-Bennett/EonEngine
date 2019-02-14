/* Created: 05/12/2014
 * Last Updated: 13/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Shaders
{
    /// <summary>
    /// Used to define a dual texture lpp shader.
    /// At Eon/Shaders/Materials/DTLPPMaterial.
    /// </summary>
    public sealed class DTLPPMaterial : Shader
    {
        Texture2D texture;
        Texture2D secondary;
        Texture2D normalMap;
        Texture2D specMap;

        /// <summary>
        /// Filepath for the material's texture.
        /// </summary>
        public string TextureFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// Filepath for the material's secondary texture.
        /// </summary>
        public string SecondaryTextureFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// Filepath for the material's normal map.
        /// </summary>
        public string NormalMapFilepath = "Eon/Textures/DefaultNormalMap";

        /// <summary>
        /// Filepath for the material's specular map.
        /// </summary>
        public string SpecularMapFilepath = "Eon/Textures/DefaultSpecularMap";

        protected override void _Load()
        {
            shaderFilepath = "Eon/Shaders/Materials/DTLPPMaterial";

            texture = LoadTexture(TextureFilepath);
            secondary = LoadTexture(SecondaryTextureFilepath);
            normalMap = LoadTexture(NormalMapFilepath);
            specMap = LoadTexture(SpecularMapFilepath);

            defaultTechnique = "Render";

            renderType = RenderTypes.LPP;

            base._Load();
        }

        public override void SetParameters(Matrix worldMatrix)
        {
            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["Texture"].SetValue(texture);
            Effect.Parameters["Texture2"].SetValue(secondary);
            Effect.Parameters["NormalMap"].SetValue(normalMap);
            Effect.Parameters["SpecularMap"].SetValue(specMap);
        }
    }
}
