/* Created: 12/05/2014
 * Last Updated: 14/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Rendering
{
    /// <summary>
    /// Defines the lighting model that Eon Engine is using.
    /// </summary>
    internal sealed class LightingPrePass
    {
        #region Fields

        ScreenQuad quad;

        RenderTarget2D opaqueTarget;
        RenderTarget2D depthTarget;
        RenderTarget2D lightTarget;
        RenderTarget2D colourTarget;

        RenderTarget2D composed;

        Effect clearGBuffer;
        Effect compose;

        Effect pointLightEffect;
        Effect directionalLightEffect;
        Effect spotLightEffect;

        Model pointLightModel;
        Model spotLightModel;

        BlendState lightingBlend;

        Texture2D blank;

        #endregion
        #region Ctor/ Init

        public LightingPrePass() { }

        public void Initialize()
        {
            quad = new ScreenQuad();

            lightingBlend = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.One,
                AlphaDestinationBlend = Blend.One,

                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.One,
                ColorDestinationBlend = Blend.One
            };

            clearGBuffer = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/ClearGBuffer");
            compose = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/Compose");

            pointLightEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/PointLight");
            directionalLightEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/DirectionalLight");
            spotLightEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/SpotLight");

            pointLightModel = Common.ContentBuilder.Load<Model>("Eon/Models/PointLightModel");
            spotLightModel = Common.ContentBuilder.Load<Model>("Eon/Models/SpotLightModel");

            blank = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/Pixel");
        }
        #endregion
        #region Rendering
        #region Clear/ Render

        public void RenderOpaque()
        {
            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.DepthStencilState = DepthStencilState.Default;
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;

            ClearGBuffer();
            RenderLPP();
        }

        public void RenderLighting()
        {
            RenderLights();
            Compose();
        }

        void ClearGBuffer()
        {
            Common.Device.DepthStencilState = DepthStencilState.DepthRead;

            Common.Device.SetRenderTargets(depthTarget,
                opaqueTarget, colourTarget);

            clearGBuffer.CurrentTechnique.Passes[0].Apply();

            Common.Device.DepthStencilState = DepthStencilState.Default;
        }

        void RenderLPP()
        {
            ModelManager.Render(RenderTypes.LPP, "Opaque");

            Common.Device.SetRenderTargets(null);

            Framework.TextureBuffer.SetBuffer("DepthMap", depthTarget);
            Framework.TextureBuffer.SetBuffer("OpaqueMap", opaqueTarget);
            Framework.TextureBuffer.SetBuffer("Scene", colourTarget);
        }

        #endregion
        #region Light Rendering

        void RenderLights()
        {
            Matrix invView = Matrix.Invert(CameraManager.CurrentCamera.View);
            Matrix viewProj = CameraManager.CurrentCamera.View * CameraManager.CurrentCamera.Projection;
            Matrix invViewProj = Matrix.Invert(viewProj);

            Common.Device.BlendState = lightingBlend;
            Common.Device.SetRenderTarget(lightTarget);
            Common.Device.Clear(Color.Transparent);

            RenderDirectionalLights(invView, invViewProj);

            Common.Device.RasterizerState = RasterizerState.CullClockwise;

            RenderPointLights(invView, invViewProj, viewProj);
            RenderMeshLights(invView, invViewProj, viewProj);
            RenderSpotLights(invView, invViewProj, viewProj);

            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;
            Common.Device.DepthStencilState = DepthStencilState.Default;

            Common.Device.SetVertexBuffer(null);
            Common.Device.Indices = null;

            Common.Device.SetRenderTargets(null);
        }

        void RenderDirectionalLights(Matrix invView, Matrix invViewProj)
        {
            List<Eon.Rendering3D.Framework.Rendering.Lighting.DirectionalLight> lights =
                LightManager.GetLights<Eon.Rendering3D.Framework.Rendering.Lighting.DirectionalLight>();

            if (lights.Count > 0)
            {
                directionalLightEffect.Parameters["IViewProj"].SetValue(invViewProj);

                directionalLightEffect.Parameters["GDSize"].SetValue(Common.TextureQuality);

                directionalLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("OpaqueMap"));
                directionalLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                for (int i = 0; i < lights.Count; i++)
                {
                    directionalLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    directionalLightEffect.Parameters["Direction"].SetValue(lights[i].Direction);
                    directionalLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    directionalLightEffect.CurrentTechnique.Passes[0].Apply();

                    quad.Render();
                }
            }
        }

        void RenderPointLights(Matrix invView, Matrix invViewProj, Matrix viewProj)
        {
            List<PointLight> lights = LightManager.GetLights<PointLight>();

            if (lights.Count > 0)
            {
                pointLightEffect.Parameters["IViewProj"].SetValue(invViewProj);

                pointLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                pointLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("OpaqueMap"));
                pointLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                Common.Device.SetVertexBuffer(pointLightModel.Meshes[0].MeshParts[0].VertexBuffer);
                Common.Device.Indices = pointLightModel.Meshes[0].MeshParts[0].IndexBuffer;

                for (int i = 0; i < lights.Count; i++)
                {
                    pointLightEffect.Parameters["World"].SetValue(lights[i].World);
                    pointLightEffect.Parameters["Pos"].SetValue(lights[i].World.Translation);
                    pointLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    pointLightEffect.Parameters["Radius"].SetValue(lights[i].Radius);
                    pointLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    pointLightEffect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        pointLightModel.Meshes[0].MeshParts[0].NumVertices,
                        pointLightModel.Meshes[0].MeshParts[0].StartIndex,
                        pointLightModel.Meshes[0].MeshParts[0].PrimitiveCount);
                }
            }
        }

        void RenderMeshLights(Matrix invView, Matrix invViewProj, Matrix viewProj)
        {
            List<MeshLight> lights = LightManager.GetLights<MeshLight>();

            if (lights.Count > 0)
            {
                pointLightEffect.Parameters["IViewProj"].SetValue(invViewProj);

                pointLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                pointLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("OpaqueMap"));
                pointLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                for (int i = 0; i < lights.Count; i++)
                    for (int j = 0; j < lights[i].MeshVolume.Meshes.Count; j++)
                        for (int k = 0; k < lights[i].MeshVolume.Meshes[j].MeshParts.Count; k++)
                        {
                            Common.Device.SetVertexBuffer(lights[i].MeshVolume.Meshes[j].MeshParts[k].VertexBuffer);
                            Common.Device.Indices = lights[i].MeshVolume.Meshes[j].MeshParts[k].IndexBuffer;

                            pointLightEffect.Parameters["World"].SetValue(lights[i].World);
                            pointLightEffect.Parameters["Pos"].SetValue(lights[i].World.Translation);
                            pointLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                            pointLightEffect.Parameters["Radius"].SetValue(lights[i].Size);
                            pointLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                            pointLightEffect.CurrentTechnique.Passes[0].Apply();

                            Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].NumVertices,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].StartIndex,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].PrimitiveCount);
                        }
            }
        }

        void RenderSpotLights(Matrix invView, Matrix invViewProj, Matrix viewProj)
        {
            List<SpotLight> lights = LightManager.GetLights<SpotLight>();

            if (lights.Count > 0)
            {
                spotLightEffect.Parameters["IViewProj"].SetValue(invViewProj);

                spotLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                spotLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("OpaqueMap"));
                spotLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                Common.Device.SetVertexBuffer(spotLightModel.Meshes[0].MeshParts[0].VertexBuffer);
                Common.Device.Indices = spotLightModel.Meshes[0].MeshParts[0].IndexBuffer;

                for (int i = 0; i < lights.Count; i++)
                {
                    spotLightEffect.Parameters["World"].SetValue(lights[i].World);
                    spotLightEffect.Parameters["Pos"].SetValue(lights[i].Position);

                    spotLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    spotLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    spotLightEffect.Parameters["Direction"].SetValue(lights[i].Direction);
                    spotLightEffect.Parameters["FallOff"].SetValue(lights[i].FallOff);
                    spotLightEffect.Parameters["OuterConeAngle"].SetValue(lights[i].OuterConeAngle);
                    spotLightEffect.Parameters["InnerConeAngle"].SetValue(lights[i].InnerConeAngle);

                    spotLightEffect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        spotLightModel.Meshes[0].MeshParts[0].NumVertices,
                        spotLightModel.Meshes[0].MeshParts[0].StartIndex,
                        spotLightModel.Meshes[0].MeshParts[0].PrimitiveCount);

                    quad.Render();
                }
            }
        }

        #endregion
        #region Composition

        void Compose()
        {
            Common.Device.BlendState = BlendState.Opaque;

            Common.Device.SetRenderTargets(composed);
            Common.Device.Clear(Color.Transparent);

            compose.Parameters["ColourMap"].SetValue(Framework.TextureBuffer.GetTexture("Scene"));
            compose.Parameters["LightMap"].SetValue(lightTarget);

            Texture2D shadowMap = null;

            try
            {
                shadowMap = Framework.TextureBuffer.GetTexture("Shadows");
            }
            catch 
            {
                shadowMap = blank;
            }

            compose.Parameters["ShadowMap"].SetValue(shadowMap);

            compose.CurrentTechnique.Passes[0].Apply();
            quad.Render();

            Common.Device.SetRenderTargets(null);

            Framework.TextureBuffer.SetBuffer("Scene", composed);
        }

        #endregion
        #endregion
        #region Misc

        public void TextureQualityChanged(int width, int height, Vector2 halfPixel)
        {
            pointLightEffect.Parameters["HalfPixel"].SetValue(halfPixel);
            spotLightEffect.Parameters["HalfPixel"].SetValue(halfPixel);

            compose.Parameters["HalfPixel"].SetValue(halfPixel);

            depthTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Single, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);

            opaqueTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.HalfVector4, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            lightTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            colourTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            composed = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
        }

        #endregion
    }
}
