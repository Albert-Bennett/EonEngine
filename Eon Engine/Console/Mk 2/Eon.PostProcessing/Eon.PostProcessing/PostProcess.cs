/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing
{
    /// <summary>
    /// Defines a post processing technique.
    /// </summary>
    public abstract class PostProcess : ObjectComponent
    {
        public static int MaxPriority = 0;
        int priority = 0;

        protected PostProcessingLocal localPostProcessing;
        protected Texture2D final;

        public int Priority
        {
            get { return priority; }
            private set
            {
                priority = value;

                if (priority > MaxPriority)
                    MaxPriority = priority;
            }
        }

        public PostProcess(string ID, int priority, PostProcessingLocal localPostProcessing)
            : base(ID)
        {
            Priority = priority;
            this.localPostProcessing = localPostProcessing;

            localPostProcessing.AddProcess(this);
        }

        public PostProcess(string ID, int priority, string renderFrameworkID)
            : base(ID)
        {
            Priority = priority;

            EngineComponent comp = EngineComponentManager.Find(renderFrameworkID);

            if (comp != null)
            {
                localPostProcessing = (PostProcessingLocal)comp.SendMessage("GetLocalPostProcessing");

                localPostProcessing.AddProcess(this);
            }
        }

        internal void _Render()
        {
            Render();

            TextureBuffer.SetBuffer("Scene", final);
        }

        protected virtual void Render() { }

        public void Destroy()
        {
            _Destroy();

            localPostProcessing.RemoveProcess(this);
        }

        protected virtual void _Destroy() { }

        /// <summary>
        /// Draws a texture to a render target with an effect.
        /// </summary>
        /// <param name="texture">The texture to be rendered.</param>
        /// <param name="renderTarget">The render target to be used.</param>
        /// <param name="effect">The effect to be used.</param>
        protected void DrawWithEffect(Texture2D texture, RenderTarget2D renderTarget, Effect effect)
        {
            Common.Device.SetRenderTarget(renderTarget);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(0, BlendState.Opaque, null, null, null, effect);
            Common.Batch.Draw(texture, renderTarget.Bounds, Color.White);
            Common.Batch.End();

            Common.Device.SetRenderTargets(null);
        }

        public virtual void TextureQualityChanged(int width, int height) { }
    }
}
