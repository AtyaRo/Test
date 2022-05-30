using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Collections.Concurrent;
using System.Reflection;

namespace Ideals_Test_Project.Helpers
{
    public static class ReporterHelper
    {
        private static ExtentReports _reporter = new ExtentReports();
        private static ExtentHtmlReporter _htmlReporter = new ExtentHtmlReporter(Assembly.GetEntryAssembly().Location);
        private static ConcurrentDictionary<string, ExtentTest> _testLogs = new ConcurrentDictionary<string, ExtentTest>();
        private static object _lock = new object();   
        static ReporterHelper()
        {
            _reporter.AttachReporter(_htmlReporter);
        }

        private static ExtentTest GetTestLog()
        {
            var testName = TestContext.CurrentContext.Test.FullName;
            if (!_testLogs.TryGetValue(testName, out var testLog))
            {
                testLog = _reporter.CreateTest(testName);
                _testLogs.TryAdd(testName, testLog);
            }

            return testLog;
        }

        public static void Log(Status status, string logText)
        {
            lock (_lock)
            {
                GetTestLog().Log(status, logText);
            }
        }

        public static void CloseReporter()
        {
            _reporter.Flush();
        }

        public static void SaveScreenshot(string imageFilePath)
        {
            lock (_lock)
            {
                GetTestLog().Error("Error", MediaEntityBuilder.CreateScreenCaptureFromPath(imageFilePath).Build());
            }
        }
    }
}
