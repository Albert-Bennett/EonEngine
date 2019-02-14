/* Created: 20/01/2015
 * Last Updated: 24/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk.Controls;
using EEDK.Crosswalk.Controls.ModControls;
using EEDK.Crosswalk.Helpers;
using Eon;
using Eon.Collections;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace EEDK.Crosswalk.Docks
{
    /// <summary>
    /// Defines a container that hold the controls
    /// apparent to a ParameterCollection
    /// </summary>
    public abstract class PropertyDock
    {
        Label endLabel;
        protected List<IModControl> ctrls = new List<IModControl>();
        protected ParameterCollection obj;
        protected TableLayoutPanel table;

        protected int maxRow = 0;
        protected int initialRow;
        protected string id;

        /// <summary>
        /// Returns the modified object.
        /// </summary>
        public virtual ParameterCollection Object
        {
            get
            {
                for (int i = 0; i < obj.Parameters.Length; i++)
                    obj.Parameters[i] = ctrls[i].ParamValue;

                return obj;
            }
        }

        public int MaxRow
        {
            get { return maxRow; }
        }

        public int InitialRow
        {
            get { return initialRow; }
            set { initialRow = value; }
        }

        public string ID
        {
            get { return id; }
        }

        public PropertyDock(ParameterCollection obj,
            TableLayoutPanel parent, int row, string id)
        {
            this.obj = obj;
            this.id = id;

            table = parent;
            maxRow = initialRow = row;
        }

        public virtual void Create()
        {
            string name = PropertyHelper.GetTypeName(obj.ObjectType);

            ConstructorInfo[] ctors = AssemblyManager.GetType(obj.ObjectType).GetConstructors();
            ConstructorInfo info = null;

            if (obj.Parameters != null)
            {
                for (int i = 0; i < ctors.Length; i++)
                    if (ctors[i].GetParameters().Length == obj.Parameters.Length)
                        info = ctors[i];
            }
            else
            {
                if (ctors.Length > 1)
                {
                    int max = 0;

                    for (int i = 0; i < ctors.Length; i++)
                        if (ctors[i].GetParameters().Length > max)
                        {
                            info = ctors[i];
                            max = info.GetParameters().Length;
                        }

                    obj.Parameters = new object[max];
                }
                else
                {
                    info = ctors[0];
                    obj.Parameters = new object[info.GetParameters().Length];
                }
            }

            ParameterInfo[] param = info.GetParameters();

            for (int i = 0; i < param.Length; i++)
            {
                string paramName = PropertyHelper.GetName(param[i].Name);

                Type t = param[i].ParameterType;

                if (t == typeof(bool))
                {
                    table.Controls.Add(ControlCreator.CreateLabel(paramName, DockStyle.Left), 0, maxRow);

                    bool val = false;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            val = (bool)obj.Parameters[i];
                        else
                            obj.Parameters[i] = val;
                    }
                    catch { }

                    ctrls.Add(new ModCheckBox("", val, i, table,
                        maxRow, paramName));

                    maxRow++;
                }
                else if (t == typeof(string))
                {
                    table.Controls.Add(ControlCreator.CreateLabel(paramName, DockStyle.Left), 0, maxRow);

                    string val = "";

                    try
                    {
                        if (obj.Parameters[i] != null)
                            val = (string)obj.Parameters[i];
                        else
                            obj.Parameters[i] = val;
                    }
                    catch { }

                    if (paramName.Contains("Filepath"))
                    {
                        ModTextBox<string> txt = new ModTextBox<string>(
                            val, "", i, table, maxRow, new string[] { ".xnb" }, true);

                        ctrls.Add(txt);
                    }
                    else
                    {
                        ModTextBox<string> txt = new ModTextBox<string>(
                            val, i, table, maxRow);

                        ctrls.Add(txt);
                    }

                    maxRow++;
                }
                else if (t == typeof(Int32))
                {
                    table.Controls.Add(ControlCreator.CreateLabel(paramName, DockStyle.Left), 0, maxRow);

                    int val = 0;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            val = (int)obj.Parameters[i];
                        else
                            obj.Parameters[i] = val;
                    }
                    catch { }

                    ctrls.Add(new ModTextBox<int>(
                        val, i, table, maxRow));

                    maxRow++;

                }
                else if (t == typeof(Single))
                {
                    table.Controls.Add(ControlCreator.CreateLabel(paramName, DockStyle.Left), 0, maxRow);

                    float val = 0.0f;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            val = (float)obj.Parameters[i];
                        else
                            obj.Parameters[i] = val;
                    }
                    catch { }

                    ctrls.Add(new ModTextBox<float>(
                       val, i, table, maxRow));

                    maxRow++;
                }
                else if (t == typeof(Vector2))
                {
                    Vector2 vec = Vector2.Zero;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            vec = (Vector2)obj.Parameters[i];
                        else
                            obj.Parameters[i] = vec;
                    }
                    catch { }

                    float[] array = new float[]
                    {
                        vec.X,vec.Y
                    };

                    string[] names = new string[]
                    {
                       "X","Y"
                    };

                    GroupedTextBox<float> group = new GroupedTextBox<float>(
                        paramName, "", array, names, i, table, maxRow);

                    ctrls.Add(group);

                    maxRow = group.MaxRow;
                }
                else if (t == typeof(Vector3))
                {
                    Vector3 vec = Vector3.Zero;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            vec = (Vector3)obj.Parameters[i];
                        else
                            obj.Parameters[i] = vec;
                    }
                    catch { }

                    float[] array = new float[]
                    {
                        vec.X,vec.Y, vec.Z
                    };

                    string[] names = new string[]
                    {
                       "X","Y","Z"
                    };

                    GroupedTextBox<float> group = new GroupedTextBox<float>(
                        paramName, "", array, names, i, table, maxRow);

                    ctrls.Add(group);

                    maxRow = group.MaxRow;
                }
                else if (t == typeof(Vector4))
                {
                    Vector4 vec = Vector4.Zero;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            vec = (Vector4)obj.Parameters[i];
                        else
                            obj.Parameters[i] = vec;
                    }
                    catch { }

                    float[] array = new float[]
                    {
                        vec.X,vec.Y,vec.Z, vec.W
                    };

                    string[] names = new string[]
                    {
                       "X","Y","Z","W"
                    };

                    GroupedTextBox<float> group = new GroupedTextBox<float>(
                        paramName, "", array, names, i, table, maxRow);

                    ctrls.Add(group);

                    maxRow = group.MaxRow;
                }
                else if (t == typeof(Microsoft.Xna.Framework.Color))
                {
                    Microsoft.Xna.Framework.Color col =
                        Microsoft.Xna.Framework.Color.White;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            col = (Microsoft.Xna.Framework.Color)obj.Parameters[i];
                        else
                            obj.Parameters[i] = col;
                    }
                    catch { }

                    byte[] array = new byte[]
                    {
                        col.R,col.G,col.B, col.A
                    };

                    string[] names = new string[]
                    {
                       "R","G","B","A"
                    };

                    GroupedTextBox<byte> group = new GroupedTextBox<byte>(
                        paramName, "", array, names, i, table, maxRow);

                    ctrls.Add(group);

                    maxRow = group.MaxRow;
                }
                else if (t == typeof(Microsoft.Xna.Framework.Rectangle))
                {
                    Microsoft.Xna.Framework.Rectangle rect =
                        Microsoft.Xna.Framework.Rectangle.Empty;

                    try
                    {
                        if (obj.Parameters[i] != null)
                            rect = (Microsoft.Xna.Framework.Rectangle)obj.Parameters[i];
                        else
                            obj.Parameters[i] = rect;
                    }
                    catch { }

                    int[] array = new int[]
                    {
                        rect.X,rect.Y,rect.Width, rect.Height
                    };

                    string[] names = new string[]
                    {
                       "X","Y","Width","Height"
                    };

                    GroupedTextBox<int> group = new GroupedTextBox<int>(
                        paramName, "", array, names, i, table, maxRow);

                    ctrls.Add(group);

                    maxRow = group.MaxRow;
                }
            }
        }

        public void EndCreate()
        {
            if (endLabel != null)
                table.Controls.Remove(endLabel);

            maxRow += 2;

            endLabel = ControlCreator.CreateLabel("", DockStyle.None);
            table.Controls.Add(endLabel, 0, maxRow);
            maxRow++;
        }

        public virtual TableLayoutPanel Destroy()
        {
            for (int i = initialRow; i <= maxRow; i++)
            {
                Control c = table.GetControlFromPosition(0, i);

                if (c != null)
                    table.Controls.Remove(c);

                c = table.GetControlFromPosition(1, i);

                if (c != null)
                    table.Controls.Remove(c);
            }

            if (endLabel != null)
                table.Controls.Remove(endLabel);

            return table;
        }
    }
}
