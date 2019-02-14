/* Created: 16/01/2015
 * Last Updated: 18/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;
using System.Windows.Forms;

namespace EEDK.Crosswalk.Controls.ModControls
{
    /// <summary>
    /// Defines an interface that modifies the 
    /// existing properties of a control 
    /// that is generated programatically.
    /// </summary>
    public interface IModControl
    {
        int ParamIndex { get; }
        string Name { get; }
        object ParamValue { get; }

        TableLayoutPanel Destroy(TableLayoutPanel panel);
    }
}
