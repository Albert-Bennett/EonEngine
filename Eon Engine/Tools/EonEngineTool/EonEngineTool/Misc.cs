using Eon.Game2D.TileEngine.Management;
using EonEngineTool.Lib.ContainerDocks;
using EonEngineTool.ObjectCreation.Shader;

namespace EonEngineTool
{
    /// <summary>
    /// Used to signal the sending back of a TileMapDeffineation.
    /// </summary>
    /// <param name="map">The TileMapDeffination to be sent back.</param>
    public delegate void OnSendInfoEvent(TileMapDeffination map);

    /// <summary>
    /// A delegate used to mage what happens when a form is closed.
    /// </summary>
    public delegate void OnCloseEvent();

    /// <summary>
    /// Defines a delegate that is used to signal the closing of a child form.
    /// </summary>
    public delegate void OnExitedChildFormEvent();

    /// <summary>
    /// Defines a delegate that is used to relay 
    /// the information from a click event of a button.
    /// </summary>
    /// <param name="sender">The object that the button is in.</param>
    public delegate void OnClickRelayEvent(SelectionDock sender);

    /// <summary>
    /// Defines a delegate that is uised to signal when
    /// an attached combo box has had it's elected item changed.
    /// </summary>
    /// <param name="sender">The object that the combo box is in.</param>
    public delegate void OnComboBoxSelectedChangedEvent(SelectionDock sender);

    /// <summary>
    /// Used to signal when a shader parameter has been removed.
    /// </summary>
    /// <param name="sender">The ShaderParameterDock to be removed.</param>
    public delegate void OnRemoveShaderParameter(ShaderParameterDock sender);

    /// <summary>
    /// Used to send back lines of text.
    /// </summary>
    /// <param name="lines">Text to be sent back.</param>
    public delegate void OnSendBackLinesEvent(string[] lines);

    /// <summary>
    /// Used to send a line of text back.
    /// </summary>
    /// <param name="text">The text to be sent back.</param>
    public delegate void OnSendBackTextEvent(string text);

    /// <summary>
    /// Used to send an object back to a parent form.
    /// </summary>
    /// <param name="obj">The object to be sent back.</param>
    public delegate void OnSendBackObjectEvent(object obj);
}
