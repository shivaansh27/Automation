using OpenQA.Selenium;

namespace EcommerceAutomationFramework.Utilities;

public static class ScreenshotHelper
{
    public static string Capture(IWebDriver driver, string testName)
    {
        Directory.CreateDirectory("Screenshots");
        var fileName = $"{Sanitize(testName)}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.png";
        var filePath = Path.Combine("Screenshots", fileName);

        if (driver is not ITakesScreenshot screenshotDriver)
        {
            return string.Empty;
        }

        try
        {
            var screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(filePath);
            return Path.GetFullPath(filePath);
        }
        catch
        {
            return string.Empty;
        }
    }

    private static string Sanitize(string value)
    {
        var invalid = Path.GetInvalidFileNameChars();
        return new string(value.Select(c => invalid.Contains(c) ? '_' : c).ToArray());
    }
}
