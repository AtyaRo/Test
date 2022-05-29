using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Collections.Concurrent;
using System.Reflection;

namespace Ideals_Test_Project.Helpers
{
    public static class ReporterHelper
    {
        private static ExtentReports reporter = new ExtentReports();
        private static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(Assembly.GetEntryAssembly().Location);
        public static ConcurrentDictionary<string, ExtentTest> TestLogs = new ConcurrentDictionary<string, ExtentTest>();
        private static object elock = new object();   
        static ReporterHelper()
        {
            reporter.AttachReporter(htmlReporter);
        }

        private static ExtentTest GetTestLog()
        {
            var testName = TestContext.CurrentContext.Test.FullName;
            if (!TestLogs.TryGetValue(testName, out var testLog))
            {
                testLog = reporter.CreateTest(testName);
                TestLogs.TryAdd(testName, testLog);
            }

            return testLog;
        }

        public static void Log(Status status, string logText)
        {
            lock (elock)
            {
                GetTestLog().Log(status, logText);
            }
        }

        public static void CloseReporter()
        {
            reporter.Flush();
        }

        public static void SaveScreenshot(string imageFilePath)
        {
            lock (elock)
            {
                GetTestLog().Error("Error", MediaEntityBuilder.CreateScreenCaptureFromPath(imageFilePath).Build());
            }
        }
    }
}
