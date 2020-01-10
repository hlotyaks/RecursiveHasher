using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using CommandLine;


namespace RecursiveHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = string.Empty;
            bool excludeRoot = false;
            bool commandlineError = false;

            Program p = new Program();

            Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>( o =>
                {
                    folderPath = o.Folder; 
                    Console.WriteLine($"Folder to hash is {folderPath}.");

                    if(o.ExcludeRoot)
                    {
                        Console.WriteLine($"Exclude root path true.");
                        excludeRoot = o.ExcludeRoot;
                    }
                })
            .WithNotParsed<Options>( err =>
                {
                    commandlineError = true;
                });

            if (commandlineError)
            {
                return;
            }

            var folder = new DirectoryInfo(folderPath);

            var task = Task.Run(async () => await p.HashFolder(folder, excludeRoot));
            var result = task.Result;

            Console.WriteLine($"Hash of {folder.Name} in {folder.FullName}");
            Console.WriteLine(result);

        }

        async Task<string> HashFolder(DirectoryInfo folder, bool excludeRoot, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            string rootPath = String.Empty;

            if(excludeRoot)
            {
                rootPath = folder.FullName.Substring(0, folder.FullName.Length - folder.FullName.Split('\\').Last().Length );
            }   

            using(var alg = MD5.Create())
            {

                var result = await alg.ComputeHashAsync(folder.EnumerateFiles(searchPattern, searchOption), rootPath);

                // Build the final string by converting each byte
                // into hex and appending it to a StringBuilder
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }

                // And return it
                return sb.ToString();

            }
        }
    }
}
