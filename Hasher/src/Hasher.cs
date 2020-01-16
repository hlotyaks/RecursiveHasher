using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;

namespace FileHasher
{
    public class Hasher
    {
        public string HashFile(FileInfo file, bool onlyContents)
        {
            using(var alg = MD5.Create())
            {
                var result = alg.ComputeHash(file, true);

                byte[] hashResult = result.Result;

                // Folder has starts with folder name.  
                // Format - name.hash
                StringBuilder sb = new StringBuilder();

                // Build the final string by converting each byte
                // into hex and appending it to a StringBuilder
                for (int i = 0; i < hashResult.Length; i++)
                {
                    sb.Append(hashResult[i].ToString("X2"));
                }

                // And return it
                return sb.ToString();
            }
        }

    }
}
