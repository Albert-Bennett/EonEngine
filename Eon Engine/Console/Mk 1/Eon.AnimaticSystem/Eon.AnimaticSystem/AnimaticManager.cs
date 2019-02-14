/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.EngineComponents;
using Eon.Interfaces;

namespace Eon.AnimaticSystem
{
    /// <summary>
    /// Defines an EngineComponent that is 
    /// used to Manage Animatics.
    /// </summary>
    public sealed class AnimaticManager : EngineComponent, IUpdate
    {
        static Animatic animatic = null;

        public AnimaticManager()
            : base("AnimaticManager")
        {
            Enabled = false;
        }

        /// <summary>
        /// Adds an Animatic to this.
        /// </summary>
        /// <param name="animatic">The Animatic to be added.</param>
        internal void Add(Animatic animatic)
        {
            AnimaticManager.animatic = animatic;
            AnimaticManager.animatic.OnFinished += new FinishedAnimaticEvent(OnFinished);

            Enabled = true;
        }

        internal AnimaticStream GetStream(int streamNumber)
        {
            if (animatic != null)
                return animatic.GetStream(streamNumber);

            return null;
        }

        void OnFinished()
        {
            animatic.Dispose();
            animatic = null;

            Enabled = false;
        }

        public void _Update()
        {
            if (GameStateManager.CurrentState == animatic.ActiveInState)
                animatic._Update();
        }
    }
}
