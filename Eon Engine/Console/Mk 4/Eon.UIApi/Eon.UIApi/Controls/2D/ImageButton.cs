/* Created 04/04/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Engine.Input;
using Eon.Rendering2D;
using Eon.Rendering2D.Text;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.UIApi.Controls._2D
{
    /// <summary>
    /// Defines a button with an image and text.
    /// </summary>
    public sealed class ImageButton : MenuItem, I2DMenuItem
    {
        TextItem txt;
        Sprite spr;

        string imageFilepath;
        string fontFilepath = "Eon/Fonts/Arial23";

        string hoveredSound = "";

        Color hoveredOver = Color.Blue;
        Color normal = Color.Black;
        Color disabled = Color.Red;

        string name;
        string language;

        float marginX = 20;
        Vector2 textPos;

        bool postRender = false;
        int drawLayer = 0;

        Rectangle bounds;

        /// <summary>
        /// Changes the texture of the ImageButton.
        /// </summary>
        public string ImageFilepath
        {
            get { return imageFilepath; }
            set
            {
                if (imageFilepath != value)
                {
                    imageFilepath = value;

                    spr.Texture = Common.ContentBuilder.Load<Texture2D>(value);
                }
            }
        }

        /// <summary>
        /// The sound to play when hovered over.
        /// </summary>
        public string HoveredOverSound
        {
            get { return hoveredSound; }
            set { hoveredSound = value; }
        }

        /// <summary>
        /// X offset for text.
        /// </summary>
        public float MarginX
        {
            get { return marginX; }
            set { marginX = value; }
        }

        /// <summary>
        /// The colour of the text when hovered over.
        /// </summary>
        public Color HoveredOverColour
        {
            get { return hoveredOver; }
            set { hoveredOver = value; }
        }

        /// <summary>
        /// The colour of the text when not hovered over.
        /// </summary>
        public Color NormalColour
        {
            get { return normal; }
            set { normal = value; }
        }

        /// <summary>
        /// The colour of the text when disabled.
        /// </summary>
        public Color DisabledColour
        {
            get { return disabled; }
            set { disabled = value; }
        }

        /// <summary>
        /// The font filepath.
        /// </summary>
        public string FontFilepath
        {
            get { return fontFilepath; }
            set
            {
                fontFilepath = value;

                if (Initialized)
                    txt.Font = Common.ContentBuilder.Load<SpriteFont>(value, "Media");
            }
        }

        /// <summary>
        /// The bounding area of the ImageButton.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// Creates a new ImageButton.
        /// </summary>
        /// <param name="id">The ID of the ImageButton.</param>
        /// <param name="menu">The MenuScreen that this is attached to.</param>
        /// <param name="text">The text to be displayed on the ImageButton.</param>
        /// <param name="imageFilepath">Image filepath.</param>
        /// <param name="size">The size of the ImageButton.</param>
        /// <param name="position">The position of the ImageButton.</param>
        /// <param name="language">The language that the ImageButton is to display.</param>
        public ImageButton(string id, MenuScreen menu, string text,
            string imageFilepath, Vector2 size, Vector2 position, string language,
            bool postRender, int drawLayer)
            : base(id, menu)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y,
                (int)size.X, (int)size.Y);

            name = text;
            this.imageFilepath = imageFilepath;

            this.language = language;

            this.postRender = postRender;
            this.drawLayer = drawLayer;
        }

        protected override void Initialize()
        {
            spr = new Sprite(ID + "spr", drawLayer, imageFilepath, Color.White, postRender, bounds);

            AttachComponent(spr);

            textPos = new Vector2((bounds.X + bounds.Width) + marginX,
                bounds.Y + bounds.Height / 2);

            txt = new TextItem(ID + "txt", drawLayer,
                name, fontFilepath, textPos, normal, postRender);

            txt.Translate();

            txt.Center(ScreenPositions.CenterY);

            AttachComponent(txt);

            bounds.Width += (int)txt.GetTextSize().X;

            base.Initialize();
        }

        protected override void Update()
        {
            if (InputHandler.GetInput("EnterStroked"))
                CheckClick();
            else if (IsInside(InputManager.MousePos) &&
                InputHandler.GetInput("EnterTapped"))
                CheckClick();

            base.Update();
        }

        protected override bool IsInside(Vector2 position)
        {
            return bounds.Contains((int)position.X, (int)position.Y);
        }

        protected override void OnHoveredOver(bool hovered)
        {
            if (Enabled)
            {
                if (hovered)
                    txt.Colour = hoveredOver;
                else
                    txt.Colour = normal;
            }
            else
                txt.Colour = disabled;

            if (hoveredSound != "")
                if (!PrevHoveredOver && hovered)
                    AudioManager.Play(hoveredSound);

            base.OnHoveredOver(hovered);
        }

        /// <summary>
        /// Moves the ImageButton.
        /// </summary>
        /// <param name="movement">The amount to move the MenuItem by.</param>
        public void Move(Vector2 movement)
        {
            bounds.X += (int)movement.X;
            bounds.Y += (int)movement.Y;

            World.Position += new Vector3(movement, 0);
        }

        /// <summary>
        /// Sets the position of the ImageBox.
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
