/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Comparison
{
    /// <summary>
    /// Defines the '!=' icon.
    /// </summary>
    internal sealed class NotEqualOperator : Token
    {
        public NotEqualOperator() { }

        public override string ToString()
        {
            return "!=";
        }
    }
}
