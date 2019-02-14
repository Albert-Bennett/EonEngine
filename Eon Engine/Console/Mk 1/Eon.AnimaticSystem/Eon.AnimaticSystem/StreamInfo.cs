/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Collections;
using Eon.EngineComponents;
using System.Collections.Generic;

namespace Eon.AnimaticSystem
{
    /// <summary>
    /// defines a class that is used to hold 
    /// information about an animatic stream.
    /// </summary>
    public class StreamInfo
    {
        public List<ParameterCollection> Actions =
            new List<ParameterCollection>();
    }

    /// <summary>
    /// Defines a class that helps to define Animatics.
    /// </summary>
    public class AnimaticInfo
    {
        public GameStates ActiveInState = GameStates.Game;

        public List<StreamInfo> Streams =
            new List<StreamInfo>();
    }
}
