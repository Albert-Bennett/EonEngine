/* Created: 05/01/2015
 * Last Updated: 25/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Helpers;
using Microsoft.Xna.Framework;

namespace EEDK.ModelViewer
{
    public class Main : Framework
    {
        Crosswalk.Message message = null;

        public Main()
        {
            EngineFileName = "ModelViewer";
        }

        protected override void Initialize()
        {
            InputManager.CreateKeyboard();

            try
            {
                message = SerializationHelper.Deserialize<Crosswalk.Message>("Temp.temp", false, "");
            }
            catch { }

            base.Initialize();

            new DefaultScene(message);
        }
    }
}
