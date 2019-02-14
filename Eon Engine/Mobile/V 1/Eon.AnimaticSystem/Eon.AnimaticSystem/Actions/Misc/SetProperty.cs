/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents;
using System;
using System.Reflection;

namespace Eon.AnimaticSystem.Actions.Misc
{
    /// <summary>
    /// Defines a Set Action.
    /// Used to set the value of a 
    /// property in an ObjectComponent.
    /// </summary>
    public sealed class SetProperty : Action
    {
        string gameObjectID;
        string componentID = "None";
        string propertyName;

        object value;

        /// <summary>
        /// Creates a new Set Property action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject which has the ObjectComponent.</param>
        /// <param name="componentID">The ID of the ObjectComponent.</param>
        /// <param name="propertyName">The name of the property to be set.</param>
        /// <param name="value">The value of the property to be set.</param>
        public SetProperty(string id, int streamNumber, string gameObjectID,
            string componentID, string propertyName, object value)
            : base(id, streamNumber)
        {
            this.gameObjectID = gameObjectID;
            this.componentID = componentID;
            this.propertyName = propertyName;

            this.value = value;
        }

        /// <summary>
        /// Creates a new Set Property action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject which has the ObjectComponent.</param>
        /// <param name="propertyName">The name of the property to be set.</param>
        /// <param name="value">The value of the property to be set.</param>
        public SetProperty(string id, int streamNumber,
            string gameObjectID, string propertyName, object value)
            : base(id, streamNumber)
        {
            this.gameObjectID = gameObjectID;
            this.propertyName = propertyName;

            this.value = value;
        }

        public override void Execute()
        {
            GameObject obj = GameObjectManager.FindGameObject(gameObjectID);

            if (obj != null && componentID != "None")
            {
                ObjectComponent comp = obj.FindComponent(componentID);

                if (comp != null)
                {
                    Type t = comp.GetType();
                    PropertyInfo prop = t.GetProperty(propertyName);

                    if (prop != null && prop.PropertyType == value.GetType())
                        prop.SetValue(comp, value, null);
                }
            }
            else if (obj != null)
            {
                Type t = obj.GetType();
                PropertyInfo prop = t.GetProperty(propertyName);

                if (prop != null && prop.PropertyType == value.GetType())
                    prop.SetValue(obj, value, null);
            }

            FinishExecution();
        }
    }
}
