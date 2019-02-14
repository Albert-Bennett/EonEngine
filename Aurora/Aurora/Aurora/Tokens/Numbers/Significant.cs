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
    /// Defines a number.
    /// </summary>
    internal sealed class Significant : Token
    {
        public Significant(params Token[] tokens) : base(tokens) { }

        public static Significant Generate(IEnumerable<Token> tokens)
        {
            Fractional fract = Fractional.Generate(tokens);

            if (fract != null)
                return new Significant(fract);

            IEnumerable<Token> tok = null;

            WholeNumber whole = WholeNumber.Generate(tokens, out tok);

            if (whole != null)
            {
                if (!tok.Any())
                    return new Significant(whole);

                fract = Fractional.Generate(tok);

                if (fract != null)
                    return new Significant(whole, fract);
            }

            return null;
        }
    }
}
