/* Created: 05/10/2013
 * Last Updated: 24/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.States
{
    /// <summary>
    /// The various possible game states.
    /// </summary>
    public enum GameStates : byte
    {
        None = 0,
        MainMenu = 1,
        InnerMenu = 2,
        Game = 3,
        Other = 4
    }
}
