/* Created 03/11/2013
 * Last Updated: 23/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using System.Collections.Generic;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// Used to define the properties of the LevelManager.
    /// </summary>
    public sealed class LevelManagerInfo
    {
        public string[] LevelNames;
        public string[] LevelFilepaths;

        public ParameterCollection PlayerObject;
    }
}
