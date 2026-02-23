using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EcommerceAutomationFramework.Utilities;

public class WaitHelper
{
    private readonly IWebDriver _driver;
    private readonly int _timeoutSeconds;

    public WaitHelper(IWebDriver driver, int timeoutSeconds)
    {
        _driver = driver;
        _timeoutSeconds = timeoutSeconds;
    }

    public IWebElement WaitForVisible(By locator)
    {
        return GetWebDriverWait().Until(ExpectedConditions.ElementIsVisible(locator));
    }

    public IWebElement WaitForClickable(By locator)
    {
        return GetWebDriverWait().Until(ExpectedConditions.ElementToBeClickable(locator));
    }

    public bool WaitForUrlContains(string text)
    {
        return GetWebDriverWait().Until(d => d.Url.Contains(text, StringComparison.OrdinalIgnoreCase));
    }

    private WebDriverWait GetWebDriverWait()
    {
        return new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeoutSeconds));
    }
}
