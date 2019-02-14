/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using Eon.Particles2D.Renders;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines an attachment.
    /// </summary>
    public interface IAttachment : IUpdate, IChangeable, IID
    {
        List<PropertySet> Properties { get; }

        void Dispose();
    }
}
