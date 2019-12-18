using System;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RecursiveHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine(p.Run());

            //var task = Task
        }

        string Run()
        {
            return "Hello World";
        }

        async Task<byte[]> HashFolder(DirectoryInfo folder, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            using(var alg = MD5.Create())
            {
                return await alg.ComputeHashAsync(folder.EnumerateFiles(searchPattern, searchOption));
            }
        }
    }
}
