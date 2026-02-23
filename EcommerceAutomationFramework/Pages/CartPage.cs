using OpenQA.Selenium;

namespace EcommerceAutomationFramework.Pages;

public class CartPage : BasePage
{
    private static readonly By CartMenu = By.CssSelector("a[href='/view_cart']");
    private static readonly By ProceedToCheckoutButton = By.CssSelector("a.check_out[href='/checkout']");
    private static readonly By AnyProceedToCheckoutButton = By.CssSelector(".check_out");
    private static readonly By DeliveryAddressHeader = By.CssSelector("#address_delivery .address_firstname.address_lastname");
    private static readonly By DeliveryAddressSection = By.Id("address_delivery");
    private static readonly By PlaceOrderButton = By.CssSelector("a[href='/payment']");
    private static readonly By CheckoutLoginLink = By.CssSelector(".modal-content a[href='/login']");
    private static readonly By CartItemRows = By.CssSelector("#cart_info_table tbody tr");

    public CartPage(IWebDriver driver) : base(driver)
    {
    }

    public void OpenCart()
    {
        Click(CartMenu);
    }

    public bool ProceedToCheckout()
    {
        var buttons = Driver.FindElements(ProceedToCheckoutButton);
        if (buttons.Count > 0)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", buttons[0]);
            return true;
        }

        var fallbackButtons = Driver.FindElements(AnyProceedToCheckoutButton);
        var visibleButton = fallbackButtons.FirstOrDefault(x => x.Displayed && x.Enabled);
        if (visibleButton != null)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", visibleButton);
            return true;
        }

        return false;
    }

    public string GetDeliveryAddressHeader()
    {
        return Driver.FindElements(DeliveryAddressHeader).Count > 0
            ? Driver.FindElement(DeliveryAddressHeader).Text
            : string.Empty;
    }

    public bool IsCheckoutPageLoaded()
    {
        return Driver.Url.Contains("/checkout", StringComparison.OrdinalIgnoreCase)
               && (Driver.FindElements(DeliveryAddressSection).Count > 0
                   || Driver.FindElements(PlaceOrderButton).Count > 0);
    }

    public bool IsCheckoutOrLoginPromptDisplayed()
    {
        return IsCheckoutPageLoaded() || Driver.FindElements(CheckoutLoginLink).Count > 0;
    }

    public bool HasCartItems()
    {
        return Driver.FindElements(CartItemRows).Count > 0;
    }
}
