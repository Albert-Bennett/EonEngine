/* Created 03/09/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.States;
using Eon.UIApi.Controls._2D;
using Eon.UIApi.Screens;
using System.Linq;

namespace Eon.UIApi.Controls.Containers
{
    /// <summary>
    /// Defines a list of check boxes where only one, 
    /// of them can be turned on at any one time.
    /// </summary>
    public sealed class CheckList : Container
    {
        int currentIdx = 0;

        /// <summary>
        /// Creates a new CheckList.
        /// </summary>
        /// <param name="id">The ID of the CheckList.</param>
        /// <param name="presidence">The presidence of the CheckList.</param>
        public CheckList(string id, GameStates presidence) : base(id, presidence) { }

        /// <summary>
        /// Adds a new MenuItem to this CheckList.
        /// If the MenuItem is a CheckBox.
        /// </summary>
        /// <param name="checkBox">The CheckBox to be added.</param>
        public override void AddControl(MenuItem checkBox)
        {
            MenuItem chk = (from c in Controls
                            where c.ID == checkBox.ID
                            select c).FirstOrDefault();

            if (chk == null)
            {
                ((CheckBox)checkBox).OnChecked += new OnCheckToogledEvent(OnChecked);
                Controls.Add(checkBox);
            }
        }

        void OnChecked(string controlID, bool check)
        {
            if (check && Controls[currentIdx].ID != controlID)
            {
                ((CheckBox)Controls[currentIdx]).Checked = false;

                bool found = false;
                int i = 0;

                while (i < Controls.Count && !found)
                {
                    if (Controls[i].ID == controlID)
                    {
                        ((CheckBox)Controls[i]).Checked = true;
                        found = true;
                    }

                    i++;
                }
            }
        }
    }
}
