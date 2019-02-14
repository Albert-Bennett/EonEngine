/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Aurora.Tokens.Operators.Infix;

namespace Aurora.Tokens.Operators.Numbers
{
    /// <summary>
    /// Defines the negitive sign.
    /// </summary>
    internal sealed class NegitiveSign : Token
    {
        public NegitiveSign(params Token[] tokens) : base(tokens) { }

        public static NegitiveSign Generate(Token token)
        {
            if (token is MinusOperator)
                return new NegitiveSign(new MinusOperator());

            return null;
        }
    }
}
