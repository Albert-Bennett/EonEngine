/* Created 19/06/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Rendering
{
    internal sealed class ForwardRendering
    {
        RenderTarget2D colourTarget;
        RenderTarget2D depthTarget;

        BlendState blend;

        public ForwardRendering()
        {
            blend = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaDestinationBlend = Blend.InverseSourceAlpha,
                AlphaSourceBlend = Blend.SourceAlpha,

                ColorBlendFunction = BlendFunction.Add,
                ColorDestinationBlend = Blend.InverseSourceAlpha,
                ColorSourceBlend = Blend.SourceAlpha
            };
        }

        public void Render()
        {
            Common.Device.RasterizerState = RasterizerState.CullNone;

            Common.Device.BlendState = blend;

            Common.Device.SetRenderTargets(colourTarget, depthTarget);
            Common.Device.Clear(Color.Transparent);

            ModelManager.Render(RenderTypes.ForwardRender);
            ModelManager.Render(RenderTypes.Transparency);

            Common.Device.SetRenderTargets(null);

            Common.Device.DepthStencilState = DepthStencilState.Default;
            Common.Device.BlendState = BlendState.Opaque;

            TextureBuffer.SetBuffer("FRColourMap", colourTarget);
            TextureBuffer.SetBuffer("FRDepthMap", depthTarget);
        }

        public void TextureQualityChanged(int width, int height)
        {
            colourTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.Depth24);

            depthTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None);
        }
    }
}
