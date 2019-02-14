/* Created: 13/06/2013
 * Last Updated: 03/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Tools;
using Eon.Testing;
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
        string id;

        PostProcessingLocal activeStore;

        protected Texture2D final;

        /// <summary>
        /// The ID of the PostProcess.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        protected PostProcessingLocal ActiveStore
        {
            get { return activeStore; }
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

        /// <summary>
        /// Creates a new PostProcess.
        /// </summary>
        /// <param name="ID">The ID of the PostProcess.</param>
        /// <param name="priority">When the PostProcess renders.</param>
        /// <param name="activeStore">What the PostProcess effects.</param>
        public PostProcess(string ID, int priority, PostProcessingLocal activeStore)
            : base()
        {
            if (activeStore != null)
            {
                this.activeStore = activeStore;

                Priority = priority;
                this.id = ID;

                activeStore.AddProcess(this);
            }
        }

        internal void _Render()
        {
            Render();

            activeStore.Buffer.SetBuffer(activeStore.Buffer.OutputTextureID, final);
        }

        protected virtual void Render() { }

        public void Destroy()
        {
            _Destroy();

            if (activeStore != null)
                activeStore.RemoveProcess(this);
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
