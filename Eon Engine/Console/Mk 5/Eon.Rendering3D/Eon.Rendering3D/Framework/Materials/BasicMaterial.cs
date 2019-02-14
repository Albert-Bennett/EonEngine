/* Created: 26/09/2014
 * Last Updated: 25/02/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Materials
{
    /// <summary>
    /// Used to define the basic effect shader.
    /// At Eon/Shaders/Materials/BasicEffect.
    /// </summary>
    public sealed class BasicMaterial : Material
    {
        Texture2D colourMap;
        Texture2D normalMap;

        /// <summary>
        /// The filepath of the texture to be used in the shader.
        /// </summary>
        public string TextureFilepath = "Eon\\Textures\\DefaultTexture";

        /// <summary>
        /// The filepath of the normal map to be used in the material.
        /// </summary>
        public string NormalMapFilepath = "Eon\\Textures\\DefaultNormalMap";

        protected override void _Load()
        {
            shaderFilepath = "Eon/Shaders/Materials/BasicEffect";

            colourMap = LoadTexture(TextureFilepath);
            normalMap = LoadTexture(NormalMapFilepath);

            defaultTechnique = "Render";

            renderType = RenderTypes.SemiLPP;

            base._Load();
        }

        public override void SetParameters(Matrix worldMatrix)
        {
            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["ColourMap"].SetValue(colourMap);
            Effect.Parameters["NormalMap"].SetValue(normalMap);
        }
    }
}
