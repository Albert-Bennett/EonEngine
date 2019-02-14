/* Created: 18/01/2015
 * Last Updated: 18/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EEDK.Crosswalk.Controls.ModControls
{
    /// <summary>
    /// Used to define a group of ModTextBoxes.
    /// </summary>
    /// <typeparam name="T">The type of each ModTextBox.</typeparam>
    public class GroupedTextBox<T> : IModControl
    {
        ModTextBox<T>[] boxes;
        TableLayoutPanel table;
        List<Label> desc = new List<Label>();

        int paramIndex;
        int row;

        string name;

        public int ParamIndex
        {
            get { return paramIndex; }
        }

        public int MaxRow
        {
            get { return row; }
        }

        public object ParamValue
        {
            get
            {
                if (boxes.Length == 4)
                {
                    if (typeof(T) == typeof(byte))
                    {
                        return new Microsoft.Xna.Framework.Color()
                        {
                            R = (byte)boxes[0].ParamValue,
                            G = (byte)boxes[1].ParamValue,
                            B = (byte)boxes[2].ParamValue,
                            A = (byte)boxes[3].ParamValue
                        };
                    }
                    else if (typeof(T) == typeof(float))
                    {
                        return new Vector4()
                        {
                            X = (float)boxes[0].ParamValue,
                            Y = (float)boxes[1].ParamValue,
                            Z = (float)boxes[2].ParamValue,
                            W = (float)boxes[3].ParamValue
                        };
                    }
                    else
                    {
                        return new Microsoft.Xna.Framework.Rectangle()
                        {
                            X = (int)boxes[0].ParamValue,
                            Y = (int)boxes[1].ParamValue,
                            Width = (int)boxes[2].ParamValue,
                            Height = (int)boxes[3].ParamValue
                        };
                    }
                }
                else if (boxes.Length == 3)
                    return new Vector3()
                    {
                        X = (float)boxes[0].ParamValue,
                        Y = (float)boxes[1].ParamValue,
                        Z = (float)boxes[2].ParamValue
                    };
                else
                    return new Vector2()
                    {
                        X = (float)boxes[0].ParamValue,
                        Y = (float)boxes[1].ParamValue
                    };
            }
        }

        public string Name
        {
            get { return name; }
        }

        public GroupedTextBox(string paramName, string name, T[] values,
            string[] names, int paramIndex,
            TableLayoutPanel parent, int row)
        {
            this.paramIndex = paramIndex;
            this.name = name;
            table = parent;

            boxes = new ModTextBox<T>[values.Length];

            row++;
            AddLabel(paramName, row);
            row++;

            for (int i = 0; i < values.Length; i++)
            {
                AddLabel(names[i], row);
                boxes[i] = new ModTextBox<T>(values[i], 0, parent, row);

                row++;
            }

            this.row = row++;
        }

        void AddLabel(string name, int row)
        {
            DockStyle dock = DockStyle.Right;

            if (name.Length > 2)
                dock = DockStyle.Left;

            Label label = ControlCreator.CreateLabel(name, dock);
            desc.Add(label);

            table.Controls.Add(label, 0, row);
        }

        public TableLayoutPanel Destroy(TableLayoutPanel panel)
        {
            for (int i = 0; i < boxes.Length; i++)
                panel.Controls.Remove(boxes[i].TextBox);

            for (int i = 0; i < desc.Count; i++)
                panel.Controls.Remove(desc[i]);

            return panel;
        }
    }
}
