using EcommerceAutomationFramework.Utilities;
using OpenQA.Selenium;

namespace EcommerceAutomationFramework.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WaitHelper Wait;

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WaitHelper(driver, ConfigReader.GetFrameworkConfig().TimeoutSeconds);
    }

    protected void Click(By by)
    {
        Wait.WaitForClickable(by).Click();
    }

    protected void Type(By by, string value)
    {
        var element = Wait.WaitForVisible(by);
        element.Clear();
        element.SendKeys(value);
    }

    protected string ReadText(By by)
    {
        return Wait.WaitForVisible(by).Text;
    }
}
