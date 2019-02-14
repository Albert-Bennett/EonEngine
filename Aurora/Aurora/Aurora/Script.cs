/* Created: 29/08/2015
 * Last Updated: 02/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Aurora.Scaning;
using Aurora.Tokens;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aurora
{
    /// <summary>
    /// Defines a .auriora script.
    /// </summary>
    public sealed class Script
    {
        Scanner scan;
        Parser parser = new Parser();

        /// <summary>
        /// Creates a new blank Script.
        /// </summary>
        public Script(string filepath)
        {
            string path = Environment.CurrentDirectory + "/" + filepath + ".Aurora";
            List<Token> tokens = null;

            if (File.Exists(path))
            {
                Scanner scan = new Scanner(File.Open(path, FileMode.Open), this);
                tokens = scan.Tokens;
            }

            if (tokens != null)
                parser.Generate(tokens);
        }

        /// <summary>
        /// Finds a particular # block id.
        /// </summary>
        /// <param name="id">The id of the object in the Script to be found.</param>
        /// <returns>The result of the search.</returns>
        public object Find(string id)
        {
            return parser.Find(id);
        }
    }
}
