/* Created: 02/01/2015
 * Last Updated: 18/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using Eon;
using Eon.Collections;
using Eon.Helpers;
using Eon.Particles.Attachments.Base;
using Eon.Particles.Cycles;
using Eon.Particles.D2.EmittionTypes.Base;
using Eon.Particles.D2.RenderMethods;
using Eon.Particles.D3.EmittionTypes.Base;
using Eon.Particles.D3.RenderMethods;
using Eon.Rendering3D.Framework.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EEDK.LibraryReader
{
    class Program
    {
        static string searchPath;
        static string savePath = "Listing.lst";

        static ObjectListing objects = new ObjectListing();

        static void Main(string[] args)
        {
            Crosswalk.Message m = SerializationHelper.Deserialize<Crosswalk.Message>("Temp.temp", false, "");
            searchPath = m.Messages[0];

            SerializationHelper.DeleteFile("Temp.temp");

            if (SerializationHelper.FolderExists(m.Messages[0]))
            {
                IEnumerable<string> files = SerializationHelper.GetFiles(m.Messages[0], ".dll");

                if (files.Count() > 0)
                {
                    Console.WriteLine(files.Count() + " Libraries Found.");
                    Console.WriteLine();

                    SearchFiles(files);

                    Console.WriteLine();
                    Console.WriteLine(objects.Objects.Count + " Viable Createable Objects Found.");

                    if (objects.Shaders.Count > 0)
                        Console.WriteLine(objects.Shaders.Count + " Materials Found.");

                    Console.WriteLine("< End Search >");
                }
                else
                    Console.WriteLine("No createable objects found in any available directories.");

                Type[] types = new Type[]
                {
                    typeof(EonDictionary<string,string>)
                };

                SerializationHelper.Serialize<ObjectListing>(objects, savePath, types);

                Console.ReadKey();
            }
            else
            {
                SerializationHelper.CreateFolder(searchPath);
                Main(null);
            }
        }

        static void SearchFiles(IEnumerable<string> files)
        {
            List<string> filePaths = new List<string>(files);

            for (int i = 0; i < filePaths.Count(); i++)
            {
                string path = filePaths[i];

                List<Type> comps = AssemblyManager.FindDerivedTypes(path, typeof(ObjectComponent));
                List<Type> objs = AssemblyManager.FindDerivedTypes(path, typeof(GameObject));
                List<Type> shaders = AssemblyManager.FindDerivedTypes(path, typeof(Shader));

                List<Type> particleAttachments = AssemblyManager.FindDerivedTypes(path, typeof(IAttachment));
                foreach (Type t in particleAttachments)
                    objects.ParticleAttachments.Add(t.FullName, path.Remove(0, searchPath.Count() + 1));

                List<Type>particleCycles = AssemblyManager.FindDerivedTypes(path,typeof(ICycle));
                foreach (Type t in particleCycles)
                    objects.ParticleCycles.Add(t.FullName, path.Remove(0, searchPath.Count() + 1));

                List<Type> particle2DEmitters = AssemblyManager.FindDerivedTypes(path, typeof(IEmitter2D));
                foreach (Type t in particle2DEmitters)
                    objects.ParticleEmitters2D.Add(t.FullName, path.Remove(0, searchPath.Count() + 1));

                List<Type> particle3DEmitters = AssemblyManager.FindDerivedTypes(path, typeof(IEmitter3D));
                foreach (Type t in particle3DEmitters)
                    objects.ParticleEmitters3D.Add(t.FullName, path.Remove(0, searchPath.Count() + 1));

                List<Type> particleRenders2D = AssemblyManager.FindDerivedTypes(path, typeof(I2DParticleRenderer));
                foreach (Type t in particleRenders2D)
                    objects.ParticleRenderMethods2D.Add(t.FullName, path.Remove(0, searchPath.Count() + 1));

                List<Type> particleRenders3D = AssemblyManager.FindDerivedTypes(path, typeof(I3DParticleRenderer));
                foreach (Type t in particleRenders3D)
                    objects.ParticleRenderMethods3D.Add(t.FullName, path.Remove(0, searchPath.Count() + 1));

                Dictionary<string, ObjectTypes> names =
                    new Dictionary<string, ObjectTypes>();

                if (comps != null)
                    foreach (Type t in comps)
                        names.Add(t.FullName, ObjectTypes.Component);

                if (objs != null)
                    foreach (Type t in objs)
                        names.Add(t.FullName, ObjectTypes.GameObject);

                if (shaders != null)
                    foreach (Type t in shaders)
                        names.Add(t.FullName, ObjectTypes.Shader);

                if (names.Count > 0)
                {
                    path = path.Remove(0, searchPath.Count() + 1);

                    string text = "< Begining " + path + " Readout >";
                    Console.WriteLine(text);

                    string breaker = CreateBreaker(text);

                    Console.WriteLine(breaker);

                    foreach (string s in names.Keys)
                    {
                        switch(names[s])
                        {
                            case ObjectTypes.GameObject:
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    objects.Objects.Add(s, path);
                                }
                                break;

                            case ObjectTypes.Component:
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    objects.Objects.Add(s, path);
                                }
                                break;

                            case ObjectTypes.Shader:
                                {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                objects.Shaders.Add(s, path);
                                    }
                                break;
                        }

                        Console.WriteLine("Type Name: " + GetReadableName(s));
                    }

                    Console.ForegroundColor = ConsoleColor.White;

                    text = "< Ending " + path + " Readout >";
                    breaker = CreateBreaker(text);

                    Console.WriteLine(breaker);
                    Console.WriteLine(text);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        static string GetReadableName(string name)
        {
            string[] split = name.Split(new char[]
            {
                '.'
            }, StringSplitOptions.RemoveEmptyEntries);

            return split[split.Length - 1];
        }

        static string CreateBreaker(string text)
        {
            string breaker = "";

            for (int i = 0; i < text.Length; i++)
                breaker += "-";

            return breaker;
        }
    }
}
