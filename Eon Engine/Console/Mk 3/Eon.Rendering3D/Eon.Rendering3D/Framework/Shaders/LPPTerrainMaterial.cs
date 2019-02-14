/* Created: 21/02/2015
 * Last Updated: 08/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Shaders
{
    /// <summary>
    /// Used to define the LOD LPP terrain shader. 
    /// At Eon/Shaders/Materials/TerrainLPP.
    /// </summary>
    public sealed class LPPTerrainMaterial : Shader
    {
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// The amount of times the textures are to be repeated at LOD 0.
        /// </summary>
        public int TextureRepeats = 16;

        /// <summary>
        /// The filepath of the weight map to be used in the shader.
        /// </summary>
        public string WeightMapFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// The filepath of the red texture to be used in the shader.
        /// </summary>
        public string RFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// The filepath of the red texture normal map to be used in the shader.
        /// </summary>
        public string RNFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// The filepath of the green texture to be used in the shader.
        /// </summary>
        public string GFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// The filepath of the green texture normal map to be used in the shader.
        /// </summary>
        public string GNFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// The filepath of the blue texture to be used in the shader.
        /// </summary>
        public string BFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// The filepath of the blue texture normal map to be used in the shader.
        /// </summary>
        public string BNFilepath = "Eon/Textures/DefaultTexture";

        protected override void _Load()
        {
            shaderFilepath = "Eon/Shaders/Materials/TerrainLPP";

            textures.Add("WeightMap", LoadTexture(WeightMapFilepath));

            textures.Add("R", LoadTexture(RFilepath));
            textures.Add("G", LoadTexture(GFilepath));
            textures.Add("B", LoadTexture(BFilepath));

            textures.Add("RN", LoadTexture(RNFilepath));
            textures.Add("GN", LoadTexture(GNFilepath));
            textures.Add("BN", LoadTexture(BNFilepath));

            defaultTechnique = "Opaque";

            renderType = RenderTypes.LPP;

            base._Load();
        }

        public override void SetParameters(Matrix worldMatrix)
        {
            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["TextureRepeats"].SetValue(TextureRepeats);

            foreach (string s in textures.Keys)
            {
                Texture2D tex = null;
                textures.TryGetValue(s, out tex);

                Effect.Parameters[s].SetValue(tex);
            }
        }
    }
}
