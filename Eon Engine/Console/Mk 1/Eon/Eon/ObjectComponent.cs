/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;

namespace Eon
{
    /// <summary>
    /// Defines an ObjectComponent. 
    /// </summary>
    public abstract class ObjectComponent : IID, IEnabled
    {
        string id;
        bool enabled = true;

        GameObject owner;

        /// <summary>
        /// The specific identification name
        /// given to this ObjectComponent.
        /// </summary>
        public string ID
        {
            get { return id; }
            private set { id = value; }
        }

        /// <summary>
        /// Wheather or not this ObjectComponent is enabled. 
        /// </summary>
        public bool Enabled
        {
            get
            {
                if (owner != null)
                    if (!owner.Enabled && this.enabled)
                        return false;

                return enabled;
            }
        }

        /// <summary>
        /// Gets the ownning GameObject of 
        /// this ObjectComponent if one exists.
        /// </summary>
        public GameObject Owner
        {
            get { return owner; }
            internal set { owner = value; }
        }

        /// <summary>
        /// Creates a new ObjectComponent.
        /// </summary>
        /// <param name="id"></param>
        public ObjectComponent(string id)
        {
            this.id = id;
        }

        internal void _Initialize()
        {
            Initialize();
        }

        protected virtual void Initialize() { }

        /// <summary>
        /// Toggle the enabled property of this ObjectComponent.
        /// </summary>
        public void ToogleEnable()
        {
            if (!enabled)
                Enable();
            else
                Disable();
        }

        /// <summary>
        /// Enables this ObjectComponent.
        /// </summary>
        public void Enable()
        {
            enabled = true;
        }

        /// <summary>
        /// Disables this ObjectComponent.
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Destroys this ObjectComponent.
        /// </summary>
        public virtual void Destroy()
        {
            owner.RemoveComponent(this.id);
        }
    }
}
