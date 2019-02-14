﻿using Eon.Collections;
using Eon.Helpers;
using Eon.Particles;
using EonEngineTool.Lib;
using EonEngineTool.Lib.ContainerDocks;
using EonEngineTool.Lib.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.Particles
{
    /// <summary>
    /// Used to add a series of controls that 
    /// are used to show what properties a ParticleEmitter has.
    /// </summary>
    public class EmitterInfo
    {
        #region Varibles

        TableLayoutPanel panel;

        List<string> classNames = new List<string>();
        List<string[]> options = new List<string[]>();

        Label endSign;
        Button btnAdd;

        ParticleEmitterInfo info;

        List<SelectionDock> docks =
            new List<SelectionDock>();

        int maxRow = 0;
        int startingRow;
        int maxOrder;

        List<TextBox> textBoxes = new List<TextBox>();

        public ParticleEmitterInfo Info
        {
            get
            {
                int count = info.Attachments.Length;
                count += 3;

                for (int i = 0; i < count; i++)
                    if (i == 0)
                        info.EmittionType = docks[i].Object;
                    else if (i == 1)
                        info.RenderType = docks[i].Object;
                    else if (i == 2)
                        info.CycleType = docks[i].Object;
                    else
                    {
                        int idx = i;
                        idx -= 3;

                        info.Attachments[idx] = docks[i].Object;
                    }

                return info;
            }
        }

        public string EmitterID
        {
            get { return FindTextBox("id").Text; }
        }

        #endregion
        #region Ctor

        public EmitterInfo(Control parent, ParticleEmitterInfo info, string emitterName)
        {
            this.info = info;

            panel = new TableLayoutPanel();
            panel.Parent = parent;

            panel.BackColor = Color.Transparent;
            panel.AutoSize = true;
            panel.Dock = DockStyle.Top | DockStyle.Left;

            CreateEndLabel("ID", 0);
            AddTextBox(0, "id", emitterName);

            CreateEndLabel("Min Mass", 1);
            AddTextBox(1, "minMass", "" + info.MinMass);

            CreateEndLabel("Max Mass", 2);
            AddTextBox(2, "maxMass", "" + info.MaxMass);

            CreateEndLabel("Min Life Time", 3);
            AddTextBox(3, "minLife", "" + info.MinLifeTime);

            CreateEndLabel("Max Life Time", 4);
            AddTextBox(4, "maxLife", "" + info.MaxLifeTime);

            maxRow += 2;
            CreateEndLabel("", maxRow);
            CreateEndLabel("", maxRow);

            startingRow = maxRow;

            classNames = new List<string>()
            {
                 "D2.EmittionType",
                 "D2.RenderType",
                 "Cycle",
                 "Attachment"
            };

            options = new List<string[]>()
            {
                 Helper.Emitter2DTypes,
                 Helper.Particle2DRenderTypes,
                 Helper.Cycle2DTypes,
                 Helper.Particle2DAttachments
            };

            for (int i = 0; i < classNames.Count - 1; i++)
            {
                maxRow++;

                ParameterCollection obj = null;

                if (i == 0)
                    obj = info.EmittionType;
                else if (i == 1)
                    obj = info.RenderType;
                else if (i == 2)
                    obj = info.CycleType;

                DisplayObject(obj, classNames[i], options[i], maxOrder);
            }

            if (info.Attachments != null)
                for (int i = 0; i < info.Attachments.Length; i++)
                    DisplayAttachment(info.Attachments[i]);
            else
                info.Attachments = new ParameterCollection[0];

            ReAddEnding();
        }

        #endregion
        #region Dock Creation

        void DisplayObject(ParameterCollection obj, string objectClass,
            string[] options, int order)
        {
            if (order > maxOrder)
                maxOrder = order;

            string folderLoc = Helper.ParticleAssemblyRef + "." + objectClass + "s.";

            if (obj == null)
            {
                obj = new ParameterCollection();

                obj.ObjectType = folderLoc + options[0];
            }

            SelectionDock dock = new SelectionDock(obj, panel, maxRow, objectClass,
                options, objectClass, folderLoc);

            dock.Index = order;
            dock.OnSelectionChanged += new OnComboBoxSelectedChangedEvent(cboSelectionChanged);

            dock.Create();
            dock.EndCreate();

            docks.Add(dock);

            maxRow = dock.MaxRow;
            maxOrder++;
        }

        void DisplayAttachment(ParameterCollection param)
        {
            maxRow++;

            AddableDock dock = new AddableDock(param, panel, maxRow, "Attachment0",
                 Helper.Particle2DAttachments, "Attachment", Helper.ParticleAssemblyRef + ".Attachments.");

            dock.Index = maxOrder;
            dock.OnClick += new OnClickRelayEvent(btnRemoveClick);
            dock.OnSelectionChanged += new OnComboBoxSelectedChangedEvent(cboSelectionChanged);

            dock.Create();

            docks.Add(dock);

            maxRow = dock.MaxRow;
            maxOrder++;
        }

        void btnRemoveClick(SelectionDock sender)
        {
            if (sender.Index == 0)
            {
                maxRow = startingRow;
                maxOrder = 0;
            }
            else
            {
                int prevIdx = sender.Index;
                prevIdx--;

                SelectionDock prev = (from d in docks
                                      where d.Index == prevIdx
                                      select d).First();

                maxRow = prev.MaxRow;
                maxOrder = prev.Index;
            }

            info = Info;

            if (info.Attachments.Length > 0)
            {
                int idx = sender.Index;
                idx -= 3;

                info.Attachments = ArrayHelper.RemoveAt<ParameterCollection>(idx, info.Attachments);
            }
            else
                info.Attachments = new ParameterCollection[0];

            if (docks.Count > 0)
            {
                for (int i = sender.Index; i < docks.Count; i++)
                    docks[i].Destroy();

                int count = 0;
                count = docks.Count - sender.Index;

                docks.RemoveRange(sender.Index, count);
                sender.Destroy();

                int idx = maxOrder++;
                idx -= 2;

                for (int i = idx; i < info.Attachments.Length; i++)
                    DisplayAttachment(info.Attachments[i]);
            }

            ReAddEnding();
        }

        void ReAddEnding()
        {
            panel.Controls.Remove(btnAdd);

            maxRow++;

            int row = maxRow;
            row--;

            btnAdd = CreateButton("Add", row, -1);
        }

        #endregion
        #region Control Creation

        Button CreateButton(string textID, int row, int index)
        {
            Button btn = ControlCreator.CreateButton("Add", DockStyle.Right);
            btn.Click += btn_Click;

            panel.Controls.Add(btn, 0, row);
            maxRow = row;

            return btn;
        }

        void btn_Click(object sender, System.EventArgs e)
        {
            ParameterCollection param = new ParameterCollection();

            param.ObjectType = Helper.ParticleAssemblyRef + ".Attachments." +
                PropertyHelper.GetClassName(Helper.Particle2DAttachments[0]);

            if (info.Attachments == null)
            {
                info.Attachments = new ParameterCollection[]
                {
                    param
                };

                DisplayAttachment(param);
            }
            else
            {
                info.Attachments = ArrayHelper.AddItem<ParameterCollection>(param, info.Attachments);

                DisplayAttachment(param);
            }

            ReAddEnding();
        }

        void AddTextBox(int row, string name, string text)
        {
            TextBox textBox = ControlCreator.CreateTextBox(text, name);
            textBox.TextChanged += textBox_TextChanged;

            panel.Controls.Add(textBox, 1, row);
            textBoxes.Add(textBox);

            maxRow = row;
        }

        Label CreateEndLabel(string text, int row)
        {
            Label label = ControlCreator.CreateLabel(text, DockStyle.Left);

            panel.Controls.Add(label, 0, row);
            maxRow = row;
            endSign = label;

            return label;
        }

        #endregion
        #region Combo Box Events

        void cboSelectionChanged(SelectionDock sender)
        {
            int idx = 0;

            if (sender is AddableDock)
            {
                maxRow = sender.MaxRow;

                idx = sender.Index;
                idx -= 3;

                info.Attachments[idx] = sender.Object;
            }

            if (sender.Index == maxOrder)
                ReAddEnding();
            else
            {
                info = Info;

                for (int i = sender.Index; i < docks.Count; i++)
                    docks[i].Destroy();

                int count = docks.Count;
                count -= sender.Index;

                docks.RemoveRange(sender.Index, count);

                int max = maxOrder;
                maxOrder = sender.Index;

                for (int i = sender.Index; i < max; i++)
                    if (i >= 0 && i < 3)
                    {
                        ParameterCollection obj = null;

                        if (i == 0)
                            obj = info.EmittionType;
                        else if (i == 1)
                            obj = info.RenderType;
                        else if (i == 2)
                            obj = info.CycleType;

                        DisplayObject(obj, classNames[i], options[i], i);
                    }
                    else
                    {
                        idx = i;
                        idx -= 3;

                        DisplayAttachment(info.Attachments[idx]);
                    }

                ReAddEnding();
            }
        }

        #endregion
        #region Textbox Events

        void textBox_TextChanged(object sender, System.EventArgs e)
        {
            TextBox txt = sender as TextBox;

            switch (txt.Name)
            {
                case "minMass":
                    {
                        float temp = 0;

                        if (float.TryParse(FindTextBox("minMass").Text, out temp))
                            info.MinMass = temp;
                    }
                    break;

                case "maxMass":
                    {
                        float temp = 0;

                        if(float.TryParse(FindTextBox("maxMass").Text, out temp))
                            info.MaxMass=temp;
                    }
                    break;

                case "minLife":
                    {
                        float temp = 0;

                        if (float.TryParse(FindTextBox("minLife").Text, out temp))
                            info.MinLifeTime = temp;
                    }
                    break;

                case "maxLife":
                    {
                        float temp = 0;

                        if (float.TryParse(FindTextBox("maxLife").Text, out temp))
                            info.MaxLifeTime = temp;
                    }
                    break;
            }
        }

        TextBox FindTextBox(string name)
        {
            TextBox box = (from b in textBoxes
                           where b.Name == name
                           select b).FirstOrDefault();

            return box;
        }

        #endregion
        #region Destruction

        public TableLayoutPanel Destroy()
        {
            panel.Controls.Clear();
            return panel;
        }

        #endregion
    }
}
