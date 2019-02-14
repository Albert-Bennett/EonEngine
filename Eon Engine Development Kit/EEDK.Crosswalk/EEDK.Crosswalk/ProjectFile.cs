/* Created: 02/01/2015
 * Last Updated: 12/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;

namespace EEDK.Crosswalk
{
    /// <summary>
    /// Used to define a EEDK project.
    /// </summary>
    public sealed class ProjectFile
    {
        public string ProjectName;
        public string ProjectRootFilepath;

        public FrameworkCreation Framework;
        public ObjectListing CreatableObjects;
    }
}
