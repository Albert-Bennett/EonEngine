/* Created 05/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.System.Management;
using Eon.System.States;
using Eon.UIApi.Screens;

namespace Eon.UIApi
{
    public class MenuManager : EngineComponent
    {
        static EonDictionary<string, ParameterCollection> screens =
            new EonDictionary<string, ParameterCollection>();

        static MenuScreen currentScreen;

        public MenuManager() : base("MenuManager") { }

        protected override void Initialize()
        {
            base.Initialize();

            CreateScreens();

            if (screens.Count > 0)
                SwitchScreen(screens.Keys[0]);
        }

        void CreateScreens()
        {
            try
            {
                MenuSystemSetup file = XmlHelper.DeserializeContent<MenuSystemSetup>("MenuSystem.Menu");

                if (file.Assemblies != null)
                    for (int i = 0; i < file.Assemblies.Length; i++)
                        AssemblyManager.AddAssemblyRef(file.Assemblies[i]);

                if (file.ScreenNames.Length > 0)
                    for (int i = 0; i < file.ScreenNames.Length; i++)
                        screens.Add(file.ScreenNames[i], file.Screens[i]);
            }
            catch { }
        }

        public static void SwitchScreen(string screenName)
        {
            bool switched = false;

            for (int i = 0; i < screens.Count; i++)
                if (screens.Keys[i] == screenName)
                {
                    if (currentScreen != null)
                    {
                        currentScreen.Destroy();
                        currentScreen = null;
                    }

                    currentScreen = (MenuScreen)AssemblyManager.CreateInstance(screens.Values[i]);
                    currentScreen._TransitionOn();

                    GameStateManager.ChangeGameState(currentScreen.ScreenPrecidence);

                    switched = true;
                }

            if (!switched)
            {
                currentScreen.Destroy();
                currentScreen = null;

                GameStateManager.ChangeGameState(GameStates.Game);
            }
        }

        /// <summary>
        /// Finds a MenuScreen of the given name.
        /// </summary>
        /// <param name="name">The name of the MenuScreen to get.</param>
        /// <returns>The result of the search.</returns>
        public static MenuScreen GetScreen(string name)
        {
            ParameterCollection param = null;

            if (screens[name] != null)
                param = screens[name];

            return AssemblyManager.CreateInstance(param) as MenuScreen;
        }
    }
}
