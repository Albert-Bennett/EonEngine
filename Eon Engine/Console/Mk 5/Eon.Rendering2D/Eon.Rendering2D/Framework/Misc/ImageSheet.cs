/* Created: 18/06/2015
 * Last Updated: 18/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.System.States;
using Eon.Testing;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering2D.Framework.Misc
{
    /// <summary>
    /// Defines a set of images that are placed in a set order.
    /// </summary>
    public sealed class ImageSheet : GameObject
    {
        ImageSet info;

        /// <summary>
        /// Creates a new ImageSheet.
        /// </summary>
        /// <param name="ID">The ID of the ImageSheet.</param>
        /// <param name="imageSetFilepath">The filepath of the ImageSet to be used.</param>
        public ImageSheet(string ID, string imageSetFilepath, string presidence)
            : base(ID)
        {
            this.Presidence = (GameStates)Enum.Parse(typeof(GameStates), presidence);

            try
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(Vector2),
                    typeof(string[]),
                    typeof(float),
                    typeof(int),
                    typeof(bool)
                };

                info = SerializationHelper.Deserialize<ImageSet>(
                    imageSetFilepath, true, ".Set", extraTypes);
            }
            catch
            {
                new Error(imageSetFilepath + " is invalid", Seriousness.Error);

                Destroy();
            }
        }

        protected override void Initialize()
        {
            Vector2 startingPos = info.StartingPos;
            Vector2 spacing = info.Spacing;
            Vector2 scale = new Vector2(info.Width, info.Height);

            float startingX = startingPos.X;

            int columns = info.Filepaths.Length / info.Rows;
            int index = 0;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < info.Rows; j++)
                {
                    Sprite spr = new Sprite(ID + index, info.DrawLayer,
                        info.Filepaths[index], Color.White, info.PostRender,
                        startingPos, scale);

                    AttachComponent(spr);

                    startingPos.X += spacing.X + info.Width;

                    index++;
                }

                startingPos.Y += spacing.Y + info.Height;
                startingPos.X = startingX;
            }

            base.Initialize();
        }
    }
}
