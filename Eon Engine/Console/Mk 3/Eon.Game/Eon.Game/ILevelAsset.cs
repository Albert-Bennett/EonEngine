﻿/* Created 27/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;

namespace Eon.Game
{
    /// <summary>
    /// Used to define an object that
    /// is in some way attached to a level.
    /// </summary>
    public interface ILevelAsset : IID
    {
        void LevelTransitionOn(string levelID);
        void LevelTransitionOff(string levelID);
    }
}