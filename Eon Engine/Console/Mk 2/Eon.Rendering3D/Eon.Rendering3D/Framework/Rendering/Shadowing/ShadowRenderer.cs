/* Created 25/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework.Rendering.Lighting;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Rendering.Shadowing
{
    /// <summary>
    /// Used to define how shadows will be rendered.
    /// </summary>
    internal sealed class ShadowRenderer
    {
        List<ICastShadows> shadowCasters = new List<ICastShadows>();
        List<ShadowVolume> shadows = new List<ShadowVolume>();

        RenderTarget2D target;
        ScreenQuad quad;

        DepthStencilState dsPass;
        DepthStencilState dsFail;
        DepthStencilState dsKeep;

        BlendState shadowBlend;
        BlendState shadowedBlend;
        Effect shadowEffect;

        static float bias = 0.1f;

        /// <summary>
        /// The depth bias. Used to avoid floating point errors.
        /// </summary>
        public static float DepthBias
        {
            get { return bias; }
        }

        public ShadowRenderer()
        {
            shadowEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/3D/Shadowing");

            quad = new ScreenQuad();

            dsPass = new DepthStencilState()
            {
                DepthBufferWriteEnable = false,
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                ReferenceStencil = 0x1,
                StencilPass = StencilOperation.Increment
            };

            dsFail = new DepthStencilState()
            {
                DepthBufferWriteEnable = false,
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                ReferenceStencil = 0x1,
                StencilPass = StencilOperation.Decrement
            };

            dsKeep = new DepthStencilState()
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.LessEqual,
                ReferenceStencil = 0x1,
                StencilPass = StencilOperation.Keep
            };

            shadowBlend = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.Zero,
                AlphaDestinationBlend = Blend.One,
            };

            shadowedBlend = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.SourceAlpha,
                AlphaDestinationBlend = Blend.InverseSourceAlpha,
            };

            TextureQualityChanged();
        }

        public void Render()
        {
            shadows.Clear();

            CreateShadowVolumes();
            RenderShadowVolumes();
        }

        void CreateShadowVolumes()
        {
            shadowCasters = LightManager.GetShadowCasters();

            List<ModelComponent> models = null;
            ModelManager.GetVisableModels(out models);

            foreach(ICastShadows shad in shadowCasters)
                for (int i = 0; i < models.Count; i++)
                {
                    ShadowVolume shadow = new ShadowVolume();

                    shadow.Build(models[i], shad);

                    shadows.Add(shadow);
                }
        }

        void RenderShadowVolumes()
        {
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;
            Common.Device.DepthStencilState = dsPass;
            Common.Device.BlendState = shadowBlend;

            Common.Device.SetRenderTarget(target);

            Common.Device.Clear(ClearOptions.Stencil | ClearOptions.DepthBuffer |
                ClearOptions.Target, Color.Transparent, 1.0f, 0);

            for (int i = 0; i < shadowCasters.Count; i++)
            {
                //Matrix view = CameraManager.CurrentCamera.View;
                //Matrix proj = CameraManager.CurrentCamera.Projection;

                shadowEffect.Parameters["IViewProj"].SetValue(Matrix.Invert(shadowCasters[i].View * shadowCasters[i].Proj));
                //shadowEffect.Parameters["IViewProj"].SetValue(Matrix.Invert(view * proj));
                shadowEffect.Parameters["GDSize"].SetValue(Common.DefaultScreenResolution);

                shadowEffect.Parameters["DepthMap"].SetValue(TextureBuffer.GetTexture("DepthMap"));

                //for (int i = 0; i < shadows.Count; i++)
                {
                    shadows[i].Render();

                    shadowEffect.CurrentTechnique.Passes[0].Apply();
                }

                Common.Device.RasterizerState = RasterizerState.CullClockwise;

                Common.Device.DepthStencilState = dsFail;

                //for (int i = 0; i < shadows.Count; i++)
                {
                    shadows[i].Render();

                    shadowEffect.CurrentTechnique.Passes[0].Apply();
                }

                Common.Device.SetRenderTarget(null);

                Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;

                Common.Device.BlendState = shadowedBlend;
                Common.Device.DepthStencilState = dsKeep;

                quad.Render();

                shadowEffect.CurrentTechnique.Passes[0].Apply();
            }

            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.DepthStencilState = DepthStencilState.Default;

            TextureBuffer.SetBuffer("Shadows", target);
        }

        public void TextureQualityChanged()
        {
            int width = (int)Common.TextureQuality.X;
            int height = (int)Common.TextureQuality.Y;

            target = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Single, DepthFormat.Depth24);
        }
    }
}
