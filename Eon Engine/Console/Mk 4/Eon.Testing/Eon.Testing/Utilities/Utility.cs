/* Created: 08/09/2014
 * Last Updated: 02/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Testing.Utilities
{
    /// <summary>
    /// Used to define a testing utility.
    /// </summary>
    internal class Utility
    {
        ErrorConsole owner;

        /// <summary>
        /// What is incapsulating the Utility.
        /// </summary>
        public ErrorConsole Owner
        {
            get { return owner; }
            internal set { owner = value; }
        }

        public virtual void Initialize() { }
        public virtual void Draw() { }
    }
}
