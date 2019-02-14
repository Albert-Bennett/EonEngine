/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using System.Linq;

namespace Eon.Collections
{
    /// <summary>
    /// Defines a class that is used to instanciate 
    /// objects from the AssemblyManager.
    /// </summary>
    public sealed class ParameterCollection
    {
        object[] parameters;
        string objectType;

        /// <summary>
        /// The amount of objects iin the collection.
        /// </summary>
        public int Count
        {
            get
            {
                if (parameters == null)
                    return 0;
                else
                    return parameters.Length;
            }
        }

        /// <summary>
        /// The type of object to be instanciated.
        /// </summary>
        public string ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }

        /// <summary>
        /// The parameters that are used to create the object.
        /// </summary>
        public object[] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        /// <summary>
        /// Creates a new ParameterCollection.
        /// </summary>
        /// <param name="objectType">The type of object to be created.</param>
        /// <param name="parameters">The paramaters in the object.</param>
        public ParameterCollection(string objectType, object[] parameters)
        {
            this.objectType = objectType;
            this.parameters = parameters;
        }

        /// <summary>
        /// Creates a new ParameterCollection.
        /// </summary>
        /// <param name="objectType">The name give to the type of object to be created.</param>
        public ParameterCollection(string objectType) : this(objectType, null) { }

        /// <summary>
        /// Creates a new ParameterCollection that isn't used to create an object. 
        /// </summary>
        public ParameterCollection() : this("", null) { }

        /// <summary>
        /// Adds a paramemeter to this.
        /// </summary>
        /// <param name="param">The parameter to be added. 
        /// These should be added in chronological order.</param>
        public void Add(object param)
        {
            Parameters = ArrayHelper.AddItem<object>(param, Parameters);
        }

        /// <summary>
        /// A check to see if this comntains a particular object.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(object obj)
        {
            return parameters.Contains(obj);
        }

        /// <summary>
        /// Clears the ParameterCollection of all objects.
        /// </summary>
        public void Clear()
        {
            parameters = new object[0];
        }
    }
}
