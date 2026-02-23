using OpenQA.Selenium;

namespace EcommerceAutomationFramework.Pages;

public class HomePage : BasePage
{
    private static readonly By ProductsMenu = By.CssSelector("a[href='/products']");

    public HomePage(IWebDriver driver) : base(driver)
    {
    }

    public void OpenProducts()
    {
        var links = Driver.FindElements(ProductsMenu);
        if (links.Count > 0)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", links[0]);
        }

        if (!Driver.Url.Contains("/products", StringComparison.OrdinalIgnoreCase))
        {
            var currentUri = new Uri(Driver.Url);
            var baseUrl = $"{currentUri.Scheme}://{currentUri.Host}";
            Driver.Navigate().GoToUrl($"{baseUrl}/products");
        }
    }
}
