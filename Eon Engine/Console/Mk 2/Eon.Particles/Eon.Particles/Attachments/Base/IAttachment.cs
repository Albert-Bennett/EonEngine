/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles.Attachments.Base
{
    /// <summary>
    /// An interface used to define attachments.
    /// </summary>
    public interface IAttachment
    {
        string ID { get; }
        AttachmentTypes AttachmentType { get; }

        object Generate();
    }
}
