/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using System;
using System.Reflection;

namespace Eon.AnimaticSystem.Actions.Misc
{
    /// <summary>
    /// Defines an Action that when active calls a method.
    /// </summary>
    public sealed class CallMethod : Action
    {
        string gameObjectID;
        string componentID;
        string methodName;

        object[] parameters;

        /// <summary>
        /// Creates a new CallMethod Action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject 
        /// that contains the desired ObjectComponent.</param>
        /// <param name="componentID">The ID of the ObjectComponent.</param>
        /// <param name="methodName">The name of the Method.</param>
        /// <param name="parameters">The parameters for the method to be called.</param>
        public CallMethod(string id, int streamNumber, string gameObjectID,
            string componentID, string methodName, object[] parameters)
            : base(id, streamNumber)
        {
            this.gameObjectID = gameObjectID;
            this.componentID = componentID;
            this.methodName = methodName;

            this.parameters = parameters;
        }

        /// <summary>
        /// Creates a new CallMethod Action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject 
        /// that contains the desired ObjectComponent.</param>
        /// <param name="componentID">The ID of the ObjectComponent.</param>
        /// <param name="methodName">The name of the Method.</param>
        public CallMethod(string id, int streamNumber, string gameObjectID,
            string componentID, string methodName)
            : this(id, streamNumber, gameObjectID, componentID, methodName, null) { }

        /// <summary>
        /// Creates a new CallMethod Action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject.</param>
        /// <param name="methodName">The name of the Method.</param>
        public CallMethod(string id, int streamNumber, string gameObjectID, string methodName) :
            this(id, streamNumber, gameObjectID, "None", methodName, null) { }

        /// <summary>
        /// Creates a new CallMethod Action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject.</param>
        /// <param name="methodName">The name of the Method.</param>
        /// <param name="parameters">The parameters for the method to be called.</param>
        public CallMethod(string id, int streamNumber, string gameObjectID, string methodName, object[] parameters) :
            this(id, streamNumber, gameObjectID, "None", methodName, parameters) { }

        public override void Execute()
        {
            GameObject obj = GameObjectManager.FindGameObject(gameObjectID);

            if (obj != null && componentID != "None")
            {
                ObjectComponent comp = obj.FindComponent(componentID);

                if (comp != null)
                {
                    Type t = comp.GetType();
                    MethodInfo info = t.GetMethod(methodName);

                    if (info != null)
                        info.Invoke(comp, parameters);
                }
            }
            else if (obj != null)
            {
                Type t = obj.GetType();
                MethodInfo info = t.GetMethod(methodName);

                if (info != null)
                    info.Invoke(obj, parameters);
            }

            FinishExecution();
        }
    }
}
