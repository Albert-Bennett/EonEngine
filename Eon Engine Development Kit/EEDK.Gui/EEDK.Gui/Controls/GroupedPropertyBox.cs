/* Created: 26/01/2015
 * Last Updated: 26/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Rendering2D;
using Eon.Rendering2D.Text;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace EEDK.Gui.Controls
{
    /// <summary>
    /// Used to define a group of controls 
    /// that define and edit a value.
    /// </summary>
    public sealed class GroupedPropertyBox : GameObject
    {
        const float Rate = 0.0001f;

        TextItem txtLabel;
        TextItem txtValue;

        ImageButton btnPlus;
        ImageButton btnMinus;

        MenuScreen owner;

        Sprite bg;

        Vector2 pos;
        Vector2 valPos;

        float value;
        bool restrict;

        internal ValueChangedEvent OnValueChanged;

        /// <summary>
        /// The value of the property.
        /// </summary>
        public float Value
        {
            get { return value; }
        }

        /// <summary>
        /// The width of the GroupedPropertyBox.
        /// </summary>
        public int Width
        {
            get { return 250; }
        }

        /// <summary>
        /// The height of the GroupedPropertyBox.
        /// </summary>
        public int Height
        {
            get { return 60; }
        }

        public GroupedPropertyBox(string id, MenuScreen owner,
            float value, Vector2 position, bool restrict)
            : base(id)
        {
            this.owner = owner;
            this.restrict = restrict;

            txtLabel = new TextItem(id + "lbl", 1, id, "GUI\\Verdana12pt",
                position + new Vector2(0, Height / 4), Color.White, true);

            txtLabel.Center(ScreenPositions.CenterX);

            valPos = position + new Vector2(0, 16 + Height / 4);

            txtValue = new TextItem(id + "val", 1, "" + value,
                "GUI\\Verdana12pt", valPos, Color.White, true);

            txtValue.Center(ScreenPositions.CenterX);

            pos = position;
            this.value = value;
        }

        protected override void Initialize()
        {
            AttachComponent(txtLabel);
            AttachComponent(txtValue);

            Rectangle bounds = new Rectangle(
                (int)pos.X - Width/2, (int)pos.Y, Width, Height);

            bg = new Sprite(ID + "bg", 0, "Eon\\Textures\\Pixel", new Color(18, 18, 18, 255), true, bounds);
            AttachComponent(bg);

            Rectangle rect = new Rectangle(bounds.X, (bounds.Y + 
                (Height / 2)) - 16, 32, 32);

            btnMinus = new ImageButton(ID + "minus", owner, rect, "GUI\\MinusIcon");
            btnMinus.OnClicked += new OnClickedEvent(MinusClicked);

            rect.X = bounds.X + bounds.Width - 32;

            btnPlus = new ImageButton(ID + "plus", owner, rect, "GUI\\PlusIcon");
            btnPlus.OnClicked += new OnClickedEvent(PlusClicked);

            base.Initialize();
        }

        void PlusClicked(string controlID)
        {
            value += Rate;

            ChangeValue();
        }

        void MinusClicked(string controlID)
        {
            value -= Rate;

            ChangeValue();
        }

        void ChangeValue()
        {
            if (restrict && value < 0)
                value = 0;

            txtValue.ChangeText("" + value);

            txtValue.Position = valPos;
            txtValue.Center(ScreenPositions.CenterX);

            if (OnValueChanged != null)
                OnValueChanged(ID, value);
        }
    }
}
