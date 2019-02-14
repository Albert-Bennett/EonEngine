/* Created 04/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework.Content;

namespace Eon.Engine
{
    /// <summary>
    /// Defines the informaton that Framework needs inorder to be created.
    /// </summary>
    public class FrameworkCreation
    {
        [ContentSerializer(FlattenContent = true, CollectionItemName = "Component")]
        public string[] EngineComponents { get; set; }

        [ContentSerializer(FlattenContent = true, CollectionItemName = "Assembly")]
        public string[] AssemblyRefferences { get; set; }
    }
}
