/* Created: 28/08/2015
 * Last Updated: 28/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Aurora.Tokens.Blocks;
using Aurora.Tokens.Misc;
using System.Collections.Generic;
using System.Linq;

namespace Aurora.Tokens.Blocks
{
    /// <summary>
    /// Defines an expresive term.
    /// </summary>
    internal sealed class Expression : Token
    {
        /// <summary>
        /// Creates a new Token.
        /// </summary>
        /// <param name="tokens">The tokens that make up the Expression.</param>
        public Expression(params object[] tokens) : base(tokens) { }

        /// <summary>
        /// Creatse a new Expression.
        /// </summary>
        /// <param name="tokens">The Tokens that comprise the Expression.</param>
        /// <returns>The new EXpression.</returns>
        public static Expression Generate(IEnumerable<Token> tokens)
        {
            //int spacesBefore = tokens.TakeWhile(s => s is BlankSpace).Count();
            //int spacesAfter = tokens.Reverse().TakeWhile(s => s is BlankSpace).Count();

            //IEnumerable<Token> noSpace = tokens.Skip(spacesBefore).SkipLast(spacesAfter).ToList();

            //NullSpaceBlock noSpaceExp = NullSpaceBlock.Generate(noSpace);

            //if (noSpaceExp != null)
            //    return new Expression(Enumerable.Repeat(new BlankSpace(), spacesBefore), noSpaceExp,
            //        Enumerable.Repeat(new BlankSpace(), spacesAfter));

            return null;
        }
    }
}
