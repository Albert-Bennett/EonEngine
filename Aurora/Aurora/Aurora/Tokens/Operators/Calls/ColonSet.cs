/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Calls
{
    /// <summary>
    /// Defines a :.
    /// Used to set the values inside of a method.
    /// ie. prop_set: Colour .colour: 255 255 255 255;
    /// </summary>
    internal sealed class ColonSet : Token
    {
        public ColonSet() { }

        public override string ToString()
        {
            return ":";       
        }
    }
}
