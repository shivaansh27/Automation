using EcommerceAutomationFramework.Models;
using RestSharp;
using System.Text.Json;

namespace EcommerceAutomationFramework.API;

public class ProductApi : BaseApiClient
{
    public ProductApi(string baseUrl) : base(baseUrl)
    {
    }

    public async Task<ProductListResponse?> GetProductsAsync()
    {
        var request = new RestRequest("productsList", Method.Get);
        var response = await ExecuteAsync(request);

        if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
        {
            return null;
        }

        return JsonSerializer.Deserialize<ProductListResponse>(response.Content);
    }

    public async Task<ProductModel?> GetProductDetailsAsync(int productId)
    {
        var products = await GetProductsAsync();
        return products?.products.FirstOrDefault(p => p.id == productId);
    }
}
