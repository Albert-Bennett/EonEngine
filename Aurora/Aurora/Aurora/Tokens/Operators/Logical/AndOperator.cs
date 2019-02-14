/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Infix
{
    /// <summary>
    /// Defines the '&' icon.
    /// </summary>
    internal sealed class AndOperator : Token
    {
        public AndOperator() { }

        public override string ToString()
        {
            return "&";
        }
    }
}
