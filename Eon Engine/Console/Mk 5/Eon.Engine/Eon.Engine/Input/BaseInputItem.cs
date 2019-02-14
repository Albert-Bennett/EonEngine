/* Created: 09/06/2013
 * Last Updated: 14/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Management;
using Eon.System.States;

namespace Eon.Engine.Input
{
    /// <summary>
    /// Defines a basic input device.
    /// </summary>
    public abstract class BaseInputItem : GameObject
    {
        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new BaseInputItem.
        /// </summary>
        /// <param name="name">The name of the input device.</param>
        public BaseInputItem(string name) : base(name)
        {
            Presidence = GameStates.None;
        }

        internal virtual void Reset() { }
    }
}
