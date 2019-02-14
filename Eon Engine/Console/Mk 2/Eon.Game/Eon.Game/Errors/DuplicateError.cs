/* Created 30/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Testing.ErrorManagement;

namespace Eon.Game.Errors
{
    internal class DuplicateError : Error
    {
        public DuplicateError(string text) : base(text, Seriousness.Warning) { }
    }
}
