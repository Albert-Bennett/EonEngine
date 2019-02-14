/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Misc
{
    /// <summary>
    /// Defines a .
    /// Used to call a method in and object.
    /// ie. mathhelper.min: 2 8;
    /// </summary>
    internal sealed class FullStop : Token
    {
        public FullStop() { }

        public override string ToString()
        {
            return ".";
        }
    }
}
