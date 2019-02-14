﻿/* Created 03/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
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
        public Dictionary<string, string> Levels =
            new Dictionary<string, string>();

        public ParameterCollection PlayerObject;
    }
}