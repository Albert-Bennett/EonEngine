/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls
{
    /// <summary>
    /// Defines an absrtract class for which all menu controls will inherit from. 
    /// </summary>
    public abstract class MenuItem : ObjectComponent
    {
        int idx;

        public MenuItem(string id) : base(id) { }

        internal void _SetIndex(int index)
        {
            this.idx = index;
        }

        public abstract bool IsTouched(Vector2 touchLocation);
    }
}
