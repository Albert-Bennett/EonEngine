/* Created: 28/08/2015
 * Last Updated: 28/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Misc
{
    /// <summary>
    /// Defines a digit. (0 -> 9).
    /// </summary>
    internal sealed class ByteDigit : Token
    {
        char value;

        /// <summary>
        /// Creates a new digit.
        /// </summary>
        /// <param name="value">The value of the digit.</param>
        public ByteDigit(char value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
