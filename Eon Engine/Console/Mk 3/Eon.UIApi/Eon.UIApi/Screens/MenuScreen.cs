/* Created: 04/09/2013
 * Last Updated: 18/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.States;

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// Defines a basic 2D / 3D screen.
    /// </summary>
    public abstract class MenuScreen : Screen
    {
        GameStates screenPrecidence = GameStates.MainMenu;

        internal GameStates ScreenPrecidence
        {
            get { return screenPrecidence; }
        }

        /// <summary>
        /// Creates a new MenuScreen.
        /// </summary>
        /// <param name="id">The id for this MenuScreen.</param>
        /// <param name="screenPrecidence">Used to manage what precidence
        /// this MenuScreen works under.</param>
        public MenuScreen(string id, GameStates screenPrecidence)
            : base(id)
        {
            this.screenPrecidence = screenPrecidence;
        }

        /// <summary>
        /// Switches between screens. 
        /// </summary>
        /// <param name="screenName">The name of a screen to switch to. 
        /// Given a name of a non existant screen results in switching to no screen.</param>
        protected void SwitchScreen(string screenName)
        {
            MenuManager.SwitchScreen(screenName);
        }
    }
}
