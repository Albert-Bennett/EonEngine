/* Created 11/06/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Used to define a bluring effect on the 
    /// background from the motion of the camera.
    /// </summary>
    public sealed class MotionBlurEffect : PostProcess
    {
        Matrix prevViewProj = Matrix.Identity;
        Vector2 maxVelocity = new Vector2(2);

        Effect effect;

        RenderTarget2D target;

        EngineComponent cameraManager;

        /// <summary>
        /// The maximum velocity that can be used 
        /// when blurring, before artifacts begin to appear.
        /// </summary>
        public Vector2 MaxVelocity
        {
            get { return maxVelocity; }
            set { maxVelocity = value; }
        }

        /// <summary>
        /// Creates a new MotionBlurEffect.
        /// </summary>
        /// <param name="maxVelocity">The maximum velocity that can be blurred to.</param>
        public MotionBlurEffect(Vector2 maxVelocity)
            : base("MotionBlurPP", 2, "Render3DFramework")
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

            effect = Common.ContentManager.Load<Effect>("Eon/Shaders/PostProcessing/MotionBlur");
            this.maxVelocity = maxVelocity;
        }

        protected override void Render()
        {
            if (cameraManager != null)
            {
                Common.Device.SetRenderTarget(target);

                Common.Device.Clear(Color.Black);

                ObjectComponent camera = (ObjectComponent)cameraManager.SendMessage("CurrentCamera", null);

                Matrix view = (Matrix)camera.SendMessage("View", null);
                Matrix proj = (Matrix)camera.SendMessage("Projection", null);

                Matrix iViewProj = Matrix.Invert(view * proj);

                effect.Parameters["IViewProj"].SetValue(iViewProj);

                effect.Parameters["PrevViewProj"].SetValue(prevViewProj);
                effect.Parameters["MaxVelocity"].SetValue(maxVelocity);

                effect.Parameters["GDSize"].SetValue(Common.TextureQuality);

                effect.Parameters["Scene"].SetValue(TextureBuffer.GetTexture("Scene"));
                effect.Parameters["DepthMap"].SetValue(TextureBuffer.GetTexture("DepthMap"));
                //effect.Parameters["DistortionMap"].SetValue(TextureBuffer.GetTexture("DistortionMap"));

                effect.CurrentTechnique.Passes[0].Apply();

                localPostProcessing.ScreenQuad.Render();

                Common.Device.SetRenderTarget(null);

                final = target;

                prevViewProj = view * proj;
            }
            else
                Destroy();
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height);

            base.TextureQualityChanged(width, height);
        }

        protected override void _Destroy()
        {
            cameraManager = null;

            base._Destroy();
        }
    }
}
