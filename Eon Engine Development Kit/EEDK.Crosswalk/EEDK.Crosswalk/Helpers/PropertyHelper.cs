/* Created: 14/01/2015
 * Last Updated: 14/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Reflection;

namespace EEDK.Crosswalk.Helpers
{
    /// <summary>
    /// Defines a class that contains helper
    /// methods for dealing with properties.
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// Gets the class name of an object from a User readable name.  
        /// </summary>
        /// <param name="name">The name to be modified.</param>
        /// <returns>The result of the modification.</returns>
        public static string GetClassName(string name)
        {
            char[] text = name.ToCharArray();

            string mod = "";

            for (int i = 0; i < text.Length; i++)
                if (text[i] != ' ')
                    mod += text[i];

            return mod;
        }

        /// <summary>
        /// Gets all of the properties of an object.
        /// </summary>
        /// <param name="obj">The object to get the properties of.</param>
        /// <returns>The result of the search.</returns>
        public static PropertyInfo[] GetProperties(object obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();

            return props;
        }

        /// <summary>
        /// Get the name of a type.
        /// </summary>
        /// <param name="typeName">The name of the type to get.</param>
        /// <returns>The type name as a user readable name.</returns>
        public static string GetTypeName(string typeName)
        {
            string[] split = typeName.Split(new char[]
            {
                '.'
            });

            return GetName(split[split.Length - 1]);
        }

        /// <summary>
        /// Gets the name of a property.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The name as a user readable string.</returns>
        public static string GetName(string name)
        {
            for (int i = 1; i < name.Length; i++)
                if (char.IsUpper(name[i]))
                {
                    name = name.Insert(i, " ");
                    i++;
                }

            return name;
        }
    }
}
