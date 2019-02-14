/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;

namespace Aurora.Tokens.Blocks
{
    /// <summary>
    /// Defines an expression that has no spaces.
    /// </summary>
    internal sealed class NullSpaceBlock : Token
    {
        public NullSpaceBlock(params object[] tokens) : base(tokens) { }

        public static NullSpaceBlock Generate(IEnumerable<Token> tokens)
        {


            return new NullSpaceBlock(tokens);
        }
    }
}
