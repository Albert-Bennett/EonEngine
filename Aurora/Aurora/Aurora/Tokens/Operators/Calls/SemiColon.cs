/* Created: 29/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Calls
{
    /// <summary>
    /// Defines a ;.
    /// Used to define the end of an exprssion.
    /// ie. create: TestObjects/textObj .array: id 1 9.0 11;
    /// </summary>
    internal sealed class SemiColon : Token
    {
        public SemiColon() { }

        public override string ToString()
        {
            return ";";
        }
    }
}
