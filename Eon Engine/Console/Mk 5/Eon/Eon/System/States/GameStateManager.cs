/* Created: 05/10/2013
 * Last Updated: 07/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.States
{
    /// <summary>
    /// Defines a EngineComponent that is used to manage game states.
    /// </summary>
    public static class GameStateManager
    {
        static GameStates currentState = GameStates.Game;
        static GameStates prevState = GameStates.Game;

        public static GameStateChangedEvent OnGameStateChanged;

        /// <summary>
        /// Returns the current state of the game.
        /// </summary>
        public static GameStates CurrentState { get { return currentState; } }

        /// <summary>
        /// Returns the previous state of the game.
        /// </summary>
        public static GameStates PreviousState { get { return prevState; } }

        /// <summary>
        /// Changes the state of the game.
        /// </summary>
        /// <param name="nextState">The GameState to change to.</param>
        public static void ChangeGameState(GameStates nextState)
        {
            if (nextState != currentState && nextState != GameStates.None)
            {
                prevState = currentState;
                currentState = nextState;

                if (OnGameStateChanged != null)
                    OnGameStateChanged(nextState);
            }
        }
    }
}
