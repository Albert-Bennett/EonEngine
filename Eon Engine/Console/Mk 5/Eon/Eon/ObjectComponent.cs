/* Created: 10/06/2013
 * Last Updated: 03/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Interfaces.Base;
using System.Collections.Generic;
using System.Reflection;

namespace Eon
{
    /// <summary>
    /// Defines an ObjectComponent. 
    /// </summary>
    public abstract class ObjectComponent : IID, IEnabled, IDestructable
    {
        List<IHoldReferences> references = new List<IHoldReferences>();

        string id;
        bool enabled = true;
        bool destroyed = false;
        bool initialized = false;

        GameObject owner;

        int priority = 0;

        /// <summary>
        /// When does the ObjectComponent update.
        /// </summary>
        public int Priority
        {
            get { return priority; }
            protected set
            {
                if (value >= 0)
                    priority = value;
            }
        }

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
        /// Whether or not the ObjectComponent is enabled. 
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                if (owner.Enabled)
                    return enabled;

                return false;
            }
            protected set { enabled = value; }
        }

        /// <summary>
        /// Whether or not the ObjectComponent has been initialized.
        /// </summary>
        protected bool Initialized
        {
            get { return initialized; }
        }

        /// <summary>
        /// Has the ObjectComponent been destroyed.
        /// </summary>
        public bool IsDestroyed
        {
            get { return destroyed; }
        }

        /// <summary>
        /// Gets the ownning GameObject of 
        /// this ObjectComponent if one exists.
        /// </summary>
        public GameObject Owner
        {
            get { return owner; }
            internal set
            {
                owner = value;

                Attached();
            }
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

        internal void _Update()
        {
            Update();
        }

        protected virtual void Update() { }

        internal void _PostUpdate()
        {
            PostUpdate();
        }

        protected virtual void PostUpdate() { }

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
        /// Removes an IHoldReference from this.
        /// </summary>
        /// <param name="reference">The IHoldReference to be removed.</param>
        public void RemoveReference(IHoldReferences reference)
        {
            if (references.Contains(reference))
                references.Remove(reference);
        }

        /// <summary>
        /// A method called when the 
        /// ObjectComponent has been attached to a GameObject.
        /// </summary>
        protected virtual void Attached() { }

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
            enabled = !enabled;
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

        public void Destroy()
        {
            if (!destroyed)
                _Destroy();

            if (owner != null)
                if (!owner.IsDestroyed)
                    owner.RemoveComponent(this);

            destroyed = true;
        }

        /// <summary>
        /// Destroys this ObjectComponent.
        /// </summary>
        protected virtual void _Destroy()
        {
            for (int i = 0; i < references.Count; i++)
                if (references[i] != null)
                {
                    references[i].Remove(this);
                    references[i] = null;
                }

            references.Clear();

            owner = null;
        }
    }
}
