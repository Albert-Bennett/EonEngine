/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Eon.Particles2D.Types;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines an attachment.
    /// </summary>
    public interface IAttachment : IUpdate, IChangeable, IID
    {
        List<PropertySet> Properties { get; }

        void ScreenResolutionChanged();
        void Dispose();
    }
}
