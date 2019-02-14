/* Created: 05/04/2015
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Engine
{
    /// <summary>
    /// A class that defines input taken fromm multiple input devices.
    /// </summary>
    public class InputHandler
    {
        static EonDictionary<string, Command> inputs;

        /// <summary>
        /// Sets the input commands.
        /// </summary>
        /// <param name="commands">The commands.</param>
        internal static void SetInputCommands(EonDictionary<string, Command> commands)
        {
            inputs = commands;
        }

        /// <summary>
        /// Check a command aginst the InputManager.
        /// </summary>
        /// <param name="commandName">The name of the Command.</param>
        /// <returns>Result of the check.</returns>
        public static bool GetInput(string commandName)
        {
            if (inputs.Contains(commandName))
                for (int i = 0; i < inputs[commandName].Messages.Length; i++)
                {
                    object res = Framework.InputManager.SendMessage(inputs[commandName].Messages[i]);

                    try
                    {
                        if ((bool)res == true)
                            return true;
                    }
                    catch { }
                }

            return false;
        }
    }
}
