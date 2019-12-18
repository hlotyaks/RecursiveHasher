using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class HashAlgorithmExtensions
{
    public static async Task<byte[]> ComputeHashAsync(this HashAlgorithm alg, IEnumerable<FileInfo> files, bool includePaths = true)
    {
        using (var cs = new CryptoStream(Stream.Null, alg, CryptoStreamMode.Write))
        {
            foreach (var file in files)
            {
                if (includePaths)
                {
                    var pathBytes = Encoding.UTF8.GetBytes(file.FullName);
                    cs.Write(pathBytes, 0, pathBytes.Length);
                }

                using (var fs = file.OpenRead())
                    await fs.CopyToAsync(cs);
            }

            cs.FlushFinalBlock();
        }

        return alg.Hash;
    }
}