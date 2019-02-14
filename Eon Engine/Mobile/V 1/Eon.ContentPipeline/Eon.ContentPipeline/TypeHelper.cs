/* Created 28/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Eon.ContentPipeline
{
    internal class TypeHelper
    {
        static XmlDocument input;

        public static void SetXmlDocument(XmlDocument document)
        {
            input = document;
        }

        public static void Process(ref XmlDocument docu, 
            string key, int paramNum, out object[] objs)
        {
            input = docu;
            objs = new object[paramNum];

            for (int i = 0; i < objs.Length; i++)
                if (input.GetElementsByTagName(key + "Param" + i).Count > 0)
                    foreach (XmlNode node in input.GetElementsByTagName(key + "Param" + i)[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "Value":
                                SerializationHelper.GetTypeValue(node.InnerText, out objs[i]);
                                break;
                        }
        }

        public static ParameterCollection GetSerializedObject(string key)
        {
            int paramCount = 0;
            object[] parameters = null;
            string typeString = " ";

            if (input.GetElementsByTagName(key).Count > 0)
                foreach (XmlNode node in input.GetElementsByTagName(key)[0].ChildNodes)
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
                TypeHelper.Process(ref input, key, paramCount, out parameters);

            return new ParameterCollection(typeString, parameters);
        }

        public static void ProcessListOfList(ref XmlDocument docu, string key, out List<List<int>> output)
        {
            input = docu;
            output = new List<List<int>>();

            if (input.GetElementsByTagName(key).Count > 0)
            {
                int lists = 0;

                foreach (XmlNode node in input.GetElementsByTagName(key)[0].ChildNodes)
                    switch (node.Name)
                    {
                        case "Count":
                            int.TryParse(node.InnerText, out lists);
                            break;
                    }

                if (lists > 0)
                    for (int i = 0; i < lists; i++)
                    {
                        List<int> list = new List<int>();
                        string value = "";

                        foreach (XmlNode node in input.GetElementsByTagName(key + i)[0].ChildNodes)
                            switch (node.Name)
                            {
                                case "Value":
                                    value = node.InnerText;
                                    break;
                            }

                        string[] items = value.Split(new char[]
                        {
                            ',',' '
                        }, StringSplitOptions.RemoveEmptyEntries);

                        for (int s = 0; s < items.Length; s++)
                            list.Add(Convert.ToInt32(items[s]));

                        output.Add(list);
                    }

                //output.Reverse();
            }
        }
    }
}
