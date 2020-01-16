using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileHasher
{
    public static class HashAlgorithmExtensions
    {
        public static async Task<byte[]> ComputeHashAsync(this HashAlgorithm alg, IEnumerable<FileInfo> files, string rootPath)
        {
            using (var cs = new CryptoStream(Stream.Null, alg, CryptoStreamMode.Write))
            {
                foreach (var file in files)
                {
                    // Will either include the rootPath or not.
                    string fileName = file.FullName.Substring(rootPath.Length, file.FullName.Length - rootPath.Length);
                    var pathBytes = Encoding.UTF8.GetBytes(fileName);
                    cs.Write(pathBytes, 0, pathBytes.Length);

                    using (var fs = file.OpenRead())
                    {
                        await fs.CopyToAsync(cs);
                    }
                }

                cs.FlushFinalBlock();
            }

            return alg.Hash;
        }

        public static async Task<byte[]> ComputeHash(this HashAlgorithm alg, FileInfo file, bool onlyContents)
        {
            using (var cs = new CryptoStream(Stream.Null, alg, CryptoStreamMode.Write))
            {
                if(!onlyContents)
                {
                    var pathBytes = Encoding.UTF8.GetBytes(file.FullName);
                    cs.Write(pathBytes, 0, pathBytes.Length);
                }

                using (var fs = file.OpenRead())
                {
                    await fs.CopyToAsync(cs);
                }

                cs.FlushFinalBlock();
            }

            return alg.Hash;
        }
    }
}