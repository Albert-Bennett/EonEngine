/* Created 05/10/2013
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
        static bool isPaused = false;

        static GameStates currentState = GameStates.MainMenu;
        static GameStates prevState = GameStates.MainMenu;

        public static GamePausedEvent OnGamePaused;
        public static GameUnPausedEvent OnGameUnPaused;

        /// <summary>
        /// Returns the current state of the game.
        /// </summary>
        public static GameStates CurrentState { get { return currentState; } }

        /// <summary>
        /// Returns the previous state of the game.
        /// </summary>
        public static GameStates PreviousState { get { return prevState; } }

        /// <summary>
        /// Wheather or not the game has been paused.
        /// </summary>
        public static bool IsGamePaused
        {
            get { return isPaused; }
        }

        /// <summary>
        /// Changes the state of the game.
        /// </summary>
        /// <param name="nextState">The GameState to change to.</param>
        public static void ChangeGameState(GameStates nextState)
        {
            prevState = currentState;
            currentState = nextState;
        }

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public static void PauseGame()
        {
            if (!isPaused)
            {
                isPaused = true;

                if (OnGamePaused != null)
                    OnGamePaused();
            }
        }

        /// <summary>
        /// Resumes the game.
        /// </summary>
        public static void ResumeGame()
        {
            if (isPaused)
            {
                isPaused = false;

                if (OnGameUnPaused != null)
                    OnGameUnPaused();
            }
        }
    }
}
