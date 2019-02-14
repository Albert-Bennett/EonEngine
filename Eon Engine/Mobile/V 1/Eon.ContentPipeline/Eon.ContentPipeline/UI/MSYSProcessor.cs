/* Created 28/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.UIApi;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;
using System.Xml;

namespace Eon.ContentPipeline.UI
{
    /// <summary>
    /// Used to process .MSYS files.
    /// </summary>
    [ContentProcessor(DisplayName = "Menu System Setup File Processor - Eon Framework")]
    public class MSYSProcessor : ContentProcessor<XmlDocument, MenuSystemSetup>
    {
        XmlDocument input;

        public override MenuSystemSetup Process(XmlDocument input, ContentProcessorContext context)
        {
            MenuSystemSetup output = new MenuSystemSetup();
            this.input = input;

            if (input.GetElementsByTagName("Setup").Count > 0)
            {
                if (input.GetElementsByTagName("Assemblies").Count > 0)
                {
                    int assemblies = 0;
                    List<string> names = new List<string>();

                    GetNodeInt("AssemblyCount", out assemblies);
                    GetNodeList("ScreenNames", out names);

                    output.ScreenNames = names;

                    if (assemblies > 0)
                    {
                        output.Assemblies = new string[assemblies];

                        for (int i = 0; i < assemblies; i++)
                        {
                            string s = "";

                            GetNodeString("Assembly" + i, out s);

                            output.Assemblies[i] = s;
                        }
                    }
                }

                output.Screens = new ParameterCollection[0];

                if (input.GetElementsByTagName("Screens").Count > 0)
                {
                    int screens = 0;

                    GetNodeInt("ScreenCount", out screens);

                    for (int i = 0; i < screens; i++)
                    {
                        int paramCount = 0;
                        object[] parameters = null;
                        string typeString = " ";

                        if (input.GetElementsByTagName("Screen" + i).Count > 0)
                            foreach (XmlNode node in input.GetElementsByTagName("Screen" + i)[0].ChildNodes)
                                switch (node.Name)
                                {
                                    case "TypeName":
                                        typeString = node.InnerText;
                                        break;

                                    case "Parameters":
                                        int.TryParse(node.InnerText, out paramCount);
                                        break;
                                }

                        if (paramCount > 0)
                            TypeHelper.Process(ref input, "Screen" + i, paramCount, out parameters);

                        ParameterCollection para = new ParameterCollection(typeString, parameters);

                        output.Screens = ArrayHelper.AddItem<ParameterCollection>(para, output.Screens);
                    }
                }
            }

            return output;
        }

        void GetNodeInt(string nodeKey, out int value)
        {
            value = 0;

            foreach (XmlNode node in input.GetElementsByTagName(nodeKey)[0].ChildNodes)
                switch (node.Name)
                {
                    case "Value":
                        int.TryParse(node.InnerText, out value);
                        break;
                }
        }

        void GetNodeString(string nodeKey, out string value)
        {
            value = "";

            foreach (XmlNode node in input.GetElementsByTagName(nodeKey)[0].ChildNodes)
                switch (node.Name)
                {
                    case "Value":
                        value = node.InnerText;
                        break;
                }
        }

        void GetNodeList(string nodeKey, out List<string> value)
        {
            value = new List<string>();

            string txt = "";
            GetNodeString(nodeKey, out txt);

            string[] items = txt.Split(new char[]
            {
                ','
            }, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < items.Length; i++)
                value.Add(items[i]);
        }
    }
}