/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace Eon
{
    /// <summary>
    /// Defines an ObjectComponent. 
    /// </summary>
    public abstract class ObjectComponent : IID, IEnabled
    {
        List<IHoldReferences> references = new List<IHoldReferences>();

        string id;
        bool enabled = true;
        bool destroyed = false;
        bool initialized = false;

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
        /// Used to determine if the current
        /// ObjectComponent has been destroyed.
        /// </summary>
        public bool Destroyed
        {
            get { return destroyed; }
        }

        /// <summary>
        /// Wheather or not this ObjectComponent is enabled. 
        /// </summary>
        public virtual bool Enabled
        {
            get { return enabled; }
            protected set { enabled = value; }
        }

        /// <summary>
        /// Wheather or not this ObjectComponent has been initialized.
        /// </summary>
        protected bool Initialized
        {
            get { return initialized; }
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
        /// <param name="id">The id of the ObjectComponent.</param>
        public ObjectComponent(string id)
        {
            this.id = id;
        }

        internal void _Initialize()
        {
            initialized = true;
            Initialize();
        }

        protected virtual void Initialize() { }

        /// <summary>
        /// Adds a link to an object that manages this ObjectComponent in some way.
        /// </summary>
        /// <param name="reference">The object that holds a refernce of this.</param>
        public void AddReference(IHoldReferences reference)
        {
            if (!references.Contains(reference))
                references.Add(reference);
        }

        /// <summary>
        /// Enables this ObjectComponent.
        /// </summary>
        public virtual void Enable()
        {
            enabled = true;
        }

        /// <summary>
        /// Disables this ObjectComponent.
        /// </summary>
        public virtual void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Toggle the enabled property of this ObjectComponent.
        /// </summary>
        public virtual void ToogleEnable()
        {
            if (!enabled)
                Enable();
            else
                Disable();
        }

        /// <summary>
        /// Used to send a message to an ObjectComponent.
        /// </summary>
        /// <param name="message">The name of the property or method 
        /// to be executed/ taken value of.</param>
        /// <param name="parameters">The parameters of the method
        /// to be executed.</param>
        /// <returns>An object if an.</returns>
        public object SendMessage(string message, object[] parameters)
        {
            MethodInfo info = GetType().GetMethod(message);

            if (info != null)
                return info.Invoke(this, parameters);
            else if (parameters == null)
                try
                {
                    return GetType().GetProperty(message).GetValue(this, null);
                }
                catch
                {
                    return null;
                }

            return null;
        }

        internal void _Destroy(bool remove)
        {
            Destroy(remove);
        }

        /// <summary>
        /// Destroys this ObjectComponent.
        /// </summary>
        public virtual void Destroy(bool remove)
        {
            if (!destroyed)
            {
                for (int i = 0; i < references.Count; i++)
                    if (references[i] != null)
                        references[i].Remove(this);

                references.Clear();

                if (remove && owner != null)
                    owner.RemoveComponent(this);
            }

            destroyed = true;
        }
    }
}
