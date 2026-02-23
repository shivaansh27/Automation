using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace EcommerceAutomationFramework.Pages;

public class ProductPage : BasePage
{
    private static readonly By SearchBox = By.Id("search_product");
    private static readonly By SearchButton = By.Id("submit_search");
    private static readonly By FirstVisibleProductName = By.CssSelector(".productinfo.text-center p");
    private static readonly By FirstAddToCartButton = By.XPath("(//div[contains(@class,'features_items')]//a[contains(@class,'add-to-cart') and @data-product-id])[1]");
    private static readonly By ContinueShoppingButton = By.CssSelector("button[data-dismiss='modal']");
    private static readonly By FirstViewProductLink = By.XPath("(//a[contains(@href,'/product_details/')])[1]");
    private static readonly By ProductDetailsAddToCartButton = By.CssSelector("button.btn.btn-default.cart");

    public ProductPage(IWebDriver driver) : base(driver)
    {
    }

    public void SearchProduct(string keyword)
    {
        Type(SearchBox, keyword);
        Click(SearchButton);
    }

    public string GetFirstSearchResultName()
    {
        return ReadText(FirstVisibleProductName);
    }

    public void AddFirstProductToCart()
    {
        if (TryAddFromProductsGrid())
        {
            if (Driver.FindElements(ContinueShoppingButton).Count > 0)
            {
                Click(ContinueShoppingButton);
            }

            return;
        }

        AddViaProductDetailsPage();
    }

    private bool TryAddFromProductsGrid()
    {
        var end = DateTime.UtcNow.AddSeconds(15);
        while (DateTime.UtcNow < end)
        {
            var buttons = Driver.FindElements(FirstAddToCartButton);
            if (buttons.Count > 0)
            {
                var button = buttons[0];
                new Actions(Driver).MoveToElement(button).Perform();
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", button);
                return true;
            }

            Thread.Sleep(250);
        }

        return false;
    }

    private void AddViaProductDetailsPage()
    {
        var viewProductLinks = Driver.FindElements(FirstViewProductLink);
        if (viewProductLinks.Count > 0)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", viewProductLinks[0]);
        }
        else
        {
            NavigateToProductDetails(1);
        }

        var addButton = Wait.WaitForClickable(ProductDetailsAddToCartButton);
        addButton.Click();

        if (Driver.FindElements(ContinueShoppingButton).Count > 0)
        {
            Click(ContinueShoppingButton);
        }
    }

    public void AddProductByIdToCart(int productId)
    {
        NavigateToProductDetails(productId);
        var addButton = Wait.WaitForClickable(ProductDetailsAddToCartButton);
        addButton.Click();

        if (Driver.FindElements(ContinueShoppingButton).Count > 0)
        {
            Click(ContinueShoppingButton);
        }
    }

    private void NavigateToProductDetails(int productId)
    {
        var currentUri = new Uri(Driver.Url);
        var baseUrl = $"{currentUri.Scheme}://{currentUri.Host}";
        Driver.Navigate().GoToUrl($"{baseUrl}/product_details/{productId}");
    }
}
