/* Created: 24/08/2014
 * Last Updated: 23/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Input;
using Eon.System.States;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Cursors
{
    /// <summary>
    /// Used to define an on screen cursor.
    /// </summary>
    public abstract class BaseCursor : GameObject
    {
        float size = 24;

        /// <summary>
        /// The size of the BaseCursor.
        /// </summary>
        public float Size
        {
            get { return size; }
        }

        /// <summary>
        /// The position of the BaseCUrsor.
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(World.Position.X, World.Position.Y); }
            set
            {
                World.Position = new Vector3(value, 0);

                if (InputManager.IsMouseConnected)
                    InputManager.SetMousePosition(value);
            }
        }

        /// <summary>
        /// Creates a new BaseCursor.
        /// </summary>
        /// <param name="id">The id of the BaseCursor.</param>
        /// <param name="size">The size of the BaseCursor.</param>
        public BaseCursor(string id, float size)
            : base(id)
        {
            this.size = size;

            Presidence = GameStates.None;

            MenuManager.Cursor = this;
        }

        protected override void Initialize()
        {
            UpdateWorld();

            World.Size = new Vector3(Size);

            base.Initialize();
        }

        protected override void Update()
        {
            UpdateWorld();

            base.Update();
        }

        protected abstract void UpdateWorld();

        /// <summary>
        /// Sets the position of the cursor.
        /// </summary>
        /// <param name="position">The new position of the cursor.</param>
        public void SetPosition(Vector2 position)
        {
            if (InputManager.IsMouseConnected)
                InputManager.SetMousePosition(position);

            World.Position = new Vector3(position, 0);
        }

        /// <summary>
        /// Centers the cursor on the screen.
        /// </summary>
        public void Center()
        {
            Vector2 center = new Vector2(Common.DefaultScreenResolution.X / 2,
                Common.DefaultScreenResolution.Y / 2);

            SetPosition(center);
        }

        protected override void TextureQualityChanged()
        {
            World.Position = Common.GetReScaled(World.Position);

            base.TextureQualityChanged();
        }
    }
}
