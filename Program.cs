using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;

namespace RecursiveHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            var folder = new DirectoryInfo(@"C:\tmp\experiment2\packageA");
            var task = Task.Run(async () => await p.HashFolder(folder));
            var result = task.Result;

            Console.WriteLine($"Hash of {folder.Name} in {folder.FullName}");
            Console.WriteLine(result);

        }

        async Task<string> HashFolder(DirectoryInfo folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            using(var alg = MD5.Create())
            {
                var result = await alg.ComputeHashAsync(folder.EnumerateFiles(searchPattern, searchOption));

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
