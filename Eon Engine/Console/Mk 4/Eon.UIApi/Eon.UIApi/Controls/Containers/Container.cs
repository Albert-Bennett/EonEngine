/* Created 03/09/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.States;
using System.Collections.Generic;
using System.Linq;

namespace Eon.UIApi.Controls.Containers
{
    /// <summary>
    /// Defines a basic Container object.
    /// </summary>
    public abstract class Container : GameObject
    {
        List<MenuItem> controls = new List<MenuItem>();

        protected List<MenuItem> Controls
        {
            get { return controls; }
        }

        /// <summary>
        /// Creates a new Container.
        /// </summary>
        /// <param name="id">The ID of the container.</param>
        /// <param name="presidence">The presidence of the Container.</param>
        public Container(string id, GameStates presidence)
            : base(id)
        {
            this.Presidence = presidence;
        }

        /// <summary>
        /// Adds a MenuItem to this Container.
        /// </summary>
        /// <param name="control">The MenuItem to be added.</param>
        public virtual void AddControl(MenuItem control)
        {
            MenuItem ctrl = (from c in Controls
                             where c.ID == control.ID
                             select c).FirstOrDefault();

            if (ctrl == null)
                _AddControl(control);
        }

        /// <summary>
        /// Adds the MenuItem to the controls.
        /// </summary>
        /// <param name="control">The MenuItem to be added.</param>
        protected virtual void _AddControl(MenuItem control)
        {
            Controls.Add(control);
        }

        public override void Destroy()
        {
            controls.Clear();

            base.Destroy();
        }
    }
}
