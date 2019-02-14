/* Created: 25/09/2015
 * Last Updated: 08/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.UIApi.Controls.Misc
{
    /// <summary>
    /// Used to define a ProgressBar.
    /// </summary>
    public sealed class ProgressBar : IDrawItem, ITextureQualityChanged
    {
        Texture2D background;
        Texture2D bar;

        Color barColour = Color.White;

        Rectangle barRect;
        Rectangle bounds;

        bool takeReverse = false;

        int drawLayer;
        bool enabled = true;
        bool postRender = true;
        bool renderDisabled = false;

        float maxValue;
        float currentValue;
        float step;
        float initialWidth;

        public int DrawLayer
        {
            get { return drawLayer; }
        }

        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
        }

        /// <summary>
        /// Subtract form right or from left side.
        /// </summary>
        public bool TakeReverse
        {
            get { return takeReverse; }
        }

        /// <summary>
        /// The maximum value of the ProgressBar.
        /// </summary>
        public float MaxValue
        {
            get { return maxValue; }
        }

        /// <summary>
        /// The current value of the ProgressBar.
        /// </summary>
        public float CurrentValue
        {
            get { return currentValue; }
        }

        /// <summary>
        /// The colour of the bar.
        /// </summary>
        public Color BarColour
        {
            get { return barColour; }
            set { barColour = value; }
        }

        /// <summary>
        /// Creates a new ProgressBar.
        /// </summary>
        /// <param name="background">The image for the background.</param>
        /// <param name="bar">The image for the bar.</param>
        /// <param name="bounds">The bounds of the ProgressBar.</param>
        /// <param name="maxValue">The maximum value of the ProgressBar.</param> 
        /// <param name="takeReverse">Subtract form right or from left side.</param>
        /// <param name="drawLayer">Draw layer.</param>
        /// <param name="postRender">Post render.</param>
        public ProgressBar(Texture2D background, Texture2D bar,
            Rectangle bounds, float maxValue, bool takeReverse,
            int drawLayer, bool postRender)
        {
            this.background = background;
            this.bar = bar;

            initialWidth = (float)bounds.Width;

            step = initialWidth / maxValue;

            this.bounds = bounds;
            barRect = bounds;

            this.maxValue = maxValue;
            currentValue = maxValue;

            this.takeReverse = takeReverse;

            this.drawLayer = drawLayer;
            this.postRender = postRender;

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        /// <summary>
        /// Creates a new ProgressBar.
        /// </summary>
        /// <param name="background">The image for the background.</param>
        /// <param name="bar">The image for the bar.</param>
        /// <param name="bounds">The bounds of the ProgressBar.</param>
        /// <param name="maxValue">The maximum value of the ProgressBar.</param> 
        /// <param name="takeReverse">Subtract form right or from left side.</param>
        /// <param name="drawLayer">Draw layer.</param>
        /// <param name="postRender">Post render.</param>
        public ProgressBar(string background, string bar,
            Rectangle bounds, float maxValue, bool takeReverse,
            int drawLayer, bool postRender)
        {
            if (background != "")
                this.background = Common.ContentBuilder.Load<Texture2D>(background);

            this.bar = Common.ContentBuilder.Load<Texture2D>(bar);

            initialWidth = (float)bounds.Width;

            step = initialWidth / maxValue;

            barRect = bounds;
            this.bounds = bounds;

            this.maxValue = maxValue;
            currentValue = maxValue;

            this.takeReverse = takeReverse;

            this.drawLayer = drawLayer;
            this.postRender = postRender;

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);
        }

        public void Draw(DrawingStage stage)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    {
                        if (background != null)
                            Common.Batch.Draw(background, bounds, Color.White);

                        Common.Batch.Draw(bar, barRect, barColour);
                    }
                    break;
            }
        }

        /// <summary>
        /// Changes the value of the ProgressBar.
        /// </summary>
        /// <param name="newValue">The new value of the ProgressBar.</param>
        public void ChangeValue(float newValue)
        {
            newValue = MathHelper.Clamp(newValue, 0, maxValue);

            float width = step * newValue;

            if (takeReverse)
            {
                float diff = (float)bounds.Width - width;

                barRect.X = (int)(bounds.X + diff);
                barRect.Width = (int)width;
            }
            else
                barRect.Width = (int)width;

            currentValue = newValue;
        }

        /// <summary>
        /// Changes the maximum value of the ProgressBar.
        /// </summary>
        /// <param name="newValue">The new value of the ProgressBar.</param>
        public void ChangeMaxValue(float newValue)
        {
            if (newValue < currentValue)
                currentValue = newValue;

            step = initialWidth / newValue;

            float width = step * currentValue;

            if (takeReverse)
            {
                float diff = (float)bounds.Width - width;

                barRect.X = (int)(bounds.X + diff);
                barRect.Width = (int)width;
            }
            else
                barRect.Width = (int)width;

            maxValue = newValue;
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }

        public void ToogleEnable()
        {
            enabled = !enabled;
        }

        public void Destroy()
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);
        }

        public void TextureQualityChanged()
        {
            bounds.X = (int)(bounds.X * Common.UpScale);
            bounds.Y = (int)(bounds.Y * Common.UpScale);
            bounds.Width = (int)(bounds.Width * Common.UpScale);
            bounds.Height = (int)(bounds.Height * Common.UpScale);

            barRect.X = (int)(barRect.X * Common.UpScale);
            barRect.Y = (int)(barRect.Y * Common.UpScale);
            barRect.Width = (int)(barRect.Width * Common.UpScale);
            barRect.Height = (int)(barRect.Height * Common.UpScale);
        }
    }
}
