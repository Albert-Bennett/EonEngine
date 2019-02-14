/* Created 05/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.EngineComponents
{
    /// <summary>
    /// The various possible game states.
    /// </summary>
    public enum GameStates
    {
        MainMenu,
        InnerMenu,
        Game
    }

    /// <summary>
    /// Defines a EngineComponent that is used to manage game states.
    /// </summary>
    public sealed class GameStateManager : EngineComponent
    {
        static GameStates currentState = GameStates.MainMenu;
        static GameStates prevState = GameStates.MainMenu;

        /// <summary>
        /// Returns the current state of the game.
        /// </summary>
        public static GameStates CurrentState { get { return currentState; } }

        /// <summary>
        /// Returns the previous state of the game.
        /// </summary>
        public static GameStates PreviousState { get { return prevState; } }

        public GameStateManager() : base("GameStateManager") { }

        /// <summary>
        /// Changes the state of the game.
        /// </summary>
        /// <param name="nextState">The GameState to change to.</param>
        public static void ChangeGameState(GameStates nextState)
        {
            prevState = currentState;
            currentState = nextState;
        }
    }
}
