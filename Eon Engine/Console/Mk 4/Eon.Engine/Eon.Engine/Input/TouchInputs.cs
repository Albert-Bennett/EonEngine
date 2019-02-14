/* Created: 05/03/2014
 * Last Updated: 14/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Engine.Input
{
    /// <summary>
    /// The different types of touch inputs.
    /// </summary>
    public enum TouchType : int
    {
        None = 0,

        /// <summary>
        /// Defines a Tap action.
        /// </summary>
        Tap = 1,

        /// <summary>
        /// Defines a Drag action.
        /// </summary>
        Drag = 2,

        /// <summary>
        /// Defines a Double Tap action.
        /// </summary>
        DoubleTap = 3,

        /// <summary>
        /// Defines if the player is hold a single point.
        /// </summary>
        Hold = 4,

        /// <summary>
        /// Determines a flick motion was preformed.
        /// </summary>
        Flick = 5
    }
}
