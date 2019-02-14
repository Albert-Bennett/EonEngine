using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eon.Engine.Audio
{
    /// <summary>
    /// A delegate to manage the automatic stopping of a JSong.
    /// </summary>
    /// <param name="song">The JSong.</param>
    internal delegate void OnStoppedEvent(JJaxCue song);
}
