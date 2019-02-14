/* Created: 10/06/2013
 * Last Updated: 03/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Interfaces.Base;
using System.Collections.Generic;

namespace Eon.Rendering2D.Drawing
{
    /// <summary>
    /// Defines a draw layer that holds several DrawItems.
    /// </summary>
    internal sealed class DrawLayerCollection : IHoldReferences
    {
        List<IDrawItem> items = new List<IDrawItem>();

        int drawLayer = 0;

        /// <summary>
        /// The draw layer that IDrawItem's are to be drawn on.
        /// </summary>
        internal int DrawLayer
        {
            get { return drawLayer; }
        }

        public DrawLayerCollection(int drawLayer)
        {
            this.drawLayer = drawLayer;
        }

        public void Add(IDrawItem item)
        {
            if (item != null)
                if (!items.Contains(item))
                {
                    items.Add(item);

                    if (item is ObjectComponent)
                        ((ObjectComponent)item).AddReference(this);
                    else if (item is GameObject)
                        ((GameObject)item).AddReference(this);
                }
        }

        public void Draw(DrawingStage stage)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].Enabled)
                {
                    if (items[i] is IDestructable)
                    {
                        if (!(items[i] as IDestructable).IsDestroyed)
                            items[i].Draw(stage);
                    }
                    else
                        items[i].Draw(stage);
                }
                else if (!items[i].Enabled && items[i].RenderDisabled)
                    items[i].Draw(stage);
        }

        public void Remove(IDrawItem item)
        {
            if (item is ObjectComponent)
            {
                for (int i = 0; i < items.Count; i++)
                    if (items[i] is ObjectComponent && ((ObjectComponent)items[i]).ID == ((ObjectComponent)item).ID)
                        items.Remove(item);
            }
            else if (item is GameObject)
            {
                for (int i = 0; i < items.Count; i++)
                    if (items[i] is GameObject && ((GameObject)items[i]).ID == ((GameObject)item).ID)
                        items.Remove(item);
            }
            else
                if (items.Contains(item))
                    items.Remove(item);
        }

        public void Remove(object obj)
        {
            Remove(obj as IDrawItem);
        }
    }
}
