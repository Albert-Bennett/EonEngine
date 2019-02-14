/* Created: 13/06/2013
 * Last Updated: 07/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Reflection;

namespace Eon.System.Management
{
    /// <summary>
    /// Defines an abstract class similar to the 
    /// component class except, This type of component is to be 
    /// used specifically for components which are to be managed by the engine. 
    /// </summary>
    public abstract class EngineComponent
    {
        string id;
        protected bool initialized = false;
        bool enabled = true;

        /// <summary>
        /// Wheather or not this EngineComponent is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            protected set { enabled = value; }
        }

        /// <summary>
        /// The unique identifaction name 
        /// given to the EngineComponent. 
        /// </summary>
        public string ID { get { return id; } }

        public EngineComponent(string id)
        {
            this.id = id;
            EngineComponentManager.AddComp(this);
        }

        internal void _Init()
        {
            initialized = true;
            Initialize();
        }

        protected virtual void Initialize() { }

        public object SendMessage(string methodName, params object[] parameters)
        {
            MethodInfo info = GetType().GetMethod(methodName);

            if (info != null)
                return info.Invoke(this, parameters);
            else if (parameters == null)
                try
                {
                    return GetType().GetProperty(methodName).GetValue(this, null);
                }
                catch
                {
                    return null;
                }

            return null;
        }

        public void Destroy()
        {
            EngineComponentManager.RemoveComponent(this);
        }

        /// <summary>
        /// Disables the EngineComponent.
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// Enables the EngineComponent.
        /// </summary>
        public void Enable()
        {
            enabled = true;
        }
    }
}
