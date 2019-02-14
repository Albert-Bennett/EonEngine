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
    /// Used to signal when the game has been paused. 
    /// </summary>
    public delegate void GamePausedEvent();

    /// <summary>
    /// Used to signal when the game has been unpaused.
    /// </summary>
    public delegate void GameUnPausedEvent();
}
