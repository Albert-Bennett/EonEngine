namespace EonEngineTool.Lib.Controls
{
    /// <summary>
    /// Defines an interface that modifies the 
    /// existing properties of a control 
    /// that is generated programatically.
    /// </summary>
    public interface IModControl
    {
        int ParamIndex { get; }
        object ParamValue { get; }
    }
}
