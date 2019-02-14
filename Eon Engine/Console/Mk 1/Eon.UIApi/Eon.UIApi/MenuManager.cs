/* Created 05/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.EngineComponents;
using Eon.Interfaces;
using Eon.UIAPI.Screens;
using System;
using System.Collections.Generic;

namespace Eon.UIAPI
{
    public class MenuManager : EngineComponent
    {
        static List<MenuScreen> screens = new List<MenuScreen>();
        static MenuScreen currentScreen;

        public MenuManager() : base("MenuManager") { }

        protected override void Initialize()
        {
            base.Initialize();

            CreateScreens();

            if (screens.Count > 0)
                SwitchScreen(screens[0].ID);
        }

        void CreateScreens()
        {
            try
            {
                MenuSystemSetup file = Common.ContentManager.Load<MenuSystemSetup>("MenuSystem");

                if (file.Assemblies.Length > 0)
                    for (int i = 0; i < file.Assemblies.Length; i++)
                        AssemblyManager.AddAssemblyRef(file.Assemblies[i]);

                if (file.Screens.Count > 0)
                    for (int i = 0; i < file.Screens.Count; i++)
                    {
                        object obj = AssemblyManager.CreateInstance(file.Screens[i]);

                        if (obj != null && obj is MenuScreen)
                            screens.Add(obj as MenuScreen);
                    }
            }
            catch (Exception ex) { }
        }

        public static void SwitchScreen(string screenName)
        {
            bool switched = false;

            for (int i = 0; i < screens.Count; i++)
                if (screens[i].ID == screenName)
                {
                    if (currentScreen != null)
                        currentScreen._TransitionOff();

                    currentScreen = screens[i];
                    currentScreen._TransitionOn();

                    GameStateManager.ChangeGameState(currentScreen.ScreenPrecidence);

                    switched = true;
                }

            if (!switched)
            {
                GameStateManager.ChangeGameState(GameStates.Game);

                if (currentScreen != null)
                    currentScreen._TransitionOff();

                currentScreen = null;
            }
        }

        public static void Remove(MenuScreen screen)
        {
            screens.Remove(screen);
        }
    }
}
