/* Created: 29/08/2015
 * Last Updated: 02/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Aurora.Tokens.Operators.Calls
{
    /// <summary>
    /// Defines a #.
    /// Used to define a block of / single 
    /// instruction that can be executed.
    /// 
    /// #btnStyle
    /// {
    /// prop_set: testID0 5;
    /// prop_set: testID1 #testColour; 
    /// }
    /// 
    /// #testColour colour: 255 255 255 255;
    /// </summary>
    internal sealed class HashID : Token
    {
        string value;

        public HashID(string value) 
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
