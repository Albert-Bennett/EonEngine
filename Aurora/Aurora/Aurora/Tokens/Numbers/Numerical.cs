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
    /// Defines a float.
    /// </summary>
    internal sealed class Numerical : Token
    {
        public Numerical(params Token[] tokens) : base(tokens) { }

        public static Numerical Generate(IEnumerable<Token> tokens)
        {
            Significant sign = Significant.Generate(tokens);

            if (sign != null)
                return new Numerical(sign);

            NegitiveSign negitive = NegitiveSign.Generate(tokens.First());

            if (negitive != null)
            {
                sign = Significant.Generate(tokens.Skip(1));

                if (sign != null)
                    return new Numerical(negitive, sign);
            }

            return null;
        }
    }
}
