/* Created: 25/01/2015
 * Last Updated: 28/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using EEDK.Gui.Controls;
using Eon;
using Eon.Rendering2D.Text;
using Eon.System.States;
using Eon.UIApi.Cursors;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EEDK.Gui.ModelViewer
{
    /// <summary>
    /// Used to define UI aspects of the ModelViewer
    /// </summary>
    public sealed class MdlViewer : MenuScreen
    {
        List<GroupedPropertyBox> boxes = 
            new List<GroupedPropertyBox>();

        OpenFileDialog openDia;
        SaveFileDialog saveDia;

        TextItem txt;

        string[] names = new string[]
        {
            "Exit","Save","Load"
        };

        string[] propNames = new string[]
        {
            "Position X",
            "Position Y",
            "Position Z",
            "Rotation X",
            "Rotation Y",
            "Rotation Z",
            "Scale"
        };

        public MdlViewer() : base("MdlViewer", GameStates.Game) { }

        protected override void Initialize()
        {
            new Eon.UIApi.Cursors.Cursor("Cursor",
                "Cursors/CenterCursor", 24);

            Vector2 initialPos = new Vector2(Common.TextureQuality.X, 10);

            foreach (string s in names)
            {
                ControlButton btn = new ControlButton(s, this,
                    initialPos, new Vector2(6, 3), true);

                btn.TextColour = Color.Red;
                btn.OnClicked += new OnClickedEvent(OnClicked);

                initialPos.X -= btn.Bounds.Width;
            }

            txt = new TextItem(ID + "txt", 0, "Model Viewer",
                "Eon\\Fonts\\Arial23", new Vector2(13, 17), Color.Red, true);

            AttachComponent(txt);

            saveDia = new SaveFileDialog();
            saveDia.InitialDirectory = @"C:\";
            saveDia.Filter = InterOperations.ModelFileFilters;
            saveDia.Title = "Save file";

            openDia = new OpenFileDialog();
            openDia.InitialDirectory = @"C:\";
            openDia.Filter = InterOperations.ModelFileFilters;
            openDia.Title = "Browze files";

            base.Initialize();
        }

        public void SetUp()
        {
            Vector2 initialPos = new Vector2(
                Common.TextureQuality.X - 150, 120);

            foreach (string s in propNames)
            {
                float val = 0.0f;

                switch (s)
                {
                    case "Position X":
                        val = MdlViewerCommon.Common.ModelTransform.Position.X;
                        break;

                    case "Position Y":
                        val = MdlViewerCommon.Common.ModelTransform.Position.Y;
                        break;

                    case "Position Z":
                        val = MdlViewerCommon.Common.ModelTransform.Position.Z;
                        break;

                    case "Rotation X":
                        val = MdlViewerCommon.Common.ModelTransform.Rotation.X;
                        break;

                    case "Rotation Y":
                        val = MdlViewerCommon.Common.ModelTransform.Rotation.Y;
                        break;

                    case "Rotation Z":
                        val = MdlViewerCommon.Common.ModelTransform.Rotation.Z;
                        break;

                    case "Scale":
                        val = MdlViewerCommon.Common.ModelTransform.Size.X;
                        break;
                }

                bool restrict = s.Contains("Scale");

                GroupedPropertyBox box =
                    new GroupedPropertyBox(s, this, val, initialPos, restrict);

                box.OnValueChanged += new ValueChangedEvent(PropValueChanged);

                initialPos.Y += box.Height + 5;
            }
        }

        void PropValueChanged(string name, float value)
        {
            switch (name)
            {
                case "Position X":
                    {
                        Vector3 vec = MdlViewerCommon.Common.ModelTransform.Position;
                        vec.X = value;

                        MdlViewerCommon.Common.ModelTransform.Position = vec;
                    }
                    break;

                case "Position Y":
                    {
                        Vector3 vec = MdlViewerCommon.Common.ModelTransform.Position;
                        vec.Y = value;

                        MdlViewerCommon.Common.ModelTransform.Position = vec;
                    }
                    break;

                case "Position Z":
                    {
                        Vector3 vec = MdlViewerCommon.Common.ModelTransform.Position;
                        vec.Z = value;

                        MdlViewerCommon.Common.ModelTransform.Position = vec;
                    }
                    break;

                case "Rotation X":
                    {
                        Quaternion vec = MdlViewerCommon.Common.ModelTransform.Rotation;
                        vec.X = value;

                        MdlViewerCommon.Common.ModelTransform.Rotation = vec;
                    }
                    break;

                case "Rotation Y":
                    {
                        Quaternion vec = MdlViewerCommon.Common.ModelTransform.Rotation;
                        vec.Y = value;

                        MdlViewerCommon.Common.ModelTransform.Rotation = vec;
                    }
                    break;

                case "Rotation Z":
                    {
                        Quaternion vec = MdlViewerCommon.Common.ModelTransform.Rotation;
                        vec.Z = value;

                        MdlViewerCommon.Common.ModelTransform.Rotation = vec;
                    }
                    break;

                case "Scale":
                    MdlViewerCommon.Common.ModelTransform.Size = new Vector3(value);
                    break;
            }

            MdlViewerCommon.Common.Updated = true;
        }

        void OnClicked(string controlID)
        {
            switch (controlID)
            {
                case "Save":
                    {
                        Common.ShowMouse(true);

                        if (saveDia.ShowDialog() == DialogResult.OK)
                            MdlViewerCommon.Common.Save(saveDia.FileName);

                        Common.ShowMouse(false);
                    }
                    break;

                case "Load":
                    {
                        Common.ShowMouse(true);

                        if (openDia.ShowDialog() == DialogResult.OK)
                            MdlViewerCommon.Common.Load(openDia.FileName);

                        Common.ShowMouse(false);
                    }
                    break;

                default:
                    Common.ExitGame();
                    break;
            }
        }
    }
}
