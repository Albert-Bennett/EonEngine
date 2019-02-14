/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.AnimaticSystem.Actions;
using Eon.System.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Eon.AnimaticSystem
{
    /// <summary>
    /// Creates a new AnimaticStream.
    /// </summary>
    internal sealed class AnimaticStream : IUpdate
    {
        List<Action> actions = new List<Action>();
        internal EndOfStreamEvent OnEnd;

        int streamNumber;
        int index = 0;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new AnimaticStream. 
        /// </summary>
        /// <param name="info">The file to get the 
        /// AnimaticStream's information from.</param>
        public AnimaticStream(StreamInfo info, int streamNumber)
        {
            this.streamNumber = streamNumber;

            for (int i = 0; i < info.Actions.Length; i++)
            {
                object act = AssemblyManager.CreateInstance(info.Actions[i]);

                if (act is Action)
                {
                    ((Action)act).OnComplete +=
                        new FinishedActionEvent(EndAction);

                    actions.Add(act as Action);
                }
            }
        }

        internal void Init()
        {
            for (int i = 0; i < actions.Count; i++)
                if (actions[i] is Loop)
                    ((Loop)actions[i]).Initialize();
        }

        /// <summary>
        /// Finds a series of Actions and stops them 
        /// from being managed by the AnimaticStream.
        /// </summary>
        /// <param name="actionIDs">The IDs of all of the Actions to find.</param>
        /// <returns>The found Actions.</returns>
        public List<Action> GetActions(string[] actionIDs)
        {
            List<Action> actions = new List<Action>();

            for (int i = 0; i < actionIDs.Length; i++)
                actions.Add(GetAction(actionIDs[i], true));

            return actions;
        }

        /// <summary>
        /// Finds an Action that has the given ID.
        /// </summary>
        /// <param name="actionID">The ID of the Action to get.</param>
        /// <param name="remove">Wheather or not to remove the 
        /// found Action(if one) from the collection of managable actions.</param>
        /// <returns>The found Action (or null if an Action hasn't been found).</returns>
        public Action GetAction(string actionID, bool remove)
        {
            Action act = null;

            act = (from a in actions
                   where a.ID == actionID
                   select a).FirstOrDefault();

            if (act != null && remove)
                actions.Remove(act);

            return act;
        }

        void EndAction()
        {
            if (actions[index] is IOutput)
            {
                Action act = GetAction(((IOutput)actions[index]).Target, false);

                if (act != null && act is IInput)
                    ((IInput)act).Input(((IOutput)actions[index]).Output());
            }

            actions.Remove(actions[index]);
            index++;
        }

        public void _Update()
        {
            if (index < actions.Count)
                actions[index].Execute();
            else
                if (OnEnd != null)
                    OnEnd(streamNumber);
        }

        public void _PostUpdate() { }

        public void Dispose()
        {
            actions.Clear();
            actions = null;
        }
    }
}
