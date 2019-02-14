/* Created 05/12/2014
 * Last Updated: 05/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon
{
    /// <summary>
    /// Used to define a message that can be sent to a GameObject.
    /// </summary>
    public class Message
    {
        public string TargetID = "";
        public string MethodName = "";
        public object[] Parameters = null;
    }
}
