/* Created: 28/08/2015
 * Last Updated: 28/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;
using System.Linq;
using System;

namespace Aurora.Tokens
{
    /// <summary>
    /// Defines a ascii character.
    /// </summary>
    internal abstract class Token
    {
        List<Token> constituentTokens;

        /// <summary>
        /// The tokens that makes up the token.
        /// </summary>
        public List<Token> ConstituentTokens
        {
            get { return constituentTokens; }
            protected set { constituentTokens = value; }
        }

        /// <summary>
        /// Creates a new defined Token.
        /// </summary>
        public Token() { }

        /// <summary>
        /// Creates a new Token.
        /// </summary>
        /// <param name="characters">The characters that make up the Token.</param>
        public Token(object[] characters)
        {
            constituentTokens = new List<Token>();

            foreach (object tkn in characters)
                if (tkn is Token)
                    constituentTokens.Add(tkn as Token);
                else if (tkn is IEnumerable<Token>)
                    foreach (Token t in tkn as IEnumerable<Token>)
                        constituentTokens.Add(t);
                else
                    throw new ArgumentException("Error in Parsing");
        }

        public override string ToString()
        {
            return constituentTokens.Select(t => t.ToString()).Concatenate();
        }
    }
}
