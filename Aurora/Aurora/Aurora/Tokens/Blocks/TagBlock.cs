/* Created: 28/08/2015
 * Last Updated: 28/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;

namespace Aurora.Tokens.Blocks
{
    /// <summary>
    /// Used to define a formula.
    /// </summary>
    internal sealed class TagBlock : Token
    {
        public TagBlock(params Token[] tokens) : base(tokens) { }

        /// <summary>
        /// Creates a formula using a set of Tokens.
        /// </summary>
        /// <param name="tokens">The tokens to be used.</param>
        /// <returns>The  generated Formula.</returns>
        public static TagBlock Generate(IEnumerable<Token> tokens)
        {
            Expression exp = Expression.Generate(tokens);

            return exp == null ? null : new TagBlock(exp);
        }
    }
}
