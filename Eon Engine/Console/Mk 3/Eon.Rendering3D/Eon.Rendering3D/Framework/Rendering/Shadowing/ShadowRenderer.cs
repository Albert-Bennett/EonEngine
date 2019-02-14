/* Created: 05/10/2014
 * Last Updated: 12/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.System.Resolution;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D.Framework.Rendering.Shadowing
{
    /// <summary>
    /// Used to define a renderer of shadows.
    /// </summary>
    internal sealed class ShadowRenderer
    {
        static List<ShadowEntry> shadows = new List<ShadowEntry>();

        Effect createMap;
        Effect createOcclusionMap;

        ScreenQuad screenQuad;

        Texture2D blank;

        PCFFiltering filter = PCFFiltering.X7;

        #region Testing

        float depthBias = 0.08f;

        public float Bias
        {
            get { return depthBias; }
            set { depthBias = value; }
        }

        #endregion

        public ShadowRenderer()
        {
            createMap = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/Shadowing/ShadowMap");
            createOcclusionMap = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/Shadowing/OcclusionMap");

            screenQuad = new ScreenQuad();

            blank = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/Pixel");
        }

        public void Render()
        {
            List<MeshPart> models;
            ModelManager.GetVisableModels(out models);

            ShadowEntry[] visable = (from s in shadows
                                     where s.IsUsed
                                     select s).ToArray();

            if (visable.Length > 0)
            {
                Common.Device.DepthStencilState = DepthStencilState.Default;
                Common.Device.RasterizerState.CullMode = CullMode.CullCounterClockwiseFace;

                CascadingSplits. CalculateSpitDepths();

                for (int i = 0; i < visable.Length; i++)
                {
                    Common.Device.SetRenderTarget(visable[i].ShadowMap);
                    Common.Device.Clear(Color.Transparent);

                    Viewport vp = Common.Device.Viewport;

                    for (int k = 0; k < CascadingSplits.Splits; k++)
                    {
                        Viewport splitView = new Viewport()
                        {
                            Width = (int)ShadowEntry.ShadowSize.X,
                            Height = (int)ShadowEntry.ShadowSize.Y,
                            MinDepth = 0,
                            MaxDepth = 1,
                            X = k * (int)ShadowEntry.ShadowSize.X,
                            Y = 0
                        };

                        Common.Device.Viewport = splitView;

                        visable[i].CalculateFrustum(CascadingSplits.SplitDepths);

                        createMap.Parameters["ViewProj"].SetValue(visable[i].SplitViewProjection[k]);

                        for (int j = 0; j < models.Count; j++)
                            if (visable[i].Shadows(models[j].BoundingSphere))
                                models[j].Render(createMap);
                    }

                    Common.Device.Viewport = vp;

                    Common.Device.SetRenderTarget(null);

                    CreateOcclusion(visable[i]);
                }

                //Testing
                Framework.TextureBuffer.SetBuffer("ShadowDepths", visable[0].ShadowMap);

                Framework.TextureBuffer.SetBuffer("Shadows", visable[0].Occlusion);
            }
            else
            {
                Framework.TextureBuffer.SetBuffer("Shadows", blank);

                //Testing
                Framework.TextureBuffer.SetBuffer("ShadowDepths", blank);
            }
        }

        void CreateOcclusion(ShadowEntry shadowTarget)
        {
            Common.Device.SetRenderTarget(shadowTarget.Occlusion);
            Common.Device.Clear(Color.Black);

            createOcclusionMap.Parameters["FilterSize"].SetValue((int)filter);
            createOcclusionMap.Parameters["MinShadow"].SetValue(0.5f);

            createOcclusionMap.Parameters["InvView"].SetValue(Matrix.Invert(CameraManager.CurrentCamera.View));
            createOcclusionMap.Parameters["SplitViewProj"].SetValue(shadowTarget.SplitViewProjection);
            createOcclusionMap.Parameters["ClipPlanes"].SetValue(CascadingSplits.ClipPlanes);
            createOcclusionMap.Parameters["FrustumCorners"].SetValue(shadowTarget.FarCorners);

            createOcclusionMap.Parameters["DepthBias"].SetValue(depthBias);
            createOcclusionMap.Parameters["ShadowMapSize"].SetValue(ShadowEntry.ShadowSize);
            createOcclusionMap.Parameters["ShadowMap"].SetValue(shadowTarget.ShadowMap);
            createOcclusionMap.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

            createOcclusionMap.CurrentTechnique.Passes[0].Apply();
            screenQuad.Render();

            Common.Device.SetRenderTarget(null);
        }

        #region Misc

        internal static void Add(ShadowEntry entry)
        {
            shadows.Add(entry);
        }

        internal static void Remove(ShadowEntry entry)
        {
            shadows.Remove(entry);
        }

        public void TextureQualityChanged()
        {
            switch (Common.CurrentTextureQuality)
            {
                case TextureQuality.LowQuality:
                    filter = PCFFiltering.X3;
                    break;

                case TextureQuality.MediumQuality:
                    filter = PCFFiltering.X4;
                    break;

                case TextureQuality.HighQuality:
                    filter = PCFFiltering.X5;
                    break;

                case TextureQuality.VeryHighQuality:
                    filter = PCFFiltering.X6;
                    break;

                case TextureQuality.UltraQuality:
                    filter = PCFFiltering.X7;
                    break;
            }

            for (int i = 0; i < shadows.Count; i++)
                shadows[i].ScreenResolutionChanged();
        }

        #endregion
    }
}
