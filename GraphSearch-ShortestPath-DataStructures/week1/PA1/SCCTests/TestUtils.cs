using System;
using System.IO;

namespace SCCTests
{
    public static class TestUtils
    {
        public static DirectoryInfo GetTestCaseDirectory()
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            return new DirectoryInfo(Path.Combine(currentDirectory.Parent.Parent.Parent.FullName, "TestCases"));
        }
    }
}
