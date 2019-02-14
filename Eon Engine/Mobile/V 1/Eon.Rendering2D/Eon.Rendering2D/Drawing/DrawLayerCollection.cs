/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
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

        public void Draw()
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i] is ObjectComponent)
                {
                    if (((ObjectComponent)items[i]).Enabled)
                        items[i].Draw();
                }
                else
                    items[i].Draw();
        }

        public void Remove(IDrawItem item)
        {
            if (item is ObjectComponent)
            {
                for (int i = 0; i < items.Count; i++)
                    if (items[i] is ObjectComponent && ((ObjectComponent)items[i]).ID == ((ObjectComponent)item).ID)
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
