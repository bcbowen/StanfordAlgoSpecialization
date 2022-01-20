using System.IO;

namespace Algorithms.Tests
{
    public static class TestUtils
    {
        public const string IgnoreLongRunningTestsMessage = "Long running tests are skipped to reduce run time. Enable for troubleshooting to more in depth testing as needed.";

        public static DirectoryInfo GetTestCaseDirectory()
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            return new DirectoryInfo(Path.Combine(currentDirectory.Parent.Parent.Parent.FullName, "Tests", "TestData"));
        }
    }
}
