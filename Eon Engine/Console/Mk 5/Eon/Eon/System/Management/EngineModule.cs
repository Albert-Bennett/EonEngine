/* Created: 13/06/2013
 * Last Updated: 05/04/2014
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
    public abstract class EngineModule
    {
        string id;
        bool enabled = true;

        /// <summary>
        /// Whether or not this EngineComponent is enabled.
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

        public EngineModule(string id)
        {
            this.id = id;
            EngineModuleManager.AddComp(this);
        }

        internal void _Init()
        {
            Initialize();
        }

        protected virtual void Initialize() { }

        /// <summary>
        /// Sends a message to the EngineModule.
        /// </summary>
        /// <param name="methodName">Method name.</param>
        /// <param name="parameters">Method parameters.</param>
        /// <returns>Result of the execution.</returns>
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

        /// <summary>
        /// Sends a message to the EngineModule.
        /// </summary>
        /// <param name="message">The message to be executed.</param>
        /// <returns>Result of the execution.</returns>
        public object SendMessage(Message message)
        {
            MethodInfo info = GetType().GetMethod(message.MethodName);

            if (info != null)
                return info.Invoke(this, message.Parameters);
            else if (message.Parameters == null)
                try
                {
                    return GetType().GetProperty(message.MethodName).GetValue(this, null);
                }
                catch
                {
                    return null;
                }

            return null;
        }

        internal void _Destroy()
        {
            Destroy();
        }

        protected virtual void Destroy()
        {
            EngineModuleManager.RemoveComponent(this);
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
