/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Eon
{
    /// <summary>
    /// Used to manage assemblies in the EON.
    /// </summary>
    public sealed class AssemblyManager
    {
        static AssemblyManager instance;

        static EonDictionary<string, Assembly> assemblies = 
            new EonDictionary<string, Assembly>();

        public static AssemblyManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AssemblyManager();

                return instance;
            }
        }

        AssemblyManager() { }

        /// <summary>
        /// Adds a new reference to an assembly to this.
        /// </summary>
        /// <param name="assemblyName">The namer of the assembly to be referenced.</param>
        public static void AddAssemblyRef(string assemblyName)
        {
            if (!assemblies.Contains(assemblyName))
            {
                Assembly assem = null;

                if (assemblyName != null)
                {
                    assem = Assembly.Load(AssemblyName.GetAssemblyName(assemblyName + ".dll"));

                    if (assem != null)
                        assemblies.Add(assemblyName, assem);
                }
            }
        }

        /// <summary>
        /// Gets the type of object from a type name.
        /// </summary>
        /// <param name="typeName">The name of the type to get the Type from.</param>
        /// <returns>A type of object.</returns>
        public static Type GetType(string typeName)
        {
            Type t = null;

            t = Type.GetType(typeName);

            if (t == null)
            {
                bool found = false;
                int idx = 0;

                while (idx < assemblies.Count && !found)
                {
                    t = assemblies[idx].Value.GetType(typeName);

                    if (t != null)
                        found = true;

                    idx++;
                }
            }

            return t;
        }

        /// <summary>
        /// Creates an instance of an object.
        /// </summary>
        /// <param name="parameters">A collections of parameters and
        /// a type name used to instanciate objects.</param>
        /// <returns>The created object.</returns>
        public static object CreateInstance(ParameterCollection parameters)
        {
            Type t = null;
            object obj = null;

            if (parameters.ObjectType != "")
            {
                t = Type.GetType(parameters.ObjectType);

                int idx = assemblies.Count - 1;

                while (t == null && idx > -1)
                {
                    t = assemblies[idx].Value.GetType(parameters.ObjectType);

                    idx--;
                }

                if (t != null)
                    if (parameters.Count <= 0)
                        obj = Activator.CreateInstance(t, null);
                    else
                        obj = Activator.CreateInstance(t, parameters.Parameters);
            }

            return obj;
        }

        /// <summary>
        /// Creates an instance of an object.
        /// </summary>
        /// <param name="objectName">The type name of the object.</param>
        /// <returns>The created object.</returns>
        public static object CreateInstance(string objectName)
        {
            Type t = null;
            object obj = null;

            t = Type.GetType(objectName);

            if (t == null)
            {
                for (int i = 0; i < assemblies.Count; i++)
                    if (t == null)
                        t = assemblies[i].Value.GetType(objectName);
            }

            if (t != null)
                obj = Activator.CreateInstance(t, null);

            return obj;
        }

        /// <summary>
        /// Gets object inherits.
        /// </summary>
        /// <param name="assemblyFilepath">The assembly filepath from where the objects are being searched.</param>
        /// <param name="baseType">The base type of the object.</param>
        /// <returns>The result of the search.</returns>
        public static List<Type> FindDerivedTypes(string assemblyFilepath, Type baseType)
        {
            Assembly assem = Assembly.Load(AssemblyName.GetAssemblyName(assemblyFilepath));

            if (assem == null)
                return null;
            else
                return new List<Type>(FindDerivedTypes(assem, baseType));
        }

        /// <summary>
        /// Gets object inherits.
        /// </summary>
        /// <param name="assembly">The assembly from where the objects are being searched.</param>
        /// <param name="baseType">The base type of the object.</param>
        /// <returns>The result of the search.</returns>
        static IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(t => t != baseType &&
                baseType.IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic);
        }
    }
}
