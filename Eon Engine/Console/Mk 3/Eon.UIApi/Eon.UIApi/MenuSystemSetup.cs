/* Created: 05/09/2013
 * Last Updated: 05/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using System.Collections.Generic;

namespace Eon.UIApi
{
    /// <summary>
    /// Defines a file from which all 
    /// screens involved in the creation of
    /// the menu system get their informatiom from.
    /// </summary>
    public sealed class MenuSystemSetup
    {
        public string[] Assemblies;

        public string[] ScreenNames;
        public ParameterCollection[] Screens;
    }
}
