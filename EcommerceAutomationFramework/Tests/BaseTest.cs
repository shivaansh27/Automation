using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using EcommerceAutomationFramework.Reports;
using EcommerceAutomationFramework.Utilities;
using OpenQA.Selenium;

namespace EcommerceAutomationFramework.Tests;

public abstract class BaseTest
{
#pragma warning disable NUnit1032
    protected IWebDriver Driver = null!;
#pragma warning restore NUnit1032
    protected string BaseUrl = string.Empty;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        ExtentReportManager.Initialize();
    }

    [SetUp]
    public void Setup()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        ExtentReportManager.CreateTest(testName);
        ExtentReportManager.GetTest().Info($"Starting: {testName}");
        Logger.Info($"Starting test: {testName}");

        BaseUrl = ConfigReader.GetBaseUrl();
        Driver = DriverFactory.GetDriver();
    }

    [TearDown]
    public void TearDown()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        ExtentReportManager.TryGetTest(out var test);

        if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            if (test != null)
            {
                if (Driver != null)
                {
                    var screenshotPath = ScreenshotHelper.Capture(Driver, TestContext.CurrentContext.Test.Name);
                    if (!string.IsNullOrWhiteSpace(screenshotPath))
                    {
                        test.AddScreenCaptureFromPath(screenshotPath);
                    }
                }

                test.Fail(MarkupHelper.CreateCodeBlock(TestContext.CurrentContext.Result.Message));
            }

            Logger.Error($"Failed test: {TestContext.CurrentContext.Test.Name} - {TestContext.CurrentContext.Result.Message}");
        }
        else
        {
            test?.Pass("Test passed");
            Logger.Info($"Passed test: {TestContext.CurrentContext.Test.Name}");
        }

        try
        {
            DriverFactory.QuitDriver();
        }
        catch (Exception ex)
        {
            Logger.Error($"Driver cleanup error: {ex.Message}");
        }

        ExtentReportManager.ClearCurrentTest();
        ExtentReportManager.Flush();
    }
}
