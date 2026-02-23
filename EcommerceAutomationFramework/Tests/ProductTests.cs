using EcommerceAutomationFramework.API;
using EcommerceAutomationFramework.Models;
using EcommerceAutomationFramework.Pages;
using EcommerceAutomationFramework.Utilities;

namespace EcommerceAutomationFramework.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class ProductTests : BaseTest
{
    [Test]
    [Retry(2)]
    public void Product_Search_Should_Show_Results()
    {
        var keyword = ConfigReader.GetSection<SearchData>("Search").Keyword;

        var homePage = new HomePage(Driver);
        var productPage = new ProductPage(Driver);

        Driver.Navigate().GoToUrl(BaseUrl);
        homePage.OpenProducts();
        productPage.SearchProduct(keyword);

        Assert.That(productPage.GetFirstSearchResultName(), Is.Not.Empty);
    }

    [Test]
    public async Task Get_Product_Details_Via_API_Should_Return_Data()
    {
        var api = new ProductApi(ConfigReader.GetFrameworkConfig().Api.BaseUrl);
        var products = await api.GetProductsAsync();

        Assert.That(products, Is.Not.Null);
        Assert.That(products!.products.Count, Is.GreaterThan(0));

        var firstProduct = await api.GetProductDetailsAsync(products.products[0].id);
        Assert.That(firstProduct, Is.Not.Null);
        Assert.That(firstProduct!.name, Is.Not.Empty);
    }
}
