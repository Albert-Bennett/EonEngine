/* Created 17/08/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.UIApi.Controls._2D
{
    /// <summary>
    /// Defines a CheckBox control. 
    /// </summary>
    public class CheckBox : MenuItem, I2DMenuItem
    {
        Sprite bg;

        string checkedImgFilepath = "GUI/Checked";
        Texture2D checkedImg;

        string unCheckedImgFilepath = "GUI/Unchecked";
        Texture2D unCheckedImg;

        Rectangle bounds;
        int drawLayer;
        bool postRender;

        bool check = false;

        public OnCheckToogledEvent OnChecked;

        /// <summary>
        /// Is the CheckBox checked.
        /// </summary>
        public bool Checked
        {
            get { return check; }
            set
            {
                check = value;

                if (bg != null)
                    CheckChecked(true);
            }
        }

        /// <summary>
        /// The position of the CheckBox.
        /// </summary>
        public Vector2 Position
        {
            get { return bounds.Location.ToVector2(); }
        }

        /// <summary>
        /// The filepath of the checked image.
        /// </summary>
        public string CheckedImage
        {
            get { return checkedImgFilepath; }
            set
            {
                checkedImgFilepath = value;
                checkedImg = Common.ContentBuilder.Load<Texture2D>(value);

                if (bg != null && check)
                    bg.Texture = checkedImg;
            }
        }

        /// <summary>
        /// The filepath of the un-checked image.
        /// </summary>
        public string UnCheckedImage
        {
            get { return unCheckedImgFilepath; }
            set
            {
                unCheckedImgFilepath = value;
                unCheckedImg = Common.ContentBuilder.Load<Texture2D>(value);

                if (bg != null && !check)
                    bg.Texture = unCheckedImg;
            }
        }

        /// <summary>
        /// The bounding area of the CheckBox.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// Creates a new CheckBox.
        /// </summary>
        /// <param name="ID">The ID of the CheckBox.</param>
        /// <param name="menu">The Screen that this is to be attached to.</param>
        /// <param name="bounds">The bounds of the CheckBox.</param>
        /// <param name="drawLayer">The layer that the CheckBox is to drawn on.</param>
        /// <param name="postRender">Should the CheckBox be post rendered.</param>
        public CheckBox(string ID, Screen menu, Rectangle bounds,
            int drawLayer, bool postRender)
            : base(ID, menu)
        {
            this.drawLayer = drawLayer;
            this.bounds = bounds;
            this.postRender = postRender;
        }

        protected override void Initialize()
        {
            if (unCheckedImg == null)
                unCheckedImg = Common.ContentBuilder.Load<Texture2D>(unCheckedImgFilepath);

            if (checkedImg == null)
                checkedImg = Common.ContentBuilder.Load<Texture2D>(checkedImgFilepath);

            string filepath = unCheckedImgFilepath;

            if (check)
                filepath = checkedImgFilepath;

            bg = new Sprite(ID + "BG", drawLayer, filepath, Color.White, postRender, bounds);
            AttachComponent(bg);

            base.Initialize();
        }

        protected override bool IsInside(Microsoft.Xna.Framework.Vector2 position)
        {
            return EonMathsHelper.IsInsideOf(bounds, position);
        }

        public override void CheckClick()
        {
            if (Enabled && IsSelectable)
            {
                Toggle(true);

                if (OnClicked != null)
                    OnClicked(ID);
            }

            base.CheckClick();
        }

        /// <summary>
        /// Toggles between checked and un-checked.
        /// </summary>
        public void Toggle(bool throwEvent)
        {
            check = !check;

            CheckChecked(throwEvent);
        }

        void CheckChecked(bool throwEvent)
        {
            if (check)
                bg.Texture = checkedImg;
            else
                bg.Texture = unCheckedImg;

            if (OnChecked != null && throwEvent)
                OnChecked(ID, check);
        }

        /// <summary>
        /// Moves the CheckBox.
        /// </summary>
        /// <param name="movement">The amount to move the MenuItem by.</param>
        public void Move(Vector2 movement)
        {
            bounds.X += (int)movement.X;
            bounds.Y += (int)movement.Y;

            World.Position += new Vector3(movement, 0);
        }

        /// <summary>
        /// Sets the position of the CheckBox.
        /// </summary>
        /// <param name="movement">The new position of the MenuItem.</param>
        public void SetPosition(Vector2 position)
        {
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;

            World.Position = new Vector3(position, 0);
        }
    }
}
