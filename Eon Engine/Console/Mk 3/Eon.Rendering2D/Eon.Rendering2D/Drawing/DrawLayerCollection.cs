/* Created: 10/06/2013
 * Last Updated: 24/11/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using System.Collections.Generic;

namespace Eon.Rendering2D.Drawing
{
    internal sealed class DrawLayerCollection : IHoldReferences
    {
        List<IDrawItem> items = new List<IDrawItem>();

        internal int _drawLayer = 0;

        public DrawLayerCollection(int drawLayer)
        {
            _drawLayer = drawLayer;
        }

        public void Add(IDrawItem item)
        {
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
                if (items[i] is IEnabled)
                {
                    if (((IEnabled)items[i]).Enabled)
                        items[i].Draw(stage);
                }
                else
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
