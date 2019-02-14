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
    /// Defines the fractional part of a float.
    /// </summary>
    internal sealed class Fractional : Token
    {
        public Fractional(params object[] tokens) : base(tokens) { }

        public static Fractional Generate(IEnumerable<Token> tokens)
        {
            if (tokens.Any())
            {
                if (tokens.First() is FullStop)
                {
                    IEnumerable<Token> processed = null;

                    DigitSequence digit = DigitSequence.Generate(
                        tokens.Skip(1), out processed);

                    if (digit != null && !processed.Any())
                        return new Fractional(new FullStop(), digit);
                }
            }
            
            return null;
        }
    }
}
