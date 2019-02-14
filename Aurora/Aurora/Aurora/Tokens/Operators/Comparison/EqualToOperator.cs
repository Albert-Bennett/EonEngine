/* Created: 02/09/2015
 * Last Updated: 02/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Comparison
{
    /// <summary>
    /// Defines the '=' icon.
    /// </summary>
    internal sealed class EqualToOperator : Token
    {
        public EqualToOperator() { }

        public override string ToString()
        {
            return "=";
        }
    }
}
