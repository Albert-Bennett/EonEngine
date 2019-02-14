/* Created 16/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;

namespace Eon.AnimaticSystem.Actions.Misc
{
    /// <summary>
    /// Used to define an object 
    /// that can preform random tasks.
    /// </summary>
    public sealed class Random : Action, IOutput
    {
        string target;

        int minNum;
        int maxNum;

        int random;

        /// <summary>
        /// The ID of the Action that will have it's 
        /// input method set to, the output 
        /// of this Random Action.
        /// </summary>
        public string Target
        {
            get { return target; }
        }

        /// <summary>
        /// Creates a new Random Action.
        /// </summary>
        /// <param name="id">The ID of this Random Action.</param>
        /// <param name="streamNumber">The stream number that 
        /// this Random will be active on.</param>
        /// <param name="targetID">The ID of the Random's target.</param>
        /// <param name="minNum">Minumum number.</param>
        /// <param name="maxNum">Maximum number.</param>
        public Random(string id, int streamNumber,
            string targetID, int minNum, int maxNum)
            : base(id, streamNumber)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;

            target = targetID;
        }

        /// <summary>
        /// Creates a new Random Action.
        /// </summary>
        /// <param name="id">The ID of this Random Action.</param>
        /// <param name="streamNumber">The stream number that 
        /// this Random will be active on.</param>
        /// <param name="targetID">The ID of the Random's target.</param>
        /// <param name="maxNum">Maximum number.</param>
        public Random(string id, int streamNumber, string targetID, int maxNum) :
            this(id, streamNumber, targetID, 0, maxNum) { }

        public override void Execute()
        {
            random = RandomHelper.GetRandomInt(minNum, maxNum);

            base.Execute();
        }

        public object Output()
        {
            return random;
        }
    }
}
