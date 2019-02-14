/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.EngineComponents;
using System;
using System.Reflection;

namespace Eon.AnimaticSystem.Actions.Misc
{
    /// <summary>
    /// Defines a Toogle Action.
    /// </summary>
    public sealed class ToogleProperty : Action
    {
        string gameObjectID;
        string componentID = "None";
        string propertyName;

        /// <summary>
        /// Creates a Toogle Action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject.</param>
        /// <param name="componentID">The ID of the ObjectComponent</param>
        /// <param name="propertyName">The name of the property to be toogled.</param>
        public ToogleProperty(string id, int streamNumber,
            string gameObjectID, string componentID, string propertyName)
            : base(id, streamNumber)
        {
            this.gameObjectID = gameObjectID;
            this.componentID = componentID;
            this.propertyName = propertyName;
        }

        /// <summary>
        /// Creates a Toogle Action.
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Action.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="gameObjectID">The ID of the GameObject.</param>
        /// <param name="propertyName">The name of the property to be toogled.</param>
        public ToogleProperty(string id, int streamNumber,
            string gameObjectID, string propertyName)
            : base(id, streamNumber)
        {
            this.gameObjectID = gameObjectID;
            this.propertyName = propertyName;
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

                    if (prop != null && prop.PropertyType == typeof(bool))
                    {
                        bool val = (bool)prop.GetValue(comp, null);

                        prop.SetValue(comp, !val, null);
                    }
                }
            }
            else if (obj != null)
            {
                Type t = obj.GetType();
                PropertyInfo prop = t.GetProperty(propertyName);

                if (prop != null && prop.PropertyType == typeof(bool))
                {
                    bool val = (bool)prop.GetValue(obj, null);

                    prop.SetValue(obj, !val, null);
                }
            }

            FinishExecution();
        }
    }
}
