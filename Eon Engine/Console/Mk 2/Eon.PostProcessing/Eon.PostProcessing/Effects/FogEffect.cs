/* Created 23/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// The ways in which the FogEffect can be rendered.
    /// </summary>
    public enum FogType
    {
        Quick,
        Thick,
        Thickest
    }

    /// <summary>
    /// Used to define a fog effect. 
    /// </summary>
    public sealed class FogEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        float fogStart;
        float fogEnd;
        float fogThickness;

        string currentTechnique;

        Vector3 fogColour;

        EngineComponent cameraManager;

        public FogEffect(Vector3 fogColour, float fogThickness,
            float fogStart, float fogEnd, FogType fogType)
            : base("FogPP", 0, "Render3DFramework")
        {
            try
            {
                cameraManager = (EngineComponent)EngineComponentManager.Find(
                    "Render3DFramework").SendMessage("GetCameraManager", null);
            }
            catch
            {
                cameraManager = null;
            }

            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/Fog");

            currentTechnique = Enum.GetName(typeof(FogType), fogType);

            this.fogColour = fogColour;
            this.fogEnd = fogEnd;
            this.fogStart = fogStart;
            this.fogThickness = fogThickness;
        }

        protected override void Render()
        {
            if (cameraManager != null)
            {
                Common.Device.SetRenderTarget(target);

                ObjectComponent camera = (ObjectComponent)cameraManager.SendMessage("CurrentCamera", null);

                Matrix view = (Matrix)camera.SendMessage("View", null);
                Matrix proj = (Matrix)camera.SendMessage("Projection", null);

                Matrix iView = Matrix.Invert(view * proj);

                effect.Parameters["IViewProj"].SetValue(iView);
                effect.Parameters["CamPos"].SetValue((Vector3)camera.SendMessage("Position", null));
                effect.Parameters["GDSize"].SetValue(Common.TextureQuality);

                effect.Parameters["FogStart"].SetValue(fogStart);
                effect.Parameters["FogEnd"].SetValue(fogEnd);
                effect.Parameters["FogThickness"].SetValue(fogThickness);
                effect.Parameters["FogColour"].SetValue(fogColour);

                effect.Parameters["Scene"].SetValue(TextureBuffer.GetTexture("Scene"));
                effect.Parameters["DepthMap"].SetValue(TextureBuffer.GetTexture("DepthMap"));

                effect.CurrentTechnique.Passes[0].Apply();

                localPostProcessing.ScreenQuad.Render();

                Common.Device.SetRenderTarget(null);

                final = target;

                base.Render();
            }
            else
                Destroy();
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device,height, width,
                false, SurfaceFormat.Color, DepthFormat.None);

            base.TextureQualityChanged(width, height);
        }

        protected override void _Destroy()
        {
            cameraManager = null;

            base._Destroy();
        }
    }
}
