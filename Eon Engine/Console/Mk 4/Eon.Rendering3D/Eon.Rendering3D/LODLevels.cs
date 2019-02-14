﻿/* Created: 02/05/2013
 * Last Updated: 02/05/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering3D
{
    /// <summary>
    /// Used to define levels of detail an object should be rendered in.
    /// </summary>
    public enum LODLevels : byte
    {
        Zero = 0, //Nearest
        One = 1,
        Two = 2,
        Three = 3 //Farthest
    }
}