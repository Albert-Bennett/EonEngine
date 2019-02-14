/* Created: 19/06/2014
 * Last Updated: 10/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework.Billboards;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Rendering
{
    /// <summary>
    /// Used to render semi transparent objects in real time.
    /// </summary>
    internal sealed class DepthPeeling
    {
        #region Fields

        Effect merge;
        ScreenQuad quad;

        Vector2 halfPixel;

        Peel[] peels;

        RenderTarget2D depthTarget;
        RenderTarget2D opaqueTarget;
        RenderTarget2D colourTarget;

        int fragments = LODManager.LODFragments.Length;

        BillboardManager billboards;

        #endregion
        #region Ctor

        public DepthPeeling()
        {
            quad = new ScreenQuad();
            billboards = new BillboardManager();

            merge = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/MergePeels");
        }

        #endregion
        #region Rendering

        internal void Render()
        {
            List<IRenderable3D> renders;
            ModelManager.GetByRenderType(RenderTypes.SemiLPP, out renders);

            if (renders.Count > 0 || billboards.Count > 0)
            {
                Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;
                Common.Device.DepthStencilState = DepthStencilState.Default;

                Common.Device.BlendState = BlendState.Opaque;

                for (int i = 0; i < 4; i++)
                {
                    Common.Device.SetRenderTargets(peels[i].DepthTarget,
                        peels[i].OpaqueTarget, peels[i].ColourTarget);

                    Common.Device.Clear(Color.Transparent);

                    for (int j = 0; j < renders.Count; j++)
                    {
                        renders[j].SetParameter("MinDepth", LODManager.LODFragments[fragments - 2 - i]);
                        renders[j].SetParameter("MaxDepth", LODManager.LODFragments[fragments - 1 - i]);

                        renders[j].SetParameter("DepthMap", Framework.TextureBuffer.GetTexture("DepthMap"));

                        renders[j].SetParameter("HalfPixel", halfPixel);

                        renders[j]._Render("Render");
                    }

                    billboards.Render(LODManager.LODFragments[fragments - 2 - i],
                        LODManager.LODFragments[fragments - 1 - i]);

                    Common.Device.SetRenderTargets(null);
                }

                billboards.EndRender();

                Merge();
            }
        }

        void Merge()
        {
            Common.Device.BlendState = BlendState.Opaque;

            Common.Device.SetRenderTargets(depthTarget,
                opaqueTarget, colourTarget);

            Common.Device.Clear(Color.Transparent);

            merge.Parameters["HalfPixel"].SetValue(halfPixel);

            merge.Parameters["Scene"].SetValue(Framework.TextureBuffer.GetTexture("Scene"));
            merge.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));
            merge.Parameters["OpaqueMap"].SetValue(Framework.TextureBuffer.GetTexture("OpaqueMap"));

            for (int i = 0; i < 4; i++)
            {
                merge.Parameters["D" + i].SetValue(peels[i].DepthTarget);
                merge.Parameters["O" + i].SetValue(peels[i].OpaqueTarget);
                merge.Parameters["C" + i].SetValue(peels[i].ColourTarget);
            }

            merge.Techniques[0].Passes[0].Apply();
            quad.Render();

            Framework.TextureBuffer.SetBuffer("Scene", colourTarget);
            Framework.TextureBuffer.SetBuffer("DepthMap", depthTarget);
            Framework.TextureBuffer.SetBuffer("OpaqueMap", opaqueTarget);
        }

        #endregion
        #region Misc

        public void TextureQualityChanged(int width, int height, Vector2 halfPixel)
        {
            if (peels == null)
            {
                peels = new Peel[fragments - 1];

                for (int i = 0; i < peels.Length; i++)
                    peels[i] = new Peel(width, height);
            }
            else
                for (int i = 0; i < peels.Length; i++)
                    peels[i].Reload(width, height);

            depthTarget = new RenderTarget2D(Common.Device, width, height,
               false, SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);

            opaqueTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            colourTarget = new RenderTarget2D(Common.Device, width, height,
               false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            this.halfPixel = halfPixel;
        }

        #endregion
    }
}
