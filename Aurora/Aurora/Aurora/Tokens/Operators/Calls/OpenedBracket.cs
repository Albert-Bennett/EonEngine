/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Calls
{
    /// <summary>
    /// Defines a (.
    /// </summary>
    internal sealed class OpenedBracket : Token
    {
        public OpenedBracket() { }

        public override string ToString()
        {
            return "(";
        }
    }
}
