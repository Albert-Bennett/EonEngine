/* Created: 26/03/2015
 * Last Updated: 26/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering3D.Framework.Materials
{
    /// <summary>
    /// Used to define a water material that has an animaterd flow.
    /// At Eon/Shaders/Materials/WaterMaterial.
    /// </summary>
    public sealed class WaterMaterial : Material
    {
        Texture2D texture;
        Texture2D flowMap;
        Texture2D n0Map;
        Texture2D n1Map;
        Texture2D specMap;

        Texture noiseMap;

        float halfCycle = 0.5f;
        TimeSpan currentTime;
        TimeSpan maxTime;

        /// <summary>
        /// Filepath for the material's texture.
        /// </summary>
        public string TextureFilepath = "Eon/Textures/DefaultTexture";

        /// <summary>
        /// Filepath for the material's normal map.
        /// </summary>
        public string NormalMap0Filepath = "Eon/Textures/DefaultNormalMap";

        /// <summary>
        /// Filepath for the material's normal map.
        /// </summary>
        public string NormalMap1Filepath = "Eon/Textures/DefaultNormalMap";

        /// <summary>
        /// Filepath for the material's normal map.
        /// </summary>
        public string FlowMapFilepath = "Eon/Textures/Pixel";

        /// <summary>
        /// The amount of time it takes befor the animation is reset.
        /// </summary>
        public float MaxCycle;

        /// <summary>
        /// Filepath for the material's specular map.
        /// </summary>
        public string SpecularMapFilepath = "Eon/Textures/DefaultSpecularMap";

        protected override void _Load()
        {
            shaderFilepath = "Eon/Shaders/Materials/WaterMaterial";

            noiseMap = LoadTexture("Eon/Textures/NoiseMap");

            texture = LoadTexture(TextureFilepath);
            flowMap = LoadTexture(FlowMapFilepath);
            n0Map = LoadTexture(NormalMap0Filepath);
            n1Map = LoadTexture(NormalMap1Filepath);
            specMap = LoadTexture(SpecularMapFilepath);

            defaultTechnique = "Render";

            renderType = RenderTypes.LPP;

            maxTime = TimeSpan.FromSeconds(MaxCycle);

            base._Load();
        }

        public override void SetParameters(Matrix worldMatrix)
        {
            Effect.Parameters["World"].SetValue(worldMatrix);
            Effect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
            Effect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

            Effect.Parameters["FlowOffset0"].SetValue((float)currentTime.TotalSeconds);
            Effect.Parameters["FlowOffset1"].SetValue((float)currentTime.TotalSeconds - halfCycle);
            Effect.Parameters["HalfCycle"].SetValue(halfCycle);
            Effect.Parameters["GDSize"].SetValue(flowMap.Bounds.Size.ToVector2());

            Effect.Parameters["Texture"].SetValue(texture);
            Effect.Parameters["SpecularMap"].SetValue(specMap);
            Effect.Parameters["FlowMap"].SetValue(flowMap);
            Effect.Parameters["NormalMap0"].SetValue(n0Map);
            Effect.Parameters["NormalMap1"].SetValue(n1Map);
            Effect.Parameters["NoiseMap"].SetValue(noiseMap);

            currentTime += Common.ElapsedTimeDelta;

            if (currentTime > maxTime)
                currentTime = TimeSpan.FromSeconds(halfCycle);
        }
    }
}
