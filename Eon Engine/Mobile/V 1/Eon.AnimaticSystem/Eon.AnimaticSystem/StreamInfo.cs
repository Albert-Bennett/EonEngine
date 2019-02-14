/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.EngineComponents;

namespace Eon.AnimaticSystem
{
    /// <summary>
    /// defines a class that is used to hold 
    /// information about an animatic stream.
    /// </summary>
    public class StreamInfo
    {
        public ParameterCollection[] Actions;
    }

    /// <summary>
    /// Defines a class that helps to define Animatics.
    /// </summary>
    public class AnimaticInfo
    {
        public GameStates ActiveInState = GameStates.Game;

        public StreamInfo[] Streams;
    }
}
