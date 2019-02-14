/* Created: 02/01/2015
 * Last Updated: 05/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace EEDK.Crosswalk
{
    /// <summary>
    /// Used to define filepath etc, that will be used 
    /// throughout the various programs that EEDK uses.
    /// </summary>
    public static class InterOperations
    {
        static string projectFilter = "Project File (*.EPROJ)|*.EPROJ";
        static string projectExt = ".EPROJ";

        static string libraryExt = "\\Libraries";
        static string contentExt = "\\Content";

        static string shaderExt = ".Shader";

        static string modelFileFilters = "Shader File (*.Shader)|*.Shader|" +
            "Particle File (*.Part3D)|*.Part3D";

        static string projName;
        static ProjectFile project;

        /// <summary>
        /// The filter to be used when loading a EEDK project.
        /// </summary>
        public static string ProjectFileFilter
        {
            get { return projectFilter; }
        }

        /// <summary>
        /// The filters used when loading model files e.g .Shader, .Part3D.
        /// </summary>
        public static string ModelFileFilters
        {
            get { return modelFileFilters; }
        }

        /// <summary>
        /// The file extention to be used when saving ProjectFiles.
        /// </summary>
        public static string ProjectFileExtention
        {
            get { return projectExt; }
        }

        /// <summary>
        /// The root filepath extention used to tell where the libraries are stored.
        /// </summary>
        public static string LibraryFilepathExtention
        {
            get { return libraryExt; }
        }

        /// <summary>
        /// The root filepath extention used to denote the content folder.
        /// </summary>
        public static string ContentFilepathExtention
        {
            get { return contentExt; }
        }

        /// <summary>
        /// Root filepath for loading objects from a particular project. 
        /// </summary>
        public static string RootFilepath;

        /// <summary>
        /// The filepath for the project.
        /// </summary>
        public static string ProjectFilepath;

        /// <summary>
        /// An extention that define a file as a model.
        /// </summary>
        public static string ShaderExtention
        {
            get { return shaderExt; }
        }

        /// <summary>
        /// The current project.
        /// </summary>
        public static ProjectFile Project
        {
            get { return project; }
            set { project = value; }
        }

        /// <summary>
        /// The name of the current project.
        /// </summary>
        public static string ProjectName
        {
            get { return projName; }
            set { projName = value; }
        }
    }
}
