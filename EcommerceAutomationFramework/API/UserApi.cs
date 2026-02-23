using EcommerceAutomationFramework.Models;
using EcommerceAutomationFramework.Utilities;
using RestSharp;
using System.Text.Json;

namespace EcommerceAutomationFramework.API;

public class UserApi : BaseApiClient
{
    public UserApi(string baseUrl) : base(baseUrl)
    {
    }

    public async Task<RestResponse> CreateUserAsync(Dictionary<string, string> payload)
    {
        var requestPayload = new Dictionary<string, string>(payload, StringComparer.OrdinalIgnoreCase);

        if (requestPayload.TryGetValue("email", out var email))
        {
            requestPayload["email"] = TestDataHelper.GetUniqueEmail(email);
        }

        var request = new RestRequest("createAccount", Method.Post);
        foreach (var kv in requestPayload)
        {
            request.AddParameter(kv.Key, kv.Value);
        }

        return await ExecuteAsync(request);
    }

    public async Task<UserModel?> GetUserByEmailAsync(string email)
    {
        var request = new RestRequest("getUserDetailByEmail", Method.Get)
            .AddQueryParameter("email", email);

        var response = await ExecuteAsync(request);
        if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
        {
            return null;
        }

        using var json = JsonDocument.Parse(response.Content);
        if (json.RootElement.TryGetProperty("user", out var userElement))
        {
            return JsonSerializer.Deserialize<UserModel>(userElement.GetRawText());
        }

        return JsonSerializer.Deserialize<UserModel>(response.Content);
    }

    public async Task<RestResponse> VerifyLoginAsync(string email, string password)
    {
        var request = new RestRequest("verifyLogin", Method.Post)
            .AddParameter("email", email)
            .AddParameter("password", password);

        return await ExecuteAsync(request);
    }
}
