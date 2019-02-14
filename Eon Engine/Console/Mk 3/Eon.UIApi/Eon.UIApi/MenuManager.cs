/* Created: 05/09/2013
 * Last Updated: 24/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.System.Management;
using Eon.System.States;
using Eon.Testing;
using Eon.UIApi.Screens;
using System.Collections.Generic;
using System.Linq;

namespace Eon.UIApi
{
    public class MenuManager : EngineComponent
    {
        static EonDictionary<string, ParameterCollection> screens =
            new EonDictionary<string, ParameterCollection>();

        static List<PopUpMenu> popUps = new List<PopUpMenu>();

        static MenuScreen currentScreen;

        string menuSystemFilepath;

        /// <summary>
        /// Creates a new MenuManager.
        /// </summary>
        public MenuManager() : base("MenuManager") 
        {
            menuSystemFilepath = "MenuSystem.Menu";
        }

        /// <summary>
        /// Creates a new MenuManager.
        /// </summary>
        /// <param name="menuSystemFilepath">The filepath for the menu system.</param>
        public MenuManager(string menuSystemFilepath)
            : base("MenuManager")
        {
            this.menuSystemFilepath = menuSystemFilepath;

            if (!this.menuSystemFilepath.Contains(".Menu"))
                this.menuSystemFilepath += ".Menu";
        }

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
                MenuSystemSetup file = SerializationHelper.Deserialize<MenuSystemSetup>(menuSystemFilepath, true, "");

                if (file.Assemblies != null)
                    for (int i = 0; i < file.Assemblies.Length; i++)
                        AssemblyManager.AddAssemblyRef(file.Assemblies[i]);

                if (file.ScreenNames.Length > 0)
                    for (int i = 0; i < file.ScreenNames.Length; i++)
                        screens.Add(file.ScreenNames[i], file.Screens[i]);
            }
            catch { }
        }

        internal static void AddPopUp(PopUpMenu popUp)
        {
            PopUpMenu pop = (from p in popUps
                             where p.ID == popUp.ID
                             select p).FirstOrDefault();

            if (pop == null)
            {
                popUp.OnClosed += new OnPopUpCloseEvent(PopUpClosed);
                popUps.Add(popUp);
            }
            else
            {
                popUp.Destroy();

                new Error("The PopUpMenu: " + popUp.ID +
                    " was deleted because it is a duplicate.", Seriousness.Warning);
            }
        }

        static void PopUpClosed(string menuID)
        {
            bool found = false;
            int idx = 0;

            while (!found && idx < popUps.Count)
            {
                if (popUps[idx].ID == menuID)
                {
                    popUps.Remove(popUps[idx]);
                    found = true;
                }

                idx++;
            }
        }

        public static void SwitchScreen(string screenName)
        {
            bool switched = false;

            for (int i = 0; i < screens.Count; i++)
                if (screens.Keys[i] == screenName)
                {
                    if (currentScreen != null)
                        if (currentScreen.ID != screenName)
                        {
                            currentScreen._TransitionOff();
                            currentScreen = null;
                        }
                        else
                        {
                            switched = true;
                            new Error("Atempting to switch to the same screen: " + screenName, Seriousness.Warning);
                        }

                    if (!switched)
                        try
                        {
                            currentScreen = (MenuScreen)AssemblyManager.CreateInstance(screens.Values[i]);

                            GameStateManager.ChangeGameState(currentScreen.ScreenPrecidence);

                            switched = true;
                        }
                        catch
                        {
                            new Error("The screen: " + screenName +
                                " doesn't exist.", Seriousness.Error);
                        }
                }

            try
            {
                if (!switched)
                {
                    currentScreen._TransitionOff();
                    currentScreen = null;

                    GameStateManager.ChangeGameState(GameStates.Game);
                }
            }
            catch
            {
                new Error("The screen: " + screenName +
                    " doesn't exist.", Seriousness.Error);
            }
        }
    }
}
