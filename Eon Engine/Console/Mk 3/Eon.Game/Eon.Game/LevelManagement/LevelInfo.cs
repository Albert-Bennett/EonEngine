/* Created 30/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using System.Collections.Generic;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// Used to define the items in an Xml file that defines a Level.
    /// </summary>
    public class LevelInfo
    {
        public string LevelName;

        public ParameterCollection[] LevelObjects;
    }
}
