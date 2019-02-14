namespace Eon.Game
{
    /// <summary>
    /// A delegate uwsed to define what
    /// happends when the level is begining to be loaded.
    /// </summary>
    public delegate void StartLoadingEvent();

    /// <summary>
    /// A delegate used to define what happends
    /// when the game has finished loading.
    /// </summary>
    public delegate void EndLoadingEvent();

    /// <summary>
    /// A delegate that is used to signal when the TimerComponent has been elapsed.
    /// </summary>
    /// <param name="id">The id of the TimerComponent.</param>
    public delegate void TimeOutEvent(string id);

    /// <summary>
    /// A delegate used to signal when the 
    /// next LevelChunk is to be loaded.
    /// </summary>
    /// <param name="id">The id of the LevelChunk to be loaded.</param>
    public delegate void NextChunkLoadEvent(string id);
}
