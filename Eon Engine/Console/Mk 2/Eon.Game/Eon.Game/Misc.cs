﻿namespace Eon.Game
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
}