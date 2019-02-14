/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Aurora.Tokens.Misc;
using System.Collections.Generic;
using System.Linq;

namespace Aurora.Tokens.Operators.Numbers
{
    /// <summary>
    /// Defines a sequence of digits.
    /// </summary>
    internal sealed class DigitSequence : Token
    {
        public DigitSequence(params object[] tokens) : base(tokens) { }

        public static DigitSequence Generate(IEnumerable<Token> tokens,
            out IEnumerable<Token> process)
        {
            IEnumerable<Token> left = tokens.TakeWhile(s => s is ByteDigit);

            if (left.Any())
            {
                process = tokens.Skip(left.Count());
                return new DigitSequence(left);
            }
            
            process = null;

            return null;
        }
    }
}
