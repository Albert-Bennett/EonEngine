using Eon.System.Interfaces;
using Eon.System.States;

namespace Eon
{
    /// <summary>
    /// An event to deal with the exiting of the game.
    /// </summary>
    public delegate void ExitingEvent();

    /// <summary>
    /// A delegate used to define what hapends when 
    /// the texture quality of the game has changed.
    /// </summary>
    public delegate void TextureQualityChangedEvent();

    /// <summary>
    /// A delegate used by Common to signal the showing of the mouse.
    /// </summary>
    public delegate void ShowMouseEvent(bool show);

    /// <summary>
    /// An event to deal with the changing of GameStates.
    /// </summary>
    public delegate void GameStateChangedEvent(GameStates state);

    /// <summary>
    /// A delegate used to manage the activation of certain controls. 
    /// </summary>
    /// <param name="activeObject">The newly active object.</param>
    public delegate void OnActivatedEvent(IActive activeObject);
}
