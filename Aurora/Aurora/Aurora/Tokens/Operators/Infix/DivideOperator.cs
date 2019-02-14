/* Created: 28/08/2015
 * Last Updated: 28/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Infix
{
    /// <summary>
    /// Defines the '/' icon.
    /// </summary>
    internal sealed class DivideOperator : Token
    {
        public DivideOperator() { }

        public override string ToString()
        {
            return "/";
        }
    }
}
