/* Created: 02/09/2015
 * Last Updated: 02/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Aurora.Tokens.Operators.Calls;
using System.Collections.Generic;
using System.Linq;

namespace Aurora.Tokens.Keywords.Variables
{
    /// <summary>
    /// Defines a row in a multi dimentional array.
    /// </summary>
    internal sealed class ArrayItemToken : Token
    {
        public ArrayItemToken(params object[] token) : base(token) { }

        //public static ArrayItemToken Generate(IEnumerable<Token> tokens)
        //{
        //    if (tokens.First() is OpenedCrotchet &&
        //        tokens.Last() is ClosedCrotchet)
        //    {

        //    }

        //    return null;
        //}

        public override string ToString()
        {
            return "arrayitem";
        }
    }
}
