/* Created: 01/01/2015
 * Last Updated: 14/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Input;
using Eon.Rendering2D.Text;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace EEDK.Gui.Controls
{
    /// <summary>
    /// Used to define a highlighatable button that only displays text.
    /// </summary>
    public sealed class TextButton : Eon.UIApi.Controls._2D.Button2D
    {
        string text;
        string fontFilepath;

        TextItem textItem;

        Vector2 pos;
        Color selected = Color.White;
        Color notSelected = Color.DarkGray;

        /// <summary>
        /// The text that is displayed in the control.
        /// </summary>
        public string Text
        {
            get { return text; }
        }

        public TextButton(string id, string text,
            string fontFilepath, MenuScreen owner, Vector2 position)
            : base(id, owner, new Rectangle(0,0,0,0), "Eon/Textures/Blank")
        {
            this.text = text;
            this.fontFilepath = fontFilepath;
            pos = position;
        }

        protected override void Initialize()
        {
            textItem = new TextItem(ID + text, 1, text, fontFilepath,
                pos, notSelected, true);

            AttachComponent(textItem);

            Vector2 measure = textItem.GetTextSize();

            Bounds = new Rectangle((int)pos.X, 
                (int)pos.Y, (int)measure.X, (int)measure.Y);

            base.Initialize();
        }

        protected override void Update()
        {
            if (IsInside(InputManager.MousePos) &&
                InputManager.IsMouseButtonStroked(MouseButtons.Left))
                CheckClick();

            base.Update();
        }

        protected override void OnHoveredOver(bool hovered)
        {
            if (hovered)
                textItem.Colour = selected;
            else
                textItem.Colour = notSelected;

            base.OnHoveredOver(hovered);
        }
    }
}
