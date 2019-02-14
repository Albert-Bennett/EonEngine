/* Created 05/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Collections;
using System.Collections.Generic;

namespace Eon.UIAPI
{
    /// <summary>
    /// Defines a file from which all 
    /// screens involved in the creation of
    /// the menu system get their informatiom from.
    /// </summary>
    public sealed class MenuSystemSetup
    {
        public string[] Assemblies;

        public List<ParameterCollection> Screens =
            new List<ParameterCollection>();
    }
}
