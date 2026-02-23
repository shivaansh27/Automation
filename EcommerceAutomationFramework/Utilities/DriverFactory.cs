using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace EcommerceAutomationFramework.Utilities;

public static class DriverFactory
{
    private static readonly ThreadLocal<IWebDriver?> Driver = new();

    public static IWebDriver GetDriver()
    {
        if (Driver.Value == null)
        {
            Driver.Value = CreateDriver();
        }

        return Driver.Value;
    }

    public static void QuitDriver()
    {
        Driver.Value?.Quit();
        Driver.Value?.Dispose();
        Driver.Value = null;
    }

    private static IWebDriver CreateDriver()
    {
        var browser = Environment.GetEnvironmentVariable("BROWSER")?.Trim();
        if (string.IsNullOrWhiteSpace(browser))
        {
            browser = ConfigReader.GetFrameworkConfig().Browser;
        }

        IWebDriver driver = browser?.ToLowerInvariant() switch
        {
            "edge" => CreateEdgeDriver(),
            _ => CreateChromeDriver()
        };

        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
        return driver;
    }

    private static IWebDriver CreateChromeDriver()
    {
        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        options.AddArgument("--disable-notifications");
        return new ChromeDriver(options);
    }

    private static IWebDriver CreateEdgeDriver()
    {
        new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);

        var options = new EdgeOptions();
        options.AddArgument("start-maximized");
        options.AddArgument("--disable-notifications");
        return new EdgeDriver(options);
    }
}
