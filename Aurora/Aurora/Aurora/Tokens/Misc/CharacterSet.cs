/* Created:: 29/08/2015
 * Last Updated: 02/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Misc
{
    /// <summary>
    /// Defines letter.
    /// </summary>
    internal sealed class CharacterSet : Token
    {
        string value;

        /// <summary>
        /// Creates a new Character.
        /// </summary>
        /// <param name="value">The value of the Character.</param>
        public CharacterSet(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
