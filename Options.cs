using System;
using CommandLine;

namespace RecursiveHasher
{
    public class Options
    {
        [Option('f', "folder", Required = true, HelpText ="Path to folder to hash.")]
        public string Folder { get; set; }

        [Option('e', "exclude", Required = false, HelpText ="Exclude root path from hash folder.")]
        public bool ExcludeRoot { get; set; }
    }
}