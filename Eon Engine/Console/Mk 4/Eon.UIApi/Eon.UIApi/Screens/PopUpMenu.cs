/* Created: 18/09/2014
 * Last Updated: 04/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.States;

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// Defines a type of menu which is used 
    /// in game.
    /// </summary>
    public abstract class PopUpMenu : Screen
    {
        internal OnPopUpCloseEvent OnClosed;

        /// <summary>
        /// Creates a new PopUpMenu.
        /// </summary>
        /// <param name="id">The ID of the PopUpMenu.</param>
        /// <param name="precidence">What game state is the PopUpMenu active in.</param>
        public PopUpMenu(string id, GameStates precidence)
            : base(id, precidence)
        {
            MenuManager.AddPopUp(this);
        }

        /// <summary>
        /// Closes the PopUpMenu.
        /// </summary>
        public void Close()
        {
            _TransitionOff();
        }

        protected override void TransitionOffAction()
        {
            if (OnClosed != null)
                OnClosed(ID);

            Destroy();

            base.TransitionOffAction();
        }
    }
}
