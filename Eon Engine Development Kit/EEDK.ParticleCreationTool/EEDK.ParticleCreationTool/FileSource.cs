/* Created: 19/01/2015
 * Last Updated: 22/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Collections;
using System.Collections.Generic;
using System.IO;

namespace EEDK.ParticleCreationTool
{
    /// <summary>
    /// Used to set and get the locations of 
    /// various ParticleEmitter realted objects. 
    /// </summary>
    public static class FileSource
    {
        static List<string> coppiedDlls = new List<string>();

        public static EonDictionary<string, string> Attachments { get; set; }
        public static EonDictionary<string, string> Cycles { get; set; }
        public static EonDictionary<string, string> RenderTypes { get; set; }
        public static EonDictionary<string, string> EmittionTypes { get; set; }

        public static List<string> CoppiedDlls
        {
            get { return coppiedDlls; }
        }

        public static void LoadAssemblies(string rootPath)
        {
            ProcessAssembly(Attachments.Values, rootPath);
            ProcessAssembly(Cycles.Values, rootPath);
            ProcessAssembly(RenderTypes.Values, rootPath);
            ProcessAssembly(EmittionTypes.Values, rootPath);
        }

        static void ProcessAssembly(List<string> assemblies, string rootPath)
        {
            for (int i = 0; i < assemblies.Count; i++)
                if (!assemblies[i].Contains("Eon."))
                    if (!coppiedDlls.Contains(assemblies[i]))
                    {
                        File.Copy(rootPath + "Libraries\\" + assemblies[i], assemblies[i]);

                        coppiedDlls.Add(assemblies[i]);
                        AssemblyManager.AddAssemblyRef(assemblies[i]);
                    }
        }

        public static void DeleteCoppiedDLLs()
        {
            for (int i = 0; i < coppiedDlls.Count; i++)
                File.Delete(coppiedDlls[i]);
        }
    }
}
