/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing
{
    /// <summary>
    /// Defines a post processing technique.
    /// </summary>
    public abstract class PostProcess
    {
        public static int MaxPriority = 0;
        int priority = 0;

        protected PostProcessingLocal localPostProcessing;
        protected Texture2D final;

        static Rectangle renderBounds = Rectangle.Empty;

        protected Rectangle RenderBounds
        {
            get { return renderBounds; }
        }

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

        public PostProcess(int priority, PostProcessingLocal localPostProcessing)
        {
            Priority = priority;
            this.localPostProcessing = localPostProcessing;

            localPostProcessing.AddProcess(this);
        }

        internal void _Render()
        {
            Render();

            localPostProcessing.FinalImage = final;
        }

        protected virtual void Render() { }

        public void Destroy()
        {
            _Destroy();

            localPostProcessing.RemoveProcess(this);
        }

        protected virtual void _Destroy() { }

        protected void DrawWithEffect(Texture2D texture, RenderTarget2D renderTarget, Effect effect)
        {
            DrawWithEffect(texture, renderTarget, effect, renderBounds);
        }

        protected void DrawWithEffect(Texture2D texture,
            RenderTarget2D renderTarget, Effect effect, Rectangle bounds)
        {
            Common.Device.SetRenderTarget(renderTarget);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(0, BlendState.Opaque, null, null, null, effect);
            Common.Batch.Draw(texture, bounds, Color.White);
            Common.Batch.End();
        }

        public virtual void ScreenResolutionChanged()
        {
            if (renderBounds == Rectangle.Empty)
                renderBounds = new Rectangle(0, 0,
                    (int)Common.ScreenResolution.X, (int)Common.ScreenResolution.Y);
        }
    }
}
