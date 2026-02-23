using System.Text.Json.Serialization;

namespace EcommerceAutomationFramework.Models;

public class ProductListResponse
{
    public int responseCode { get; set; }
    public List<ProductModel> products { get; set; } = new();
}

public class ProductModel
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? price { get; set; }

    [JsonPropertyName("brand")]
    public string? brand { get; set; }
}
