/* Created: 26/01/2015
 * Last Updated: 18/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace EEDK.Gui
{
    /// <summary>
    /// Used to signal when the value of a 
    /// GroupedControlBox has had it's value changed.
    /// </summary>
    /// <param name="name">The name of the GroupedControlBox.</param>
    /// <param name="value">The new value in the GroupedControlBox.</param>
    internal delegate void ValueChangedEvent(string name, float value);

    /// <summary>
    /// Used to signal when a request to save an item has been made.
    /// </summary>
    /// <param name="filepath">The file path.</param>
    public delegate void SaveRequestEvent(string filepath);

    /// <summary>
    /// Used to signal when a requst to load a file has been made. 
    /// </summary>
    /// <param name="filepath">The file path.</param>
    public delegate void LoadRequestEvent(string filepath);

    /// <summary>
    /// A delegate that is signaled when the 
    /// source tile for selecting tiles has changed.
    /// </summary>
    /// <param name="index">The index of the source tile.</param>
    public delegate void SourceTileChangedEvent(int index);

    /// <summary>
    /// A delegate used to signal when a Tilemap has been loaded sucessfully.
    /// </summary>
    public delegate void LoadedTilemapEvent();
}
