/* Created 03/09/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Engine.Input;
using Microsoft.Xna.Framework;

namespace EEDK.SkeletalAnimator
{
    public class Main : Framework
    {
        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Esc))
                Exit();

            base.Update(gameTime);
        }
    }
}
