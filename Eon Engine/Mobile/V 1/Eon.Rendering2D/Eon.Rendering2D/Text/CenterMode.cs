/* Created 14/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering2D.Text
{
    /// <summary>
    /// The ways in which a TextItem can be centered.
    /// </summary>
    public enum CenterMode
    {
        /// <summary>
        /// Center the TextItem in the Middle of the screen.
        /// </summary>
        Screen,

        /// <summary>
        /// Center the TextItem so that the TextItem's 
        /// center is where it's position should be.
        /// </summary>
        Local,

        /// <summary>
        /// Dosn't center the TextItem.
        /// </summary>
        None
    }
}
