/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles.Attachments.Base
{
    /// <summary>
    /// Defines an Attachment that can be updated.
    /// </summary>
    public interface IUpdateAttachment : IAttachment
    {
        object Generate(object property);
    }
}
