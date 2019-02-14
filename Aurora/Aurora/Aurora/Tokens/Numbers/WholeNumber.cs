/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;
using System.Linq;

namespace Aurora.Tokens.Operators.Numbers
{
    /// <summary>
    /// Defines the number before the decimal point.
    /// </summary>
    internal sealed class WholeNumber : Token
    {
        public WholeNumber(params Token[] tokens) : base(tokens) { }

        public static WholeNumber Generate(IEnumerable<Token> tokens,
            out IEnumerable<Token> process)
        {
            IEnumerable<Token> left = null;

            DigitSequence digits = DigitSequence.Generate(tokens, out left);

            if (digits != null)
            {
                process = left;

                return new WholeNumber(digits);
            }

            process = null;

            return null;
        }
    }
}
