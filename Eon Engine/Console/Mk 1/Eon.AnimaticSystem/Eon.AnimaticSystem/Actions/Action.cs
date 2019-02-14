/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.AnimaticSystem.Actions
{
    /// <summary>
    /// Used to define an action.
    /// </summary>
    public abstract class Action
    {
        string id;

        protected int streamNumber;

        /// <summary>
        /// A unique identifaction name 
        /// given to the Action.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        internal FinishedActionEvent OnComplete;

        public Action(string id, int streamNumber)
        {
            this.id = id;
            this.streamNumber = streamNumber;
        }

        /// <summary>
        /// Used to Execute an Action (Called every Update when active).
        /// </summary>
        public virtual void Execute() { }

        /// <summary>
        /// Used to end the execution of an Action.
        /// </summary>
        protected virtual void FinishExecution()
        {
            if (OnComplete != null)
                OnComplete();
        }
    }
}
