/* Created: 18/01/2015
 * Last Updated: 18/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace Eon.System.Attributes
{
    /// <summary>
    /// Used to define a field in a class as
    /// non accessable by any development kit program.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class NoAccessField : Attribute
    {
        public override string ToString()
        {
            return "No Access Field";
        }
    }
}
