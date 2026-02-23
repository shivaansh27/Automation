using EcommerceAutomationFramework.Pages;

namespace EcommerceAutomationFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class CheckoutTests : BaseTest
{
    [Test]
    [Retry(1)]
    public void Add_To_Cart_And_Checkout_Flow_Should_Succeed()
    {
        var homePage = new HomePage(Driver);
        var productPage = new ProductPage(Driver);
        var cartPage = new CartPage(Driver);

        Driver.Navigate().GoToUrl(BaseUrl);

        homePage.OpenProducts();
        productPage.AddFirstProductToCart();
        cartPage.OpenCart();

        if (!cartPage.HasCartItems())
        {
            productPage.AddProductByIdToCart(1);
            cartPage.OpenCart();
        }

        var proceeded = cartPage.ProceedToCheckout();

        Assert.That(proceeded, Is.True, $"Proceed to checkout button not found. Current URL: {Driver.Url}");
        Assert.That(cartPage.IsCheckoutOrLoginPromptDisplayed(), Is.True, $"Checkout flow did not continue. Current URL: {Driver.Url}");
    }
}
