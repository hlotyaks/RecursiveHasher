using System;
using Xunit;
using System.IO;

namespace HasherTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string cwd = Directory.GetCurrentDirectory();
            string testfile = $"{cwd}\\testcases\\testcase1\\A.txt";

            FileHasher.Hasher hasher = new FileHasher.Hasher();
            FileInfo f = new FileInfo(testfile);

           string hash = hasher.HashFile(f, true);
        }
    }
}
