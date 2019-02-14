/* Created: 10/07/2015
 * Last Updated: 10/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;

namespace Eon.Rendering3D.Framework.Rendering
{
    /// <summary>
    /// Used to define a part of the render cycle.
    /// </summary>
    public abstract class RenderPass : IEnabled
    {
        int order = 0;
        RenderPhases phase;

        bool enabled = true;

        internal int Order
        {
            get { return order; }
        }

        internal RenderPhases Phase
        {
            get { return phase; }
        }

        public bool Enabled
        {
            get { return enabled; }
        }

        /// <summary>
        /// Creates a new RenderPass.
        /// </summary>
        /// <param name="order">The when this RenderPass is to be activated.</param>
        /// <param name="phase">When should the RenderPass be executed.</param>
        public RenderPass(int order, RenderPhases phase)
        {
            if (order < 0)
                order = 0;

            this.order = order;
            this.phase = phase;

            RenderManager.AddPass(this);
        }

        internal void _Render()
        {
            if (enabled)
                Render();
        }

        protected abstract void Render();

        /// <summary>
        /// Destroys the RenderPass.
        /// </summary>
        public void Destroy()
        {
            RenderManager.RemovePass(this);
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }

        public void ToogleEnable()
        {
            enabled = !enabled;
        }
    }
}
