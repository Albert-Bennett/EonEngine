/* Created 03/09/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.States;
using Eon.UIApi.Screens;

namespace EEDK.Gui.Skeletal
{
    /// <summary>
    /// Defines the main screen of the SkeletalAnimatior tool.
    /// </summary>
    public sealed class MainScreen : MenuScreen
    {
        public MainScreen() : base("MainScreen", GameStates.Game) { }
    }
}
