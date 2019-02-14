/* Created: 14/10/2015
 * Last Updated: 14/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Eon.Rendering2D.Text;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.UIApi.Controls._2D
{
    /// <summary>
    /// Defines a control that is used to select 
    /// items in a list using buttons. 
    /// </summary>
    public sealed class ListSelector : MenuItem
    {
        List<string> items;
        int selectedIndex;

        string backgroundFilepath;
        string buttonFilepath;
        string fontFilepath;

        string hoveredOverSnd;

        Rectangle leftButton;
        Rectangle rightButton;
        Rectangle bgBounds;

        Color hovered = Color.Black;
        Color main = Color.White;
        Color unSelectable = Color.Gray;
        Color fontColour = Color.Black;

        Sprite leftButtonSpr;
        Sprite rightButtonSpr;
        Sprite background;

        TextItem text;
        Vector2 center;

        int drawLayer;
        bool postRender;

        int selected = -1;
        int prevSelected = -1;

        /// <summary>
        /// The items contained in the ListSelector.
        /// </summary>
        public List<string> Items
        {
            get { return items; }
            set
            {
                if (value != null)
                {
                    items = value;

                    if (selectedIndex >= items.Count)
                        selectedIndex = items.Count - 1;
                }
            }
        }

        /// <summary>
        /// The selected index of the control.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value > -1)
                {
                    prevSelected = selected;

                    if (value < items.Count)
                        selectedIndex = value;
                    else
                        selectedIndex = items.Count - 1;

                    if (Initialized)
                    {
                        text.ChangeText(items[selectedIndex]);
                        text.Position = center;
                        text.Center(ScreenPositions.LocalCenter);
                    }
                }
            }
        }

        /// <summary>
        /// The currently selected item.
        /// </summary>
        public string SelectedItem
        {
            get { return items[selectedIndex]; }
        }

        /// <summary>
        /// The sound to make when hovered over.
        /// </summary>
        public string HoveredOverSound
        {
            get { return hoveredOverSnd; }
            set { hoveredOverSnd = value; }
        }

        /// <summary>
        /// The colour of the buttons when hovered over.
        /// </summary>
        public Color HoveredOverColour
        {
            get { return hovered; }
            set
            {
                switch (selected)
                {
                    case 0:
                        {
                            if (leftButtonSpr != null)
                                leftButtonSpr.Colour = value;
                        }
                        break;

                    case 1:
                        {
                            if (rightButtonSpr != null)
                                rightButtonSpr.Colour = value;
                        }
                        break;
                }

                hovered = value;
            }
        }

        /// <summary>
        /// The default colour of the control.
        /// </summary>
        public Color DefaultColour
        {
            get { return main; }
            set
            {
                main = value;

                if (Initialized)
                {
                    switch (selected)
                    {
                        case 0:
                            {
                                rightButtonSpr.Colour = value;
                                background.Colour = value;
                            }
                            break;

                        case 1:
                            {
                                leftButtonSpr.Colour = value;
                                background.Colour = value;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// The colour of the conrtrol when un-selectable.
        /// </summary>
        public Color UnSelectableColour
        {
            get { return unSelectable; }
            set
            {
                unSelectable = value;

                if (Initialized)
                {
                    rightButtonSpr.Colour = value;
                    background.Colour = value;
                    leftButtonSpr.Colour = value;
                }
            }
        }

        /// <summary>
        /// The colour of the font.
        /// </summary>
        public Color FontColour
        {
            get { return fontColour; }
            set
            {
                fontColour = value;

                if (Initialized)
                    text.Colour = value;
            }
        }

        /// <summary>
        /// Creates a new ListSelector control.
        /// </summary>
        /// <param name="id">The ID of the ListSelector control.</param>
        /// <param name="owner">The owner of the ListSelector control.</param>
        /// <param name="items">The items contained in the ListSelector.</param>
        /// <param name="backgroundFilepath">The image filepath foe the background.</param>
        /// <param name="fontFilepath">The font filepath.</param>
        /// <param name="buttonFilepath">The image filepath for the buttons.</param>
        /// <param name="position">The initial position of the control.</param>
        /// <param name="buttonSize">The size of teh buttons.</param>
        /// <param name="width">The total width of the control.</param>
        /// <param name="drawLayer">The drawlayer of the conrtrol.</param>
        /// <param name="postRender">Post render the control.</param>
        public ListSelector(string id, Screen owner, List<string> items,
            string backgroundFilepath, string fontFilepath, string buttonFilepath,
            Vector2 position, Vector2 buttonSize, float width, int drawLayer, bool postRender)
            : base(id, owner)
        {
            this.items = items;

            this.backgroundFilepath = backgroundFilepath;
            this.buttonFilepath = buttonFilepath;
            this.fontFilepath = fontFilepath;

            this.drawLayer = drawLayer;
            this.postRender = postRender;

            leftButton = new Rectangle((int)position.X,
                (int)position.Y, (int)buttonSize.X, (int)buttonSize.Y);

            bgBounds = new Rectangle((int)(position.X + buttonSize.X),
                (int)position.Y, (int)(width - (buttonSize.X * 2)), (int)buttonSize.Y);

            rightButton = new Rectangle((int)(position.X + width - buttonSize.X),
                (int)position.Y, (int)buttonSize.X, (int)buttonSize.Y);
        }

        protected override void Initialize()
        {
            leftButtonSpr = new Sprite(ID + "LeftSpr", drawLayer, buttonFilepath, main, postRender, leftButton);
            leftButtonSpr.SpriteEffect = SpriteEffects.FlipHorizontally;
            AttachComponent(leftButtonSpr);

            rightButtonSpr = new Sprite(ID + "RightSpr", drawLayer, buttonFilepath, main, postRender, rightButton);
            AttachComponent(rightButtonSpr);

            background = new Sprite(ID + "BGSpr", drawLayer, backgroundFilepath, main, postRender, bgBounds);
            AttachComponent(background);

            center = new Vector2(bgBounds.Center.X, bgBounds.Center.Y);

            text = new TextItem(ID + "txt", drawLayer, SelectedItem, fontFilepath, center, fontColour, postRender);

            text.Center(ScreenPositions.LocalCenter);
            AttachComponent(text);

            base.Initialize();
        }

        protected override bool IsInside(Vector2 position)
        {
            prevSelected = selected;

            if (EonMathsHelper.IsInsideOf(leftButton, position))
                selected = 0;
            else if (EonMathsHelper.IsInsideOf(rightButton, position))
                selected = 1;
            else
                selected = -1;

            return selected != -1;
        }

        protected override void OnHoveredOver(bool hovered)
        {
            if (Enabled)
            {
                if (hovered)
                {
                    switch (selected)
                    {
                        case 0:
                            {
                                leftButtonSpr.Colour = this.hovered;
                                rightButtonSpr.Colour = main;
                            }
                            break;

                        case 1:
                            {
                                rightButtonSpr.Colour = this.hovered;
                                leftButtonSpr.Colour = main;
                            }
                            break;

                        default:
                            {
                                rightButtonSpr.Colour = main;
                                leftButtonSpr.Colour = main;
                            }
                            break;
                    }
                }
            }
            else
            {
                rightButtonSpr.Colour = unSelectable;
                leftButtonSpr.Colour = unSelectable;
                background.Colour = unSelectable;
            }

            if (hoveredOverSnd != "")
                if (!PrevHoveredOver && hovered && selected != prevSelected)
                    AudioManager.Play(hoveredOverSnd);

            base.OnHoveredOver(hovered);
        }

        public override void Selectable()
        {
            rightButtonSpr.Colour = main;
            leftButtonSpr.Colour = main;
            background.Colour = main;

            base.Selectable();
        }

        public override void UnSelectable()
        {
            rightButtonSpr.Colour = unSelectable;
            leftButtonSpr.Colour = unSelectable;
            background.Colour = unSelectable;

            base.UnSelectable();
        }

        public override void CheckClick()
        {
            if (Enabled && IsSelectable)
            {
                switch (selected)
                {
                    case 0:
                        MoveUp();
                        break;

                    case 1:
                        MoveDown();
                        break;
                }
            }

            base.CheckClick();
        }

        void MoveUp()
        {
            selectedIndex -= 1;

            if (selectedIndex < 0)
                selectedIndex = items.Count - 1;

            SelectedIndexChanged();

            if (OnClicked != null)
                OnClicked(ID);
        }

        void SelectedIndexChanged()
        {
            text.ChangeText(SelectedItem);
            text.Center(ScreenPositions.LocalCenter);
        }

        void MoveDown()
        {
            selectedIndex += 1;

            if (selectedIndex >= items.Count)
                selectedIndex = 0;

            SelectedIndexChanged();

            if (OnClicked != null)
                OnClicked(ID);
        }
    }
}
