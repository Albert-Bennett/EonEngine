/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;

namespace Eon.Engine.Input
{
    /// <summary>
    /// Defines a basic input device.
    /// </summary>
    public abstract class BaseInputItem : EngineComponent, IUpdate
    {
        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new BaseInputItem.
        /// </summary>
        /// <param name="name">The name of the input device.</param>
        public BaseInputItem(string name) : base(name) { }

        protected virtual void PreUpdate() { }
        protected virtual void Update() { }
        protected virtual void _PostUpdate() { }
        public virtual void Reset() { }

        public void PostUpdate()
        {
            _PostUpdate();
        }

        public void _Update()
        {
            PreUpdate();
            Update();
        }

        public void ToggleEnabled()
        {
            Enabled = !Enabled;
        }
    }
}
