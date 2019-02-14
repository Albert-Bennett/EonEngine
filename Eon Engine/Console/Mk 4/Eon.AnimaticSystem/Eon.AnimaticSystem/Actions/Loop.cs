/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using System.Collections.Generic;

namespace Eon.AnimaticSystem.Actions
{
    /// <summary>
    /// Defines a loop action.
    /// </summary>
    public sealed class Loop : Action
    {
        List<Action> actions = new List<Action>();
        string[] actionIDs;

        int loopAmount = 1;
        int currentLoop = 0;

        int index = 0;

        /// <summary>
        /// Creates a new Loop Action. 
        /// </summary>
        /// <param name="id">The unique identifaction name 
        /// to be given to the Loop.</param>
        /// <param name="streamNumber">The index of the AnimaticStream 
        /// that this will be executing on.</param>
        /// <param name="loopNumber">The number of times to 
        /// cycle through each Action.</param>
        /// <param name="actionIDs">The ID's of the Actions 
        /// that are going to be looped through.</param>
        public Loop(string id, int streamNumber,
            int loopNumber, string[] actionIDs)
            : base(id, streamNumber)
        {
            loopAmount = loopNumber;
            this.actionIDs = actionIDs;
        }

        internal void Initialize()
        {
            AnimaticManager am = (AnimaticManager)EngineModuleManager.Find("AnimaticManager");

            if (am != null)
            {
                AnimaticStream stream = am.GetStream(streamNumber);
                actions = stream.GetActions(actionIDs);
            }

            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].OnComplete = null;
                actions[i].OnComplete += new FinishedActionEvent(Complete);
            }
        }

        void Complete()
        {
            index++;

            if (index >= actions.Count)
            {
                index = 0;
                currentLoop++;
            }
        }

        public override void Execute()
        {
            if (currentLoop < loopAmount)
                ExecuteAction();
            else
                FinishExecution();
        }

        void ExecuteAction()
        {
            actions[index].Execute();
        }
    }
}
