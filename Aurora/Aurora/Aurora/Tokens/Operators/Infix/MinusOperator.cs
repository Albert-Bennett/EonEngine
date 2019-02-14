/* Created: 28/08/2015
 * Last Updated: 28/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Infix
{
    /// <summary>
    /// Defines the '-' icon.
    /// </summary>
    internal class MinusOperator : Token
    {
        public MinusOperator() { }

        public override string ToString()
        {
            return "-";
        }
    }
}
