/* Created: 02/09/2015
 * Last Updated: 02/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Keywords.Variables
{
    /// <summary>
    /// Defines a vector icon.
    /// </summary>
    internal sealed class VectorToken : Token
    {
        int size;

        public int Size { get { return size; } }

        public VectorToken(int size)
        {
            this.size = size;
        }

        public override string ToString()
        {
            return "Vector";
        }
    }
}
