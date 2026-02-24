using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace EcommerceAutomationFramework.Reports;

public static class ExtentReportManager
{
    private static readonly object LockObject = new();
    private static readonly ThreadLocal<ExtentTest?> CurrentTest = new();
    private static ExtentReports? _extent;

    public static void Initialize()
    {
        lock (LockObject)
        {
            if (_extent != null)
            {
                return;
            }

            Directory.CreateDirectory("Reports");
            var reportPath = Path.GetFullPath(Path.Combine("Reports", $"AutomationReport_{DateTime.UtcNow:yyyyMMdd_HHmmss}.html"));
            var spark = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(spark);
            _extent.AddSystemInfo("Framework", ".NET 8 + NUnit");
        }
    }

    public static void CreateTest(string testName)
    {
        if (_extent == null)
        {
            Initialize();
        }

        CurrentTest.Value = _extent!.CreateTest(testName);
    }

    public static ExtentTest GetTest()
    {
        return CurrentTest.Value ?? throw new InvalidOperationException("Extent test is not initialized for current thread.");
    }

    public static bool TryGetTest(out ExtentTest? test)
    {
        test = CurrentTest.Value;
        return test != null;
    }

    public static void ClearCurrentTest()
    {
        CurrentTest.Value = null;
    }

    public static void Flush()
    {
        lock (LockObject)
        {
            _extent?.Flush();
        }
    }
}
