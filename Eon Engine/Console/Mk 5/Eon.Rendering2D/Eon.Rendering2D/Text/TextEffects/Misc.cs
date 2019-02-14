/* Created: 09/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Rendering2D.Text.TextEffects
{
    /// <summary>
    /// A delegate used to signal the completion of a Text Effect.
    /// </summary>
    /// <param name="id">The ID of the Text Effect.</param>
    public delegate void FinishedTextEffectEvent(string id);

    /// <summary>
    /// A delegate used to signal when a Texteffect has generated something.
    /// </summary>
    /// <param name="id">The ID of the TextEffect.</param>
    public delegate void HasGeneratedEvent(string id);
}
